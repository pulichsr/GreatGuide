using System;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface ISearchRegionRepository
  {
    DataObjects.Region GetById(Int32 id);
    DataObjects.Regions GetMatching(string searchCriteria, Int16 maxRows);
  }
}