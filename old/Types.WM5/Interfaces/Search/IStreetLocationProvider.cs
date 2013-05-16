using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IStreetLocationProvider
  {
    Boolean GetLocation(DataObjects.Region region, DataObjects.Street street, Int16 streetNumber,out double latitude, out double longitude);
  }
}