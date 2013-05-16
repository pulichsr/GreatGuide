using System;
using System.Text;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Plugins;

namespace Nucleo.GoodGuide.RegionTrigger
{
  [Plugin(PluginKinds.Module, "RegionTrigger", "Region Trigger Plugin for GoodGuide", "Nucleo Technologies", "1.0")]
  public class Plugin : GoodGuidePlugin, IGoodGuidePlugin
  {
    public string Status
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(Name + "\r\n");
        sb.Append(string.Empty.PadRight(Name.Length, '-') + "\r\n");
        sb.Append(string.Format("Total Regions: {0}\r\n", regionCache.Regions.Count));

        lock(regionTrigger.RegionChecker.ActiveRegionLock)
        {
          sb.Append(string.Format("Active Regions: {0}\r\n", regionTrigger.RegionChecker.ActiveRegions.Count));
          foreach (Region activeRegion in regionTrigger.RegionChecker.ActiveRegions)
            sb.Append(string.Format("Region: {0}\r\n", activeRegion.Id));
        }

        return sb.ToString();
      }
    }

    public override void Initialise(NamedParameters initialisationParameters)
    {
      base.Initialise(initialisationParameters);

      LoadConfiguration();

      regionDal = RepositoryLocator.LocateRepository<IGpsRegionRepository>();

      regionBll = new GpsRegionBll(regionDal);
      regionCache = new RegionCache(regionBll);
      regionTrigger = new RegionTrigger(regionCache);

      LoggingServices.RegisterHelper(regionTrigger.logger);

      regionTrigger.Initialise();
      EventBroker.Register(regionTrigger);
    }
    public override void Finalise()
    {
      regionTrigger.Finalise();
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
      string loggingParameterName = "RegionTrigger.IsLoggingActive";
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

    private IGpsRegionRepository regionDal = null;
    private GpsRegionBll regionBll = null;
    private RegionCache regionCache = null;
    private RegionTrigger regionTrigger = null;
  }
}
