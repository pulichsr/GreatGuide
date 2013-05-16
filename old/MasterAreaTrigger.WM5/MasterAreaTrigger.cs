using System;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Events.ControlEvents;

namespace Nucleo.GoodGuide.MasterAreaTrigger
{
  public class MasterAreaTrigger
  {
    public const Int16 CheckInterval = 60;

    public MasterAreaTrigger(IRegionCache cache)
    {
      this.cache = cache;

      regionChecker.RegionEnter += regionChecker_RegionEnter;
      regionChecker.RegionExit += regionChecker_RegionExit;
    }

    [EventPublisher(EventTopics.MasterAreaTrigger.MasterAreaEnter)]
    public event EventHandler<GoodGuideEventArgs> MasterAreaEnter;

    [EventPublisher(EventTopics.MasterAreaTrigger.MasterAreaExit)]
    public event EventHandler<GoodGuideEventArgs> MasterAreaExit;

    [EventSubscriber(EventTopics.MasterAreaTrigger.Configuration)]
    public void ConfigurationHandler(object sender, GoodGuideEventArgs e)
    {

    }

    [EventSubscriber(EventTopics.GpsAdapter.GpsPosition)]
    public void GpsPositionHandler(object sender,GoodGuideEventArgs e)
    {
      if (runState == false)
        return;

      TimeSpan ElapsedTime = DateTime.Now - lastCheckDtm;
      if (ElapsedTime.TotalSeconds < CheckInterval)
        return;

      if (e.EventData is GpsPositionEvent == false)
        throw new InvalidOperationException(string.Format("Invalid EventData {0} in {1}",e.EventData.GetType().Name,GetType().Name));

      GpsPositionEvent EventData = (GpsPositionEvent)e.EventData;

      regionChecker.CheckGpsPosition(cache.Regions,EventData);
      lastCheckDtm = DateTime.Now;
    }

    [EventSubscriber(EventTopics.MasterAreaTrigger.RunState)]
    public void RunStateHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is RunStateEvent == false)
        throw new InvalidOperationException(string.Format("Invalid EventData {0} in {1}", e.EventData.GetType().Name, GetType().Name));

      runState = ((RunStateEvent)e.EventData).IsRunning;
    }

    private void regionChecker_RegionEnter(object sender, RegionEventArgs e)
    {
      OnMasterAreaEnter(e.Region, e.GpsPositionEvent);
    }
    private void regionChecker_RegionExit(object sender, RegionEventArgs e)
    {
      OnMasterAreaExit(e.Region, e.GpsPositionEvent);
    }

    private void OnMasterAreaEnter(Region region, GpsPositionEvent gpsPosition)
    {
      if (MasterAreaEnter == null)
        return;

      MasterAreaEnterEvent EventData = new MasterAreaEnterEvent(region, gpsPosition);
      GoodGuideEventArgs EventArgs = new GoodGuideEventArgs(GetType().Name, EventData);

      MasterAreaEnter(this, EventArgs);
    }
    private void OnMasterAreaExit(Region region, GpsPositionEvent gpsPosition)
    {
      if (MasterAreaExit == null)
        return;

      MasterAreaEnterEvent EventData = new MasterAreaEnterEvent(region, gpsPosition);
      GoodGuideEventArgs EventArgs = new GoodGuideEventArgs(GetType().Name, EventData);

      MasterAreaExit(this, EventArgs);
    }

    private readonly IRegionCache cache = null;
    private readonly RegionChecker regionChecker = new RegionChecker(true);
    private DateTime lastCheckDtm = DateTime.MinValue;
    private Boolean runState = false;
  }
}