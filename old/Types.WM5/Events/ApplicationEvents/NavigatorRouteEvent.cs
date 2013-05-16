
using System;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class NavigatorRouteEvent : ApplicationEvent
  {
    public enum Actions
    {
      NavigateTo,
      CancelRoute
    }

    public NavigatorRouteEvent()
    {      
    }
    public NavigatorRouteEvent(Actions action)
    {
      Action = action;
    }
    public NavigatorRouteEvent(Actions action,float latitude,float longitude)
    {
      Position = new Point(latitude, longitude);
      this.Action = action;
    }
    public NavigatorRouteEvent(float latitude, float longitude)
    {
      Position = new Point(latitude,longitude);
      this.Action = Actions.NavigateTo;
    }


    public Point Position = new Point();
    public Actions Action;
    public Boolean IsSuccessful = false;
  }
}
