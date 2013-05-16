using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IStreetSearcher
  {
    Boolean Search(DataObjects.Region region, ref string searchCriteria);
  }
}
