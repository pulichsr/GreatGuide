using System;
using System.Collections.Generic;
using System.Threading;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Telogis.GeoBase;
using Telogis.GeoBase.Navigation;

namespace Nucleo.GoodGuide.GeoBaseNavigator
{
  public class GeoBaseGps:
    IGps
  {
    public GeoBaseGps()
    {
      Thread EventQueueThread = new Thread(EventQueueManager);
      EventQueueThread.Name = "CarApplication.GeoBaseGps.EventQueueThread";
      EventQueueThread.Start();
    }

    #region IGps Members

    public event Action<Exception> GPSException;

    public Position Position
    {
      get 
      {
        lock (positionLock)
        {
          return position; 
        }
      }
    }

    public void PowerDown()
    {
      position.quality.Mode = GpsFixType.FixNone;
      powerFlag = false;
    }

    public void PowerUp()
    {
      position.quality.Mode = GpsFixType.FixType2d;
      position.quality.NumSats = 3;
      position.units = SpeedUnit.MetersPerSecond;
      powerFlag = true;
    }

    public bool PoweredUp
    {
      get { return powerFlag; }
    }

    public event EventHandler Update;

    #endregion

    public void QueueEventData(GpsPositionEvent gpsPositionEvent)
    {
      if (gpsPositionEvent.Mode != GpsPositionEvent.GpsFixType.FixType3d)
        return;

      lock (eventDataStackLock)
      {
        eventDataStack.Push(gpsPositionEvent);
      }
      stackPushEvent.Set();
    }

    private void EventQueueManager()
    {
      Logger.Write(this, "EventQueueManager thread starting");
      Thread.CurrentThread.IsBackground = true;
      bool loadPosition = false;
      while(true)
      {
        stackPushEvent.WaitOne();
        lock (eventDataStackLock)
        {
          if (eventDataStack.Count > 0)
          {
            tempGpsPositionEvent = eventDataStack.Pop();
            eventDataStack.Clear();
            loadPosition = true;
          }
        }
        if (loadPosition)
        {
          lock (positionLock)
          {
            try
            {
              UpdatePositionData(tempGpsPositionEvent);
            }
            catch (Exception exc)
            {
              Logger.Write(this,"Error",exc);
            }
          }
          loadPosition = false;
        }
      }
    }
    private void UpdatePositionData(GpsPositionEvent positionEvent)
    {
      //heading = The recorded heading, measured in degrees. See remarks.  
      position.heading = positionEvent.Heading;

      //location = The recorded location.  
      position.location = new LatLon(positionEvent.Latitude, positionEvent.Longitude);

      //PositionNumber = A tag used to identify this Position.  
      position.PositionNumber = positionNumber++;

      //quality = The GpsQuality of the GPS unit, when this Position was recorded.  
      position.quality.Mode = (GpsFixType)positionEvent.Mode;
      position.quality.indicators = (GpsQualityIndicators)positionEvent.Indicators;
      position.quality.NumSats = positionEvent.Satellites;
      position.quality.hdop = positionEvent.HDop;
      position.quality.vdop = positionEvent.VDop;
      position.quality.pdop = positionEvent.PDop;

      //speed = The recorded speed, measured in this Position's units.  
      position.speed = positionEvent.Speed;

      //TickCount = The value of the system TickCount when this Position was recorded.  
      position.TickCount = (int)positionEvent.TickCount;

      //time = A timestamp identifying the time at which this Position was recorded.  
      position.time = positionEvent.FixDtm;

      //units = The SpeedUnits in which speed is measured. 
      position.units = SpeedUnit.KilometersPerHour;

      if (Update != null)
        Update(this,new EventArgs());
    }

    #region Fields

    private const bool signaled = true;
    private const bool nonSignaled = false;
    private readonly AutoResetEvent stackPushEvent = new AutoResetEvent(nonSignaled);

    private readonly object eventDataStackLock = new object();
    private readonly Stack<GpsPositionEvent> eventDataStack = new Stack<GpsPositionEvent>(5);

    private GpsPositionEvent tempGpsPositionEvent;

    private bool powerFlag = false;
    private int positionNumber = 0;
    private readonly object positionLock = new object();
    private Telogis.GeoBase.Navigation.Position position;
    public readonly LoggingHelper Logger = new LoggingHelper();

    #endregion
  }
}