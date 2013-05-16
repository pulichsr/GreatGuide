using System;
using Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IStreetNumberEntry
  {
    Boolean EnterNumber(Street street,out Int16 streetNumber);
  }
}
