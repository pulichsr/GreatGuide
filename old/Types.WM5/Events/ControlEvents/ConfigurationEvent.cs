
namespace Nucleo.GoodGuide.Types.Events.ControlEvents
{
  public class ConfigurationEvent : ControlEvent
  {
    public ConfigurationEvent(NamedParameters configurrationParameters)
    {
      this.configurationParameters = configurrationParameters;
    }

    public NamedParameters ConfigurationParameters
    {
      get { return configurationParameters; }
    }

    private readonly NamedParameters configurationParameters = null;
  }
}
