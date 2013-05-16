using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IRegionConfirmer
  {
    Boolean Confirm(DataObjects.Region region);
  }
}
