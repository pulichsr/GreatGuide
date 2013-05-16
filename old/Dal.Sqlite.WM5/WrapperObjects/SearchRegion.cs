using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  public class SearchRegion
  {
    [DbAutoColumnName]
    [DbPrimaryKey]
    public int Id
    {
      get { return id; }
      set { id = value; }
    }

    [DbAutoColumnName]
    public int? ParentId
    {
      get { return parentId; }
      set { parentId = value; }
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
    public int Level
    {
      get { return level; }
      set { level = value; }
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

    private Int32 id;
    private Int32? parentId;
    private string searchKey;
    private string name;
    private string collatedName;
    private Int32 level;
    private double latitude;
    private double longitude;
  }

  public class SearchRegions: List<SearchRegion>
  {
  }
}