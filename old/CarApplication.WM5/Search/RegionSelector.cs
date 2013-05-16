using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.CarApplication;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.CarApplication
{
  public class RegionSelector:
    IRegionSelector
  {
    public RegionSelector(ILogger logger, IRegionSearchDataProvider dataProvider)
    {
      this.dataProvider = dataProvider;
      this.logger = logger;
    }

    #region IRegionSelector Members
    public Boolean Select(string searchCriteria, out DataObjects.Region region)
    {
      DialogResult result = frmSelectRegion.Select(logger, dataProvider, searchCriteria, out region);
      return result == DialogResult.OK;
    }
    #endregion

    private readonly IRegionSearchDataProvider dataProvider;
    private readonly ILogger logger;
  }
}