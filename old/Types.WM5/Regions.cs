using System;
using System.Collections.Generic;

namespace Nucleo.GoodGuide.Types
{
  public class Regions : List<Region>
  {
    public Boolean IsInsideRegion(float latitude, float longitude)
    {
//      if (GeoRegion.IsInsideRegion(MinLatitude,MaxLatitude,MinLongitude,MaxLongitude,latitude,longitude) == false)
//        return false;

      for (Int32 RegionNo = 0; RegionNo < Count; RegionNo++)
        if (this[RegionNo].IsInsideRegion(latitude, longitude) == true)
          return true;

      return false;
    }
    public Regions InsideRegions(float latitude,float longitude)
    {
      Regions Regions = new Regions();

      for (Int32 RegionNo = 0; RegionNo < Count; RegionNo++)
        if (this[RegionNo].IsInsideRegion(latitude,longitude) == true)
          Regions.Add(this[RegionNo]);

      return Regions;
    }
    public void CalculateBoundingRegion()
    {
      MinLatitude = float.MaxValue;
      MaxLatitude = float.MinValue;
      MinLongitude = float.MaxValue;
      MaxLongitude = float.MinValue;

      for (Int32 RegionNo = 0; RegionNo < Count; RegionNo++)
      {
        if (this[RegionNo].MinLatitude < MinLatitude)
          MinLatitude = this[RegionNo].MinLatitude;
        if (this[RegionNo].MaxLatitude > MaxLatitude)
          MaxLatitude = this[RegionNo].MaxLatitude;
        if (this[RegionNo].MinLongitude < MinLongitude)
          MinLongitude = this[RegionNo].MinLongitude;
        if (this[RegionNo].MaxLongitude > MaxLongitude)
          MaxLongitude = this[RegionNo].MaxLongitude;
      }
    }

    public float MinLatitude = float.MaxValue;
    public float MaxLatitude = float.MinValue;
    public float MinLongitude = float.MaxValue;
    public float MaxLongitude = float.MinValue;
  }
}
