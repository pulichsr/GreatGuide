using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class FillerDelayRule : IContentManagerRule
  {
    public const Int32 DefaultMinimumDistanceForDelayedContentSquared = 10000;

    public Boolean IsValid(
      ChannelGroupManager channelGroupContent,
      ChannelContentDataset.ChannelContentRow channelContentItem,
      GpsPositionEvent lastGpsPosition,
      GpsPositionEvent lastMediaStartedGpsPosition,
      DateTime currentDtm,
      out string message)
    {
      message = string.Empty;

      TimeSpan Elapsed = currentDtm - channelGroupContent.fillerContentBaseTime;
      Int32 ElapsedSeconds = (Int32)Elapsed.TotalSeconds;

      // If there is no filler delay defined then this rule is nt applicable
      if (channelContentItem.IsFillerDelayNull() == true)
        return true;

      // If the filler delay time has not elapsed, then this content item cannot be played.
      if (ElapsedSeconds < channelContentItem.FillerDelay)
      {
        message = string.Format("FillerDelay={0}, ElapsedSeconds={1}", channelContentItem.FillerDelay,ElapsedSeconds);
        return false;
      }

      return true;
    }

  }
}
