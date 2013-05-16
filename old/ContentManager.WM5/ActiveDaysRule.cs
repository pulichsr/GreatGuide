using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class ActiveDaysRule : IContentManagerRule
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

      if (channelContentItem.IsActiveDaysNull() == true) 
        return true;

      Int16 FixDayMask = (Byte)(1 << (Int16)currentDtm.DayOfWeek);
      Boolean Valid = (channelContentItem.ActiveDays & FixDayMask) != 0;

      if (Valid == false)
        message = string.Format("ActiveDays={0}, FixDayMask={1}", channelContentItem.ActiveDays,FixDayMask);

      return Valid;
    }
  }
}
