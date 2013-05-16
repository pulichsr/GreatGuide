using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Nucleo.GoodGuide.TopbandMediaPlayer.WindowsSupport;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.TopbandMediaPlayer
{
  public sealed class TopbandMediaPlayerManager
  {
    public TopbandMediaPlayerManager(string executablePath, string audioExecutable, string videoExecutable)
    {
      Guard.ArgumentNotNullOrEmptyString(executablePath, "executablePath");
      Guard.ArgumentNotNullOrEmptyString(audioExecutable, "audioExecutable");
      Guard.ArgumentNotNullOrEmptyString(videoExecutable, "videoExecutable");

      if (File.Exists(System.IO.Path.Combine(executablePath,audioExecutable)) == false)
      {
        Logger.Write(this,"Audio Player not found");
      }
      if (File.Exists(System.IO.Path.Combine(executablePath,videoExecutable)) == false)
      {
        Logger.Write(this,"Video Player not found");
      }

      this.executablePath = executablePath;
      this.audioExecutable = audioExecutable;
      this.videoExecutable = videoExecutable;
    }

    #region Constants

    public const string EXECUTABLE_PATH_DEFAULT = @"ResidentFlash2\MyShell\";
    public const string PVVIDEO_EXECUTABLE_DEFAULT = "PvVideo.exe";
    public const string PVAUDIO_EXECUTABLE_DEFAULT = "PvAudio.exe";
    public const UInt32 STARTUP_DELAY_DEFAULT = 1000;

    public const string PVVIDEO_CLASS = "PVVIDEO";
    public const string PVAUDIO_CLASS = "PVAUDIO";

    public const UInt32 WM_PVAPP_QUIT = (UInt32)WindowsMessages.WM_USER + 331;

    public const UInt32 WM_TUOBANG_AUDIO_PAUSE = (UInt32)WindowsMessages.WM_USER + 2000;
    public const UInt32 WM_TUOBANG_AUDIO_STOP = (UInt32)WindowsMessages.WM_USER + 2001;
    public const UInt32 WM_TUOBANG_AUDIO_VOLUP = (UInt32)WindowsMessages.WM_USER + 2002;
    public const UInt32 WM_TUOBANG_AUDIO_VOLDOWN = (UInt32)WindowsMessages.WM_USER + 2003;

    // 1.8 Media Started (as a result of a play or resume command)
    public const UInt32 WM_TUOBANG_AUDIO_STARTED = (UInt32)WindowsMessages.WM_USER + 2009;
    // 1.9 Media Stopped (as a result of a pause or stop command)
    public const UInt32 WM_TUOBANG_AUDIO_STOPPED = (UInt32)WindowsMessages.WM_USER + 2010;
    // 1.10Media Stopped (as a result of a media clip reaching its end and finishing)
    public const UInt32 WM_TUOBANG_AUDIO_REACH_END = (UInt32)WindowsMessages.WM_USER + 2011;
    // 1.11Media Position (as a percentage): wParam:  percent;      lParam:  0
    public const UInt32 WM_TUOBANG_AUDIO_POSITION = (UInt32)WindowsMessages.WM_USER + 2012;
    // 1.12Move playback to position (as a percentage): wParam:  percent;      lParam:  0
    public const UInt32 WM_TUOBANG_AUDIO_SETPOSITION = (UInt32)WindowsMessages.WM_USER + 2013;

    public const UInt32 WM_TUOBANG_VIDEO_PAUSE = (UInt32)WindowsMessages.WM_USER + 2004;
    public const UInt32 WM_TUOBANG_VIDEO_STOP = (UInt32)WindowsMessages.WM_USER + 2005;
    public const UInt32 WM_TUOBANG_VIDEO_VOLUP = (UInt32)WindowsMessages.WM_USER + 2006;
    public const UInt32 WM_TUOBANG_VIDEO_VOLDOWN = (UInt32)WindowsMessages.WM_USER + 2007;
    public const UInt32 WM_TUOBANG_VIDEO_CLOSE = (UInt32)WindowsMessages.WM_USER + 2008;

    // 2.8 Media Started (as a result of a play or resume command)
    public const UInt32 WM_TUOBANG_VIDEO_STARTED = (UInt32)WindowsMessages.WM_USER + 2014;
    // 2.9 Media Stopped (as a result of a pause or stop command)
    public const UInt32 WM_TUOBANG_VIDEO_STOPPED = (UInt32)WindowsMessages.WM_USER + 2015;
    // 2.10Media Stopped (as a result of a media clip reaching its end and finishing)
    public const UInt32 WM_TUOBANG_VIDEO_REACH_END = (UInt32)WindowsMessages.WM_USER + 2016;
    // 2.11Media Position (as a percentage): wParam:  percent;      lParam:  0
    public const UInt32 WM_TUOBANG_VIDEO_POSITION = (UInt32)WindowsMessages.WM_USER + 2017;
    // 2.12Move playback to position (as a percentage): wParam:  percent;      lParam:  0
    public const UInt32 WM_TUOBANG_VIDEO_SETPOSITION = (UInt32)WindowsMessages.WM_USER + 2018;
    // 2.13Full screen or Half screen wParam:  0;      lParam:  0
    public const UInt32 WM_TUOBANG_VIDEO_FULL_OR_HALF_SCREEN = (UInt32)WindowsMessages.WM_USER + 2019;

    #endregion

    public enum PlayerType
    {
      None, Audio, Video
    }
    public enum RequestedAction
    {
      None, Stop = 1, Play, Pause, Resume, Volume_Up, Volume_Down, Position, ToggleFullHalfScreen
    }

    public void Close()
    {
      switch (activePlayer)
      {
        case PlayerType.Audio:
          AudioPlayerQuit();
          break;
        case PlayerType.Video:
          VideoPlayerQuit();
          break;
      }
      activePlayer = PlayerType.None;
    }

    #region Public methods
    public void SetPlayerType(MediaTypes type)
    {
      switch (type)
      {
        #region Requested type: Sound
        case MediaTypes.Sound:
          switch (activePlayer)
          {
            #region Current type: None
            case PlayerType.None:
              Logger.Write(this, "Starting audio player");
              AudioPlayerOpen();
              Thread.Sleep((int)startupDelay);
              break;
            #endregion

            #region Current type: Audio
            case PlayerType.Audio:
              break;
            #endregion

            #region Current type: Video
            case PlayerType.Video:
              Logger.Write(this, "Quitting video player");
              VideoPlayerQuit();

              Logger.Write(this, "Starting audio player");
              AudioPlayerOpen();
              Thread.Sleep((int)startupDelay);
              break;
            #endregion
          }
      
          activePlayer = PlayerType.Audio;
          break;
        #endregion

        #region Request type: Video
        case MediaTypes.Video:
          switch (activePlayer)
          {
            #region Current type: None
            case PlayerType.None:
              Logger.Write(this, "Open video player");
              VideoPlayerOpen();
              Thread.Sleep((int)startupDelay);
              break;
            #endregion

            #region Current type: Audio
            case PlayerType.Audio:
              Logger.Write(this, "Quitting audio player");
              AudioPlayerQuit();

              Logger.Write(this, "Open video player");
              VideoPlayerOpen();
              Thread.Sleep((int)startupDelay);
              break;
            #endregion

            #region Current type: Video
            case PlayerType.Video:
              break;
            #endregion
          }

          activePlayer = PlayerType.Video;
          break;
        #endregion
      }
    }
    public MediaControlEvent.MediaControlResults SetMediaFilename(string filename, MediaTypes mediaType)
    {
//      Logger.Write(this, string.Format(">> SetMediaFilename({0},{1})", filename, mediaType));

      lock (requestLock)
      {
        mediaSourceInitialized = false;
        if (string.IsNullOrEmpty(filename) == true)
        {
          Logger.Write(this, "Variable mediaFilename is undefined");
          return MediaControlEvent.MediaControlResults.Exception;
        }

        #region Validate that media file exists
//        Logger.Write(this,string.Format("Checking if file {0} exists",filename));
        if (File.Exists(filename) == false)
        {
          Logger.Write(this, string.Format("File {0} does not exist", filename));
          return MediaControlEvent.MediaControlResults.Exception;
        }
        #endregion

        if (mediaType == MediaTypes.Unknown)
        {
          Logger.Write(this, "Invalid media type");
          return MediaControlEvent.MediaControlResults.Exception;
        }
        mediaFilename = filename;
        mediaSourceType = mediaType;
        mediaSourceInitialized = true;

        return MediaControlEvent.MediaControlResults.Ok;
      }
    }
    public MediaControlEvent.MediaControlResults RequestAction(RequestedAction action)
    {
//      Logger.Write(this, string.Format(">> RequestAction({0})",action));
      lock (requestLock)
      {
        MediaControlEvent.MediaControlResults result = MediaControlEvent.MediaControlResults.Exception;
        try
        {
          if (mediaSourceInitialized)
          {
            requestedAction = action;
            result = ProcessRequest();
          }
          else
          {
            Logger.Write(this, "Media source not initialized");
            result = MediaControlEvent.MediaControlResults.MediaPlayerNotAvailable;
          }
        }
        catch (Exception exception)
        {
          Logger.Write(this, string.Format("Error requesting action: {0}", action.ToString()), exception);
        }
        return result;
      }
    }
    public MediaControlEvent.MediaControlResults RequestPosition(int percentage)
    {
      lock (requestLock)
      {
        MediaControlEvent.MediaControlResults result = MediaControlEvent.MediaControlResults.Exception;
        try
        {
          if (mediaSourceInitialized)
          {
            Boolean validPosition = (percentage >= 0) && (percentage <= 100);
            if (validPosition == false) 
              percentage = 0;
            requestedAction = RequestedAction.Position;
            requestedPosition = percentage;
            result = ProcessRequest();
          }
          else
          {
            Logger.Write(this, "Media source not initialized");
            result = MediaControlEvent.MediaControlResults.MediaPlayerNotAvailable;
          }
        }
        catch (Exception exception)
        {
          Logger.Write(this, string.Format("Error requesting position: {0}", percentage.ToString()), exception);
        }
        return result;
      }
    }

    #endregion

    #region Marshal

    private MediaControlEvent.MediaControlResults ProcessRequest()
    {
      switch (requestedAction)
      {
        case RequestedAction.Stop:
          StopActivePlayer();
          break;
        case RequestedAction.Play:
          PlayActivePlayer();
          break;
        case RequestedAction.Pause:
          PauseActivePlayer();
          break;
        case RequestedAction.Resume:
          ResumeActivePlayer();
          break;
        case RequestedAction.Volume_Up:
          VolUpActivePlayer();
          break;
        case RequestedAction.Volume_Down:
          VolDownActivePlayer();
          break;
        case RequestedAction.Position:
          SetMediaPositionForActivePlayer();
          break;
        case RequestedAction.ToggleFullHalfScreen:
          ToggleFullHalfScreenOfVideoPlayer();
          break;
        default:
          return MediaControlEvent.MediaControlResults.Undefined;
      }
      return MediaControlEvent.MediaControlResults.Ok;
    }

    #endregion

    #region Indirect control of media players
    private void PlayActivePlayer()
    {
//      Logger.Write(this, ">> PlayActivePlayer");
//      Logger.Write(this, string.Format("  Requested media type: {0}",mediaSourceType));
//      Logger.Write(this, string.Format("  Current media type: {0}", activePlayer));

      switch (mediaSourceType)
      {
        #region Requested type: Sound
        case MediaTypes.Sound:
          switch (activePlayer)
          {
            #region Current type: None
            case PlayerType.None:
              Logger.Write(this, "Starting audio player");
              AudioPlayerOpen();
              Thread.Sleep((int)startupDelay);
              break;
            #endregion

            #region Current type: Audio
            case PlayerType.Audio:
              break;
            #endregion

            #region Current type: Video
            case PlayerType.Video:
              Logger.Write(this, "Quitting video player");
              VideoPlayerQuit();

              Logger.Write(this, "Starting audio player");
              AudioPlayerOpen();
              Thread.Sleep((int)startupDelay);
              break;
            #endregion
          }

//          Logger.Write(this, "Playing on audio player");
          AudioPlayerPlay();
          activePlayer = PlayerType.Audio;
          break;
          #endregion

        #region Request type: Video
        case MediaTypes.Video:
          switch (activePlayer)
          {
            #region Current type: None
            case PlayerType.None:
              Logger.Write(this, "Open video player");
              VideoPlayerOpen();
              Thread.Sleep((int)startupDelay);
              break;
            #endregion

            #region Current type: Audio
            case PlayerType.Audio:
              Logger.Write(this, "Quitting audio player");
              AudioPlayerQuit();

              Logger.Write(this, "Open video player");
              VideoPlayerOpen();
              Thread.Sleep((int)startupDelay);
              break;
            #endregion

            #region Current type: Video
            case PlayerType.Video:
              break;
            #endregion
          }

          VideoPlayerPlay();
          activePlayer = PlayerType.Video;
          break;
          #endregion
      }

      playerState = MediaStateEvent.MediaStates.Playing;
    }
    private void StopActivePlayer()
    {
      Logger.Write(this, ">> StopActivePlayer");

      if (activePlayer == PlayerType.None) return;
      switch (activePlayer)
      {
        case PlayerType.Audio:
          AudioPlayerStop();
          break;
        case PlayerType.Video:
          VideoPlayerStop();
          break;
      }
      playerState = MediaStateEvent.MediaStates.Stopped;
    }
    private void PauseActivePlayer()
    {
      Logger.Write(this, ">> PauseActivePlayer");

      if (activePlayer == PlayerType.None) return;
      if (playerState == MediaStateEvent.MediaStates.Playing)
      {
        switch (activePlayer)
        {
          case PlayerType.Audio:
            AudioPlayerPause();
            break;
          case PlayerType.Video:
            VideoPlayerPause();
            break;
        }
        playerState = MediaStateEvent.MediaStates.Paused;
      }
    }
    private void ResumeActivePlayer()
    {
      Logger.Write(this, ">> ResumeActivePlayer");

      if (activePlayer == PlayerType.None) return;
      if (playerState == MediaStateEvent.MediaStates.Paused)
      {
        switch (activePlayer)
        {
          case PlayerType.Audio:
            AudioPlayerResume();
            break;
          case PlayerType.Video:
            VideoPlayerResume();
            break;
        }
        playerState = MediaStateEvent.MediaStates.Playing;
      }
    }
    private void VolUpActivePlayer()
    {
      Logger.Write(this, ">> VolUpActivePlayer");

      switch (activePlayer)
      {
        case PlayerType.Audio:
          AudioPlayerVolUp();
          break;
        case PlayerType.Video:
          VideoPlayerVolUp();
          break;
      }
    }
    private void VolDownActivePlayer()
    {
      Logger.Write(this, ">> VolDownActivePlayer");

      switch (activePlayer)
      {
        case PlayerType.Audio:
          AudioPlayerVolDown();
          break;
        case PlayerType.Video:
          VideoPlayerVolDown();
          break;
      }
    }
    private void SetMediaPositionForActivePlayer()
    {
      Logger.Write(this, ">> SetMediaPositionForActivePlayer");

      switch (activePlayer)
      {
        case PlayerType.Audio:
          AudioPlayerSetPosition();
          break;
        case PlayerType.Video:
          VideoPlayerSetPosition();
          break;
      }
    }
    private void ToggleFullHalfScreenOfVideoPlayer()
    {
      Logger.Write(this, ">> ToggleFullHalfScreenOfVideoPlayer");

      if (activePlayer == PlayerType.Video)
      {
        VideoPlayerToggleFullHalfScreen();
      }
    }

    #endregion

    #region Direct control of audio players

    private IntPtr GetAudioPlayerHandle_IntPtr()
    {
      IntPtr handle = WindowsMethods.GetWindowId_IntPtr(PVAUDIO_CLASS, null);
//      Logger.Write(this, string.Format("AudioPlayerHandle: {0}", handle.ToInt32()));
      return handle;
    }
    private int GetAudioPlayerHandle_Int()
    {
      int handle = WindowsMethods.GetWindowId_Int(PVAUDIO_CLASS, null);
//      Logger.Write(this, string.Format("AudioPlayerHandle",handle));
      return handle;
    }
    private void AudioPlayerOpen()
    {
//      Logger.Write(this, ">> AudioPlayerOpen");

      string startInfo = executablePath + audioExecutable;
//      Logger.Write(this, string.Format("Starting audio player: {0}",startInfo));
      Process.Start(startInfo, null);
    }
    private void AudioPlayerQuit()
    {
//      Logger.Write(this, ">> AudioPlayerQuit");
      WindowsMethods.PostMessage(GetAudioPlayerHandle_Int(), (int)WM_PVAPP_QUIT, (int)0, (int)0);
    }
    private void AudioPlayerPlay()
    {
//      Logger.Write(this, ">> AudioPlayerPlay");
      cMsgStrings.SendMsgString(GetAudioPlayerHandle_IntPtr(), mediaFilename);
    }
    private void AudioPlayerStop()
    {
//      Logger.Write(this, ">> AudioPlayerStop");
      WindowsMethods.PostMessage(GetAudioPlayerHandle_Int(), (int)WM_TUOBANG_AUDIO_STOP, (int)0, (int)0);
    }
    private void AudioPlayerPause()
    {
//      Logger.Write(this, ">> AudioPlayerPause");
      WindowsMethods.PostMessage(GetAudioPlayerHandle_Int(), (int)WM_TUOBANG_AUDIO_PAUSE, (int)0, (int)0);
    }
    private void AudioPlayerResume()
    {
//      Logger.Write(this, ">> AudioPlayerResume");
      WindowsMethods.PostMessage(GetAudioPlayerHandle_Int(), (int)WM_TUOBANG_AUDIO_PAUSE, (int)0, (int)0);
    }
    private void AudioPlayerVolUp()
    {
//      Logger.Write(this, ">> AudioPlayerVolUp");
      WindowsMethods.PostMessage(GetAudioPlayerHandle_Int(), (int)WM_TUOBANG_AUDIO_VOLUP, (int)0, (int)0);
    }
    private void AudioPlayerVolDown()
    {
//      Logger.Write(this, ">> AudioPlayerVolDown");
      WindowsMethods.PostMessage(GetAudioPlayerHandle_Int(), (int)WM_TUOBANG_AUDIO_VOLDOWN, (int)0, (int)0);
    }
    private void AudioPlayerSetPosition()
    {
//      Logger.Write(this, ">> AudioPlayerSetPosition");
      WindowsMethods.PostMessage(GetAudioPlayerHandle_Int(), (int)WM_TUOBANG_AUDIO_SETPOSITION, requestedPosition, (int)0);
    }

    #endregion

    #region Direct control of video player

    private IntPtr GetVideoPlayerHandle_IntPtr()
    {
      return WindowsMethods.GetWindowId_IntPtr(PVVIDEO_CLASS, null);
    }
    private int GetVideoPlayerHandle_Int()
    {
      return WindowsMethods.GetWindowId_Int(PVVIDEO_CLASS, null);
    }
    private void VideoPlayerOpen()
    {
      Logger.Write(this, ">> VideoPlayerOpen");
      Process.Start(executablePath + videoExecutable, null);
    }
    private void VideoPlayerQuit()
    {
      Logger.Write(this, ">> VideoPlayerQuit");
      WindowsMethods.PostMessage(GetVideoPlayerHandle_Int(), (int)WM_PVAPP_QUIT, (int)0, (int)0);
    }
    private void VideoPlayerClose()
    {
      Logger.Write(this, ">> VideoPlayerClose");
      WindowsMethods.PostMessage(GetVideoPlayerHandle_Int(), (int)WM_TUOBANG_VIDEO_CLOSE, (int)0, (int)0);
    }
    private void VideoPlayerPlay()
    {
      Logger.Write(this, ">> VideoPlayerPlay");
      cMsgStrings.SendMsgString(GetVideoPlayerHandle_IntPtr(), mediaFilename);
    }
    private void VideoPlayerStop()
    {
      Logger.Write(this, ">> VideoPlayerStop");
      WindowsMethods.PostMessage(GetVideoPlayerHandle_Int(), (int)WM_TUOBANG_VIDEO_STOP, (int)0, (int)0);
    }
    private void VideoPlayerPause()
    {
      Logger.Write(this, ">> VideoPlayerPause");
      WindowsMethods.PostMessage(GetVideoPlayerHandle_Int(), (int)WM_TUOBANG_VIDEO_PAUSE, (int)0, (int)0);
    }
    private void VideoPlayerResume()
    {
      Logger.Write(this, ">> VideoPlayerResume");
      WindowsMethods.PostMessage(GetVideoPlayerHandle_Int(), (int)WM_TUOBANG_VIDEO_PAUSE, (int)0, (int)0);
    }
    private void VideoPlayerVolUp()
    {
      Logger.Write(this, ">> VideoPlayerVolUp");
      WindowsMethods.PostMessage(GetVideoPlayerHandle_Int(), (int)WM_TUOBANG_VIDEO_VOLUP, (int)0, (int)0);
    }
    private void VideoPlayerVolDown()
    {
      Logger.Write(this, ">> VideoPlayerVolDown");
      WindowsMethods.PostMessage(GetVideoPlayerHandle_Int(), (int)WM_TUOBANG_VIDEO_VOLDOWN, (int)0, (int)0);
    }
    private void VideoPlayerSetPosition()
    {
      Logger.Write(this, ">> VideoPlayerSetPosition");
      WindowsMethods.PostMessage(GetVideoPlayerHandle_Int(), (int)WM_TUOBANG_VIDEO_SETPOSITION, requestedPosition, (int)0);
    }
    public void VideoPlayerToggleFullHalfScreen()
    {
      Logger.Write(this, ">> VideoPlayerToggleFullHalfScreen");
      WindowsMethods.PostMessage(GetVideoPlayerHandle_Int(), (int)WM_TUOBANG_VIDEO_FULL_OR_HALF_SCREEN, (int)0, (int)0);
    }

    #endregion

    #region Fields
    public readonly LoggingHelper Logger = new LoggingHelper();

    private readonly object requestLock = new object();

    private bool mediaSourceInitialized = false;
    private string mediaFilename = string.Empty;
    private MediaTypes mediaSourceType = MediaTypes.Unknown;

    private RequestedAction requestedAction = RequestedAction.None;
    private int requestedPosition = 0;

    private PlayerType activePlayer = PlayerType.None;
    private MediaStateEvent.MediaStates playerState = MediaStateEvent.MediaStates.Stopped;

    private readonly string executablePath = string.Empty;
    private readonly string audioExecutable = string.Empty;
    private readonly string videoExecutable = string.Empty;
    private readonly UInt32 startupDelay = STARTUP_DELAY_DEFAULT;

    #endregion
  }
}
