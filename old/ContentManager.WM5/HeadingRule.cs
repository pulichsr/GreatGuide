using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class HeadingRule : IContentManagerRule
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

      if ((channelContentItem.IsHeadingNull() == true) || (channelContentItem.IsHeadingVarianceNull() == true))
        return true;

      if (lastGpsPosition == null)
        return true;

      Boolean Result = Arc.SpansAngle(
        (Int16)(channelContentItem.Heading - channelContentItem.HeadingVariance),
        (Int16)(channelContentItem.Heading + channelContentItem.HeadingVariance), 
        (Int16)lastGpsPosition.Heading);

      if (Result == false)
        message = string.Format("ContentItemHeading={0}-{1}, Heading={2}", 
          channelContentItem.Heading - channelContentItem.HeadingVariance, 
          channelContentItem.Heading + channelContentItem.HeadingVariance,
          lastGpsPosition.Heading);

      return Result;
    }
  }
}
