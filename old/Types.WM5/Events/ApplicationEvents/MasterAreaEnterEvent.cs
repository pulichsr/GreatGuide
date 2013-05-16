namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class MasterAreaEnterEvent : RegionEvent
  {
    public MasterAreaEnterEvent(Region region, GpsPositionEvent gpsPosition) 
      : base(region,gpsPosition)
    {
    }
  }
}
