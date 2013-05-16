using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.MasterAreaTrigger
{
  public class MasterAreaCache : IRegionCache
  {
    public MasterAreaCache(MasterAreaBll bll)
    {
      this.bll = bll;
    }

    public Regions Regions
    {
      get { return regions; }
    }

    public void FetchRegions()
    {
      lock(bll)
      {
        regions = bll.GetRegions();
      }
    }

    private Regions regions = new Regions();
    private readonly MasterAreaBll bll = null;
  }
}