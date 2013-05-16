using System;

namespace Nucleo.GoodGuide.Types.Events.ControlEvents
{
  public class RunStateEvent : ControlEvent
  {
    public RunStateEvent()
    {}
    public RunStateEvent(Boolean isRunning)
    {
      this.isRunning = isRunning;
    }

    public Boolean IsRunning
    {
      get { return isRunning; }
      set { isRunning = value; }
    }

    private Boolean isRunning = false;
  }
}
