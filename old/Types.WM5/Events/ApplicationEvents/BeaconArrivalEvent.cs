namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class BeaconArrivalEvent : ApplicationEvent
  {
    public BeaconArrivalEvent(string beaconIdentifier)
    {
      this.beaconIdentifier = beaconIdentifier;
    }

    public string BeaconIdentifier
    {
      get { return beaconIdentifier; }
      set { beaconIdentifier = value.Trim(); }
    }

    private string beaconIdentifier = string.Empty;

  }
}
