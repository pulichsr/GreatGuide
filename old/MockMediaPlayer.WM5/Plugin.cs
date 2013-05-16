using System;
using System.Collections.Generic;
using System.Text;
using Nucleo.Plugins;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.MockMediaPlayer
{
  [Plugin(PluginKinds.Module,"MockMediaPlayer", "Mock Media Manager Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin, IGoodGuidePlugin
  {
    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");
        sb.Append("Loaded items:\r\n");
        if (mediaManager.MediaItems == null)
          sb.Append("None\r\n");
        else
        {
          foreach (MediaItem mediaItem in mediaManager.MediaItems)
            sb.Append(string.Format("{0}: {1}sec\r\n",mediaItem.Name,mediaItem.Duration));
        }

        sb.Append("Playing items:\r\n");
        lock (mediaManager.Channels)
        {
          Dictionary<Int32,Channel>.Enumerator enumerator = mediaManager.Channels.GetEnumerator();

          while (true)
          {
            if (enumerator.MoveNext() == false)
              break;

            KeyValuePair<Int32,Channel> entry = enumerator.Current;
            if (entry.Value == null)
              continue;


            sb.Append(string.Format("ChannelGroup {0}: {1}\r\n",entry.Value.ChannelGroup,entry.Value.Item.Name));
          }

          return sb.ToString();
        }
      }
    }

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);
      LoadConfiguration();

      LoggingServices.RegisterHelper(mediaManager.Logger);

      EventBroker.Register(mediaManager);
      mediaManager.Initialise();
    }
    public override void Finalise()
    {
      mediaManager.Finalise();

      EventBroker.Unregister(mediaManager);

      LoggingServices.UnregisterHelper(mediaManager.Logger);

      base.Finalise();
    }

    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = "MockMediaPlayer.IsLoggingActive";
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

    private readonly MediaManager mediaManager = new MediaManager();
  }
}