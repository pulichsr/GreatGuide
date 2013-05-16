using Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.Factories
{
  public class RegionFactory
  {
    public Region Create(SearchRegion searchRegion)
    {
      return new Region(searchRegion.Id,searchRegion.ParentId,searchRegion.SearchKey,searchRegion.Name,searchRegion.Level,searchRegion.CollatedName,searchRegion.Latitude,searchRegion.Longitude);
    }
  }
}
