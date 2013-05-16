namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class NavigatorDirectionsEvent : ApplicationEvent
  {
    public NavigatorDirectionsEvent(string[] directions)
    {
      Guard.ArgumentNotNull(directions, "directions");

      this.directions = directions;
    }

    public string[] Directions
    {
      get { return directions; }
    }

    private readonly string[] directions;

  }
}
