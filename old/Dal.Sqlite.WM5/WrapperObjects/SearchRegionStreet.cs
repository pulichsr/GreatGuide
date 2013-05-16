using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  public class SearchRegionStreet
  {
    [DbAutoColumnName]
    [DbPrimaryKey(0)]
    public int RegionId
    {
      get { return regionId; }
      set { regionId = value; }
    }

    [DbAutoColumnName]
    [DbPrimaryKey(1)]
    public int StreetId
    {
      get { return streetId; }
      set { streetId = value; }
    }


    private Int32 regionId;
    private Int32 streetId;
  }

  public class SearchRegionStreets : List<SearchRegionStreet>
  {
  }
}