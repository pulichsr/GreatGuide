namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class ActiveRegionsEvent : ApplicationEvent
  {
    public ActiveRegionsEvent(Regions activeRegions) 
    {
      this.activeRegions = activeRegions;
    }

    public Regions ActiveRegions
    {
      get { return activeRegions; }
    }

    private Regions activeRegions = null;

  }
}
