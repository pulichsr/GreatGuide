using System;
using System.Windows.Forms;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.CarApplication
{
  public class StreetNumberEntry :
    IStreetNumberEntry
  {
    #region IStreetNumberEntry
    public Boolean EnterNumber(DataObjects.Street street, out Int16 streetNumber)
    {
      DialogResult result = frmEnterStreetNumber.EnterNumber(street,out streetNumber);
      return result == DialogResult.OK;
    }
    #endregion
  }
}