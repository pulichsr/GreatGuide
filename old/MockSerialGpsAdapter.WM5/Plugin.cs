using System;
using System.Text;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.MockSerialGpsAdapter
{
  [Plugin(PluginKinds.Module, "MockSerialGps", "Mock Serial GPS Adapter Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin,IGoodGuidePlugin
  {
    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");
        sb.Append(string.Format("Running: {0}", gps.IsRunning));
        return sb.ToString();
      }
    }

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);
      LoadConfiguration();

      // Create GPS adapter class and register it with the event broker so that the GpsPositionEvent it publishes will be distributed.
      gps = new MockSerialGps();

      LoggingServices.RegisterHelper(gps.Logger);

      EventBroker.Register(gps);
      gps.Initialise();

    }
    public override void Finalise()
    {
      gps.Stop();
      gps.Finalise();

      EventBroker.Unregister(gps);

      LoggingServices.UnregisterHelper(gps.Logger);

      base.Finalise();
    }

    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = "MockSerialGps.IsLoggingActive";
      Boolean? logging = ParameterRepository.GetBoolean(loggingParameterName);
      if (logging == null)
      {
        ParameterRepository.SetBoolean(loggingParameterName, false);
        IsLoggingEnabled = false;
      }
      else
        IsLoggingEnabled = logging.Value;
      #endregion
    }

    private MockSerialGps gps = null;
  }
}