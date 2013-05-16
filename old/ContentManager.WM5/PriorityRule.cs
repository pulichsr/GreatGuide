using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class PriorityRule : IContentManagerRule
  {
    public Boolean IsValid(
      ChannelGroupManager channelGroupContent,
      ChannelContentDataset.ChannelContentRow channelContentItem,
      GpsPositionEvent lastGpsPosition,
      GpsPositionEvent lastMediaStartedGpsPosition,
      DateTime currentDtm,
      out string message)
    {
      message = string.Empty;

      if (channelGroupContent.IsCurrentlyPlaying() == false)
        return true;

      Boolean Result = channelGroupContent.currentlyPlayingContent.Priority > channelContentItem.Priority;
      if (Result == false)
        message = string.Format("Playing priority={0}, ContentItem priority={1}", channelGroupContent.currentlyPlayingContent.Priority, channelContentItem.Priority);

      return Result;
    }
  }
}
