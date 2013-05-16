using System;
using System.Threading;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.Gps.Nmea;

namespace Nucleo.GoodGuide.GpsRawDataTranslator
{
  public class RawDataTranslator
  {
    public const float ValidHeadingMinimumSpeed = 2;
    public const Int16 DefaultFixQualityThreshold = 20;
    public const Int16 DefaultTimeOffset = 0;

    public RawDataTranslator(INamedParameterRepository parameterRepository,IGpsEventModifier eventModifier)
    {
      Guard.ArgumentNotNull(parameterRepository, "parameterRepository");

      this.parameterRepository = parameterRepository;
      this.eventModifier = eventModifier;
    }

    #region Eventbroker registration
    [EventPublisher(EventTopics.GpsAdapter.GpsPosition)]
    public event EventHandler<GoodGuideEventArgs> NewGpsPosition;

    [EventPublisher(EventTopics.GpsAdapter.FixState)]
    public event EventHandler<GoodGuideEventArgs> GpsFixStateChanged;

    [EventSubscriber(EventTopics.GpsAdapter.RequestCurrentFixState)]
    public void RequestCurrentFixStateHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is GpsFixStateEvent == false)
        throw new InvalidOperationException(string.Format("Invalid event type {0} in {1}.RequestCurrentFixStateHandler", e.EventData.GetType().Name, GetType().Name));

