using System;
using System.Text;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.MasterAreaTrigger
{
  [Plugin(PluginKinds.Module, "MasterAreaTrigger", "Master Area Trigger Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin, IGoodGuidePlugin
  {
    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");
        sb.Append(string.Format("Regions: {0}\r\n", regionCache.Regions.Count));
        return sb.ToString();
      }
    }

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);
      LoadConfiguration();

      regionDal = RepositoryLocator.LocateRepository<IMasterAreaRepository>();

      regionBll = new MasterAreaBll(regionDal);
      regionCache = new MasterAreaCache(regionBll);
      regionTrigger = new MasterAreaTrigger(regionCache);

      EventBroker.Register(regionTrigger);

      regionCache.FetchRegions();
    }
    public override void Finalise()
    {
      EventBroker.Unregister(regionTrigger);

      regionTrigger = null;
      regionCache = null;
      regionBll = null;
      regionDal = null;

      base.Finalise();
    }

    private void LoadConfiguration()
    {
      #region Get IsLoggingActive
      string loggingParameterName = "MasterAreaTrigger.IsLoggingActive";
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

    private IMasterAreaRepository regionDal = null;
    private MasterAreaBll regionBll = null;
    private MasterAreaCache regionCache = null;
    private MasterAreaTrigger regionTrigger = null;
  }
}