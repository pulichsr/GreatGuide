using System;
using Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IStreetConfirmer
  {
    Boolean Confirm(Street street);
  }
}
