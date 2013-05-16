using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class DestinationNavigationEvent : ApplicationEvent
  {
    public enum Actions
    {
      NavigateTo,
      CancelRoute,
      PauseRoute,
      ResumeRoute,
      Arrival
    }

    public DestinationNavigationEvent()
    {      
    }
    public DestinationNavigationEvent(Actions action)
    {
      Action = action;
    }

    public DestinationNavigationEvent(Actions action,DestinationDataset.DestinationRow destination)
    {
      this.Destination = destination;
      this.Action = action;
    }

    public DestinationDataset.DestinationRow Destination = null;
    public Actions Action;
    public Boolean IsSuccessful = false;
    public Boolean IsRecentDestination = false;
  }
}
