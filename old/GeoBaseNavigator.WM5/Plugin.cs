using System;
using System.Text;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.GeoBaseNavigator
{
  [Plugin(PluginKinds.Module, "GeoBaseNavigator", "GeoBase Navigator Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin, IGoodGuidePlugin
  {
    #region IGoodGuidePlugin Members

    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");
        sb.Append(string.Format("Initialized:{0}\r\n", ((navigator == null) ? "True" : "False")));
        return sb.ToString();
      }
    }

    #endregion

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);

      LoadConfiguration();

      #region Map control
      Language language = GeoBaseNavigator.GeoBaseDefaultLanguage;
      NamedParameter mpLanguage = initialisationParameters[ConfigurationParameters.Navigator.Language];
      if (mpLanguage != null)
      {
        language = (Language)mpLanguage.Value;
      }

      NamedParameter mpMapControl = initialisationParameters[ConfigurationParameters.Navigator.MapControl];
      if (mpMapControl != null)
      {
        Logger.Write(this, "  MapControl assigned");
        mapControl = (Control)mpMapControl.Value;

        Logger.Write(this, "  Creating GeoBaseNavigator");
        navigator = new GeoBaseNavigator(ParameterRepository,language);
        LoggingServices.RegisterHelper(navigator.Logger);

        Logger.Write(this, "  Initialising GeoBaseNavigator");
        navigator.Initialise(mapControl);

        adapter = new NavigatorAdapter(navigator);
        LoggingServices.RegisterHelper(adapter.Logger);
        EventBroker.Register(adapter);
      }
      #endregion

    }
    public override void Finalise()
    {
      if (navigator != null)
      {
        navigator.CancelRoute();

        EventBroker.Unregister(navigator);
        LoggingServices.UnregisterHelper(navigator.Logger);

        navigator.Finalise();
        navigator = null;
      }

      base.Finalise();
    }
    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = "GeoBaseNavigator.IsLoggingActive";
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

    private GeoBaseNavigator navigator = null;
    private Control mapControl = null;
    private NavigatorAdapter adapter;
  }
}
