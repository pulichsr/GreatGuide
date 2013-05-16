using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.Threading;
using Telogis.GeoBase;
using Telogis.GeoBase.Mobile;
using Telogis.GeoBase.Navigation;
using Telogis.GeoBase.Repositories;
using Telogis.GeoBase.Routing;
using Direction=Telogis.GeoBase.Routing.Direction;
using Timer=System.Threading.Timer;


namespace Nucleo.GoodGuide.GeoBaseNavigator
{

  public class GeoBaseNavigator : INavigator
  {
    #region Const

    #region Repository Keys
    public const string ResourcePathKey = "GeoBaseNavigator.ResourcePath";
    public const string RepositoryPathKey = "GeoBaseNavigator.RepositoryPath";
    public const string SoundPathKey = "GeoBaseNavigator.SoundPath";
    public const string LanguageInUseKey = "GeoBaseNavigator.LanguageInUse";

    public const string NotificationDefinitionsKey = "GeoBaseNavigator.NotificationDefinitions";
    public const string NotificationsTimeoutKey = "GeoBaseNavigator.NotificationsTimeout";
    public const string IsDepartureNotificationActiveKey = "GeoBaseNavigator.IsDepartureNotificationActive";
    public const string IsAfterTurnDistanceNotificationActiveKey = "GeoBaseNavigator.IsAfterTurnDistanceNotificationActive";
    public const string IsRoundaboutExitNotificationActiveKey = "GeoBaseNavigator.IsRoundaboutExitNotificationActive";
    public const string IsNotificationsActiveKey = "GeoBaseNavigator.IsNotificationsActive";
    public const string RoutingStrategyKey = "GeoBaseNavigator.RoutingStrategy";
    #endregion

    public const Int32 DefaultRadius = 30;
    public const Language GeoBaseDefaultLanguage = Language.English;

    /* GPS Latitude and Longitude Distance Calculator
     * http://www.csgnetwork.com/gpsdistcalc.html
     */
    public const double OneMeterDistanceInDegreesOnLongitude = 0.000006F;
    #endregion

    #region ctor
    public GeoBaseNavigator(INamedParameterRepository parameterRepository,Language language)
    {
      this.parameterRepository = parameterRepository;
      this.languageInUse = language;

      this.Logger.RegisterChild(geoBaseGps.Logger);
      delUpdateDistance = UpdateDistance;
      distanceTextTimer = new MultiShotTimer(1000);
    }
    #endregion

    #region INavigator
    public event EventHandler BeforeTurnNotification;
    public event EventHandler AfterTurnNotification;
    public event EventHandler DirectionsChanged;

    public bool IsNotificationsActive
    {
      set
      {
        isNotificationsActive = value;
      }
    }
    public ColourScheme ColourScheme
    {
      set { colourScheme = value; }
    }
    public string[] Directions
    {
      get
      {
        try
        {
          #region Get directions
          navigationManager.Navigator.Directions.MessagesBundle = navigationManager.Navigator.MessagesBundle;
          Directions directions = navigationManager.Navigator.Directions;
          if (directions == null)
            return new string[0];
          #endregion

          double totalDist = 0;
          List<string> instructions = new List<string>();
          string distanceUnitText = "km";
          DistanceUnit distanceUnit = DistanceUnit.KILOMETERS;
          switch(units)
          {
            case Units.Metric:
              distanceText = "km";
              distanceUnit = DistanceUnit.KILOMETERS;
              break;
            case Units.Imperial:
              distanceText = "mi";
              distanceUnit = DistanceUnit.MILES;
              break;
          }

          #region Create directions list
          foreach (Direction direction in directions)
          {
            /* A note isn't as detailed as a Movement, so just display its instructions */
            if (direction is Note)
            {
              instructions.Add(string.Format("Note: {0}", direction.Instructions));
            }

            if (direction is Movement)
            {
              if (units == Units.Metric)
                totalDist += direction.GetDistance(distanceUnit);
              else
                totalDist += direction.GetDistance(distanceUnit);

              if (direction.IsDeparture)
              {
                instructions.Add(string.Format("Start: {0}", direction.Instructions));
              }

              if (direction.IsArrival)
              {
                instructions.Add(string.Format("End: {0}", direction.Instructions));
              }
              else
              {
                instructions.Add(string.Format("{0} {1}: {2}", totalDist.ToString("0.0"), distanceText,direction.Instructions));
                //instructions.Add(string.Format("Now traveling {0} on '{1}'", direction.HeadingString, direction.Street));
              }
            }
          }
          #endregion

          return instructions.ToArray();
        }
        catch (Exception exc)
        {
          Logger.Write(this,"Error getting directions",exc);
          return new string[0];
        }
      }
    }

