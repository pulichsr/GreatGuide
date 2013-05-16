using System;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Events.ControlEvents;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.GeoBaseNavigator
{
  public class NavigatorAdapter
  {
    public NavigatorAdapter(INavigator navigator)
    {
      Guard.ArgumentNotNull(navigator, "navigator");
      this.navigator = navigator;
      this.navigator.BeforeTurnNotification += navigator_BeforeTurnNotification;
      this.navigator.AfterTurnNotification += navigator_AfterTurnNotification;
      this.navigator.DirectionsChanged += navigator_DirectionsChanged;
    }

    #region Event broker events
    [EventPublisher(EventTopics.Navigator.BeforeNotification)]
    public event EventHandler<GoodGuideEventArgs> BeforeNotification;

    [EventPublisher(EventTopics.Navigator.AfterNotification)]
    public event EventHandler<GoodGuideEventArgs> AfterNotification;

    [EventPublisher(EventTopics.Navigator.Directions)]
    public event EventHandler<GoodGuideEventArgs> Directions;
    #endregion

    private void navigator_BeforeTurnNotification(object sender, EventArgs e)
    {
      if (BeforeNotification == null)
        return;

      BeforeNotification(this,new GoodGuideEventArgs(this.GetType().Name,null));
    }
    private void navigator_AfterTurnNotification(object sender, EventArgs e)
    {
      if (AfterNotification == null)
        return;

      AfterNotification(this, new GoodGuideEventArgs(this.GetType().Name, null));
    }
    private void navigator_DirectionsChanged(object sender, EventArgs e)
    {
      Logger.Write(this, ">> navigator_DirectionsChanged");

      if (Directions == null)
        return;
      if (navigator.Directions == null)
        return;
      if (navigator.Directions.Length == 0)
        return;

      try
      {
        NavigatorDirectionsEvent eventData = new NavigatorDirectionsEvent(navigator.Directions);
        Directions(this, new GoodGuideEventArgs(GetType().Name,eventData));
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error",exc);
      }
    }

    #region Eventbroker subscription
    [EventSubscriber(EventTopics.GpsAdapter.GpsPosition)]
    public void GpsPositionHandler(object sender, GoodGuideEventArgs e)
    {
//      Logger.Write(this, ">>GpsPositionHandler");
      if (e.EventData is GpsPositionEvent == false)
      {
        Logger.Write(this, string.Format("Invalid EventData {0} in {1}", e.EventData.GetType().Name, GetType().Name));
        return;
      }

      GpsPositionEvent eventData = (GpsPositionEvent)e.EventData;

      if (navigator == null)
      {
        Logger.Write(this, "navigator is null");
        return;
      }

      navigator.GpsDataHandler(eventData);
    }

    [EventSubscriber(EventTopics.Navigator.NavigatorCommand)]
    public void NavigatorRouteEventHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is NavigatorRouteEvent == false)
        throw new InvalidOperationException(string.Format("Invalid event type {0} in {1}.NavigatorRouteEventHandler", e.EventData.GetType().Name, GetType().Name));

      NavigatorRouteEvent EventData = (NavigatorRouteEvent)e.EventData;
      NavigatorRoute(EventData);
    }

    //[EventSubscriber(EventTopics.Navigator.GetPoiCategoryList)]
    //public void GetPoiCategoryListHandler(object sender, GoodGuideEventArgs e)
    //{
    //  if (e.EventData is PoiCategoryListEvent == false)
    //    throw new InvalidOperationException(string.Format("Invalid event type {0} in {1}.GetPoiCategoryListHandler", e.EventData.GetType().Name, GetType().Name));

    //  PoiCategoryListEvent EventData = (PoiCategoryListEvent)e.EventData;

    //  EventData.Categories = navigator.GetPoiCategories();
    //}

    //[EventSubscriber(EventTopics.Navigator.GetPoiSubcategoryList)]
    //public void GetPoiSubcategoryListHandler(object sender, GoodGuideEventArgs e)
    //{
    //  if (e.EventData is PoiSubcategoryListEvent == false)
    //    throw new InvalidOperationException(string.Format("Invalid event type {0} in {1}.GetPoiSubcategoryListHandler", e.EventData.GetType().Name, GetType().Name));

    //  PoiSubcategoryListEvent EventData = (PoiSubcategoryListEvent)e.EventData;
    //  EventData.Subcategories = navigator.GetPoiSubCategories();
    //}

    //[EventSubscriber(EventTopics.Navigator.GetPoiDestinationList)]
    //public void GetPoiDestinationListHandler(object sender, GoodGuideEventArgs e)
    //{
    //  if (e.EventData is PoiDestinationListEvent == false)
    //    throw new InvalidOperationException(string.Format("Invalid event type {0} in {1}.GetPoiDestinationListHandler", e.EventData.GetType().Name, GetType().Name));

    //  PoiDestinationListEvent EventData = (PoiDestinationListEvent)e.EventData;
    //  if (EventData.SubcategoryIndex == PoiSubcategory.AllSubcategories)
    //    EventData.Destinations = GetPoiByCategory(EventData.CategoryIndex, EventData.Latitude, EventData.Longitude, EventData.Radius);
    //  else
    //    EventData.Destinations = GetPoiByCategorySubcategory(EventData.CategoryIndex, EventData.SubcategoryIndex, EventData.Latitude, EventData.Longitude, EventData.Radius);
    //  navigator.GetPoiDestinations();
    //}

    [EventSubscriber(EventTopics.Navigator.SetLanguage)]
    public void SetLanguageHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is LanguageEvent == false)
        throw new InvalidOperationException(string.Format("Invalid event type {0} in {1}.SetLanguageHandler", e.EventData.GetType().Name, GetType().Name));

      LanguageEvent EventData = (LanguageEvent)e.EventData;
      navigator.SetLanguage(EventData.Language);
    }

    [EventSubscriber(EventTopics.Navigator.SetUnits)]
    public void SetUnitsHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is UnitsEvent == false)
        throw new InvalidOperationException(string.Format("Invalid event type {0} in {1}.SetUnitsHandler", e.EventData.GetType().Name, GetType().Name));

      UnitsEvent EventData = (UnitsEvent)e.EventData;
      navigator.SetUnits(EventData.Units);
    }

    [EventSubscriber(EventTopics.Navigator.GetAddressOfPosition)]
    public void GetAddressOfPositionHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is NavigatorPositionEvent == false)
        throw new InvalidOperationException(string.Format("Invalid event type {0} in {1}.GetAddressOfPositionHandler", e.EventData.GetType().Name, GetType().Name));

      NavigatorPositionEvent EventData = (NavigatorPositionEvent)e.EventData;

      string house;
      string street;
      string city;
      string zip;
      string description;
      string telephoneNo;
      navigator.GetAddressOfPosition(EventData.Latitude, EventData.Longitude, out house, out street, out city, out zip, out description, out telephoneNo);
    }

    [EventSubscriber(EventTopics.Navigator.SearchStreetAddress)]
    public void AddressSearchHandler(object sender, GoodGuideEventArgs e)
    {
      NavigatorAddressSearchEvent eventData = e.EventData as NavigatorAddressSearchEvent;
      if (eventData == null)
      {
        Logger.Write(this,string.Format("Invalid event type {0} in {1}.AddressSearchHandler",e.EventData.GetType().Name,GetType().Name));
        return;
      }

      eventData.Addresses = navigator.SearchStreetAdress(eventData.SearchText);
    }

    [EventSubscriber(EventTopics.Navigator.IsNotificationsActive)]
    public void IsNotificationsActiveHandler(object sender, GoodGuideEventArgs e)
    {
      RunStateEvent eventData = e.EventData as RunStateEvent;
      if (eventData == null)
      {
        Logger.Write(this, string.Format("Invalid event type {0} in {1}.IsNotificationsActiveHandler", e.EventData.GetType().Name, GetType().Name));
        return;
      }

      navigator.IsNotificationsActive = eventData.IsRunning;
    }

    #endregion

    private void NavigatorRoute(NavigatorRouteEvent eventData)
    {
      try
      {
        switch (eventData.Action)
        {
          case NavigatorRouteEvent.Actions.CancelRoute:
            navigator.CancelRoute();
            break;
          case NavigatorRouteEvent.Actions.NavigateTo:
            navigator.RouteTo(eventData.Position.Latitude,eventData.Position.Longitude);
            break;
        }
      }
      catch (Exception exc)
      {
        Logger.Write(this,"NavigatorRoute", exc);
      }

      eventData.IsSuccessful = true;
    }

    private readonly INavigator navigator;
    public readonly LoggingHelper Logger = new LoggingHelper();
  }
}
