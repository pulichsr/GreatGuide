using System;
using System.Collections.Generic;
using Nucleo.Math.Geo;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class Region
  {
    public Region(int id, string searchKey, string name, int level, string collatedName, double latitude, double longitude):
      this(id,null,searchKey,name,level,collatedName,latitude,longitude)
    {
    }
    public Region(int id,int? parentId,string searchKey,string name,int level,string collatedName,double latitude,double longitude)
    {
      this.id = id;
      this.parentId = parentId;
      this.searchKey = searchKey;
      this.name = name;
      this.level = level;
      this.collatedName = collatedName;
      this.latitude = latitude;
      this.longitude = longitude;
    }

    #region Properties
    public int Id
    {
      get { return id; }
      set { id = value; }
    }

    public int? ParentId
    {
      get { return parentId; }
      set { parentId = value; }
    }

    public string SearchKey
    {
      get { return searchKey; }
      set { searchKey = value; }
    }

    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    public int Level
    {
      get { return level; }
      set { level = value; }
    }

    public string CollatedName
    {
      get { return collatedName; }
      set { collatedName = value; }
    }

    public double Latitude
    {
      get { return latitude; }
      set { latitude = value; }
    }

    public double Longitude
    {
      get { return longitude; }
      set { longitude = value; }
    }
    #endregion

    #region Fields
    private Int32 id;
    private Int32? parentId;
    private string searchKey;
    private string name;
    private Int32 level;
    private string collatedName;
    private double latitude;
    private double longitude;
    #endregion
  }

  public class Regions : List<Region>
  {
    public void SortByDistance(double latitude, double longitude)
    {
      this.Sort(new RegionDistanceComparer(latitude, longitude));
    }
  }

  internal class RegionDistanceComparer :
    IComparer<Region>
  {
    public RegionDistanceComparer(double latitude, double longitude)
    {
      this.latitude = latitude;
      this.longitude = longitude;
    }

    #region IComparer<Street>
    public int Compare(Region x, Region y)
    {
      Guard.ArgumentNotNull(x, "x");
      Guard.ArgumentNotNull(y, "y");

      double xDistance = GeoCalculations.ApproximateDistance(latitude, longitude, x.Latitude, x.Longitude);
      double yDistance = GeoCalculations.ApproximateDistance(latitude, longitude, y.Latitude, y.Longitude);

      if (xDistance == yDistance)
        return 0;
      if (yDistance > xDistance)
        return -1;

      return 1;
    }
    #endregion

    private readonly double latitude;
    private readonly double longitude;
  }


}