
namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class UnitsEvent : ApplicationEvent
  {
    public UnitsEvent()
    {
    }
    public UnitsEvent(Units units)
    {
      this.units = units;
    }

    public Units Units
    {
      get { return units; }
      set { units = value; }
    }

    private Units units = Units.Metric;

  }
}
