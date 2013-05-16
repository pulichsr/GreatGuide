using System;
using Nucleo.Gps.Nmea;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class GpsPositionEvent : ApplicationEvent
  {
    public GpsPositionEvent()
    {      
    }
    public GpsPositionEvent(float latitude,float longitude)
    {
      Latitude = latitude;
      Longitude = longitude;
    }
    public GpsPositionEvent(DateTime fixDtm, 
                            GpGga.FixQualities fixQuality, 
                            float latitude, 
                            float longitude, 
                            float altitude, 
                            float speed, 
                            float heading,
                            Int16 satellites)
    {
      FixDtm = fixDtm;
      FixQuality = fixQuality;
      Latitude = latitude;
      Longitude = longitude;
      Altitude = altitude;
      Speed = speed;
      Heading = heading;
      Satellites = satellites;
    }

    public DateTime FixDtm = DateTime.Now;
    public GpGga.FixQualities FixQuality = GpGga.FixQualities.Invalid;
    public float Latitude = 0;
    public float Longitude = 0;
    public float Altitude = 0;
    public float Speed = 0;
    public float Heading = 0;
    public Int16 Satellites = 0;
    public GpsRawEvent GpsRawEvent = null;
    public GpsPositionSources Source = GpsPositionSources.Undefined;

    /* Extended fields */

    public Int64 TickCount = 0;
    public GpsQualityIndicators Indicators; // Flags to indicate which values are currently recorded and available for use.  
    public GpsFixType Mode;                 // The type of the GPS fix.  
    public double HDop = 0;                 // Horizontal Dilution Of Precision.  
    public double PDop = 0;                 // Positional Dilution Of Precision.  
    public double VDop = 0;                 // Vertical Dilution Of Precision.  

    public enum GpsQualityIndicators
    {
      None = 0,      // No GPS fix.  
      NumSats = 1,   // The number of satellites we have a lock on.  
      Mode = 2,      // The type of the GPS fix.  
      hdop = 4,      // Horizontal Dilution Of Precision.  
      pdop = 8,      // Positional Dilution Of Precision.  
      vdop = 16,     // Vertical Dilution Of Precision.    
    }

    public enum GpsFixType
    {
      FixNone = 0,    // No fix - we have no positional information.  
      FixType2d = 1,  // 2D fix - position, but no height information. Sufficient for location information.  
      FixType3d = 2,   // 3D fix - position and height information. Typically required for routing operations.       
      FixTypeCombined = 3,   // 3D fix incl external sensors
    }
  }
}



