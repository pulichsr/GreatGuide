using System;
using System.Text;
using Nucleo.GoodGuide.Types;
using Nucleo.Plugins;

namespace Nucleo.Gooduide.DestinationManager
{
  [Plugin(PluginKinds.Module,"Destination Manager", "Destination Manager Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin, IGoodGuidePlugin
  {
    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");
        sb.Append(string.Format("Router State: {0}\r\n",destinationManager.RouterState));

        if (destinationManager.CurrentDestination == null)
          sb.Append(string.Format("Current Destination: \r\n"));
        else
          sb.Append(string.Format("Current Destination: {0}\r\n", destinationManager.CurrentDestination.Code));

        return sb.ToString();
      }
    }

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);

      LoadConfiguration();

      destinationManager = new DestinationManager(RepositoryLocator);
      LoggingServices.RegisterHelper(destinationManager.Logger);
      destinationManager.Initialise();

      EventBroker.Register(destinationManager);
    }
    public override void Finalise()
    {
      EventBroker.Unregister(destinationManager);

      destinationManager.Finalise();
      LoggingServices.UnregisterHelper(destinationManager.Logger);
      destinationManager = null;

      base.Finalise();
    }

    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = "DestinationManager.IsLoggingActive";
      Boolean? logging = ParameterRepository.GetBoolean(loggingParameterName);
      if (logging == null)
      {
        IsLoggingEnabled = false;
        ParameterRepository.SetBoolean(loggingParameterName, IsLoggingEnabled);
      }
      else
        IsLoggingEnabled = logging.Value;
      #endregion
    }

    private DestinationManager destinationManager = null;
  }
}