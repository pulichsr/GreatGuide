using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class MinimumDistanceRule : IContentManagerRule
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

      if ((channelContentItem.TriggerType == Region.EntryTrigger) || (channelContentItem.TriggerType == Region.ExitTrigger))
        return true;

      if (channelContentItem.IsFillerDelayNull() == true)
        return true;

      // Check that the minimum distance was travelled before the item ay be triggered
      if ((lastGpsPosition == null) || (lastMediaStartedGpsPosition == null))
        return true;

      // Calculate distance that was travelled
      double Dy = Math.Abs(lastGpsPosition.Latitude - lastMediaStartedGpsPosition.Latitude) * Constants.AngularToDistanceFactor;
      double Dx = Math.Abs(lastGpsPosition.Longitude - lastMediaStartedGpsPosition.Longitude) * Constants.AngularToDistanceFactor;

      if (Dy * Dy + Dx * Dx > DefaultMinimumDistanceForDelayedContentSquared)
        return true;  // Minimum distance was travelled

      // Minimum distance was not travelled
      message = "Minimum distance was not travelled";
      return false;
    }

  }
}