    public void Initialise(Control mapControl)
    {
      Logger.Write(this, ">> Initialise");

      Logger.Write(this, "   Increasing pages");
      Ram.IncreasePages(20);

      Logger.Write(this, "   Loading configuration");
      LoadConfiguration();
      InitialiseGeoBasePoiCategories();
      
      MapCtrl mapCtrl = (MapCtrl) mapControl;

      //Set up default data repository for all threads
      string multiRepositoryPath = System.IO.Path.Combine(resourcePath,repositoryPath);
      Logger.Write(this, string.Format("MultiRepositoryPath: {0}", multiRepositoryPath));
      if (Directory.Exists(multiRepositoryPath) == false)
        Logger.Write(this,string.Format("MultiRepositoryPath {0} not found",multiRepositoryPath));
      Repository.Default = new MultiRepository(multiRepositoryPath);
     
      //Set up navigation manager
      string languageRepositoryPath = System.IO.Path.Combine(resourcePath, soundPath);
      Logger.Write(this, string.Format("LanguageRepositoryPath: {0}", languageRepositoryPath));
      if (Directory.Exists(languageRepositoryPath) == false)
        Logger.Write(this, string.Format("LanguageRepositoryPath {0} not found", languageRepositoryPath));

      Logger.Write(this, string.Format("languageInUse: {0}", languageInUse));
      CultureInfo culture = LanguageHelper.CreateCulture(languageInUse);
      if (culture == null)
        culture = new CultureInfo("en-US");
      Logger.Write(this, string.Format("Culture: {0}", culture.Name));
      navigationManager = new NavigationManager(mapCtrl, languageRepositoryPath, culture);

      navigationManager.Navigator.Strategy = RoutingStrategyFactory.Create(routingStrategy);
      navigationManager.Navigator.Update += Navigator_Update;
      Logger.Write(this, string.Format("Set routing strategy to {0}",navigationManager.Navigator.Strategy.GetType().Name));

      //Add to renderer list
      mapCtrl.Renderer = renderList;
      renderList.Add(navigationManager);

      SetupNotifications();
      CreateControls(mapCtrl);

      // Inject the Gps manager
      navigationManager.SetGps(geoBaseGps);
      Logger.Write(this,navigationManager.Announcer.GetType().FullName );

      distanceTextTimer.TimerExpired += distanceTextTimer_TimerExpired;
    }
    public void Finalise()
    {
      if (navigationManager == null) 
        return;

      CancelRoute();
      navigationManager.Navigator.Gps.PowerDown();

      distanceTextTimer.TimerExpired -= distanceTextTimer_TimerExpired;
    }

    public bool RouteTo(float latitude, float longitude)
    {
      Logger.Write(this, string.Format("RouteTo: {0},{1}", latitude, longitude));

      if (navigationManager == null)
      {
        Logger.Write(this,"NavigationManager not initialised");
        return false;
      }

      try
      {
        Logger.Write(this, string.Format("SetDestination: {0},{1}", latitude, longitude));
        navigationManager.SetControls(lbInstructions, null, null, turnbox, lbEta);
        navigationManager.SetDestination(new LatLon(latitude, longitude));
        renderList.Add(lbDirections);
        renderList.Add(lbDistance);
        isRouting = true;

        distanceTextTimer.Start();
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error in RouteTo",exc);
      }

      return true;
    }
    public void CancelRoute()
    {
      Logger.Write(this, string.Format("CancelRoute"));

      if (navigationManager == null)
      {
        Logger.Write(this, "NavigationManager not initialised");
        return;
      }

      try
      {
        isRouting = false;
        navigationManager.SetControls(null, null, null, null, null);
        navigationManager.Stop();
        renderList.Remove(lbDirections);
        renderList.Remove(lbDistance);

        distanceTextTimer.Stop();
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error in CancelRoute", exc);
      }
    }

    public PoiCategories GetPoiCategories()
    {
      if (navigationManager == null) return null;
      PoiCategories poiCategories = new PoiCategories();
      foreach (KeyValuePair<int, string> keyValuePair in geoBasePoiCategories)
      {
        poiCategories.Add(new PoiCategory(keyValuePair.Key, keyValuePair.Value));
      }
      return poiCategories;
    }
    public PoiSubcategories GetPoiSubCategories(PoiCategory category)
    {
      return null;
    }
    public DestinationDataset.DestinationDataTable GetPoiDestinations(PoiCategory category, PoiSubcategory subcategory,float latitude, float longitude, int radiusMeters)
    {
      if (navigationManager == null) return null;

      DestinationDataset.DestinationDataTable PoiDestinations = null;

      #region Get Pois from map
      BoundingBox boundingBox = new BoundingBox();
      boundingBox.Add(new LatLon(latitude, longitude));
      boundingBox.Inflate((OneMeterDistanceInDegreesOnLongitude * radiusMeters));
      Poi[] poiArray = DataQuery.QueryPoi(boundingBox, new PoiType[] { (PoiType)category.Index }, null);
      #endregion

      if (poiArray.Length > 0)
      {
        PoiDestinations = new DestinationDataset.DestinationDataTable();

        foreach (Poi poiElement in poiArray)
        {
          Address address = GeoCoder.ReverseGeoCode(new LatLon(poiElement.Location.Lat, poiElement.Location.Lon));

          DestinationDataset.DestinationRow PoiDestination = PoiDestinations.NewDestinationRow();
          PoiDestination.Code = poiElement.Type.ToString();                         // Point.Description;
          PoiDestination.Latitude = (float)poiElement.Location.Lat;                 // Point.Latitude;
          PoiDestination.Longitude = (float)poiElement.Location.Lon;                // Point.Longtitude;
          PoiDestination.TelephoneNo = poiElement.Phone;                            // Point.Telephone;
          if (address != null)
          {
            PoiDestination.Address = string.Format("{0}\n\r{1}\n\r{2}\r\n{3}", 
              address.Number, address.PrimaryName, address.City, address.PostalCode // Point.House, Point.Street, Point.City, Point.Zip
              );
            PoiDestination.Location = address.City;                                 // Point.City;
          }
          else
          {
            PoiDestination.Address = string.Empty; 
            PoiDestination.Location = string.Empty;
          }
          PoiDestination.ArrivalZoneData = CircularRegion.GetRegionData(PoiDestination.Latitude, PoiDestination.Longitude, DefaultRadius);
          float MinLatitude;
          float MaxLatitude;
          float MinLongitude;
          float MaxLongitude;
          CircularRegion.CalculateMinMaxLatLong(PoiDestination.Latitude, 
                                                PoiDestination.Longitude, 
                                                DefaultRadius, 
                                                out MinLatitude, out MaxLatitude, out MinLongitude, out MaxLongitude);
          PoiDestination.ArrivalZoneMinLatitude = MinLatitude;
          PoiDestination.ArrivalZoneMaxLatitude = MaxLatitude;
          PoiDestination.ArrivalZoneMinLongitude = MinLongitude;
          PoiDestination.ArrivalZoneMaxLongitude = MaxLongitude;

          PoiDestinations.AddDestinationRow(PoiDestination);
        }
      }
      return PoiDestinations;
    }
    public NavigatorAddresses SearchStreetAdress(string searchText)
    {
      Logger.Write(this,string.Format("Searching for street address: {0}",searchText));
      NavigatorAddresses addresses = new NavigatorAddresses();

      try
      {
        GeocodeAddress[] geoAddresses = GeoCoder.GeoCode(searchText, Country.SouthAfrica);
        foreach (GeocodeAddress geoAddress in geoAddresses)
        {
          string displayText = string.Format("{0} {1} {2}",geoAddress.Line1,geoAddress.City,geoAddress.Region);
          NavigatorAddress address = new NavigatorAddress(displayText,geoAddress);
          addresses.Add(address);
        }

        Logger.Write(this, string.Format("Found {0} street address(es)", addresses.Count));
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error in SearchStreetAdress", exc);
        return addresses;
      }

      return addresses;
    }

