using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nucleo.GoodGuide.Search;
using Nucleo.GoodGuide.Types;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Text;
using Nucleo.Threading;
using Nucleo.WinCe;
using Telogis.GeoBase;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSearch : Form
  {
    public static DialogResult ShowModal(ILogger logger,DrillDownGeoCoder geoCoder, ISearchStreetRepository searchStreetRepository, ISearchRegionRepository searchRegionRepository,IDestinationRepository destinationRepository,ICurrentLocationSource currentLocationSource,out double latitude, out double longitude,out string name)
    {
      latitude = 0;
      longitude = 0;
      name = string.Empty;

      frmSearch frm = new frmSearch(logger,geoCoder,searchStreetRepository,searchRegionRepository,destinationRepository,currentLocationSource);
      DialogResult result = frm.ShowDialog();
      if (result == DialogResult.Cancel)
        return result;

      latitude = frm.latitude;
      longitude = frm.longitude;
      name = frm.name;

      return DialogResult.OK;
    }

    private frmSearch(ILogger logger, DrillDownGeoCoder geoCoder, ISearchStreetRepository searchStreetRepository, ISearchRegionRepository searchRegionRepository,IDestinationRepository destinationRepository ,ICurrentLocationSource currentLocationSource)
    {
      logger.Write(this, ">> ctor");

      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(geoCoder, "geoCoder");
      Guard.ArgumentNotNull(searchStreetRepository, "searchStreetRepository");
      Guard.ArgumentNotNull(searchRegionRepository, "searchRegionRepository");
      Guard.ArgumentNotNull(destinationRepository, "destinationRepository");
      Guard.ArgumentNotNull(currentLocationSource, "currentLocationSource");
      
      InitializeComponent();

      this.logger = logger;
      this.geoCoder = geoCoder;
      this.searchStreetRepository = searchStreetRepository;
      this.searchRegionRepository = searchRegionRepository;
      this.destinationRepository = destinationRepository;
      this.currentLocationSource = currentLocationSource;
      
      LoadResources();

      messageDisplay = new MessageDisplay();

      #region Street search
      searchStreetDataProvider = new StreetSearchDataProvider(logger,searchStreetRepository, currentLocationSource, 10);
      selectStreetDataProvider = new StreetSearchDataProvider(logger, searchStreetRepository, currentLocationSource, 100);
      streetLocationProvider = new DrillDownGeocoderStreetLocationProvider(logger, geoCoder);

      searchByStreetController = new SearchByStreetController(
        logger,
        searchRegionRepository,
        streetLocationProvider,
        new StreetSearcher(logger, searchStreetDataProvider),
        new StreetSelector(logger, selectStreetDataProvider),
        new StreetConfirmer(),
        new StreetNumberEntry(),
        busyIndicator,
        messageDisplay);
      #endregion

      #region Region search
      searchRegionDataProvider = new RegionSearchDataProvider(logger, searchRegionRepository, currentLocationSource, 10);
      selectRegionDataProvider = new RegionSearchDataProvider(logger, searchRegionRepository, currentLocationSource, 100);
      searchRegionStreetDataProvider = new StreetSearchDataProvider(logger, searchStreetRepository, currentLocationSource, 10);
      selectRegionStreetDataProvider = new StreetSearchDataProvider(logger, searchStreetRepository, currentLocationSource, 100);

      searchByRegionController = new SearchByRegionController(
        logger,
        searchRegionRepository,
        streetLocationProvider,
        new RegionSearcher(logger, searchRegionDataProvider),
        new RegionSelector(logger, selectRegionDataProvider),
        new RegionConfirmer(),
        new StreetSearcher(logger, searchRegionStreetDataProvider),
        new StreetSelector(logger, selectRegionStreetDataProvider),
        new StreetConfirmer(),
        new StreetNumberEntry(),
        busyIndicator,
        messageDisplay);

      #endregion

      #region Destination search
      searchDestinationDataProvider = new DestinationSearchDataProvider(logger, destinationRepository, currentLocationSource, 10);
      selectDestinationDataProvider = new DestinationSearchDataProvider(logger, destinationRepository, currentLocationSource, 100);

      searchDestinationController = new SearchDestinationController(
        logger,
        destinationRepository,
        new DestinationSearcher(logger, searchDestinationDataProvider),
        new DestinationSelector(logger, selectDestinationDataProvider),
        busyIndicator,
        messageDisplay);
      #endregion

      addressTimer = new SingleShotTimer(100);
      updateCurrentAddressDelegate = UpdateCurrentAddress;

      logger.Write(this, "<< ctor");
    }

    private void LoadResources()
    {
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
    }
    private void SearchByStreet()
    {
      WaitCursor.Show(true);

      GoodGuide.Search.SearchResult result = searchByStreetController.Execute(out latitude, out longitude);

      switch (result)
      {
        case GoodGuide.Search.SearchResult.Error:
          frmMessage.ShowText("Could not determine location");
          return;
        case GoodGuide.Search.SearchResult.Cancel:
          return;
      }

      name = string.Empty;
      Address address = GeoCoder.ReverseGeoCode(new LatLon(latitude, longitude));
      if (address != null)
        name = address.ToString();

      DialogResult = DialogResult.OK;
    }
    private void SearchByRegion()
    {
      WaitCursor.Show(true);

      GoodGuide.Search.SearchResult result = searchByRegionController.Execute(out latitude, out longitude);

      switch (result)
      {
        case GoodGuide.Search.SearchResult.Error:
          frmMessage.ShowText("Could not determine location");
          return;
        case GoodGuide.Search.SearchResult.Cancel:
          return;
      }

      name = string.Empty;
      Address address = GeoCoder.ReverseGeoCode(new LatLon(latitude, longitude));
      if (address != null)
        name = address.ToString();

      DialogResult = DialogResult.OK;
    }
    private void SearchDestinations()
    {
      WaitCursor.Show(true);

      DataObjects.Destination destination;
      GoodGuide.Search.SearchResult result = searchDestinationController.Execute(out destination);

      switch (result)
      {
        case GoodGuide.Search.SearchResult.Error:
          frmMessage.ShowText("Could not determine location");
          return;
        case GoodGuide.Search.SearchResult.Cancel:
          return;
      }

      logger.Write(this,string.Format("Selected destination {0}",destination.Code));
      latitude = (double)destination.Latitude;
      longitude = (double)destination.Longitude;
      name = destination.Code;

      DialogResult = DialogResult.OK;
    }
    private void UpdateCurrentAddress(string address)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(updateCurrentAddressDelegate,address);
      }
      else
      {
        lblAddress.Text = address;
      }
    }

    private void frmSearch_Load(object sender, System.EventArgs e)
    {
      WaitCursor.Show(false);
      addressTimer.TimerExpired += addressTimer_TimerExpired;

      if (currentLocationSource.Latitude != 0)
        addressTimer.Start();
    }
    private void frmSearch_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      addressTimer.TimerExpired -= addressTimer_TimerExpired;
    }
    private void btnByStreet_Click(object sender, System.EventArgs e)
    {
      SearchByStreet();
    }
    private void btnByRegion_Click(object sender, System.EventArgs e)
    {
      SearchByRegion();
    }
    private void btnSearchDestinations_Click(object sender, System.EventArgs e)
    {
      SearchDestinations();
    }
    private void btnBack_Click(object sender, System.EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
    private void btnSearchGpsLocation_Click(object sender, System.EventArgs e)
    {
      #region Get lat/long
      DialogResult Result = frmEnterLatLong.EnterValues(out latitude, out longitude);
      if (Result == DialogResult.Cancel)
        return;
      #endregion

      name = string.Empty;
      Address address = GeoCoder.ReverseGeoCode(new LatLon(latitude, longitude));
      if (address != null)
        name = address.ToString();
    }
    private void addressTimer_TimerExpired(object sender, System.EventArgs e)
    {
      addressTimer.Dispose();

      try
      {
        GeocodedAddressFactory factory = new GeocodedAddressFactory(geoCoder);
        IEnumerable<string> addressEntries = factory.Create(currentLocationSource.Latitude, currentLocationSource.Longitude);

        UpdateCurrentAddress(StringUtils.Pack(", ", addressEntries));
      }
      catch (Exception exc)
      {
        logger.Write(this,"Error getting current address",exc);
      }
    }

    #region Fields
    private readonly ILogger logger;
    private readonly DrillDownGeoCoder geoCoder;
    private readonly ICurrentLocationSource currentLocationSource;
    private readonly IBusyIndicator busyIndicator = new BusyIndicator();
    private readonly SingleShotTimer addressTimer;

    private readonly ISearchStreetRepository searchStreetRepository;
    private readonly ISearchRegionRepository searchRegionRepository;
    private readonly IDestinationRepository destinationRepository;

    private readonly SearchByStreetController searchByStreetController;
    private readonly IStreetSearchDataProvider searchStreetDataProvider;
    private readonly IStreetSearchDataProvider selectStreetDataProvider;
    private readonly IStreetLocationProvider streetLocationProvider;

    private readonly SearchByRegionController searchByRegionController;
    private readonly IRegionSearchDataProvider searchRegionDataProvider;
    private readonly IRegionSearchDataProvider selectRegionDataProvider;
    private readonly IStreetSearchDataProvider searchRegionStreetDataProvider;
    private readonly IStreetSearchDataProvider selectRegionStreetDataProvider;
    private readonly IMessageDisplay messageDisplay;

    private readonly SearchDestinationController searchDestinationController;
    private readonly IDestinationSearchDataProvider searchDestinationDataProvider;
    private readonly IDestinationSearchDataProvider selectDestinationDataProvider;

    private double latitude;
    private double longitude;
    private string name;
    private UpdateStringDelegate updateCurrentAddressDelegate;
    #endregion

  }
  
}