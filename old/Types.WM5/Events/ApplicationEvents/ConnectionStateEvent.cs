using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class ConnectionStateEvent : ApplicationEvent
  {

    public ConnectionStateEvent()
    {
    }

    public ConnectionStateEvent(EboxStateEvent.ConnectionStates oldState, EboxStateEvent.ConnectionStates newState)
    {
      this.oldState = oldState;
      this.newState = newState;
    }

    public EboxStateEvent.ConnectionStates OldState
    {
      get { return oldState; }
      set { oldState = value; }
    }

    public EboxStateEvent.ConnectionStates NewState
    {
      get { return newState; }
      set { newState = value; }
    }

    private EboxStateEvent.ConnectionStates oldState;
    private EboxStateEvent.ConnectionStates newState;
  }
}