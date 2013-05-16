using System;

namespace Nucleo.GoodGuide.Types.Events.ControlEvents
{
  public class LoggingStateEvent : ControlEvent
  {
    public LoggingStateEvent()
    {}
    public LoggingStateEvent(Boolean isActive)
    {
      this.isActive = isActive;
    }

    public Boolean IsActive
    {
      get { return isActive; }
      set { isActive = value; }
    }

    private Boolean isActive = false;
  }
}
