using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  public class SearchStreet
  {
    [DbAutoColumnName]
    [DbPrimaryKey]
    public int Id
    {
      get { return id; }
      set { id = value; }
    }

    [DbAutoColumnName]
    public int RegionId
    {
      get { return regionId; }
      set { regionId = value; }
    }

    [DbAutoColumnName]
    public string SearchKey
    {
      get { return searchKey; }
      set { searchKey = value; }
    }

    [DbAutoColumnName]
    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    [DbAutoColumnName]
    public string CollatedName
    {
      get { return collatedName; }
      set { collatedName = value; }
    }

    [DbAutoColumnName]
    [DbReadonlyColumn]
    public string RegionCollatedName
    {
      get { return regionCollatedName; }
      set { regionCollatedName = value; }
    }

    [DbAutoColumnName]
    public double Latitude
    {
      get { return latitude; }
      set { latitude = value; }
    }

    [DbAutoColumnName]
    public double Longitude
    {
      get { return longitude; }
      set { longitude = value; }
    }

    [DbAutoColumnName]
    public string StreetNumbers
    {
      get { return streetNumbers; }
      set { streetNumbers = value; }
    }

    private Int32 id;
    private Int32 regionId;
    private string searchKey;
    private string name;
    private string collatedName;
    private string regionCollatedName;
    private double latitude;
    private double longitude;
    private string streetNumbers;
  }

  public class SearchStreets : List<SearchStreet>
  {}
}