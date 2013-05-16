using System;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Interfaces.Media;
using Nucleo.Threading;

namespace Nucleo.GoodGuide.TopbandMediaPlayer
{
  public class TopbandMediaPlayer : IMediaPlayer
  {
    public TopbandMediaPlayer(INamedParameterRepository parameterRepository)
    {
      this.parameterRepository = parameterRepository;
    }

    public const string CONTENT_BASE_PATH_DEFAULT = @"ResidentFlash\";
    public const int CHANNEL_GROUP_ID_DEFAULT = 1;
    public const int DefaultMaxPlayDuration = 5 * 60 * 1000;
  
    public MediaStateEvent.MediaStates MediaState
    {
      get { return mediaState; }
      private set
      {
        if (mediaState == value)
        {
          Logger.Write(this, "set_MediaState: mediaState == value");
          return;
        }

        MediaStateEvent.MediaStates old = mediaState;
        mediaState = value;

        OnMediaStateChanged(old, mediaState, playingMediaType);
      }
    }

    #region IMediaPlayer Members
    public event EventHandler<GoodGuideEventArgs> MediaStateChanged;
    public event EventHandler<GoodGuideEventArgs> MediaPositionChanged;

    public void Initialise()
    {
      Logger.Write(this, ">> Initialise");
      LoadConfiguration();

      maxPlayDurationTimer = new SingleShotTimer(maxPlayDuration);
      maxPlayDurationTimer.TimerExpired += maxPlayDurationTimer_TimerExpired;

      mediaPlayerManager = new TopbandMediaPlayerManager(executablePath,audioExecutable,videoExecutable);
      if (mediaPlayerManager == null)
        throw new ApplicationException("Could not create mediaPlayerManager");

      Logger.RegisterChild(mediaPlayerManager.Logger);

      mediaPlayerManager.SetPlayerType(MediaTypes.Sound);

      mediaEventManager.AudioReachEnd += mediaEventManager_AudioReachEnd;
      mediaEventManager.AudioStarted += mediaEventManager_AudioStarted;
      mediaEventManager.AudioStopped += mediaEventManager_AudioStopped;
      mediaEventManager.AudioPositionChanged += mediaEventManager_AudioPositionChanged;
      mediaEventManager.VideoStarted += mediaEventManager_VideoStarted;
      mediaEventManager.VideoStopped += mediaEventManager_VideoStopped;
      mediaEventManager.VideoReachEnd += mediaEventManager_VideoReachEnd;
      mediaEventManager.VideoPositionChanged += mediaEventManager_VideoPositionChanged;
    }

    public void Finalise()
    {
      maxPlayDurationTimer.TimerExpired -= maxPlayDurationTimer_TimerExpired;
      maxPlayDurationTimer.Dispose();

      if (mediaPlayerManager != null)
        mediaPlayerManager.Close();
    }

    public string ContentBasePath
    {
      get { return contentBasePath; }
      set
      {
        contentBasePath = value;
        Logger.Write(this, string.Format("Setting ContentBasePath to '{0}'", contentBasePath));
      }
    }

