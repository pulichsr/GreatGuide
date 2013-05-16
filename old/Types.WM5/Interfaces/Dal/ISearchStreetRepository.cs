using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface ISearchStreetRepository
  {
    DataObjects.Streets GetMatching(string searchCriteria, Int16 maxRows);
    DataObjects.Streets GetMatching(DataObjects.Region region, string searchCriteria, Int16 maxRows);
  }
}