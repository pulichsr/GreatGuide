using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IRegionSelector
  {
    Boolean Select(string searchCriteria,out DataObjects.Region region);
  }
}