    public MediaControlEvent.MediaControlResults Play(string filename, Boolean filenameIncludesPath,MediaTypes mediaTypeToPlay)
    {
      Logger.Write(this, string.Format(">> Play({0},{1},{2})",filename,filenameIncludesPath,mediaTypeToPlay));

      #region Validate state/command
      if (MediaState != MediaStateEvent.MediaStates.Stopped)
      {
        Logger.Write(this, string.Format("Invalid state/command. Play command received in {0} state", MediaState));
        return MediaControlEvent.MediaControlResults.InvalidStateCommand;
      }
      #endregion

      #region Build filename
      if (filenameIncludesPath == true)
      {
        mediaFilename = filename;
        Logger.Write(this,"Filename includes path");
      }
      else
      {
//        Logger.Write(this, string.Format("Filename excludes path. Adding ContentBasePath: {0}", contentBasePath));
        mediaFilename = string.Format("{0}{1}", contentBasePath, filename);
      }
//      Logger.Write(this, string.Format("MediaFilename: {0}", mediaFilename));
      #endregion

      #region Set the mediaplayer filename
      playingMediaType = mediaTypeToPlay;
      MediaControlEvent.MediaControlResults result = mediaPlayerManager.SetMediaFilename(mediaFilename, mediaTypeToPlay);
      if (result != MediaControlEvent.MediaControlResults.Ok)
        return result;
      #endregion

      #region Initiate play
      awaitingMediaState = MediaStateEvent.MediaStates.Playing;

      Logger.Write(this, "RequestAction(Play)");
      result = mediaPlayerManager.RequestAction(TopbandMediaPlayerManager.RequestedAction.Play);
      Logger.Write(this, string.Format("RequestAction(Play) result: {0}", result));
      #endregion

      if (result == MediaControlEvent.MediaControlResults.Ok)
      {
        Logger.Write(this, "Setting media state to playing");
        MediaState = MediaStateEvent.MediaStates.Playing;

        if (playingMediaType == MediaTypes.Sound)
        {
          Logger.Write(this, "Starting maxPlayDurationTimer");
          maxPlayDurationTimer.Start();
        }
      }

      return result;
    }
    public MediaControlEvent.MediaControlResults Stop()
    {
      Logger.Write(this, ">> Stop");

      #region Validate state/command
      if ((MediaState != MediaStateEvent.MediaStates.Playing) && (MediaState != MediaStateEvent.MediaStates.Paused))
      {
        Logger.Write(this, string.Format("Invalid state/command. Stop command received in {0} state", MediaState));
        return MediaControlEvent.MediaControlResults.InvalidStateCommand;
      }
      #endregion

      lastStopCommand = MediaControlEvent.MediaControlStates.Stop;

      #region Initiate stop
      awaitingMediaState = MediaStateEvent.MediaStates.Stopped;

      Logger.Write(this, "Requesting stop");
      MediaControlEvent.MediaControlResults result = mediaPlayerManager.RequestAction(TopbandMediaPlayerManager.RequestedAction.Stop);
      #endregion

      if (result == MediaControlEvent.MediaControlResults.Ok)
      {
        MediaState = MediaStateEvent.MediaStates.Stopped;
        Logger.Write(this, "Stopping maxPlayDurationTimer");
        maxPlayDurationTimer.Stop();
      }

      return result;
    }
    public MediaControlEvent.MediaControlResults Pause()
    {
      Logger.Write(this, ">> Pause");

      #region Validate state/command
      if (MediaState != MediaStateEvent.MediaStates.Playing)
      {
        Logger.Write(this, string.Format("Invalid state/command. Pause command received in {0} state", MediaState));
        return MediaControlEvent.MediaControlResults.InvalidStateCommand;
      }
      #endregion
        
      lastStopCommand = MediaControlEvent.MediaControlStates.Pause;

      #region Initiate pause
      awaitingMediaState = MediaStateEvent.MediaStates.Paused;

      Logger.Write(this, "Requesting pause");
      MediaControlEvent.MediaControlResults result = mediaPlayerManager.RequestAction(TopbandMediaPlayerManager.RequestedAction.Pause);
      #endregion

      if (result == MediaControlEvent.MediaControlResults.Ok)
      {
        MediaState = MediaStateEvent.MediaStates.Paused;
      }

      return result;
    }
    public MediaControlEvent.MediaControlResults Resume()
    {
      Logger.Write(this, ">> Resume");

      #region Validate state/command
      if (MediaState != MediaStateEvent.MediaStates.Paused)
      {
        Logger.Write(this, string.Format("Invalid state/command. Resume command received in {0} state", MediaState));
        return MediaControlEvent.MediaControlResults.InvalidStateCommand;
      }
      #endregion

      #region Initiate resume
      awaitingMediaState = MediaStateEvent.MediaStates.Playing;

      Logger.Write(this, "Requesting resume");
      MediaControlEvent.MediaControlResults result = mediaPlayerManager.RequestAction(TopbandMediaPlayerManager.RequestedAction.Resume);
      #endregion

      if (result == MediaControlEvent.MediaControlResults.Ok)
      {
        MediaState = MediaStateEvent.MediaStates.Playing;
      
        if (playingMediaType == MediaTypes.Sound)
        {
        }
      }

      return result;
    }
    public void SetPosition(ushort position)
    {
      if (mediaPlayerManager != null)
        mediaPlayerManager.RequestPosition(position);
    }
    public void ToggleHalfFull()
    {
      mediaPlayerManager.VideoPlayerToggleFullHalfScreen();
    }

