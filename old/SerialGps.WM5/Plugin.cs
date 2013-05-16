using System;
using System.Text;
using Nucleo.GoodGuide.Types;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.SerialGps
{
  [Plugin(PluginKinds.Module, "SerialGpsAdapterPlugin", "Serial GPS Adapter Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin,IGoodGuidePlugin
  {
    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();

        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");

        sb.Append(string.Format("Running: {0}", gpsAdapter.IsRunning));
        return sb.ToString();
      }
    }

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);

      LoadConfiguration();

      // Create GPS adapter class and register it with the event broker so that the GpsPositionEvent it publishes will be distributed.
      gpsAdapter = new SerialGpsAdapter(ParameterRepository);
      LoggingServices.RegisterHelper(gpsAdapter.LoggingHelper);

      gpsAdapter.Initialise();

      EventBroker.Register(gpsAdapter);
    }
    public override void Finalise()
    {
      EventBroker.Unregister(gpsAdapter);

      gpsAdapter.Stop();
      gpsAdapter.Finalise();
      LoggingServices.UnregisterHelper(gpsAdapter.LoggingHelper);
      gpsAdapter = null;

      base.Finalise();
    }

    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = ConfigurationParameters.Gps.IsLoggingActive;
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

    private SerialGpsAdapter gpsAdapter = null;
  }
}