namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class NavigatorPositionEvent : ApplicationEvent
  {
    public NavigatorPositionEvent()
    {      
    }
    public NavigatorPositionEvent(float latitude,float longitude)
    {
      Latitude = latitude;
      Longitude = longitude;
    }

    public float Latitude = 0;
    public float Longitude = 0;
    public string House = string.Empty;
    public string Street = string.Empty;
    public string City = string.Empty;
    public string Zip = string.Empty;
    public string Description = string.Empty;
    public string TelephoneNo = string.Empty;
  }
}
