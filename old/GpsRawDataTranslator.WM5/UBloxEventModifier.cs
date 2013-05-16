using System;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.Gps.Nmea;

namespace Nucleo.GoodGuide.GpsRawDataTranslator
{
  public class UBloxEventModifier:
    IGpsEventModifier
  {
    #region IGpsEventModifier Members
    public Boolean Modify(GpsRawEvent rawEvent, GpsPositionEvent positionEvent)
    {
      if (string.IsNullOrEmpty(rawEvent.UserField1) == true)
        return false;

      string uBloxGpsFix = rawEvent.UserField1;
      Int32 fixQuality;

      try
      {
        fixQuality = Convert.ToInt32(uBloxGpsFix);
      }
      catch
      {
        return false;
      }

      switch(fixQuality)
      {
        case 1:
          positionEvent.FixQuality = GpGga.FixQualities.Estimated;
          positionEvent.Mode = GpsPositionEvent.GpsFixType.FixNone;
          positionEvent.HDop = 0;  // To ensure that HDOP always < threshold (effectively ignored)
          break;
        case 2:
          positionEvent.FixQuality = GpGga.FixQualities.GpsFix;
          positionEvent.Mode = GpsPositionEvent.GpsFixType.FixType2d;
          break;
        case 3:
          positionEvent.FixQuality = GpGga.FixQualities.GpsFix;
          positionEvent.Mode = GpsPositionEvent.GpsFixType.FixType3d;
          break;
        case 4:
          positionEvent.FixQuality = GpGga.FixQualities.GpsFix;
          positionEvent.Mode = GpsPositionEvent.GpsFixType.FixTypeCombined;
          break;
      }

      return true;
    }

    #endregion
  }
}
