using System;
using System.Threading;
using Nucleo.Events;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.Gooduide.DestinationManager
{
  public class DestinationManager
  {
    #region Eventbroker publications
    [EventPublisher(EventTopics.DestinationManager.DestinationRouterState)]
    public event EventHandler<GoodGuideEventArgs> DestinationRouterStateChanged;

    [EventPublisher(EventTopics.Navigator.NavigatorCommand)]
    public event EventHandler<GoodGuideEventArgs> NavigatorRouteCommand;

    [EventPublisher(EventTopics.DestinationManager.DestinationCommand)]
    public event EventHandler<GoodGuideEventArgs> DestinationArrival;
    #endregion

    #region Eventbroker subscriptions
    [EventSubscriber(EventTopics.GpsAdapter.GpsPosition)]
    public void GpsPositionEventHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is GpsPositionEvent == false)
        return;

      GpsPositionEvent EventData = (GpsPositionEvent)e.EventData;
      GpsPositionEvent(EventData);
    }

    [EventSubscriber(EventTopics.DestinationManager.DestinationCommand)]
    public void DestinationNavigationHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is DestinationNavigationEvent == false)
        return;

      DestinationNavigationEvent EventData = (DestinationNavigationEvent)e.EventData;
      DestinationNavigation(EventData);
    }
    #endregion

    public DestinationManager(IRepositoryLocator repositoryLocator)
    {
      this.repositoryLocator = repositoryLocator;
    }

    public DestinationDataset.DestinationRow CurrentDestination
    {
      get { return currentDestination; }
    }
    public DestinationRouterStateEvent.RouterStates RouterState
    {
      get { return routerState; }
    }

    public void Initialise()
    {
      if (repositoryLocator.LocateRepository<IRecentDestinationRepository>() == null)
        throw new InvalidOperationException("IRecentDestinationRepository not found");
    }
    public void Finalise()
    {
    }

    private void GpsPositionEvent(GpsPositionEvent gpsPositionEvent)
    {
      // If the router is paused, there is no arrival zone
      if (arrivalZoneRegion == null)
        return;

      // Check if position is in arrival zone
      Boolean IsInsideRegion = arrivalZoneRegion.IsInsideRegion(gpsPositionEvent.Latitude,gpsPositionEvent.Longitude);
      if (IsInsideRegion == false)
        return;

      Logger.Write(this,"In destination arrival zone");
      DestinationDataset.DestinationRow oldDestination = CurrentDestination;

      // Cancel the routing if arrived at destination
      CancelRouting();

      // If position is in arrival zone, signal the destination arrival
      Logger.Write(this, "Signal destination arrival");
      OnDestinationArrival(oldDestination);
    }
    private void DestinationNavigation(DestinationNavigationEvent eventData)
    {
      switch (eventData.Action)
      {
        case DestinationNavigationEvent.Actions.CancelRoute:
          CancelRouting();
          break;

        case DestinationNavigationEvent.Actions.NavigateTo:
          NavigateTo(eventData.Destination,eventData.IsRecentDestination);
          break;

        case DestinationNavigationEvent.Actions.PauseRoute:
          PauseRoute();
          break;

        case DestinationNavigationEvent.Actions.ResumeRoute:
          if (RouterState == DestinationRouterStateEvent.RouterStates.NoDestination)
            return;

          ResumeRoute(CurrentDestination);
          break;
      }

      eventData.IsSuccessful = true;
    }
    private void CancelRouting()
    {
      Logger.Write(this, ">> CancelRouting");
      Logger.Write(this, string.Format("Current RouterState: {0}", RouterState));
      DestinationRouterStateEvent.RouterStates currentRouterState = RouterState;

      // Make the setting of the destination thread safe as it may be executed on the GPS thread
      // or the UI thread
      lock (destinationLock)
      {
        // Update router state
        SetRouterState(DestinationRouterStateEvent.RouterStates.NoDestination, CurrentDestination);

        // Clear destination and destination arrival zone region
        arrivalZoneRegion = null;
        currentDestination = null;
      }

      // Instruct navigator to cancel routing
      if (currentRouterState == DestinationRouterStateEvent.RouterStates.Routing)
      {
        Logger.Write(this, "Cancelling routing");

        NavigatorRouteEvent EventData = new NavigatorRouteEvent(NavigatorRouteEvent.Actions.CancelRoute);
        OnNavigatorRouteCommand(EventData);
      }
    }
    private void NavigateTo(DestinationDataset.DestinationRow destinationToNavigateTo,Boolean isRecentDestination)
    {
      Logger.Write(this, string.Format(">> NavigateTo({0},{1})", destinationToNavigateTo.Code, isRecentDestination));
      // Make the setting of the destination thread safe as it may be executed on the GPS thread
      // or the UI thread

      try
      {
        lock (destinationLock)
        {
          // Update router state
          SetRouterState(DestinationRouterStateEvent.RouterStates.Routing, destinationToNavigateTo);

          currentDestination = destinationToNavigateTo;

          // Create arrival zone region
          CreateArrivalZoneRegion(destinationToNavigateTo);
        }
      }
      catch (Exception exc)
      {
        Logger.Write(this,string.Format("Error in DestinationManager.NavigateTo(destinatio id = {0})",destinationToNavigateTo.Id),exc);
        return;
      }

      // Instruct navigator to route to destination lat/long
      NavigatorRouteEvent EventData = new NavigatorRouteEvent(destinationToNavigateTo.Latitude, destinationToNavigateTo.Longitude);
      OnNavigatorRouteCommand(EventData);

      if ((isRecentDestination == false) && (destinationToNavigateTo.Code != string.Empty))
      {
        Logger.Write(this,"Saving to recent destination");
        StartSaveToRecentDestinations();
      }
    }
    private void PauseRoute()
    {
      // Make the setting of the destination thread safe as it may be executed on the GPS thread
      // or the UI thread
      lock (destinationLock)
      {
        // Update router state
        SetRouterState(DestinationRouterStateEvent.RouterStates.RoutePaused, CurrentDestination);

        // Clear arrival zone region
        arrivalZoneRegion = null;
      }

      // Instruct navigator to cancel routing
      NavigatorRouteEvent EventData = new NavigatorRouteEvent(NavigatorRouteEvent.Actions.CancelRoute);
      OnNavigatorRouteCommand(EventData);
    }
    private void ResumeRoute(DestinationDataset.DestinationRow destinationToNavigateTo)
    {
      if (CurrentDestination == null)
        return;

      // Make the setting of the destination thread safe as it may be executed on the GPS thread
      // or the UI thread
      lock (destinationLock)
      {
        // Update router state
        SetRouterState(DestinationRouterStateEvent.RouterStates.Routing, CurrentDestination);

        // Recreate arrival zone region
        CreateArrivalZoneRegion(destinationToNavigateTo);
      }

      // Instruct navigator to route to destination lat/long
      NavigatorRouteEvent EventData = new NavigatorRouteEvent(destinationToNavigateTo.Latitude, destinationToNavigateTo.Longitude);
      OnNavigatorRouteCommand(EventData);
    }
    private void CreateArrivalZoneRegion(DestinationDataset.DestinationRow destinationToNavigateTo)
    {
      if (destinationToNavigateTo == null)
        return;

      // Circular region
      if (CurrentDestination.ArrivalZoneType == CircularRegion.RegionType)
        arrivalZoneRegion = new CircularRegion(
          destinationToNavigateTo.Id,
          destinationToNavigateTo.ArrivalZoneMinLatitude,
          destinationToNavigateTo.ArrivalZoneMaxLatitude,
          destinationToNavigateTo.ArrivalZoneMinLongitude,
          destinationToNavigateTo.ArrivalZoneMaxLongitude,
          destinationToNavigateTo.ArrivalZoneData,
          false);

      // Polygon region
      if (CurrentDestination.ArrivalZoneType == PolygonRegion.RegionType)
        arrivalZoneRegion = new PolygonRegion(
          destinationToNavigateTo.Id,
          destinationToNavigateTo.ArrivalZoneMinLatitude,
          destinationToNavigateTo.ArrivalZoneMaxLatitude,
          destinationToNavigateTo.ArrivalZoneMinLongitude,
          destinationToNavigateTo.ArrivalZoneMaxLongitude,
          destinationToNavigateTo.ArrivalZoneData,
          false);
    }
    private void SetRouterState(DestinationRouterStateEvent.RouterStates newState, DestinationDataset.DestinationRow destination)
    {
      Logger.Write(this, string.Format(">> SetRouterState({0})",newState));

      DestinationRouterStateEvent.RouterStates oldState = RouterState;
      routerState = newState;

      OnDestinationRouterStateChanged(oldState, RouterState, destination);
    }
    private void StartSaveToRecentDestinations()
    {
      Thread SaveThread = new Thread(SaveToRecentDestinations);
      SaveThread.Name = "SaveToRecentDestinations";
      SaveThread.Start();
    }
    private void SaveToRecentDestinations()
    {
      RecentDestinationDataset.RecentDestinationRow RecentDestination;

      try
      {
        if (CurrentDestination.IsIdNull() == false)
        {
          #region currentDestination is Destination
          Logger.Write(this, "currentDestination is a DESTINATION");
          Boolean isRecent = repositoryLocator.LocateRepository<IRecentDestinationRepository>().IsDestinationIdRecent(CurrentDestination.Id);
          if (isRecent == true)
          {
            Logger.Write(this, "Destination found. Not adding");
            return;
          }

          RecentDestinationDataset.RecentDestinationDataTable RecentDestinations = new RecentDestinationDataset.RecentDestinationDataTable();
          RecentDestination = RecentDestinations.NewRecentDestinationRow();
          RecentDestination.DestinationId = CurrentDestination.Id;
          #endregion
        }
        else
        {
          #region currentDestination is not Destination
          Logger.Write(this, string.Format("currentDestination {0} is not a DESTINATION",CurrentDestination.Code));
          Boolean isRecent = repositoryLocator.LocateRepository<IRecentDestinationRepository>().IsDestinationNameRecent(CurrentDestination.Code);
          if (isRecent == true)
          {
            Logger.Write(this, "Destination found. Not adding");
            return;
          }

          RecentDestinationDataset.RecentDestinationDataTable RecentDestinations = new RecentDestinationDataset.RecentDestinationDataTable();
          RecentDestination = RecentDestinations.NewRecentDestinationRow();
          #endregion
        }

        Logger.Write(this, "Adding destination");
        RecentDestination.Latitude = CurrentDestination.Latitude;
        RecentDestination.Longitude = CurrentDestination.Longitude;
        RecentDestination.Name = CurrentDestination.Code;
        RecentDestination.VisitedDat = DateTime.Today;

        repositoryLocator.LocateRepository<IRecentDestinationRepository>().Insert(RecentDestination);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error saving RECENT_DESTINATION",exc);
      }
    }

    #region Event dispatchers
    private void OnDestinationRouterStateChanged(DestinationRouterStateEvent.RouterStates oldState,DestinationRouterStateEvent.RouterStates newState,DestinationDataset.DestinationRow destination)
    {
      if (DestinationRouterStateChanged == null)
        return;

      DestinationRouterStateEvent EventData = new DestinationRouterStateEvent(oldState,newState,destination);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,EventData);

      DestinationRouterStateChanged(this, e);
    }
    private void OnNavigatorRouteCommand(NavigatorRouteEvent eventData)
    {
      if (NavigatorRouteCommand == null)
        return;

      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);

      NavigatorRouteCommand(this, e);
    }
    private void OnDestinationArrival(DestinationDataset.DestinationRow destinationArrivedAt)
    {
      if (DestinationArrival == null)
        return;

      DestinationNavigationEvent EventData = new DestinationNavigationEvent(DestinationNavigationEvent.Actions.Arrival,destinationArrivedAt);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, EventData);

      DestinationArrival(this, e);
    }
    #endregion

    private IRepositoryLocator repositoryLocator;
    public readonly LoggingHelper Logger = new LoggingHelper();
    private Region arrivalZoneRegion = null;
    private volatile DestinationDataset.DestinationRow currentDestination = null;
    private volatile DestinationRouterStateEvent.RouterStates routerState = DestinationRouterStateEvent.RouterStates.NoDestination;
    private object destinationLock = new object();
  }
}