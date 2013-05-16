using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.CarApplication;

namespace Nucleo.GoodGuide.CarApplication
{
  public class DestinationSearcher:
    IDestinationSearcher
  {
    public DestinationSearcher(ILogger logger, IDestinationSearchDataProvider dataProvider)
    {
      this.dataProvider = dataProvider;
      this.logger = logger;
    }

    #region IDestinationSearcher
    public Boolean Search(ref string searchCriteria)
    {
      DialogResult result = frmSearchDestination.Search(logger, dataProvider, ref searchCriteria);
      return result == DialogResult.OK;
    }
    #endregion

    private readonly IDestinationSearchDataProvider dataProvider;
    private readonly ILogger logger;
  }
}