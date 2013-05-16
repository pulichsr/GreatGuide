using System;
using System.Collections.Generic;
using System.Threading;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Events.ControlEvents;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.MockMediaPlayer
{
  public class MediaManager
  {
    #region EventBroker publications & subscriptions
    [EventPublisher(EventTopics.MediaPlayer.MediaStateChange)]
    public event EventHandler<GoodGuideEventArgs> MediaStateChanged;

    [EventPublisher(EventTopics.MediaPlayer.ConnectedStateChange)]
    public event EventHandler<GoodGuideEventArgs> ConnectedStateChanged;

    [EventSubscriber(EventTopics.System.StopAllContent)]
    public void SystemStopAllContentHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is SystemStopAllContentEvent == false)
        return;

      StopAllContent();
    }

    [EventSubscriber(EventTopics.ContentManager.MediaControl)]
    public void MediaControlHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is MediaControlEvent == false)
        throw new InvalidOperationException(string.Format("Invalid event type {0} in {1}.ContentManagerPlayMediaHandler", e.EventData.GetType().Name, GetType().Name));

      MediaControl((MediaControlEvent)e.EventData);
    }

    [EventSubscriber(EventTopics.EboxMediaManager.RunState)]
    public void RunStateHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is RunStateEvent == false)
        return;

      RunState((RunStateEvent)e.EventData);
    }

    #endregion

    public MediaItems MediaItems
    {
      get { return mediaItems; }
    }
    public Dictionary<int,Channel> Channels
    {
      get { return channels; }
    }

    public void Initialise()
    {
      string mediaFilename = string.Format("{0}MockMediaPlayer.MediaItems.xml", Path.AssemblyPath(this)); 
      Logger.Write(this,"MediaFilename = " + mediaFilename);

      try
      {
        if (System.IO.File.Exists(mediaFilename) == false)
        {
          mediaItems = new MediaItems();
          MediaItem item = new MediaItem();
          item.Name = "Name";
          item.Duration = 30;
          MediaItems.Add(item);
          XmlSerialization.Serialize(MediaItems,mediaFilename);
        }

        mediaItems = XmlSerialization.DeserializeFromFile<MediaItems>(mediaFilename);
      }
      catch
      {
        mediaItems = null;
      }
    }
    public void Finalise()
    {
    }

    public void RunState(RunStateEvent eventData)
    {
      RunState(eventData.IsRunning);
    }
    public void RunState(Boolean runState)
    {
      if (runState == true)
        OnConnectionStateChanged(EboxStateEvent.ConnectionStates.Disconnected, EboxStateEvent.ConnectionStates.Connected);
      else
        OnConnectionStateChanged(EboxStateEvent.ConnectionStates.Disconnected, EboxStateEvent.ConnectionStates.Disconnected);
    }

    public void Play(MediaControlEvent eventData)
    {
      try
      {
        MediaItem item = new MediaItem();// MediaItems.Find(eventData.Filename);
        item.Duration = 10;
        item.Name = eventData.Filename;

        if (item == null)
        {
          Logger.Write(this,string.Format("Media item '{0}' not found",eventData.Filename));
          eventData.Result = MediaControlEvent.MediaControlResults.MediaNotFound;
          return;
        }

        Logger.Write(this, string.Format("Playing '{0}' on ChannelGroup {1} for {2} seconds", eventData.Filename, eventData.ChannelGroupId, item.Duration));
        eventData.Result = MediaControlEvent.MediaControlResults.Ok;

        lock (Channels)
        {
          Channel channel = new Channel(eventData.ChannelGroupId, item);
          Timer timer = new Timer(TimerElapsed, channel, item.Duration * 1000, Timeout.Infinite);
          channel.Timer = timer;
          Channels[eventData.ChannelGroupId] = channel;
        }

        OnMediaStateChanged(eventData.ChannelGroupId, MediaStateEvent.MediaStates.Stopped, MediaStateEvent.MediaStates.Playing,eventData.Filename);
        eventData.Result = MediaControlEvent.MediaControlResults.Ok;
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error posting play request", exc);
        eventData.Result = MediaControlEvent.MediaControlResults.Exception;
      }
    }
    public void Stop(MediaControlEvent eventData)
    {     
      try
      {
        lock (Channels)
        {
          Channel channel;
          Boolean gotChannel = Channels.TryGetValue(eventData.ChannelGroupId,out channel);
          if (gotChannel == false)
            return;

          channel.Timer.Dispose();
          Channels[eventData.ChannelGroupId] = null;
        }

        Logger.Write(this,string.Format("Stopping media item '{0}' on ChannelGroup {1}", eventData.Filename,eventData.ChannelGroupId));

        OnMediaStateChanged(eventData.ChannelGroupId, MediaStateEvent.MediaStates.Playing, MediaStateEvent.MediaStates.Stopped,"");
        eventData.Result = MediaControlEvent.MediaControlResults.Ok;
      }
      catch (Exception exc)
      {
        eventData.Result = MediaControlEvent.MediaControlResults.Exception;
      }
    }
    public void StopAll()
    {
    }

    private void MediaControl(MediaControlEvent eventData)
    {
      switch (eventData.State)
      {
        case MediaControlEvent.MediaControlStates.Stop:
          Stop(eventData);
          break;
        case MediaControlEvent.MediaControlStates.Play:
          Play(eventData);
          break;
      }
    }
    private void StopAllContent()
    {
      Dictionary<Int32,Channel>.Enumerator enumerator = Channels.GetEnumerator();

      while (true)
      {
        if (enumerator.MoveNext() == false)
          break;

        KeyValuePair<Int32, Channel> entry = enumerator.Current;
        if (entry.Value == null)
          continue;

        entry.Value.Timer.Dispose();
      }
    }

    #region Event handlers
    private void TimerElapsed(object state)
    {
      Channel channel = (Channel)state;

      lock (Channels)
      {
        channel.Timer.Dispose();
        Channels[channel.ChannelGroup] = null;
        Logger.Write(this,string.Format("Media item '{0}' on ChannelGroup {1} stopped after {2} seconds", channel.Item.Name, channel.ChannelGroup, channel.Item.Duration));
      }

      OnMediaStateChanged(channel.ChannelGroup,MediaStateEvent.MediaStates.Playing, MediaStateEvent.MediaStates.Stopped,"");
    }
    #endregion

    #region Event dispatchers
    private void OnConnectionStateChanged(EboxStateEvent.ConnectionStates oldState, EboxStateEvent.ConnectionStates newState)
    {
      if (ConnectedStateChanged == null)
        return;

      ConnectionStateEvent eventData = new ConnectionStateEvent(oldState, newState);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);
      ConnectedStateChanged(this, e);
    }
    private void OnMediaStateChanged(Int32 channelGroupId, MediaStateEvent.MediaStates oldState, MediaStateEvent.MediaStates newState,string filename)
    {
      if (MediaStateChanged == null)
        return;

      MediaStateEvent eventData = new MediaStateEvent(channelGroupId, oldState, newState, filename);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);

      MediaStateChanged(this, e);
    }
    #endregion

    #region Fields
    public readonly LoggingHelper Logger = new LoggingHelper();

    private MediaItems mediaItems = null;
    private readonly Dictionary<Int32, Channel> channels = new Dictionary<int, Channel>();
    #endregion
  }

  public class Channel
  {
    public Channel(int channelGroup,MediaItem item)
    {
      this.channelGroup = channelGroup;
      this.item = item;
    }
    public Channel(int channelGroup,Timer timer,MediaItem item)
    {
      this.channelGroup = channelGroup;
      this.timer = timer;
      this.item = item;
    }

    public int ChannelGroup
    {
      get { return channelGroup; }
      set { channelGroup = value; }
    }
    public Timer Timer
    {
      get { return timer; }
      set { timer = value; }
    }
    public MediaItem Item
    {
      get { return item; }
      set { item = value; }
    }

    private int channelGroup;
    private Timer timer = null;
    private MediaItem item = null;
  }
}