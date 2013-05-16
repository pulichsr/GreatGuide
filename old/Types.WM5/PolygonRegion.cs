using System;

namespace Nucleo.GoodGuide.Types
{
  public class PolygonRegion : Region
  {
    public const string RegionType = "P";

    public static void ParseRegionData(string regionData, PolygonRegion region)
    {
      try
      {
        Points.FromString(regionData,region.points);
      }
      catch (Exception exc)
      {
        throw new FormatException(string.Format("Conversion error. Invalid RegionData: '{0}'", regionData), exc);
      }
    }
    public static string GetRegionData(PolygonRegion region)
    {
      return region.Points.ToString();
    }
    public static void CalculateMinMaxLatLong(PolygonRegion region,
      out float minLatitude, out float maxLatitude, out float minLongitude, out float maxLongitude)
    {
      minLatitude = float.MaxValue;
      maxLatitude = float.MinValue;
      minLongitude = float.MaxValue;
      maxLongitude = float.MinValue;

      for (Int32 PointNo = 0;PointNo < region.Points.Count;PointNo++)
      {
        if (region.Points[PointNo].Latitude < minLatitude)
          minLatitude = region.Points[PointNo].Latitude;

        if (region.Points[PointNo].Latitude > maxLatitude)
          maxLatitude = region.Points[PointNo].Latitude;

        if (region.Points[PointNo].Longitude < minLongitude)
          minLongitude = region.Points[PointNo].Longitude;

        if (region.Points[PointNo].Longitude > maxLongitude)
          maxLongitude = region.Points[PointNo].Longitude;
      }
    }

    public PolygonRegion(string regionData)
    {
      ParseRegionData(regionData, this);
      CalculateMinMaxLatLong(this, out MinLatitude, out MaxLatitude, out MinLongitude, out MaxLongitude);     
    }
    public PolygonRegion(Int32 id, string regionData)
    {
      Id = id;

      ParseRegionData(regionData, this);
      CalculateMinMaxLatLong(this, out MinLatitude, out MaxLatitude, out MinLongitude, out MaxLongitude);
    }
    public PolygonRegion(
      Int32 id, float minLatitude, float maxLatitude, float minLongitude, float maxLongitude, string regionData,Boolean resetOnEntry) : 
      base(id,minLatitude,maxLatitude,minLongitude,maxLongitude,resetOnEntry)
    {
      ParseRegionData(regionData,this);
    }

    public Points Points
    {
      get { return points; }
    }

    public override Boolean IsInsideRegion(float latitude, float longitude)
    {
      Boolean IsInRegion = false;
      Int32 Index1 = 0;
      Int32 Index2 = Points.Count - 1;

      while (Index1 < Points.Count)
      {
        if ((((Points[Index1].Longitude <= longitude) && (longitude < Points[Index2].Longitude)) || ((Points[Index2].Longitude <= longitude) && (longitude < Points[Index1].Longitude))))
        {
          if (latitude < (Points[Index2].Latitude - Points[Index1].Latitude) * (longitude - Points[Index1].Longitude) / (Points[Index2].Longitude - Points[Index1].Longitude) + Points[Index1].Latitude)
            IsInRegion = !IsInRegion;
        }

        Index2 = Index1;
        Index1++;
      }

      return IsInRegion;
    }

    private readonly Points points = new Points();
  }
}

