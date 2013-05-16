using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IStreetSelector
  {
    Boolean Select(DataObjects.Region region,string searchCriteria, out DataObjects.Street street);
  }
}