    public bool SetLanguage(Language language)
    {
      Logger.Write(this, string.Format("SetLanguage: {0}", language));

      if (navigationManager == null)
      {
        Logger.Write(this, "NavigationManager not initialised");
        return false;
      }

      try
      {
        languageInUse = language;
        parameterRepository.SetInt32(LanguageInUseKey, (Int32)languageInUse);

        CultureInfo newCulture = LanguageHelper.CreateCulture(languageInUse);
        Logger.Write(this,string.Format("New culture:{0}",newCulture.Name));

        navigationManager.Suspend();
        navigationManager.ChangeCulture(System.IO.Path.Combine(resourcePath, soundPath), newCulture,false);
        navigationManager.Resume();
        navigationManager.Navigator.RecalculateRoute();
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error in SetLanguage", exc);
      }
      return true;
    }

    public bool SetUnits(Units value)
    {
      Logger.Write(this, string.Format("SetUnits: {0}", units));
      this.units = value;

      if (navigationManager == null)
      {
        Logger.Write(this, "NavigationManager not initialised");
        return false;
      }

      /*
        Member name   Value Description 
        Unknown       0     Unsure of which system.  
        Metric        1     Using Metric (meters).  
        ImperialFeet  2     Using Imperial (feet).  
        ImperialYards 3     Using Imperial (yards).  
       */

      try
      {
        switch (units)
        {
          case Units.Metric:
            navigationManager.Units = UnitSystem.Metric;
            break;
          case Units.Imperial:
            navigationManager.Units = UnitSystem.ImperialFeet;
            break;
          default:
            return false;
        }
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error in SetUnits", exc);
      }

      return true;
    }

    public bool GetAddressOfPosition(float latitude, float longitude,
                                     out string house, out string street, out string city, out string zip,
                                     out string description, out string telephoneNo)
    {
      Logger.Write(this, string.Format("GetAddressOfPosition: {0}.{1}",latitude,longitude));

      house = street = city = zip = description = telephoneNo = string.Empty;

      if (navigationManager == null)
      {
        Logger.Write(this, "NavigationManager not initialised");
        return false;
      }

      try
      {
        Address address = GeoCoder.ReverseGeoCode(new LatLon(latitude, longitude));
        if (address != null)
        {
          house = address.Number.ToString();
          street = address.PrimaryName;
          city = address.City;
          zip = address.PostalCode;
          description = address.Line1;
          telephoneNo = string.Empty;

          Logger.Write(this,string.Format("House: {0}",house));
          Logger.Write(this,string.Format("Street: {0}",street));
          Logger.Write(this, string.Format("City: {0}", city));
          Logger.Write(this, string.Format("Zip: {0}", zip));
          Logger.Write(this, string.Format("Description: {0}", description));
          Logger.Write(this, string.Format("TelephoneNo: {0}", telephoneNo));
        }
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error in GetAddressOfPosition", exc);
      }
      return true;
    }

    public bool GpsDataHandler(GpsPositionEvent eventData)
    {
//      Logger.Write(this,string.Format("Queueing GPS data: {0},{1}",eventData.Latitude,eventData.Longitude));
      geoBaseGps.QueueEventData(eventData);
      return true;
    }

    #endregion

    #region Private Properties
    #endregion

    #region Private Methods

