using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class ActivePeriodRule : IContentManagerRule
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

      if ((channelContentItem.IsActivePeriodStartDtmNull() == true) ||
        (channelContentItem.IsActivePeriodEndDtmNull() == true) ||
        (channelContentItem.IsActivePeriodIsSeasonNull() == true))
        return true;

      DateTime StartDtm;
      DateTime EndDtm;
      DateTime FixDtm = currentDtm;
      if (channelContentItem.ActivePeriodIsSeason == true)
      {
        StartDtm = new DateTime(
          FixDtm.Year,
          channelContentItem.ActivePeriodStartDtm.Month,
          channelContentItem.ActivePeriodStartDtm.Day,
          channelContentItem.ActivePeriodStartDtm.Hour,
          channelContentItem.ActivePeriodStartDtm.Minute,
          channelContentItem.ActivePeriodStartDtm.Second);
        EndDtm = new DateTime(
          FixDtm.Year,
          channelContentItem.ActivePeriodEndDtm.Month,
          channelContentItem.ActivePeriodEndDtm.Day,
          channelContentItem.ActivePeriodEndDtm.Hour,
          channelContentItem.ActivePeriodEndDtm.Minute,
          channelContentItem.ActivePeriodEndDtm.Second);

        // Adjust if period wraps new-year
        if (StartDtm > EndDtm)
        {
          StartDtm = StartDtm.AddYears(-1);
          if (FixDtm > EndDtm)
            FixDtm = FixDtm.AddYears(-1);
        }

      }
      else
      {
        StartDtm = channelContentItem.ActivePeriodStartDtm;
        EndDtm = channelContentItem.ActivePeriodEndDtm;
      }

      if ((FixDtm >= StartDtm) && (FixDtm <= EndDtm))
        return true;

      message = string.Format("StartDtm={0}, EndDtm={1}, FixDtm={2}", StartDtm.ToString("yyyy-MM-dd"), EndDtm.ToString("yyyy-MM-dd"), FixDtm.ToString("yyyy-MM-dd"));
      return false;
    }
  }
}
