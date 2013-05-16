using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.CarApplication;

namespace Nucleo.GoodGuide.CarApplication
{
  public class RegionSearcher:
    IRegionSearcher
  {
    public RegionSearcher(ILogger logger, IRegionSearchDataProvider dataProvider)
    {
      this.dataProvider = dataProvider;
      this.logger = logger;
    }

    #region IRegionSearcher
    public Boolean Search(ref string searchCriteria)
    {
      DialogResult result = frmSearchRegion.Search(logger, dataProvider, ref searchCriteria);
      return result == DialogResult.OK;
    }
    #endregion

    private readonly IRegionSearchDataProvider dataProvider;
    private readonly ILogger logger;
  }
}