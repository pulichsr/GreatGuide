using System;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class EboxInterfaceStateEventArgs : EventArgs
  {
    public EboxInterfaceStateEventArgs(EboxStateEvent.ConnectionStates oldState,EboxStateEvent.ConnectionStates newState)
    {
      this.oldState = oldState;
      this.newState = newState;
    }

    public EboxStateEvent.ConnectionStates OldState
    {
      get { return oldState; }
    }
    public EboxStateEvent.ConnectionStates NewState
    {
      get { return newState; }
    }

    private readonly EboxStateEvent.ConnectionStates oldState;
    private readonly EboxStateEvent.ConnectionStates newState;
  }
}