    public void SetMediaType(MediaTypes mediaType)
    {
      Logger.Write(this,string.Format(">> SetMediaType({0})",mediaType));
      mediaPlayerManager.SetPlayerType(mediaType);
    }

    #endregion

    #region Incoming media player events
    private void mediaEventManager_VideoPositionChanged(object sender, ProgressEventArgs e)
    {
      Logger.Write(this, ">> mediaEventManager_VideoPositionChanged");
      MediaPosition = e.Progress;
    }
    private void mediaEventManager_VideoReachEnd(object sender, EventArgs e)
    {
      Logger.Write(this, ">> mediaEventManager_VideoReachEnd");

      Logger.Write(this, "   Stopping maxPlayDurationTimer");
      maxPlayDurationTimer.Stop();

      MediaState = MediaStateEvent.MediaStates.Ended;
      MediaState = MediaStateEvent.MediaStates.Stopped;
    }
    private void mediaEventManager_VideoStopped(object sender, EventArgs e)
    {
      Logger.Write(this, ">> mediaEventManager_VideoStopped");
      switch (lastStopCommand)
      {
        case MediaControlEvent.MediaControlStates.Pause:
          MediaState = MediaStateEvent.MediaStates.Paused;
          break;
        case MediaControlEvent.MediaControlStates.Stop:
          MediaState = MediaStateEvent.MediaStates.Stopped;
          break;
      }

      lastStopCommand = MediaControlEvent.MediaControlStates.None;
    }
    private void mediaEventManager_VideoStarted(object sender, EventArgs e)
    {
      Logger.Write(this, ">> mediaEventManager_VideoStarted");

      MediaState = MediaStateEvent.MediaStates.Playing;
    }

    private void mediaEventManager_AudioPositionChanged(object sender, ProgressEventArgs e)
    {
      Logger.Write(this, ">> mediaEventManager_AudioPositionChanged");
      MediaPosition = e.Progress;
    }
    private void mediaEventManager_AudioStopped(object sender, EventArgs e)
    {
      Logger.Write(this, ">> mediaEventManager_AudioStopped");

      //switch (lastStopCommand)
      //{
      //  case MediaControlEvent.MediaControlStates.Pause:
      //    MediaState = MediaStateEvent.MediaStates.Paused;
      //    break;
      //  case MediaControlEvent.MediaControlStates.Stop:
      //    MediaState = MediaStateEvent.MediaStates.Stopped;
      //    break;
      //}

      //lastStopCommand = MediaControlEvent.MediaControlStates.None;
    }
    private void mediaEventManager_AudioStarted(object sender, EventArgs e)
    {
      Logger.Write(this, ">> mediaEventManager_AudioStarted");

//      MediaState = MediaStateEvent.MediaStates.Playing;
    }
    private void mediaEventManager_AudioReachEnd(object sender, EventArgs e)
    {
      Logger.Write(this, ">> mediaEventManager_AudioReachEnd");

      Logger.Write(this, "   Stopping maxPlayDurationTimer");
      maxPlayDurationTimer.Stop();
      
      MediaState = MediaStateEvent.MediaStates.Ended;
      MediaState = MediaStateEvent.MediaStates.Stopped;
    }
    #endregion

    private void maxPlayDurationTimer_TimerExpired(object sender, EventArgs e)
    {
      Logger.Write(this, ">> maxPlayDurationTimer_TimerExpired");

      lastStopCommand = MediaControlEvent.MediaControlStates.Stop;

      #region Initiate stop
      Logger.Write(this, "Requesting stop");
      awaitingMediaState = MediaStateEvent.MediaStates.Stopped;

      MediaControlEvent.MediaControlResults result = MediaControlEvent.MediaControlResults.Undefined;
      try
      {
        result = mediaPlayerManager.RequestAction(TopbandMediaPlayerManager.RequestedAction.Stop);
        Logger.Write(this, "Requested stop");
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error requesting stop",exc);
      }
      #endregion

      Logger.Write(this, string.Format("Stop result: {0}", result));

      if (result == MediaControlEvent.MediaControlResults.Ok)
        MediaState = MediaStateEvent.MediaStates.Stopped;
    }

    private void OnMediaStateChanged(MediaStateEvent.MediaStates oldState, MediaStateEvent.MediaStates newState, MediaTypes mediaType)
    {
      if (MediaStateChanged == null)
        return;

      MediaStateEvent eventData = new MediaStateEvent(CHANNEL_GROUP_ID_DEFAULT, oldState, newState, mediaFilename, mediaType);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);
      MediaStateChanged(this, e);
    }

