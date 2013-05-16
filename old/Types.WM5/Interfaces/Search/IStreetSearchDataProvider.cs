using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IStreetSearchDataProvider
  {
    event EventHandler DataChanged;

    string SearchCriteria { get;set;}
    DataObjects.Region Region { set;}
    DataObjects.Streets Data { get;}
  }
}