    private void LoadConfiguration()
    {
      Logger.Write(this, ">> LoadConfiguration");

      resourcePath = parameterRepository.GetString(ResourcePathKey);
      if (resourcePath == null)
      {
        resourcePath = "SDMMC\\GeoBaseResources\\";// Nucleo.Path.ExecutablePath;
        parameterRepository.SetString(ResourcePathKey, resourcePath);
      }
      repositoryPath = parameterRepository.GetString(RepositoryPathKey);
      if (repositoryPath == null)
      {
        repositoryPath = @"data\gb.3.7";
        parameterRepository.SetString(RepositoryPathKey, repositoryPath);
      }
      soundPath = parameterRepository.GetString(SoundPathKey);
      if (soundPath == null)
      {
        soundPath = "langs";
        parameterRepository.SetString(SoundPathKey, soundPath);
      }

      Int32? _notificationsTimeout = parameterRepository.GetInt32(NotificationsTimeoutKey);
      if (_notificationsTimeout == null)
      {
        notificationsTimeout = 10;
        parameterRepository.SetInt32(NotificationsTimeoutKey, notificationsTimeout);
      }
      else
        notificationsTimeout = _notificationsTimeout.Value;

      routingStrategy = parameterRepository.GetString(RoutingStrategyKey);
      if (routingStrategy == null)
      {
        routingStrategy = "F";
        parameterRepository.SetString(RoutingStrategyKey, routingStrategy);
      }

      notificationDefinitionsMetadata = parameterRepository.GetString(NotificationDefinitionsKey);
      if (string.IsNullOrEmpty(notificationDefinitionsMetadata) == true)
      {
        notificationDefinitionsMetadata = "T,0,3;T,0,8;T,0,20";
        parameterRepository.SetString(NotificationDefinitionsKey, notificationDefinitionsMetadata);
      }

      isDepartureNotificationActive = parameterRepository.GetBoolean(IsDepartureNotificationActiveKey);
      if (isDepartureNotificationActive == null)
      {
        isDepartureNotificationActive = true;
        parameterRepository.SetBoolean(IsDepartureNotificationActiveKey, isDepartureNotificationActive.Value);
      }

      isAfterTurnDistanceNotificationActive = parameterRepository.GetBoolean(IsAfterTurnDistanceNotificationActiveKey);
      if (isAfterTurnDistanceNotificationActive == null)
      {
        isAfterTurnDistanceNotificationActive = true;
        parameterRepository.SetBoolean(IsAfterTurnDistanceNotificationActiveKey, isAfterTurnDistanceNotificationActive.Value);
      }

      isRoundaboutExitNotificationActive = parameterRepository.GetBoolean(IsRoundaboutExitNotificationActiveKey);
      if (isRoundaboutExitNotificationActive == null)
      {
        isRoundaboutExitNotificationActive = true;
        parameterRepository.SetBoolean(IsRoundaboutExitNotificationActiveKey, isRoundaboutExitNotificationActive.Value);
      }

      Logger.Write(this, string.Format("  resourcePath:{0}", resourcePath));
      Logger.Write(this, string.Format("  repositoryPath:{0}", repositoryPath));
      Logger.Write(this, string.Format("  soundPath:{0}", soundPath));
      Logger.Write(this, string.Format("  languageInUse:{0}", languageInUse));
      Logger.Write(this, string.Format("  notificationsTimeout:{0}", notificationsTimeout));
      Logger.Write(this, string.Format("  routingStrategy:{0}", routingStrategy));
      Logger.Write(this, string.Format("  notificationDefinitionsMetadata:{0}", notificationDefinitionsMetadata));
      Logger.Write(this, string.Format("  isDepartureNotificationActive:{0}", isDepartureNotificationActive));
      Logger.Write(this, string.Format("  isAfterTurnDistanceNotificationActive:{0}", isAfterTurnDistanceNotificationActive));
      Logger.Write(this, string.Format("  isRoundaboutExitNotificationActive:{0}", isRoundaboutExitNotificationActive));

      colourScheme = ColourScheme.Day;
    }

    #region Notifications
    private void SetupNotifications()
    {
      Logger.Write(this, ">> SetupNotifications");
      navigationManager.AnnouncerEnabled = false;
      navigationManager.Navigator.ClearNotifications();

      NotificationDefinitions notificationDefinitions;
      try
      {
        Logger.Write(this, string.Format("   Getting notification from metadata {0}",notificationDefinitionsMetadata));
        notificationDefinitions = NotificationDefinitionsFactory.Create(notificationDefinitionsMetadata);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error creating notification definitions from metadata",exc);
        return;
      }

      foreach (NotificationDefinition notificationDefinition in notificationDefinitions)
      {
        switch (notificationDefinition.NotificationType)
        {
          case NotificationType.Time:
            Logger.Write(this, string.Format("   Creating BeforeTurnTimeNotification: ({0}-{1})", notificationDefinition.Lower, notificationDefinition.Upper));
            navigationManager.Navigator.AddNotification(new BeforeTurnTimeNotification(TimeSpan.FromSeconds(notificationDefinition.Upper), TimeSpan.FromSeconds(notificationDefinition.Lower), Announce));
            break;
          case NotificationType.Distance:
            Logger.Write(this, string.Format("   Creating BeforeTurnDistanceNotification: ({0}-{1})", notificationDefinition.Lower, notificationDefinition.Upper));
            navigationManager.Navigator.AddNotification(new BeforeTurnDistanceNotification(notificationDefinition.Upper, notificationDefinition.Lower, DistanceUnit.METERS, Announce));
            break;
        }
      }

      if (isDepartureNotificationActive == true)
      {
        Logger.Write(this, "Creating DepartureNotification");
        navigationManager.Navigator.AddNotification(new DepartureNotification(Announce));
      }
      else
        Logger.Write(this, "DepartureNotification not enabled");

      if (isAfterTurnDistanceNotificationActive == true)
      {
        Logger.Write(this, "Creating AfterTurnDistanceNotification");
        navigationManager.Navigator.AddNotification(new AfterTurnDistanceNotification(40, DistanceUnit.METERS, Announce));
      }
      else
        Logger.Write(this, "AfterTurnDistanceNotification not enabled");

      if (isRoundaboutExitNotificationActive == true)
      {
        Logger.Write(this, "Creating RoundaboutExitNotification");
        navigationManager.Navigator.AddNotification(new RoundaboutExitNotification(QuickBeep));
      }
      else
        Logger.Write(this, "RoundaboutExitNotification not enabled");
    }
    private void Announce(object sender, NavigationEvent e)
    {
      Logger.Write(this, ">> Announce");
      Logger.Write(this, string.Format("  DirectionQualifier:{0}", e.DirectionQualifier));
      Logger.Write(this, string.Format("  DirectionType:{0}", e.DirectionType));
      Logger.Write(this, string.Format("  GetDistance:{0}", e.GetDistance(DistanceUnit.METERS)));
      Logger.Write(this, string.Format("  Number:{0}", e.Number));
      Logger.Write(this, string.Format("  TargetStreet:{0}", e.TargetStreet));
      Logger.Write(this, string.Format("  TurnDirection:{0}", e.TurnDirection));

      if (isNotificationsActive == false)
      {
        Logger.Write(this, "Notification not active");
        return;
      }

      StartNotificationsTimer();
      navigationManager.Announcer.SayMovement(e, navigationManager.Navigator.NextNavigationEvent);
    }
    private void QuickBeep(object sender, NavigationEvent e)
    {
      Logger.Write(this, ">> QuickBeep");

      if (isNotificationsActive == false)
      {
        Logger.Write(this, "Notification not active");
        return;
      }

      navigationManager.Announcer.Bell();
    }

