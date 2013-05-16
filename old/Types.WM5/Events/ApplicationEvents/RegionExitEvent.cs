namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class RegionExitEvent : RegionEvent
  {
    public RegionExitEvent(Region region, GpsPositionEvent gpsPosition)
      : base(region, gpsPosition)
    {
    }
  }
}
