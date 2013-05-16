using System;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class MediaControlEvent : ApplicationEvent
  {
    public enum MediaControlStates
    {
      None,
      Stop,
      Play,
      Pause,
      Resume,
    }
    public enum MediaControlResults
    {
      Undefined = -1,
      Ok = 0,
      InvalidChannelGroupId,
      MediaNotFound,
      Timeout,
      MediaPlayerNotAvailable,
      NoResponse,
      Exception,
      InvalidStateCommand,
    }

    public MediaControlEvent(MediaControlStates state, Int32 channelGroupId, string filename)
    {
      State = state;
      ChannelGroupId = channelGroupId;
      Filename = filename;
    }
    public MediaControlEvent(MediaControlStates state, Int32 channelGroupId, string filename, MediaTypes mediaType, Boolean filenameIncludesPath)
    {
      State = state;
      ChannelGroupId = channelGroupId;
      Filename = filename;
      MediaType = mediaType;
      FilenameIncludesPath = filenameIncludesPath;
    }

    public MediaControlStates State = MediaControlStates.None;
    public Int32 ChannelGroupId;
    public string Filename;
    public MediaControlResults Result = MediaControlResults.Undefined;
    public MediaTypes MediaType = MediaTypes.Sound;
    public Boolean FilenameIncludesPath = false;

  }
}
