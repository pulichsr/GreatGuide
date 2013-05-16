namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class ContentControlEvent : ApplicationEvent
  {
    public enum ControlStates
    {
      None,
      Repeat,
      Pause,
      Resume,
      Stop
    }

    public ContentControlEvent(ControlStates controlState)
    {
      this.controlState = controlState;
    }

    public ControlStates ControlState
    {
      get { return controlState; }
      set { controlState = value; }
    }

    private ControlStates controlState = ControlStates.None;
  }
}
