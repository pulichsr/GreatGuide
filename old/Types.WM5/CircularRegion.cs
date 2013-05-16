using System;

namespace Nucleo.GoodGuide.Types
{
  public class CircularRegion : Region
  {
    public const string RegionType = "C";

    public static void ParseRegionData(string regionData, ref float latitude, ref float longitude,ref Int32 radius,ref Int32 radiusSquared)
    {
      string[] RegionDataFields = regionData.Split('|');
      if (RegionDataFields.Length != 3)
        throw new FormatException(string.Format("Invalid format for RegionData: '{0}'",regionData));

      string PositionData = RegionDataFields[0];
      string[] PositionFields = PositionData.Split(',');
      if (PositionFields.Length != 2)
        throw new FormatException(string.Format("Invalid format for PositionData: '{0}'", PositionData));

      try
      {
        latitude = Convert.ToSingle(PositionFields[0]);
        longitude = Convert.ToSingle(PositionFields[1]);
        radius = Convert.ToInt32(RegionDataFields[1]);
        radiusSquared = Convert.ToInt32(RegionDataFields[2]);
      }
      catch(Exception exc)
      {
        throw new FormatException(string.Format("Conversion error. Invalid RegionData: '{0}'", regionData), exc);
      }
    }
    public static string GetRegionData(float latitude,float longitude,Int32 radius)
    {
      return string.Format("{0},{1}|{2}|{3}", latitude, longitude, radius,radius * radius);
    }
    public static void CalculateMinMaxLatLong(float latitude, float longitude, Int32 radius,
      out float minLatitude, out float maxLatitude, out float minLongitude, out float maxLongitude)
    {
      float Angle = radius * Constants.DistanceToAngularFactor;
      minLatitude = latitude - Angle;
      maxLatitude = latitude + Angle;
      minLongitude = longitude - Angle;
      maxLongitude = longitude + Angle;
    }

    public CircularRegion(string regionData)
    {
      ParseRegionData(regionData, ref Latitude, ref Longitude, ref Radius, ref RadiusSquared);
      CalculateMinMaxLatLong(Latitude,Longitude,Radius,out MinLatitude,out MaxLatitude,out MinLongitude,out MaxLongitude);
    }
    public CircularRegion(Int32 id,string regionData)
    {
      Id = id;

      ParseRegionData(regionData, ref Latitude, ref Longitude, ref Radius, ref RadiusSquared);
      CalculateMinMaxLatLong(Latitude, Longitude, Radius, out MinLatitude, out MaxLatitude, out MinLongitude, out MaxLongitude);
    }
    public CircularRegion(
      Int32 id, float minLatitude, float maxLatitude, float minLongitude, float maxLongitude, string regionData,Boolean resetOnEntry) : 
      base(id,minLatitude,maxLatitude,minLongitude,maxLongitude,resetOnEntry)
    {
      ParseRegionData(regionData,ref Latitude,ref Longitude,ref Radius,ref RadiusSquared);
    }

    public override Boolean IsInsideRegion(float latitude,float longitude)
    {
      // THIS METHOD DOES NOT YET TAKE ACCOUNT OF WRAPPING OVER HEMISPHERE BOUNDARIES

      if (GeoRegion.IsInsideRegion(MinLatitude, MaxLatitude, MinLongitude, MaxLongitude, latitude, longitude) == false)
        return false;

      double Dy = System.Math.Abs(Latitude - latitude) * Constants.AngularToDistanceFactor;
      double Dx = System.Math.Abs(Longitude - longitude) * Constants.AngularToDistanceFactor;

      if (Dy * Dy + Dx * Dx > RadiusSquared)
        return false;

      return true;
    }
    public string RegionData
    {
      get
      {
        return GetRegionData(Latitude,Longitude,Radius);
      }
    }

    public float Latitude = 0;
    public float Longitude = 0;
    public Int32 Radius = 0;
    public Int32 RadiusSquared = 0;
  }
}
