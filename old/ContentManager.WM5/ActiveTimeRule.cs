using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class ActiveTimeRule : IContentManagerRule
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

      if ((channelContentItem.IsActiveStartTimeNull() == true) &&
        (channelContentItem.IsActiveEndTimeNull() == true))
        return true;

      Int32 StartSeconds = 0;
      Int32 EndSeconds = 24 * 60 * 60;

      if (channelContentItem.IsActiveStartTimeNull() == false)
        StartSeconds = channelContentItem.ActiveStartTime;
      if (channelContentItem.IsActiveEndTimeNull() == false)
        EndSeconds = channelContentItem.ActiveEndTime;

      Int32 FixSeconds = currentDtm.Hour * 3600 + currentDtm.Minute * 60 + currentDtm.Second;

      if ((FixSeconds >= StartSeconds) && (FixSeconds <= EndSeconds))
        return true;

      message = string.Format("StartSeconds={0}, EndSeconds={1}, FixSeconds={2}",StartSeconds,EndSeconds,FixSeconds);
      return false;
    }
  }
}
