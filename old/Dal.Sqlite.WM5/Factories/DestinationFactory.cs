using WrapperObjects=Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.Factories
{
  public class DestinationFactory
  {
    public DataObjects.Destination Create(WrapperObjects.Destination data)
    {
      DataObjects.Destination destination = new DataObjects.Destination();

      destination.Id = data.Id;
      destination.Code = data.Code;
      destination.Address = data.Address;
      destination.Latitude = data.Latitude;
      destination.Longitude = data.Longitude;
      destination.DestinationTypeDescription = data.DestinationTypeDescription;

      return destination;
    }
  }
}
