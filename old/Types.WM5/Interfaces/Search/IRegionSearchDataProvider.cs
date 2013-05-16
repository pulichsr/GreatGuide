using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IRegionSearchDataProvider
  {
    event EventHandler DataChanged;

    string SearchCriteria {get; set;}
    DataObjects.Regions Data {get;}
  }
}