    private void StartNotificationsTimer()
    {
      Logger.Write(this, "Starting NotificationsTimer");
      lock (notificationTimerLock)
      {
        Boolean wasRunning = notificationTimer != null;

        if (wasRunning == true)
          notificationTimer.Dispose();

        Logger.Write(this, "  Creating NotificationsTimer");
        notificationTimer = new Timer(NotificationTimeout, null, notificationsTimeout * 1000, Timeout.Infinite);

        if (wasRunning == false)
          OnBeforeTurnNotification();
      }
    }
    private void NotificationTimeout(object state)
    {
      Logger.Write(this, ">> NotificationTimeout");
      lock (notificationTimerLock)
      {
        if (notificationTimer == null)
          return;

        Logger.Write(this, "  Disposing NotificationsTimer");
        notificationTimer.Dispose();
        notificationTimer = null;

      }

      OnAfterTurnNotification();
    }
    #endregion

    private void CreateControls(MapCtrl control)
    {
      /* | TurnBox  |
       * | Distance |
       * |
       * |
       * | Eta |   | Instructions |   | Directions |
       * 
       * */
      turnbox = new TurnBox();
      turnbox.DrivingSide = DrivingSide.Left;
      turnbox.Size = new Size(80, 80);
      turnbox.Location = new System.Drawing.Point(0,0);

      lbDistance = new LabelBox();
      lbDistance.Size = new Size(80, 40);
      lbDistance.Location = new System.Drawing.Point(0, turnbox.Height);
      lbDistance.Font = new Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);

      lbEta = new LabelBox();
      lbEta.Size = new Size(90, 50);
      lbEta.Location = new System.Drawing.Point(0, control.Height - 50);

      lbDirections = new LabelBox();
      lbDirections.Text = "Directions";
      lbDirections.Size = new Size(80, 40);
      lbDirections.Location = new System.Drawing.Point(control.Width - lbDirections.Width, control.Height - 50);
      lbDirections.Click += lbDirections_Click;

