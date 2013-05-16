using System;
using System.Collections.Generic;
using Nucleo.Math.Geo;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class Street
  {
    public Street(int id, int regionId, string name, string regionCollatedName, double latitude, double longitude, string streetNumbers)
    {
      this.id = id;
      this.regionId = regionId;
      this.name = name;
      this.regionCollatedName = regionCollatedName;
      this.latitude = latitude;
      this.longitude = longitude;
      this.streetNumbers = streetNumbers;
    }

    #region Properties
    public int Id
    {
      get { return id; }
      set { id = value; }
    }

    public int RegionId
    {
      get { return regionId; }
      set { regionId = value; }
    }

    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    public string RegionCollatedName
    {
      get { return regionCollatedName; }
      set { regionCollatedName = value; }
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

    public string StreetNumbers
    {
      get { return streetNumbers; }
      set { streetNumbers = value; }
    }
    #endregion

    public new string ToString()
    {
      return string.Format("{0}: {1}",name,regionCollatedName);
    }

    #region Fields
    private Int32 id;
    private Int32 regionId;
    private string name;
    private string regionCollatedName;
    private double latitude;
    private double longitude;
    private string streetNumbers;
    #endregion
  }

  public class Streets: List<Street>
  {
    public void SortByDistance(double latitude,double longitude)
    {
      this.Sort(new StreetDistanceComparer(latitude, longitude));
    }
  }

  internal class StreetDistanceComparer :
    IComparer<Street>
  {
    public StreetDistanceComparer(double latitude, double longitude)
    {
      this.latitude = latitude;
      this.longitude = longitude;
    }

    #region IComparer<Street>
    public int Compare(Street x, Street y)
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