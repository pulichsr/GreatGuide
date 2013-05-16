using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class DestinationRouterStateEvent : ApplicationEvent
  {
    public enum RouterStates
    {
      NoDestination,
      Routing,
      RoutePaused
    }

    public DestinationRouterStateEvent()
    {
    }
    public DestinationRouterStateEvent(RouterStates oldState, RouterStates newState)
    {
      this.oldState = oldState;
      this.newState = newState;
    }
    public DestinationRouterStateEvent(RouterStates oldState,RouterStates newState,DestinationDataset.DestinationRow destination)
    {
      this.oldState = oldState;
      this.newState = newState;
      this.destination = destination;
    }

    public RouterStates OldState
    {
      get { return oldState; }
      set { oldState = value; }
    }

    public RouterStates NewState
    {
      get { return newState; }
      set { newState = value; }
    }
    public DestinationDataset.DestinationRow Destination
    {
      get { return destination; }
      set { destination = value; }
    }

    private RouterStates oldState;
    private RouterStates newState;
    private DestinationDataset.DestinationRow destination = null;
  }
}