      GpsFixStateEvent EventData = (GpsFixStateEvent)e.EventData;
      EventData.IsFixValid = isFixValid;
    }

    [EventSubscriber(EventTopics.GpsAdapter.GpsRawData)]
    public void GpsRawDataHandler(object sender, GoodGuideEventArgs e)
    {
      GpsRawEvent EventData = (GpsRawEvent)e.EventData;
      ProcessRawData(EventData);
    }

    #endregion

    public int InputCount
    {
      get { return inputCount; }
    }
    public int OutputCount
    {
      get { return outputCount; }
    }

    public void Initialise()
    {
      LoadConfiguration();
      StartTimer();
    }
    public void Finalise()
    {
      SaveConfiguration();
      StopTimer();
    }

    #region Event dispatchers
    private void OnNewGpsPosition(GpsPositionEvent gpsPositionEvent)
    {
      if (NewGpsPosition == null)
        return;

      try
      {
        GoodGuideEventArgs Event = new GoodGuideEventArgs(GetType().Name,gpsPositionEvent);
        NewGpsPosition(this, Event);
      }
      catch(Exception exc)
      {
        Logger.Write(this,"Error raising NewGpsPosition", exc);
      }
    }
    private void OnGpsFixStateChanged(Boolean gpsFixState, DateTime fixTime)
    {
      if (GpsFixStateChanged == null)
        return;

      GpsFixStateEvent EventData = new GpsFixStateEvent(gpsFixState,fixTime);

      try
      {
        GoodGuideEventArgs Event = new GoodGuideEventArgs(GetType().Name, EventData);
        GpsFixStateChanged(this, Event);
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error raising GpsFixStateChanged", exc);
      }
    }
    #endregion

    private void StartTimer()
    {
      lock(timerLock)
      {
        if (timer != null)
        {
          timer.Dispose();
          timer = null;
        }

        timer = new Timer(timerElapsed,null,FixTimeout,Timeout.Infinite);
      }
    }
    private void StopTimer()
    {
      lock (timerLock)
      {
        if (timer == null)
          return;

        timer.Dispose();
        timer = null;
      }
    }
    private void LoadConfiguration()
    {
      try
      {
        fixQualityThreshold = parameterRepository.GetInt16(ConfigurationParameters.Gps.FixQualityThreshold) ?? DefaultFixQualityThreshold;
        timeOffset = parameterRepository.GetInt16(ConfigurationParameters.Gps.TimeOffset) ?? DefaultTimeOffset;
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error loading configuration", exc);

        fixQualityThreshold = DefaultFixQualityThreshold;
        timeOffset = DefaultTimeOffset;
      }
    }
    private void SaveConfiguration()
    {
      try
      {
        parameterRepository.SetInt16(ConfigurationParameters.Gps.FixQualityThreshold, fixQualityThreshold);
        parameterRepository.SetInt16(ConfigurationParameters.Gps.TimeOffset, timeOffset);
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error saving configuration", exc);
      }
    }

    private void timerElapsed(object state)
    {
      Boolean fixTimeout = false;
      Logger.Write(this,"Checking fix timeout");
      Logger.Write(this, string.Format("Old:{0}, Current:{1}",oldInputCount,inputCount));

      lock (timerLock)
      {
        timer.Dispose();

        if (oldInputCount == inputCount)
          fixTimeout = true;

        oldInputCount = inputCount;

        timer = new Timer(timerElapsed, null, FixTimeout, Timeout.Infinite);
      }

      if ((isFixValid == true) && (fixTimeout == true))
        SetIsFixValid(false,DateTime.Now);
    }
       
    private void SetIsFixValid(Boolean value, DateTime fixTime)
    {
      Logger.Write(this, string.Format("Setting IsFixValid: {0}", value));

      if (value == isFixValid)
      {
        Logger.Write(this, string.Format("value == isFixValid == {0}. Returning", value));
        return;
      }

      Logger.Write(this, string.Format("Setting IsFixValid: {0}", value));

      isFixValid = value;
      OnGpsFixStateChanged(value, fixTime);
    }
    private void ProcessRawData(GpsRawEvent eventData)
    {
      #region Log some info
      Logger.Write(this, ">> ProcessRawData");

      Logger.Write(this, eventData.RmcData);
      Logger.Write(this, eventData.VtgData);
      Logger.Write(this, eventData.GgaData);
      #endregion

      #region Gather Status stats
      lock(InputLock)
      {
        inputCount++;
        GgaData = eventData.GgaData;
        RmcData = eventData.RmcData;
        VtgData = eventData.VtgData;
      }
      #endregion

      #region Parse NMEA strings
      Logger.Write(this, "  Parsing GPS raw data");
      GpRmc GpRmc;
      GpVtg GpVtg;
      GpGga GpGga;
      try
      {
        GpRmc = new GpRmc(eventData.RmcData);
        GpVtg = new GpVtg(eventData.VtgData);
        GpGga = new GpGga(eventData.GgaData);
      }
      catch (Exception exc)
      {
        Logger.Write(this, "  Error parsing GPS raw data", exc);
        return;
      }

      #region Log incoming raw data
      Logger.Write(this, "  GPRMC data");
      Logger.Write(this, string.Format("    FixDateTime:{0}", GpRmc.FixDateTime));
      Logger.Write(this, string.Format("    IsFixValid:{0}", GpRmc.IsFixValid));
      Logger.Write(this, string.Format("    Latitude:{0}", GpRmc.Latitude));
      Logger.Write(this, string.Format("    Longitude:{0}", GpRmc.Longitude));
      Logger.Write(this, string.Format("    MagneticVariation:{0}", GpRmc.MagneticVariation));
      Logger.Write(this, string.Format("    ModeIndicator:{0}", GpRmc.ModeIndicator));
      Logger.Write(this, string.Format("    SpeedOverGround:{0}", GpRmc.SpeedOverGround));
      Logger.Write(this, string.Format("    TrackAngle:{0}", GpRmc.TrackAngle));

      Logger.Write(this, "  GPVTG data");
      Logger.Write(this, string.Format("    GroundSpeedKmh:{0}", GpVtg.GroundSpeedKmh));
      Logger.Write(this, string.Format("    GroundSpeedKnots:{0}", GpVtg.GroundSpeedKnots));
      Logger.Write(this, string.Format("    ModeIndicator:{0}", GpVtg.ModeIndicator));
      Logger.Write(this, string.Format("    TrackMadeGoodMagnetic:{0}", GpVtg.TrackMadeGoodMagnetic));
      Logger.Write(this, string.Format("    TrackMadeGoodTrue:{0}", GpVtg.TrackMadeGoodTrue));

      Logger.Write(this, "  GPGGA data");
      Logger.Write(this, string.Format("    Altitude:{0}", GpGga.Altitude));
      Logger.Write(this, string.Format("    FixDateTime:{0}", GpGga.FixDateTime));
      Logger.Write(this, string.Format("    FixQuality:{0}", GpGga.FixQuality));
      Logger.Write(this, string.Format("    HeightOfGeoid:{0}", GpGga.HeightOfGeoid));
      Logger.Write(this, string.Format("    HorisontalDilutionOfPosition:{0}", GpGga.HorisontalDilutionOfPosition));
      Logger.Write(this, string.Format("    Latitude:{0}", GpGga.Latitude));
      Logger.Write(this, string.Format("    Longitude:{0}", GpGga.Longitude));
      Logger.Write(this, string.Format("    NumberOfSatellites:{0}", GpGga.NumberOfSatellites));

      Logger.Write(this, "  User data");
      Logger.Write(this, string.Format("    UserField1:{0}", eventData.UserField1));
      Logger.Write(this, string.Format("    UserField2:{0}", eventData.UserField2));
      Logger.Write(this, string.Format("    UserField3:{0}", eventData.UserField3));
      Logger.Write(this, string.Format("    UserField4:{0}", eventData.UserField4));
      Logger.Write(this, string.Format("    UserField5:{0}", eventData.UserField5));
      #endregion

      #endregion

      #region Create and populate GPS position event
      Logger.Write(this, "  Populate GPS position event");
      GpsPositionEvent GpsPositionEvent = new GpsPositionEvent();
      GpsPositionEvent.Source = eventData.Source;

      GpsPositionEvent.Altitude = GpGga.Altitude;
      GpsPositionEvent.FixQuality = GpGga.FixQuality;
      GpsPositionEvent.FixDtm = GpRmc.FixDateTime;
      GpsPositionEvent.Latitude = GpRmc.Latitude;
      GpsPositionEvent.Longitude = GpRmc.Longitude;
      GpsPositionEvent.Satellites = GpGga.NumberOfSatellites;
      GpsPositionEvent.Speed = GpVtg.GroundSpeedKmh;
      GpsPositionEvent.HDop = GpGga.HorisontalDilutionOfPosition;
      GpsPositionEvent.TickCount = DateTime.Now.Ticks;

      GpsPositionEvent.Mode = GpsPositionEvent.GpsFixType.FixType3d;
      GpsPositionEvent.Indicators = GpsPositionEvent.GpsQualityIndicators.Mode;

      if (timeOffset != 0)
        GpsPositionEvent.FixDtm += new TimeSpan(timeOffset, 0, 0);

      if (GpsPositionEvent.Speed < ValidHeadingMinimumSpeed)
        GpsPositionEvent.Heading = lastHeading;
      else
      {
        GpsPositionEvent.Heading = GpVtg.TrackMadeGoodTrue;
        lastHeading = GpsPositionEvent.Heading;
      }

      #region Log GPS event content
      Logger.Write(this, "  GPS event data:");
      Logger.Write(this, string.Format("    Altitude:{0}", GpsPositionEvent.Altitude));
      Logger.Write(this, string.Format("    FixDtm:{0}", GpsPositionEvent.FixDtm));
      Logger.Write(this, string.Format("    FixQuality:{0}", GpsPositionEvent.FixQuality));
      Logger.Write(this, string.Format("    HDop:{0}", GpsPositionEvent.HDop));
      Logger.Write(this, string.Format("    Heading:{0}", GpsPositionEvent.Heading));
      Logger.Write(this, string.Format("    Indicators:{0}", GpsPositionEvent.Indicators));
      Logger.Write(this, string.Format("    Latitude:{0}", GpsPositionEvent.Latitude));
      Logger.Write(this, string.Format("    Longitude:{0}", GpsPositionEvent.Longitude));
      Logger.Write(this, string.Format("    Mode:{0}", GpsPositionEvent.Mode));
      Logger.Write(this, string.Format("    PDop:{0}", GpsPositionEvent.PDop));
      Logger.Write(this, string.Format("    Satellites:{0}", GpsPositionEvent.Satellites));
      Logger.Write(this, string.Format("    Source:{0}", GpsPositionEvent.Source));
      Logger.Write(this, string.Format("    Speed:{0}", GpsPositionEvent.Speed));
      Logger.Write(this, string.Format("    TickCount:{0}", GpsPositionEvent.TickCount));
      Logger.Write(this, string.Format("    VDop:{0}", GpsPositionEvent.VDop));
      #endregion

      #endregion

      #region Gather some stats
      lock (OutputLock)
      {
        outputCount++;
      }
      #endregion

      #region Modify event if required
      if (eventModifier != null)
      {
        Logger.Write(this, "  Event modifier is set. Modifying event");
        eventModifier.Modify(eventData, GpsPositionEvent);
      }
      else
      {
        Logger.Write(this, "  Event modifier is not set");
      }
      #endregion

      #region Log GPS event content
      Logger.Write(this, "  GPS event data:");
      Logger.Write(this, string.Format("    Altitude:{0}", GpsPositionEvent.Altitude));
      Logger.Write(this, string.Format("    FixDtm:{0}", GpsPositionEvent.FixDtm));
      Logger.Write(this, string.Format("    FixQuality:{0}", GpsPositionEvent.FixQuality));
      Logger.Write(this, string.Format("    HDop:{0}", GpsPositionEvent.HDop));
      Logger.Write(this, string.Format("    Heading:{0}", GpsPositionEvent.Heading));
      Logger.Write(this, string.Format("    Indicators:{0}", GpsPositionEvent.Indicators));
      Logger.Write(this, string.Format("    Latitude:{0}", GpsPositionEvent.Latitude));
      Logger.Write(this, string.Format("    Longitude:{0}", GpsPositionEvent.Longitude));
      Logger.Write(this, string.Format("    Mode:{0}", GpsPositionEvent.Mode));
      Logger.Write(this, string.Format("    PDop:{0}", GpsPositionEvent.PDop));
      Logger.Write(this, string.Format("    Satellites:{0}", GpsPositionEvent.Satellites));
      Logger.Write(this, string.Format("    Source:{0}", GpsPositionEvent.Source));
      Logger.Write(this, string.Format("    Speed:{0}", GpsPositionEvent.Speed));
      Logger.Write(this, string.Format("    TickCount:{0}", GpsPositionEvent.TickCount));
      Logger.Write(this, string.Format("    VDop:{0}", GpsPositionEvent.VDop));
      #endregion

      #region Determine if fix is valid. Return if fix not valid
      Logger.Write(this, "  Checking if fix is valid");
      Logger.Write(this, "  Checking RMC if fix is valid");
      Boolean gpRmcFixValid = GpRmc.IsFixValid;
      Logger.Write(this, string.Format("  gpRmcFixValid: {0}", gpRmcFixValid));

      Logger.Write(this, string.Format("  Checing HDOP if fix is valid: HDOP:{0}, Threshold:{1}",GpsPositionEvent.HDop,fixQualityThreshold));
      Boolean hdopFixValid = GpsPositionEvent.HDop < fixQualityThreshold;
      Logger.Write(this, string.Format("  hdopFixValid: {0}", hdopFixValid));

      Boolean gpsIsFixValid = gpRmcFixValid && hdopFixValid;
      Logger.Write(this, string.Format("  gpsIsFixValid: {0}", gpsIsFixValid));

      if (gpsIsFixValid == false)
      {
        if (isFixValid == true)
        {
          Logger.Write(this, "  Current fix is valid. Setting to invalid");
          SetIsFixValid(false, DateTime.Now);
        }

        Logger.Write(this, "  GPS fix invalid. Returning...");

        return;
      }
      else
      {
        if (isFixValid == false)
        {
          Logger.Write(this, "  Current fix is invalid. Setting to valid");
          SetIsFixValid(true, GpRmc.FixDateTime);
        }
      }
      #endregion

      Logger.Write(this, "  Sending new GPS position event");
      OnNewGpsPosition(GpsPositionEvent);
    }

    #region Fields
    public LoggingHelper Logger = new LoggingHelper();
    private readonly INamedParameterRepository parameterRepository = null;
    private readonly IGpsEventModifier eventModifier;

    private Int16 fixQualityThreshold = 10;
    private volatile Boolean isFixValid = false;
    private Int16 timeOffset = 0;
    private float lastHeading = 0;

    public object InputLock = new object();
    public string RmcData = string.Empty;
    public string GgaData = string.Empty;
    public string VtgData = string.Empty;

    public object OutputLock = new object();
    private volatile Int32 outputCount = 0;
    private volatile Int32 inputCount = 0;
    private volatile Int32 oldInputCount = 0;
    private readonly object timerLock = new object();
    private System.Threading.Timer timer = null;
    #endregion

    private const Int32 FixTimeout = 10000;
  }
}