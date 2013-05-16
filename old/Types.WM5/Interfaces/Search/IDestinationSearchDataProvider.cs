using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IDestinationSearchDataProvider
  {
    event EventHandler DataChanged;

    string SearchCriteria {get; set;}
    DataObjects.Destinations Data {get;}
  }
}