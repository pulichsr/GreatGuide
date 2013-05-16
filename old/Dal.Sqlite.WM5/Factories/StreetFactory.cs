using Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.Factories
{
  public class StreetFactory
  {
    public Street Create(SearchStreet searchStreet)
    {
      return new Street(searchStreet.Id,searchStreet.RegionId,searchStreet.Name,searchStreet.RegionCollatedName,searchStreet.Latitude,searchStreet.Longitude,searchStreet.StreetNumbers);
    }
  }
}
