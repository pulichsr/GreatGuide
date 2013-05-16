using System;
using System.Text;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Datasets;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.ContentManager
{
  [Plugin(PluginKinds.Module,"ContentManager", "Content Manager Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin,IGoodGuidePlugin
  {
    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");
        sb.Append(string.Format("Running: {0}\r\n", contentManager.IsRunning));
        sb.Append("Filler Content:\r\n");

        lock (contentManager.FillerContentLock)
        {
          ChannelContentCollection filler = contentManager.FillerContent;
          foreach (ChannelContentDataset.ChannelContentRow row in filler)
          {
            string priority = string.Empty;
            if (row.IsPriorityNull() == false)
              priority = row.Priority.ToString();

            string fillerDelay = string.Empty;
            if (row.IsFillerDelayNull() == false)
              fillerDelay = row.FillerDelay.ToString();

            string presentedCount = string.Empty;
            if (row.IsPresentedCountNull() == false)
              presentedCount = row.PresentedCount.ToString();

            string triggerType = string.Empty;
            if (row.IsTriggerTypeNull() == false)
              triggerType = row.TriggerType.ToString();

            sb.Append(
              string.Format(
                "{0}: Priority:{1}, ChannelGroup:{2}, Delay:{3}, Presented:{4}, TriggerType:{5}\r\n",
                row.ContentItemFilename,
                priority,
                row.ChannelGroupId,
                fillerDelay,
                presentedCount,
                triggerType));
          }
        }
        return sb.ToString();
      }
    }

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);

      LoadConfiguration();

      channelContentDal = RepositoryLocator.LocateRepository<IChannelContentRepository>();
      channelGroupDal = RepositoryLocator.LocateRepository<IChannelGroupRepository>();

      channelContentBll = new ChannelContentBll(channelContentDal);
      channelGroupBll = new ChannelGroupBll(channelGroupDal);

      contentManager = new ContentManager(RepositoryLocator,channelContentBll,channelGroupBll);

      EventBroker.Register(contentManager);
      LoggingServices.RegisterHelper(contentManager.Logger);

      contentManager.Rules.Add(new PriorityRule());
      contentManager.Rules.Add(new FillerDelayRule());
//      contentManager.Rules.Add(new MinimumDistanceRule());
      contentManager.Rules.Add(new PresentedCountRule());
      contentManager.Rules.Add(new ActivePeriodRule());
      contentManager.Rules.Add(new HeadingRule());
      contentManager.Rules.Add(new ActiveTimeRule());
      contentManager.Rules.Add(new ActiveDaysRule());
      contentManager.Rules.Add(new SequenceRule());
      contentManager.Rules.Add(new SpeedBelowThresholdRule());
      contentManager.Rules.Add(new SpeedAboveThresholdRule());
      contentManager.Rules.Add(new SpeedBetweenThresholdsRule());

      contentManager.CurrentMediaUpdaters.Add(new SequenceDependencyUpdater(channelContentBll));
      contentManager.CurrentMediaUpdaters.Add(new PresentedCountUpdater(channelContentBll));

      contentManager.Initialise();
    }
    public override void Finalise()
    {
      contentManager.Finalise();

      LoggingServices.UnregisterHelper(contentManager.Logger);

      EventBroker.Unregister(contentManager);

      contentManager = null;
      channelContentBll = null;
      channelGroupBll = null;
      channelContentDal = null;
      channelGroupDal = null;

      base.Finalise();
    }

    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = "ContentManager.IsLoggingActive";
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

    #region Fields
    private ContentManager contentManager = null;
    private IChannelContentRepository channelContentDal = null;
    private ChannelContentBll channelContentBll = null;
    private IChannelGroupRepository channelGroupDal = null;
    private ChannelGroupBll channelGroupBll = null;
    #endregion
  }
}
