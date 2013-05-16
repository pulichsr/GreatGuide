using System;
using System.Text;
using Nucleo.GoodGuide.Types;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.GpsRawDataTranslator
{
  [Plugin(PluginKinds.Module, "GPSRawDataTranslator", "Gps Raw Data Translator Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin, IGoodGuidePlugin
  {
    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");

        lock (translator.InputLock)
        {
          sb.Append(string.Format("Input: {0}\r\n",translator.InputCount));
          sb.Append(string.Format("  {0}\r\n",translator.GgaData));
          sb.Append(string.Format("  {0}\r\n",translator.RmcData));
          sb.Append(string.Format("  {0}\r\n",translator.VtgData));
        }

        lock (translator.OutputLock)
        {
          sb.Append(string.Format("Output {0}\r\n",translator.OutputCount));
        }

        return sb.ToString();
      }
    }

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);
      LoadConfiguration();

      // Create GPS adapter class and register it with the event broker
      translator = new RawDataTranslator(ParameterRepository,new UBloxEventModifier());

      LoggingServices.RegisterHelper(translator.Logger);

      EventBroker.Register(translator);

      translator.Initialise();
    }
    public override void Finalise()
    {
      translator.Finalise();
      EventBroker.Unregister(translator);

      LoggingServices.UnregisterHelper(translator.Logger);

      translator = null;

      base.Finalise();
    }

    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = "GPSRawDataTranslator.IsLoggingActive";
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

    private RawDataTranslator translator = null;
  }
}