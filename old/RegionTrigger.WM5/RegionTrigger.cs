using System;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.RegionTrigger
{
  public class RegionTrigger
  {
    public RegionTrigger(RegionCache cache)
    {
      this.cache = cache;

      RegionChecker.RegionEnter += regionChecker_RegionEnter;
      RegionChecker.RegionExit += regionChecker_RegionExit;
      RegionChecker.CurrentRegionsChanged += regionChecker_CurrentRegionsChanged;

    }

    public RegionChecker RegionChecker
    {
      get { return regionChecker; }
    }

    public void Initialise()
    {
    }
    public void Finalise()
    {
    }

    [EventPublisher(EventTopics.RegionTrigger.RegionEnter)]
    public event EventHandler<GoodGuideEventArgs> RegionEnter;

    [EventPublisher(EventTopics.RegionTrigger.RegionExit)]
    public event EventHandler<GoodGuideEventArgs> RegionExit;

    [EventPublisher(EventTopics.RegionTrigger.ActiveRegions)]
    public event EventHandler<GoodGuideEventArgs> ActiveRegions;

    [EventPublisher(EventTopics.System.ResetCounts)]
    public event EventHandler<GoodGuideEventArgs> SystemResetCounts;

    [EventSubscriber(EventTopics.RegionTrigger.Configuration)]
    public void ConfigurationHandler(object sender, GoodGuideEventArgs e)
    {

    }

    [EventSubscriber(EventTopics.GpsAdapter.GpsPosition)]
    public void GpsPositionHandler(object sender,GoodGuideEventArgs e)
    {
      if (e.EventData is GpsPositionEvent == false)
        throw new InvalidOperationException(string.Format("Invalid EventData {0} in {1}", e.EventData.GetType().Name, GetType().Name));

      GpsPositionEvent EventData = (GpsPositionEvent)e.EventData;

      try
      {
        //Console.WriteLine(string.Format("{0}|{1}|RegionTrigger| GPS processed time:{2}, Diff:{3}", DateTime.Now.ToString("HH:mm:ss"), Thread.CurrentThread.Name, EventData.FixDtm,EventData.FixDtm.Subtract(DateTime.Now).TotalSeconds));
        //Console.WriteLine(string.Format("{0}|{1}|RegionTrigger| Starting stopwatch before region-checking GPS position", DateTime.Now.ToString("HH:mm:ss"), Thread.CurrentThread.Name));
        //Stopwatch watch = new Stopwatch();
        //watch.Start();

        RegionChecker.CheckGpsPosition(cache.Regions,EventData);

        //watch.Stop();
        //Console.WriteLine(string.Format("{0}|{1}|RegionTrigger| Region-checked GPS position in {2} msec", DateTime.Now.ToString("HH:mm:ss"), Thread.CurrentThread.Name, watch.ElapsedMilliseconds));
      }
      catch (Exception exc)
      {
      }
    }

    [EventSubscriber(EventTopics.MasterAreaTrigger.MasterAreaEnter)]
    public void MasterAreaEnterHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is MasterAreaEnterEvent == false)
        throw new InvalidOperationException(string.Format("Invalid EventData {0} in {1}", e.EventData.GetType().Name, GetType().Name));

      MasterAreaEnterEvent EventData = (MasterAreaEnterEvent)e.EventData;

      logger.Write(this,string.Format("Loading regions for MasterArea {0}",EventData.Region.Id));
      cache.FetchByMasterArea(EventData.Region.Id);
      logger.Write(this, string.Format("Loaded {0} regions", cache.Regions.Count));
    }

    private void regionChecker_RegionEnter(object sender, RegionEventArgs e)
    {
      if (e.Region.ResetOnEntry == true)
        OnSystemResetCounts();

      logger.Write(this,string.Format("Entered region {0}",e.Region.Id));
      //Console.WriteLine(string.Format("{0}|{1}|RegionTrigger| Starting stopwatch before distributing region entry event", DateTime.Now.ToString("HH:mm:ss"), Thread.CurrentThread.Name));
      //Stopwatch watch = new Stopwatch();
      //watch.Start();

      OnRegionEnter(e.Region, e.GpsPositionEvent);

      //watch.Stop();
      //Console.WriteLine(string.Format("{0}|{1}|RegionTrigger| Distributed region entry event in {2} msec", DateTime.Now.ToString("HH:mm:ss"), Thread.CurrentThread.Name, watch.ElapsedMilliseconds));
    }
    private void regionChecker_RegionExit(object sender, RegionEventArgs e)
    {
      //Console.WriteLine(string.Format("{0}|{1}|RegionTrigger| Starting stopwatch before distributing region exit event", DateTime.Now.ToString("HH:mm:ss"), Thread.CurrentThread.Name));
      //Stopwatch watch = new Stopwatch();
      //watch.Start();

      logger.Write(this, string.Format("Exit region {0}", e.Region.Id));
      OnRegionExit(e.Region, e.GpsPositionEvent);

      //watch.Stop();
      //Console.WriteLine(string.Format("{0}|{1}|RegionTrigger| Distributed region exit event in {2} msec", DateTime.Now.ToString("HH:mm:ss"), Thread.CurrentThread.Name, watch.ElapsedMilliseconds));
    }
    private void regionChecker_CurrentRegionsChanged(object sender, RegionsEventArgs e)
    {
      OnActiveRegions(e.Regions);
    }

    private void OnRegionEnter(Region region,GpsPositionEvent gpsPosition)
    {
      if (RegionEnter == null)
        return;

      RegionEnterEvent EventData = new RegionEnterEvent(region,gpsPosition);
      GoodGuideEventArgs EventArgs = new GoodGuideEventArgs(GetType().Name, EventData);

      RegionEnter(this,EventArgs);
    }
    private void OnRegionExit(Region region, GpsPositionEvent gpsPosition)
    {
      if (RegionExit == null)
        return;

      RegionExitEvent EventData = new RegionExitEvent(region,gpsPosition);
      GoodGuideEventArgs EventArgs = new GoodGuideEventArgs(GetType().Name, EventData);

      RegionExit(this, EventArgs);
    }
    private void OnActiveRegions(Regions activeRegions)
    {
      if (ActiveRegions == null)
        return;

      ActiveRegionsEvent EventData = new ActiveRegionsEvent(activeRegions);
      GoodGuideEventArgs EventArgs = new GoodGuideEventArgs(GetType().Name, EventData);

      ActiveRegions(this, EventArgs);
    }
    private void OnSystemResetCounts()
    {
      if (SystemResetCounts == null)
        return;

      SystemResetCountsEvent Event = new SystemResetCountsEvent();
      GoodGuideEventArgs Args = new GoodGuideEventArgs("", Event);
      SystemResetCounts(this, Args);
    }

    public LoggingHelper logger = new LoggingHelper();
    private readonly RegionCache cache = null;
    private readonly RegionChecker regionChecker = new RegionChecker();
  }
}
