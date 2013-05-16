namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class EboxStateEvent : ApplicationEvent
  {
    public enum ConnectionStates
    {
      Disconnected,
      EstablishingConnection,
      Connected
    }

    public EboxStateEvent()
    {
    }

    public EboxStateEvent(ConnectionStates oldState,ConnectionStates newState)
    {
      this.oldState = oldState;
      this.newState = newState;
    }

    public ConnectionStates OldState
    {
      get { return oldState; }
      set { oldState = value; }
    }

    public ConnectionStates NewState
    {
      get { return newState; }
      set { newState = value; }
    }

    private ConnectionStates oldState;
    private ConnectionStates newState;
  }
}
