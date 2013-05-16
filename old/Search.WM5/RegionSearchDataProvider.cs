using System;
using Nucleo;
using Nucleo.GoodGuide.Types.DataObjects;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Threading;

namespace Nucleo.GoodGuide.Search
{
  public class RegionSearchDataProvider:
    IRegionSearchDataProvider,
    IDisposable
  {
    #region ctor
    public RegionSearchDataProvider(ILogger logger,ISearchRegionRepository regionRepository, ICurrentLocationSource locationSource, Int16 maxRows)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(regionRepository, "regionRepository");
      Guard.ArgumentNotNull(locationSource, "locationSource");
      if (maxRows < 1)
        throw new ArgumentException("Invalid maxRows");

      this.logger = logger;
      this.regionRepository = regionRepository;
      this.locationSource = locationSource;
      this.maxRows = maxRows;

      timer = new SingleShotTimer(500);
      timer.TimerExpired += timer_TimerExpired;
    }
    public void Dispose()
    {
      timer.Dispose();
    }
    #endregion

    #region Events
    public event EventHandler DataChanged;
    #endregion

    public string SearchCriteria
    {
      get { return searchCriteria; }
      set
      {
        searchCriteria = value;
        timer.Start();
      }
    }
    public DataObjects.Regions Data
    {
      get { return regions; }
      private set
      {
        regions = value;
        OnDataChanged();
      }
    }

    private void timer_TimerExpired(object sender, EventArgs e)
    {
      string criteria = searchCriteria.Trim().ToUpper();

      if (string.IsNullOrEmpty(criteria) == true)
      {
        Data = new DataObjects.Regions();
        return;
      }

      try
      {
        Regions unsortedRegions = regionRepository.GetMatching(criteria, maxRows);
        unsortedRegions.SortByDistance(locationSource.Latitude,locationSource.Longitude);
        Data = unsortedRegions;
      }
      catch (Exception exc)
      {
        logger.Write(this,"Error getting matching data",exc);
      }
    }

    #region Event dispatchers
    private void OnDataChanged()
    {
      if (DataChanged == null)
        return;

      DataChanged(this, new EventArgs());
    }
    #endregion

    #region Fields
    private readonly ILogger logger;
    private readonly ISearchRegionRepository regionRepository;
    private readonly ICurrentLocationSource locationSource;
    private readonly SingleShotTimer timer;
    private string searchCriteria;
    private DataObjects.Regions regions;
    private readonly Int16 maxRows;
    #endregion
  }
}