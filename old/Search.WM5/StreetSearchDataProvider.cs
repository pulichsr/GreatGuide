using System;
using Nucleo;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Threading;

namespace Nucleo.GoodGuide.Search
{
  public class StreetSearchDataProvider:
    IStreetSearchDataProvider,
    IDisposable
  {
    #region ctor
    public StreetSearchDataProvider(ILogger logger,ISearchStreetRepository streetRepository, ICurrentLocationSource locationSource, Int16 maxRows)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(streetRepository, "streetRepository");
      Guard.ArgumentNotNull(locationSource, "locationSource");
      if (maxRows < 1)
        throw new ArgumentException("Invalid maxRows");

      this.logger = logger;
      this.streetRepository = streetRepository;
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
    public DataObjects.Region Region
    {
      set
      {
        region = value;
      }
    }
    public DataObjects.Streets Data
    {
      get { return streets; }
      private set
      {
        streets = value;
        OnDataChanged();
      }
    }

    private void timer_TimerExpired(object sender, EventArgs e)
    {
      string criteria = searchCriteria.Trim().ToUpper();

      if (string.IsNullOrEmpty(criteria) == true)
      {
        Data = new DataObjects.Streets();
        return;
      }

      DataObjects.Streets unsortedStreets;

      try
      {
        if (region == null)
          unsortedStreets = streetRepository.GetMatching(criteria, maxRows);
        else
          unsortedStreets = streetRepository.GetMatching(region,criteria, maxRows);

        unsortedStreets.SortByDistance(locationSource.Latitude, locationSource.Longitude);
        Data = unsortedStreets;
      }
      catch (Exception exc)
      {
        logger.Write(this, "Error getting matching data", exc);
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
    private readonly ISearchStreetRepository streetRepository;
    private readonly ICurrentLocationSource locationSource;
    private readonly SingleShotTimer timer;
    private string searchCriteria;
    private DataObjects.Region region;
    private DataObjects.Streets streets;
    private readonly Int16 maxRows;
    #endregion
  }
}