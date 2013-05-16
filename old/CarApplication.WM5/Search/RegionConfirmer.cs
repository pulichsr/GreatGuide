using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.CarApplication;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.CarApplication
{
  public class RegionConfirmer:
    IRegionConfirmer
  {
    #region IRegionConfirmer
    public Boolean Confirm(DataObjects.Region region)
    {
      DialogResult result = frmConfirmRegion.Confirm(region);
      return result == DialogResult.OK;
    }
    #endregion

  }
}