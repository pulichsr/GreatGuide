
namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public abstract class RegionEvent : ApplicationEvent
  {
    public RegionEvent(Region region, GpsPositionEvent gpsPosition)
    {
      Region = region;
      GpsPosition = gpsPosition;
    }

    public Region Region = null;
    public GpsPositionEvent GpsPosition = null;


  }
}
