

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class TripRecorderStateEvent: ApplicationEvent
  {
    public TripRecorderStateEvent()
    {
    }
    public TripRecorderStateEvent(TripRecorderStates oldState,TripRecorderStates state, string message)
    {
      this.OldState = oldState;
      this.State = state;
      this.Message = message;
    }

    public TripRecorderStates OldState;
    public TripRecorderStates State;
    public string Message = string.Empty;
  }
}