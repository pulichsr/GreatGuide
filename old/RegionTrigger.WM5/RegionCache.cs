using System;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.RegionTrigger
{
  public class RegionCache : IRegionCache
  {
    public RegionCache(GpsRegionBll bll)
    {
      this.bll = bll;
    }

    public Regions Regions
    {
      get { return regions; }
    }

    public void FetchByMasterArea(Int32 masterAreaId)
    {
      regions = bll.GetRegionsByMasterArea(masterAreaId);
    }

    private Regions regions = new Regions();
    private readonly GpsRegionBll bll = null;
  }
}
