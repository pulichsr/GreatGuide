using System;
using System.Text;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.GpsRawDataRecorder
{
  [Plugin(PluginKinds.Module, "GPSRawDataRecorder", "Gps Raw Data Recorder Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin,IGoodGuidePlugin
  {
    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");
        sb.Append(string.Format("State:{0}\r\n",recorder.State));
        sb.Append(string.Format("Trip:{0}\r\n",recorder.CurrentTripName));
        return sb.ToString();
      }
    }

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);
      LoadConfiguration();

      recorder = new GpsRawDataRecorder(ParameterRepository);
      LoggingServices.RegisterHelper(recorder.Logger);
      recorder.Initialise();

      EventBroker.Register(recorder);
    }
    public override void Finalise()
    {
      recorder.Stop();

      EventBroker.Unregister(recorder);
      LoggingServices.UnregisterHelper(recorder.Logger);

      recorder.Finalise();
      recorder = null;

      base.Finalise();
    }

    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = "GPSRawDataRecorder.IsLoggingActive";
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

    private GpsRawDataRecorder recorder = null;
  }
}