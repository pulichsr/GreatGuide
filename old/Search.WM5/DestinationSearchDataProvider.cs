using System;
using Nucleo;
using Nucleo.GoodGuide.Types.DataObjects;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Threading;

namespace Nucleo.GoodGuide.Search
{
  public class DestinationSearchDataProvider:
    IDestinationSearchDataProvider,
    IDisposable
  {
    #region ctor
    public DestinationSearchDataProvider(ILogger logger,IDestinationRepository destinationRepository, ICurrentLocationSource locationSource, Int16 maxRows)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(destinationRepository, "destinationRepository");
      Guard.ArgumentNotNull(locationSource, "locationSource");
      if (maxRows < 1)
        throw new ArgumentException("Invalid maxRows");

      this.logger = logger;
      this.destinationRepository = destinationRepository;
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
    public DataObjects.Destinations Data
    {
      get { return data; }
      private set
      {
        data = value;
        OnDataChanged();
      }
    }

    private void timer_TimerExpired(object sender, EventArgs e)
    {
      string criteria = searchCriteria.Trim().ToUpper();

      if (string.IsNullOrEmpty(criteria) == true)
      {
        Data = new DataObjects.Destinations();
        return;
      }

      DataObjects.Destinations unsorted = new Destinations();

      try
      {
        unsorted = destinationRepository.GetMatching(criteria, maxRows);

        unsorted.SortByDistance(locationSource.Latitude, locationSource.Longitude);
        Data = unsorted;
      }
      catch (Exception exc)
      {
        logger.Write(this,string.Format("Error getting matching data. Count:{0}",unsorted.Count),exc);
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
    private readonly IDestinationRepository destinationRepository;
    private readonly ICurrentLocationSource locationSource;
    private readonly SingleShotTimer timer;
    private string searchCriteria;
    private DataObjects.Destinations data;
    private readonly Int16 maxRows;
    #endregion
  }
}