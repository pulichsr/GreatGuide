using System;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IDestinationSearcher
  {
    Boolean Search(ref string searchCriteria);
  }
}
