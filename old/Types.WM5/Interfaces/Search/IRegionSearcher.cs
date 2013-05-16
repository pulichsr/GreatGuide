using System;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IRegionSearcher
  {
    Boolean Search(ref string searchCriteria);
  }
}
