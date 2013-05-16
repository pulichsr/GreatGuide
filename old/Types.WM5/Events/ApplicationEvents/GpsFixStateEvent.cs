using System;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class GpsFixStateEvent : ApplicationEvent
  {
    public GpsFixStateEvent()
    {
    }
    public GpsFixStateEvent(Boolean isFixValid)
    {
      this.isFixValid = isFixValid;
    }
    public GpsFixStateEvent(Boolean isFixValid, DateTime fixTime)
    {
      this.isFixValid = isFixValid;
      this.fixTime = fixTime;
    }

    public Boolean IsFixValid
    {
      get { return isFixValid; }
      set { isFixValid = value; }
    }

    public DateTime FixTime
    {
      get { return fixTime; }
      set { fixTime = value; }
    }

    private Boolean isFixValid = false;
    private DateTime fixTime;
  }
}
