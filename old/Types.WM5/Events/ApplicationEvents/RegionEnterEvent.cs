namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class RegionEnterEvent : RegionEvent
  {
    public RegionEnterEvent(Region region,GpsPositionEvent gpsPosition) 
      : base(region,gpsPosition)
    {
    }
  }
}
