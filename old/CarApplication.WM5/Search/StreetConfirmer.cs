using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.CarApplication;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.CarApplication
{
  public class StreetConfirmer:
    IStreetConfirmer
  {
    #region IStreetConfirmer
    public Boolean Confirm(DataObjects.Street street)
    {
      DialogResult result = frmConfirmStreet.Confirm(street);
      return result == DialogResult.OK;
    }
    #endregion

  }
}