using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IDestinationSelector
  {
    Boolean Select(string searchCriteria,out DataObjects.Destination destination);
  }
}
