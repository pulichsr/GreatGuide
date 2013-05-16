using System;
using System.Windows.Forms;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.CarApplication
{
  public class DestinationSelector:
    IDestinationSelector
  {
    public DestinationSelector(ILogger logger, IDestinationSearchDataProvider dataProvider)
    {
      this.dataProvider = dataProvider;
      this.logger = logger;
    }

    #region IDestinationSelector
    public Boolean Select(string searchCriteria, out DataObjects.Destination destination)
    {
      destination = null;
      DialogResult result = frmSelectDestination.Select(logger, dataProvider, searchCriteria, out destination);
      return result == DialogResult.OK;
    }
    #endregion

    private readonly IDestinationSearchDataProvider dataProvider;
    private readonly ILogger logger;
  }
}