      lbInstructions = new LabelBox();
      lbInstructions.Size = new Size(lbDirections.Left - lbEta.Right, 50);
      lbInstructions.Location = new System.Drawing.Point(lbEta.Right, control.Height - 50);
      lbInstructions.Font = new Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
    }
    private void InitialiseGeoBasePoiCategories()
    {
      geoBasePoiCategories.Add(0, "Unknown");
      geoBasePoiCategories.Add(2084, "Winery");
      geoBasePoiCategories.Add(3578, "ATM");
      geoBasePoiCategories.Add(4013, "TrainStation");
      geoBasePoiCategories.Add(4100, "CommuterRailStation");
      geoBasePoiCategories.Add(4170, "BusStation");
      geoBasePoiCategories.Add(4482, "FerryTerminal");
      geoBasePoiCategories.Add(4493, "Marina");
      geoBasePoiCategories.Add(4580, "PublicSportAirport");
      geoBasePoiCategories.Add(4581, "Airport");
      geoBasePoiCategories.Add(5000, "Business");
      geoBasePoiCategories.Add(5400, "GroceryStore");
      geoBasePoiCategories.Add(5511, "CarDealer");
      geoBasePoiCategories.Add(5540, "PetrolStation");
      geoBasePoiCategories.Add(5571, "MotorcycleDealership");
      geoBasePoiCategories.Add(5800, "Restaurant");
      geoBasePoiCategories.Add(5913, "Nightlife");
      geoBasePoiCategories.Add(5999, "Monument");
      geoBasePoiCategories.Add(6000, "Bank");
      geoBasePoiCategories.Add(6512, "Shopping");
      geoBasePoiCategories.Add(7011, "HotelMotel");
      geoBasePoiCategories.Add(7012, "SkiResort");
      geoBasePoiCategories.Add(7389, "TouristInformation");
      geoBasePoiCategories.Add(7392, "FireStation");
      geoBasePoiCategories.Add(7510, "RentalCar");
      geoBasePoiCategories.Add(7520, "ParkingLot");
      geoBasePoiCategories.Add(7521, "ParkingGarage");
      geoBasePoiCategories.Add(7522, "ParkAndRide");
      geoBasePoiCategories.Add(7538, "CarRepair");
      geoBasePoiCategories.Add(7832, "Cinema");
      geoBasePoiCategories.Add(7897, "RestArea");
      geoBasePoiCategories.Add(7929, "PerformingArts");
      geoBasePoiCategories.Add(7933, "BowlingCenter");
      geoBasePoiCategories.Add(7940, "SportsComplex");
      geoBasePoiCategories.Add(7947, "Park");
      geoBasePoiCategories.Add(7985, "Casino");
      geoBasePoiCategories.Add(7990, "ConventionCenter");
      geoBasePoiCategories.Add(7992, "GolfCourse");
      geoBasePoiCategories.Add(7994, "CommunityCenter");
      geoBasePoiCategories.Add(7996, "AmusementPark");
      geoBasePoiCategories.Add(7997, "SportsCenter");
      geoBasePoiCategories.Add(7998, "IceSkatingRink");
      geoBasePoiCategories.Add(7999, "TouristAttraction");
      geoBasePoiCategories.Add(8060, "Hospitals");
      geoBasePoiCategories.Add(8200, "HigherEducation");
      geoBasePoiCategories.Add(8211, "Schools");
      geoBasePoiCategories.Add(8231, "Library");
      geoBasePoiCategories.Add(8410, "Museum");
      geoBasePoiCategories.Add(8699, "AutomobileClub");
      geoBasePoiCategories.Add(9121, "CityHall");
      geoBasePoiCategories.Add(9211, "CourtHouse");
      geoBasePoiCategories.Add(9221, "PoliceStation");
      geoBasePoiCategories.Add(9591, "Cemetery");
      geoBasePoiCategories.Add(9992, "Church");
      geoBasePoiCategories.Add(9994, "BusinessService");
      geoBasePoiCategories.Add(9995, "BookStore");
      geoBasePoiCategories.Add(9996, "CoffeeShop");
      geoBasePoiCategories.Add(9997, "Pharmacy");
      geoBasePoiCategories.Add(9999, "BorderCrossing");
      geoBasePoiCategories.Add(10001, "BritishRail");
      geoBasePoiCategories.Add(10002, "LondonUnderground");
      geoBasePoiCategories.Add(20000, "TruckStop");
      geoBasePoiCategories.Add(20001, "TruckDealer");
      geoBasePoiCategories.Add(20002, "Toll");
      geoBasePoiCategories.Add(20003, "Laundry");
      geoBasePoiCategories.Add(20005, "PostOffice");
      geoBasePoiCategories.Add(20006, "EntryPoint");
      geoBasePoiCategories.Add(20007, "CulturalCenter");
      geoBasePoiCategories.Add(20009, "FireAidPost");
      geoBasePoiCategories.Add(20011, "DepartmentStore");
      geoBasePoiCategories.Add(20012, "TravelAgency");
      geoBasePoiCategories.Add(20013, "PublicPhone");
      geoBasePoiCategories.Add(20014, "Warehouse");
      geoBasePoiCategories.Add(20015, "Zoo");
      geoBasePoiCategories.Add(20016, "ScenicPanoramaView");
      geoBasePoiCategories.Add(20017, "SwimmingPool");
      geoBasePoiCategories.Add(20018, "TransportCompany");
      geoBasePoiCategories.Add(20023, "CargoCentre");
      geoBasePoiCategories.Add(20024, "CarShippingTerminal");
      geoBasePoiCategories.Add(20025, "AirlineAccess");
      geoBasePoiCategories.Add(20026, "Campground");
      geoBasePoiCategories.Add(20027, "CaravanSite");
      geoBasePoiCategories.Add(20028, "CoachandLorryParking");
      geoBasePoiCategories.Add(20029, "Customs");
      geoBasePoiCategories.Add(20030, "Embassy");
      geoBasePoiCategories.Add(20031, "GovernmentOffice");
      geoBasePoiCategories.Add(20032, "RoadSideDiner");
      geoBasePoiCategories.Add(20033, "Stadium");
      geoBasePoiCategories.Add(20034, "TollPlaza");
      geoBasePoiCategories.Add(20035, "CityCentre");
      geoBasePoiCategories.Add(20036, "Kindergarten");
      geoBasePoiCategories.Add(20037, "EmergencyCallStation");
      geoBasePoiCategories.Add(20038, "EmergencyMedicalService");
      geoBasePoiCategories.Add(20039, "FireBrigade");
      geoBasePoiCategories.Add(20040, "Freeport");
      geoBasePoiCategories.Add(20042, "Brunnel");
      geoBasePoiCategories.Add(20043, "CentrePointofFeature");
      geoBasePoiCategories.Add(20044, "FreewayExitAccess");
      geoBasePoiCategories.Add(20045, "RoadNode");
      geoBasePoiCategories.Add(20046, "WaterCentreLineJunction");
      geoBasePoiCategories.Add(20047, "RailwayNode");
      geoBasePoiCategories.Add(20048, "Company");
      geoBasePoiCategories.Add(20051, "Hippodrome");
      geoBasePoiCategories.Add(20052, "Beach");
      geoBasePoiCategories.Add(20053, "DrivethroughBottleShop");
      geoBasePoiCategories.Add(20054, "RestaurantArea");
      geoBasePoiCategories.Add(20056, "Shop");
      geoBasePoiCategories.Add(20057, "ParkandRecreation");
      geoBasePoiCategories.Add(20058, "Court");
      geoBasePoiCategories.Add(20059, "MoutainPeak");
      geoBasePoiCategories.Add(20060, "Opera");
      geoBasePoiCategories.Add(20061, "ConcertHall");
      geoBasePoiCategories.Add(20062, "BovagGarage");
      geoBasePoiCategories.Add(20063, "TennisCourt");
      geoBasePoiCategories.Add(20064, "SkatingRink");
      geoBasePoiCategories.Add(20065, "WaterSport");
      geoBasePoiCategories.Add(20066, "MusicCentre");
      geoBasePoiCategories.Add(20067, "Doctor");
      geoBasePoiCategories.Add(20068, "Dentist");
      geoBasePoiCategories.Add(20069, "Veterinarian");
      geoBasePoiCategories.Add(20070, "CafePub");
      geoBasePoiCategories.Add(20071, "ConventionCentre");
      geoBasePoiCategories.Add(20072, "LeisureCenter");
      geoBasePoiCategories.Add(20074, "YachtBasin");
      geoBasePoiCategories.Add(20075, "Condominium");
      geoBasePoiCategories.Add(20076, "CommercialBuilding");
      geoBasePoiCategories.Add(20077, "IndustrialBuilding");
      geoBasePoiCategories.Add(20079, "GeneralPOI");
      geoBasePoiCategories.Add(20080, "BreakDownService");
      geoBasePoiCategories.Add(20081, "VehicleEquipmentProvider");
      geoBasePoiCategories.Add(20082, "IndustrialArea");
      geoBasePoiCategories.Add(20083, "IndustrialHarborArea");
      geoBasePoiCategories.Add(20084, "Entertainment");
      geoBasePoiCategories.Add(20085, "Abbey");
      geoBasePoiCategories.Add(20087, "ArtCenter");
      geoBasePoiCategories.Add(20088, "BuildingFootprintPoint");
      geoBasePoiCategories.Add(20089, "Castle");
      geoBasePoiCategories.Add(20092, "FactoryGroundPhilips");
      geoBasePoiCategories.Add(20093, "Fortress");
      geoBasePoiCategories.Add(20096, "HolidayArea");
      geoBasePoiCategories.Add(20098, "Lighthouse");
      geoBasePoiCategories.Add(20099, "NationalCemetery");
      geoBasePoiCategories.Add(20100, "Monastery");
      geoBasePoiCategories.Add(20102, "NaturalReserve");
      geoBasePoiCategories.Add(20103, "Prison");
      geoBasePoiCategories.Add(20105, "Rocks");
      geoBasePoiCategories.Add(20106, "SportsHall");
      geoBasePoiCategories.Add(20107, "StatePoliceOffice");
      geoBasePoiCategories.Add(20108, "WalkingArea");
      geoBasePoiCategories.Add(20109, "WaterMill");
      geoBasePoiCategories.Add(20110, "Windmill");
      geoBasePoiCategories.Add(20112, "Rentacarparking");
      geoBasePoiCategories.Add(20113, "CarRacetrack");
      geoBasePoiCategories.Add(20114, "ToiletPublicAmenities");
      geoBasePoiCategories.Add(20115, "TrafficControlCameraLocation");
      geoBasePoiCategories.Add(20116, "BoatLaunchingRamp");
      geoBasePoiCategories.Add(20117, "MountainPass");
      geoBasePoiCategories.Add(20118, "Playground");
      geoBasePoiCategories.Add(20119, "ClubsAssociations");
      geoBasePoiCategories.Add(20120, "ProfessionalServices");
      geoBasePoiCategories.Add(20121, "RetailOutlet");
      geoBasePoiCategories.Add(20122, "TrafficLight");
      geoBasePoiCategories.Add(20123, "PublicTransportStop");
      geoBasePoiCategories.Add(20124, "AutoDealershipUsedCars");
      geoBasePoiCategories.Add(20125, "BarorPub");
      geoBasePoiCategories.Add(20126, "ClothingStore");
      geoBasePoiCategories.Add(20127, "ConsumerElectronicsStore");
      geoBasePoiCategories.Add(20128, "ConvenienceStore");
      geoBasePoiCategories.Add(20129, "Hamlet");
      geoBasePoiCategories.Add(20130, "HighwayExit");
      geoBasePoiCategories.Add(20131, "HomeImprovementandHardwareStore");
      geoBasePoiCategories.Add(20132, "HomeSpecialtyStore");
      geoBasePoiCategories.Add(20133, "IndustrialZone");
      geoBasePoiCategories.Add(20135, "OfficeSupplyServicesStore");
      geoBasePoiCategories.Add(20136, "OtherAccommodation");
      geoBasePoiCategories.Add(20137, "SpecialtyStore");
      geoBasePoiCategories.Add(20138, "SportingGoodsStore");

      geoBasePoiRestaurantSubCategories.Add(0, "AFRICAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(1, "AMERICAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(2, "AUSTRALIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(3, "AUSTRIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(4, "BALKAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(5, "BARBECUE_OR_SOUTHERN");
      geoBasePoiRestaurantSubCategories.Add(6, "BELGIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(7, "BISTRO");
      geoBasePoiRestaurantSubCategories.Add(8, "BOHEMIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(9, "BREWPUB");
      geoBasePoiRestaurantSubCategories.Add(10, "BRITISH_ISLES_FOOD");
      geoBasePoiRestaurantSubCategories.Add(11, "CAJUN_OR_CARIBBEAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(12, "CALIFORNIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(13, "CANADIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(14, "CHINESE_FOOD");
      geoBasePoiRestaurantSubCategories.Add(15, "CONTINENTAL_FOOD");
      geoBasePoiRestaurantSubCategories.Add(16, "DUTCH_FOOD");
      geoBasePoiRestaurantSubCategories.Add(17, "EAST_EUROPEAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(18, "FAST_FOOD");
      geoBasePoiRestaurantSubCategories.Add(19, "FILIPINO_FOOD");
      geoBasePoiRestaurantSubCategories.Add(20, "FRENCH_FOOD");
      geoBasePoiRestaurantSubCategories.Add(21, "GERMAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(22, "GREEK_FOOD");
      geoBasePoiRestaurantSubCategories.Add(23, "GRILL");
      geoBasePoiRestaurantSubCategories.Add(24, "HAWAIIAN_OR_POLYNESIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(25, "HUNGARIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(26, "INDIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(27, "INDONESIAN_OR_MALAYSIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(28, "INTERNATIONAL_FOOD");
      geoBasePoiRestaurantSubCategories.Add(29, "ITALIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(30, "JAPANESE_FOOD");
      geoBasePoiRestaurantSubCategories.Add(31, "JEWISH_OR_KOSHER_FOOD");
      geoBasePoiRestaurantSubCategories.Add(32, "KOREAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(33, "LATIN_AMERICAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(34, "MALTESE_FOOD");
      geoBasePoiRestaurantSubCategories.Add(35, "MEXICAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(36, "MIDDLE_EASTERN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(37, "OTHER");
      geoBasePoiRestaurantSubCategories.Add(38, "POLISH_FOOD");
      geoBasePoiRestaurantSubCategories.Add(39, "PORTUGUESE_FOOD");
      geoBasePoiRestaurantSubCategories.Add(40, "RUSSIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(41, "SANDWICH");
      geoBasePoiRestaurantSubCategories.Add(42, "SCANDINAVIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(43, "SEAFOOD");
      geoBasePoiRestaurantSubCategories.Add(44, "SNACKS_AND_BEVERAGES");
      geoBasePoiRestaurantSubCategories.Add(45, "SOUTH_AMERICAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(46, "SOUTHEAST_ASIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(47, "SOUTHWESTERN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(48, "SPANISH_FOOD");
      geoBasePoiRestaurantSubCategories.Add(49, "STEAK_HOUSE");
      geoBasePoiRestaurantSubCategories.Add(50, "SURINAMESE_FOOD");
      geoBasePoiRestaurantSubCategories.Add(51, "SWISS_FOOD");
      geoBasePoiRestaurantSubCategories.Add(52, "THAI_FOOD");
      geoBasePoiRestaurantSubCategories.Add(53, "TURKISH_FOOD");
      geoBasePoiRestaurantSubCategories.Add(54, "UNKNOWN");
      geoBasePoiRestaurantSubCategories.Add(55, "VEGETARIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(56, "VIETNAMESE_FOOD");
      geoBasePoiRestaurantSubCategories.Add(57, "FINNISH_FOOD");
      geoBasePoiRestaurantSubCategories.Add(58, "PIZZA");
      geoBasePoiRestaurantSubCategories.Add(59, "BURGERS");
      geoBasePoiRestaurantSubCategories.Add(60, "CHICKEN");
      geoBasePoiRestaurantSubCategories.Add(61, "ICE_CREAM");
      geoBasePoiRestaurantSubCategories.Add(62, "IRISH_FOOD");
      geoBasePoiRestaurantSubCategories.Add(63, "CAJUN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(64, "CARIBBEAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(65, "BREAKFAST");
      geoBasePoiRestaurantSubCategories.Add(66, "VEGAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(67, "MOROCCAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(68, "ARGENTINEAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(69, "BRASILIAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(70, "CHILEAN_FOOD");
      geoBasePoiRestaurantSubCategories.Add(71, "BALTIC_FOOD");
    }
    private void UpdateDistance(string text)
    {
      try
      {
        if (lbDistance.InvokeRequired)
        {
          lbDistance.Invoke(delUpdateDistance,text);
        }
        else
        {
          lbDistance.Text = text;
        }
      }
      catch (ObjectDisposedException exc)
      {
        Logger.Write(this,string.Format("Error updating distance. Object {0} disposed",exc.ObjectName),exc);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error updating distance",exc);
      }
    }

    #endregion

    #region Event handlers
    private void lbDirections_Click(object sender, EventArgs e)
    {
      Logger.Write(this, ">> lbDirections_Click");

      if (DirectionsChanged == null)
        return;

      DirectionsChanged(this, new EventArgs());
    }
    private void Navigator_Update(object sender, EventArgs e)
    {
      if (isRouting == false)
        return;

      try
      {
        string majorDistanceUnitText = "km";
        string minorDistanceUnitText = "m";
        switch (units)
        {
          case Units.Metric:
            majorDistanceUnitText = "km";
            minorDistanceUnitText = "m";
            break;
          case Units.Imperial:
            majorDistanceUnitText = "mi";
            minorDistanceUnitText = "ft";
            break;
        }

        if (navigationManager.Navigator.CurrentNavigationEvent != null)
        {
          double distance = 0;
          switch(units)
          {
            case Units.Metric:
              distance = navigationManager.Navigator.CurrentNavigationEvent.GetDistance(DistanceUnit.METERS);
              Logger.Write(this, string.Format("Distance:{0}{1}", distance, minorDistanceUnitText));

              if (distance > 1000)
              {
                distance /= 1000;
                distanceText = string.Format("{0:0.0}{1}", distance,majorDistanceUnitText);
              }
              else
              {
                distanceText = string.Format("{0:0}{1}", distance,minorDistanceUnitText);
              }
              break;
            case Units.Imperial:
              distance = navigationManager.Navigator.CurrentNavigationEvent.GetDistance(DistanceUnit.FEET);
              Logger.Write(this, string.Format("Distance:{0}{1}", distance, minorDistanceUnitText));

              if (distance > 5280)  // 5280 ft in a mile
              {
                distance /= 5280;
                distanceText = string.Format("{0:0.0}{1}", distance, majorDistanceUnitText);
              }
              else
              {
                distanceText = string.Format("{0:0}{1}", distance, minorDistanceUnitText);
              }
              break;
          }
        }
        else
          distanceText = string.Empty;

        Logger.Write(this, string.Format("distanceText:{0}", distanceText));
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error updating distance",exc);
      }
    }
    private void distanceTextTimer_TimerExpired(object sender,EventArgs e)
    {
      Logger.Write(this, ">> distanceTextTimer_TimerExpired");
      UpdateDistance(distanceText);
    }
    #endregion

    #region Event Dispatchers

    private void OnBeforeTurnNotification()
    {
      if (BeforeTurnNotification == null)
        return;

      BeforeTurnNotification(this, new EventArgs());
    }
    private void OnAfterTurnNotification()
    {
      Logger.Write(this, "AfterTurnNotification");
      if (AfterTurnNotification == null)
        return;

      AfterTurnNotification(this, new EventArgs());
    }

    #endregion

    #region Fields

    public readonly LoggingHelper Logger = new LoggingHelper();
    private readonly INamedParameterRepository parameterRepository = null;
    private Units units = Units.Metric;
    private Boolean isRouting = false;

    private readonly RendererList renderList = new RendererList();
    private NavigationManager navigationManager;
    
    private readonly GeoBaseGps geoBaseGps = new GeoBaseGps();

    private string resourcePath;
    private string repositoryPath;
    private string soundPath;

    private TurnBox turnbox;
    private LabelBox lbInstructions;
    private LabelBox lbEta;
    private LabelBox lbDirections;
    private LabelBox lbDistance;
    private volatile string distanceText = string.Empty;

    private Language languageInUse;
    private Boolean isNotificationsActive = true;
    private System.Threading.Timer notificationTimer;
    private object notificationTimerLock = new object();
    private Int32 notificationsTimeout = 10;
    private string routingStrategy = "F";
    private string notificationDefinitionsMetadata;
    private Boolean? isDepartureNotificationActive;
    private Boolean? isAfterTurnDistanceNotificationActive;
    private Boolean? isRoundaboutExitNotificationActive;

    private ColourScheme colourScheme;
    private readonly UpdateStringDelegate delUpdateDistance;

    private readonly Dictionary<int, string> geoBasePoiCategories = new Dictionary<int, string>();
    private readonly Dictionary<int, string> geoBasePoiRestaurantSubCategories = new Dictionary<int, string>();
    private readonly MultiShotTimer distanceTextTimer;
    #endregion
  }
}