using System;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Events.ControlEvents;
using Nucleo.GoodGuide.Types.Interfaces.Media;

namespace Nucleo.GoodGuide.TopBandMediaPlayer
{
  public class MediaPlayerAdapter
  {
    public MediaPlayerAdapter(IMediaPlayer mediaPlayer)
    {
      Guard.ArgumentNotNull(mediaPlayer, "mediaPlayer");

      this.mediaPlayer = mediaPlayer;     
      this.mediaPlayer.MediaStateChanged += mediaPlayer_MediaStateChanged;
      this.mediaPlayer.MediaPositionChanged += mediaPlayer_MediaPositionChanged;
    }

    #region Event broker publications
    [EventPublisher(EventTopics.MediaPlayer.MediaStateChange)]
    public event EventHandler<GoodGuideEventArgs> StateChanged;
    #endregion

    #region Eventbroker subscription
    [EventSubscriber(EventTopics.ContentManager.MediaControl)]
    public void ContentManagerMediaControlHandler(object sender, GoodGuideEventArgs e)
    {
      MediaControlEvent eventData = e.EventData as MediaControlEvent;
      if (eventData == null)
      {
        Logger.Write(this, string.Format("Invalid event type {0} in MediaPlayerConfigurationHandler", e.EventData.GetType().Name));
        return;
      }

      switch (eventData.State)
      {
        case MediaControlEvent.MediaControlStates.None:
          break;
        case MediaControlEvent.MediaControlStates.Pause:
          mediaPlayer.Pause();
          break;
        case MediaControlEvent.MediaControlStates.Play:
          eventData.Result = mediaPlayer.Play(eventData.Filename, eventData.FilenameIncludesPath, eventData.MediaType);
          break;
        case MediaControlEvent.MediaControlStates.Resume:
          mediaPlayer.Resume();
          break;
        case MediaControlEvent.MediaControlStates.Stop:
          eventData.Result = mediaPlayer.Stop();
          break;
      }
    }

    [EventSubscriber(EventTopics.MediaPlayer.Configuration)]
    public void MediaPlayerConfigurationHandler(object sender, GoodGuideEventArgs e)
    {
      ConfigurationEvent eventData = e.EventData as ConfigurationEvent;
      if (eventData == null)
      {
        Logger.Write(this, string.Format("  Invalid event type {0} in MediaPlayerConfigurationHandler", e.EventData.GetType().Name));
        return;
      }

      NamedParameter npContentBasePath = eventData.ConfigurationParameters[ConfigurationParameters.MediaManager.ContentBasePath];
      if (npContentBasePath == null)
      {
        Logger.Write(this, string.Format("  Parameter {0} not found", ConfigurationParameters.MediaManager.ContentBasePath));
        return;
      }

      mediaPlayer.ContentBasePath = (string)npContentBasePath.Value;
    }

    [EventSubscriber(EventTopics.MediaPlayer.MediaTypeControl)]
    public void MediaTypeControlHandler(object sender, GoodGuideEventArgs e)
    {
      MediaTypeEvent eventData = e.EventData as MediaTypeEvent;
      if (eventData == null)
      {
        Logger.Write(this, string.Format("  Invalid event type {0} in MediaTypeControlHandler", e.EventData.GetType().Name));
        return;
      }

      mediaPlayer.SetMediaType(eventData.MediaType);
    }

    [EventSubscriber(EventTopics.MediaPlayer.VideoSizeControl)]
    public void VideoSizeControlHandler(object sender, GoodGuideEventArgs e)
    {
      VideoSizeEvent eventData = e.EventData as VideoSizeEvent;
      if (eventData == null)
      {
        Logger.Write(this, string.Format("  Invalid event type {0} in VideoSizeControlHandler", e.EventData.GetType().Name));
        return;
      }

      mediaPlayer.ToggleHalfFull();
    }

    #endregion

    #region Event handlers
    private void mediaPlayer_MediaStateChanged(object sender, GoodGuideEventArgs e)
    {
      if (StateChanged == null)
        return;

      StateChanged(this,e);
    }
    private void mediaPlayer_MediaPositionChanged(object sender, GoodGuideEventArgs e)
    {
    }
    #endregion

    #region Fields
    private readonly IMediaPlayer mediaPlayer;
    public readonly LoggingHelper Logger = new LoggingHelper();
    #endregion
  }
}
