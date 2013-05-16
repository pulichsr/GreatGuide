using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class PresentedCountRule : IContentManagerRule
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

      if ((channelContentItem.IsPresentedCountNull() == true) || (channelContentItem.IsMaxPresentedCountNull() == true))
        return true;

      if (channelContentItem.PresentedCount < channelContentItem.MaxPresentedCount)
        return true;

      message = string.Format("PresentedCount={0}, MaxPresentedCount={1}", channelContentItem.PresentedCount, channelContentItem.MaxPresentedCount);
      return false;
    }
  }
}
