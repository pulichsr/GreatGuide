using System;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.Types
{
  public class RegionChecker
  {
    public RegionChecker()
    {
    }
    public RegionChecker(bool enterFirstOnly)
    {
      this.enterFirstOnly = enterFirstOnly;
    }

    public event EventHandler<RegionEventArgs> RegionEnter;
    public event EventHandler<RegionEventArgs> RegionExit;
    public event EventHandler<RegionsEventArgs> CurrentRegionsChanged;

    public Regions ActiveRegions
    {
      get
      {
         return activeRegions;
      }
      set { activeRegions = value; }
    }

    public void CheckGpsPosition(Regions regions,GpsPositionEvent gpsPosition)
    {
      Boolean RegionsChanged = false;

      #region Get all the regions overlapping the GPS position
      Regions RegionsOverlappingPosition = regions.InsideRegions(gpsPosition.Latitude, gpsPosition.Longitude);
      if (enterFirstOnly == true)
      {
        if (RegionsOverlappingPosition.Count > 1)
        {
          Region FirstRegion = RegionsOverlappingPosition[0];
          RegionsOverlappingPosition.Clear();
          RegionsOverlappingPosition.Add(FirstRegion);
        }
      }
      #endregion

      #region For all the regions overlapping the GPS position that are not in the active regions, publish a RegionEnter event
      for (Int32 RegionNo = 0;RegionNo < RegionsOverlappingPosition.Count;RegionNo++)
        if (ActiveRegions.Contains(RegionsOverlappingPosition[RegionNo]) == false)
        {
          RegionsChanged = true;

          OnRegionEnter(RegionsOverlappingPosition[RegionNo],gpsPosition);
        }
      #endregion

      #region For all the regions in the active regions not overlapping the GPS position, publish a RegionExitEvent
      for (Int32 RegionNo = 0; RegionNo < ActiveRegions.Count; RegionNo++)
        if (RegionsOverlappingPosition.Contains(ActiveRegions[RegionNo]) == false)
        {
          RegionsChanged = true;

          OnRegionExit(ActiveRegions[RegionNo], gpsPosition);
        }
      #endregion

      // The regions overlapping the GPS positions now become the active regions
      lock (ActiveRegionLock)
      {
        ActiveRegions = RegionsOverlappingPosition;
      }

      if (RegionsChanged == true)
        OnCurrentRegionsChanged(ActiveRegions);
    }

    private void OnRegionEnter(Region region, GpsPositionEvent gpsPositionEvent)
    {
      if (RegionEnter == null)
        return;

      RegionEnter(this,new RegionEventArgs(region,gpsPositionEvent));
    }
    private void OnRegionExit(Region region, GpsPositionEvent gpsPositionEvent)
    {
      if (RegionExit == null)
        return;

      RegionExit(this, new RegionEventArgs(region, gpsPositionEvent));
    }
    private void OnCurrentRegionsChanged(Regions currentRegions)
    {
      if (CurrentRegionsChanged == null)
        return;

      CurrentRegionsChanged(this, new RegionsEventArgs(currentRegions));
    }

    public object ActiveRegionLock = new object();
    private Regions activeRegions = new Regions();
    private Boolean enterFirstOnly = false;
  }

  public class RegionEventArgs : EventArgs
  {
    public RegionEventArgs(Region region,GpsPositionEvent gpsPositionEvent)
    {
      this.region = region;
      this.gpsPositionEvent = gpsPositionEvent;
    }

    public Region Region
    {
      get { return region; }
    }
    public GpsPositionEvent GpsPositionEvent
    {
      get { return gpsPositionEvent; }
    }

    private readonly Region region = null;
    private readonly GpsPositionEvent gpsPositionEvent = null;
  }
  public delegate void RegionEventHandler(object sender,RegionEventArgs e);

  public class RegionsEventArgs : EventArgs
  {
    public RegionsEventArgs(Regions regions)
    {
      this.regions = regions;
    }

    public Regions Regions
    {
      get { return regions; }
    }

    private readonly Regions regions = null;
  }
  public delegate void RegionsEventHandler(object sender, RegionsEventArgs e);
}