    private Int16 MediaPosition
    {
      get { return playingMediaPosition; }
      set
      {
        if (playingMediaPosition == value)
          return;

        playingMediaPosition = value;

        OnMediaPositionChanged(playingMediaPosition);
      }
    }
    private void OnMediaPositionChanged(Int16 position)
    {
      if (MediaPositionChanged == null)
        return;

      MediaPositionEvent eventData = new MediaPositionEvent(position);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);
      MediaPositionChanged(this, e);
    }

    private void LoadConfiguration()
    {
      Logger.Write(this, ">> LoadConfiguration");

      contentBasePath = parameterRepository.GetString(contentBasePathKey);
      if (contentBasePath == null)
      {
        contentBasePath = CONTENT_BASE_PATH_DEFAULT;
        parameterRepository.SetString(contentBasePathKey, contentBasePath);
      }

      executablePath = parameterRepository.GetString(executablePathKey);
      if (executablePath == null)
      {
        executablePath = TopbandMediaPlayerManager.EXECUTABLE_PATH_DEFAULT;
        parameterRepository.SetString(executablePathKey, executablePath);
      }
      audioExecutable = parameterRepository.GetString(audioExecutableKey);
      if (audioExecutable == null)
      {
        audioExecutable = TopbandMediaPlayerManager.PVAUDIO_EXECUTABLE_DEFAULT;
        parameterRepository.SetString(audioExecutableKey, audioExecutable);
      }
      videoExecutable = parameterRepository.GetString(videoExecutableKey);
      if (videoExecutable == null)
      {
        videoExecutable = TopbandMediaPlayerManager.PVVIDEO_EXECUTABLE_DEFAULT;
        parameterRepository.SetString(videoExecutableKey, videoExecutable);
      }

      Int32? _maxPlayDuration = parameterRepository.GetInt32(maxPlayDurationKey);
      if (_maxPlayDuration == null)
      {     
        maxPlayDuration = DefaultMaxPlayDuration;
        parameterRepository.SetInt32(maxPlayDurationKey, maxPlayDuration);
      }
      else
      {
        maxPlayDuration = _maxPlayDuration.Value;
      }

      Logger.Write(this,string.Format("  contentBasePath: {0}",contentBasePath));
      Logger.Write(this, string.Format("  executablePath: {0}", executablePath));
      Logger.Write(this, string.Format("  audioExecutable: {0}", audioExecutable));
      Logger.Write(this, string.Format("  videoExecutable: {0}", videoExecutable));
      Logger.Write(this, string.Format("  maxPlayDuration: {0}", maxPlayDuration));
    }

    #region Repository Keys

    private const string contentBasePathKey = "ContentBasePath";

    private const string executablePathKey = "TopbandMediaPlayer.ExecutablePath";
    private const string audioExecutableKey = "TopbandMediaPlayer.AudioExecutable";
    private const string videoExecutableKey = "TopbandMediaPlayer.VideoExecutable";
    private const string maxPlayDurationKey = "TopbandMediaPlayer.MaxPlayDuration";

    #endregion

    #region Fields

    public readonly LoggingHelper Logger = new LoggingHelper();
    private readonly INamedParameterRepository parameterRepository = null;
    
    private volatile MediaTypes playingMediaType = MediaTypes.Unknown;
    private volatile MediaStateEvent.MediaStates mediaState = MediaStateEvent.MediaStates.Stopped;
    private volatile MediaStateEvent.MediaStates awaitingMediaState = MediaStateEvent.MediaStates.Stopped;
    private volatile Int16 playingMediaPosition = 0;

    private readonly TopbandMediaEventManager mediaEventManager = new TopbandMediaEventManager();
    private TopbandMediaPlayerManager mediaPlayerManager = null;
    private volatile string mediaFilename;

    private string contentBasePath = string.Empty;

    private string executablePath = string.Empty;
    private string audioExecutable = string.Empty;
    private string videoExecutable = string.Empty;

    private Int32 maxPlayDuration;
    private SingleShotTimer maxPlayDurationTimer;

    // The media player raises STOPPED event when Pausing or Stopping. Use this variable to distinguish
    private MediaControlEvent.MediaControlStates lastStopCommand;

    #endregion
  }
}