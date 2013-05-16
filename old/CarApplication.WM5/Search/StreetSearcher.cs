using System;
using System.Windows.Forms;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.CarApplication;

namespace Nucleo.GoodGuide.CarApplication
{
  public class StreetSearcher:
    IStreetSearcher
  {
    public StreetSearcher(ILogger logger,IStreetSearchDataProvider dataProvider)
    {
      this.dataProvider = dataProvider;
      this.logger = logger;
    }

    #region IStreetSearcher
    public Boolean Search(DataObjects.Region region,ref string searchCriteria)
    {
      dataProvider.Region = region;
      DialogResult result = frmSearchStreet.Search(logger,dataProvider, ref searchCriteria);
      return result == DialogResult.OK;
    }
    #endregion

    private readonly IStreetSearchDataProvider dataProvider;
    private readonly ILogger logger;
  }
}