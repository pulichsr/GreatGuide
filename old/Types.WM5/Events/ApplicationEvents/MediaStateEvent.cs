using System;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class MediaStateEvent: ApplicationEvent
  {
    public enum MediaStates
    {
      Stopped,
      Playing,
      Paused,
      Ended,
    }

    public MediaStateEvent(Int32 channelGroupId, MediaStates oldState, MediaStates state, string filename)
    {
      ChannelGroupId = channelGroupId;
      this.OldState = oldState;
      this.State = state;
      this.Filename = filename;
    }
    public MediaStateEvent(Int32 channelGroupId, MediaStates oldState, MediaStates state, string filename, MediaTypes mediaType)
    {
      ChannelGroupId = channelGroupId;
      this.OldState = oldState;
      this.State = state;
      this.Filename = filename;
      this.MediaType = mediaType;
    }

    public Int32 ChannelGroupId;
    public MediaStates OldState;
    public MediaStates State;
    public string Filename = string.Empty;
    public MediaTypes MediaType = MediaTypes.Unknown;


  }
}
