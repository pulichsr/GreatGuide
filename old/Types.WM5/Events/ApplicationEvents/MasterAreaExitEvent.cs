namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class MasterAreaExitEvent : RegionEvent
  {
    public MasterAreaExitEvent(Region region, GpsPositionEvent gpsPosition)
      : base(region, gpsPosition)
    {
    }
  }
}
