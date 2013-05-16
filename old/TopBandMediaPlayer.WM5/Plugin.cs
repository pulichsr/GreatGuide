using System;
using System.Text;
using Nucleo.GoodGuide.TopBandMediaPlayer;
using Nucleo.GoodGuide.Types;
using Nucleo;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.TopbandMediaPlayer
{
  [Plugin(PluginKinds.Module, "TopbandMediaPlayer", "Topband Media Player Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
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
        sb.Append(string.Format("Initialized:{0}\r\n", ((mediaPlayer == null) ? "True" : "False")));
        return sb.ToString();
      }
    }

    #endregion

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);

      LoadConfiguration();

      Logger.Write(this,"Loaded configuration");

      Logger.Write(this, "Creating and initialising media player");
      mediaPlayer = new TopbandMediaPlayer(ParameterRepository);
      LoggingServices.RegisterHelper(mediaPlayer.Logger);
      mediaPlayer.Initialise();

      adapter = new MediaPlayerAdapter(mediaPlayer);
      LoggingServices.RegisterHelper(adapter.Logger);
      EventBroker.Register(adapter);
    }
    public override void Finalise()
    {
      EventBroker.Unregister(adapter);

      mediaPlayer.Finalise();

      LoggingServices.UnregisterHelper(adapter.Logger);
      LoggingServices.UnregisterHelper(mediaPlayer.Logger);

      mediaPlayer = null;

      base.Finalise();
    }
    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = "TopbandMediaPlayer.IsLoggingActive";
      Boolean? logging = ParameterRepository.GetBoolean(loggingParameterName);
      if (logging == null)
      {
        IsLoggingEnabled = true;
        ParameterRepository.SetBoolean(loggingParameterName, IsLoggingEnabled);
      }
      else
        IsLoggingEnabled = logging.Value;
      #endregion
    }

    private TopbandMediaPlayer mediaPlayer = null;
    private MediaPlayerAdapter adapter;
  }
}