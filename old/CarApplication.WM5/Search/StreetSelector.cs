using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.CarApplication;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.CarApplication
{
  public class StreetSelector:
    IStreetSelector
  {
    public StreetSelector(ILogger logger,IStreetSearchDataProvider dataProvider)
    {
      this.dataProvider = dataProvider;
      this.logger = logger;
    }

    #region IStreetSelector Members
    public Boolean Select(DataObjects.Region region, string searchCriteria, out DataObjects.Street street)
    {
      dataProvider.Region = region;

      DialogResult result = frmSelectStreet.Select(logger,dataProvider, searchCriteria, out street);
      return result == DialogResult.OK;
    }
    #endregion

    private readonly IStreetSearchDataProvider dataProvider;
    private readonly ILogger logger;
  }
}