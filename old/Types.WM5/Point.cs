using System;
using System.Collections.Generic;

namespace Nucleo.GoodGuide.Types
{
  public class Point
  {
    public static string ToString(Point point)
    {
      return string.Format("{0},{1}", point.latitude, point.longitude);
    }
    public static void ToGridReference(float latitude,float longitude,out Int32 gridReferenceY,out Int32 gridReferenceX)
    {
      gridReferenceX = (Int32)(longitude * 100);
      gridReferenceY = (Int32)(latitude * 100);
    }

    public Point()
    {
    }
    public Point(float latitude, float longitude)
    {
      this.latitude = latitude;
      this.longitude = longitude;
    }
    public Point(string pointData)
    {
      FromString(pointData);
    }

    public float Latitude
    {
      get { return latitude; }
      set { latitude = value; }
    }
    public float Longitude
    {
      get { return longitude; }
      set { longitude = value; }
    }

    public new string ToString()
    {
      return ToString(this);
    }
    public void FromString(string pointData)
    {
      FromString(pointData,this);
    }
    public void FromString(string pointData, Point point)
    {
      string[] Fields = pointData.Split(',');
      if (Fields.Length != 2)
        throw new FormatException(string.Format("Invalid point data {0} in {1}", pointData, GetType().Name));

      try
      {
        point.latitude = Convert.ToSingle(Fields[0]);
        point.longitude = Convert.ToSingle(Fields[1]);
      }
      catch
      {
        throw new FormatException(string.Format("Could not parse point data {0} in {1}", pointData, GetType().Name));
      }
    }

    private float latitude = 0;
    private float longitude = 0;
  }

  public class Points : List<Point>
  {
    public static string ToString(Points points)
    {
      string Result = string.Empty;

      for (Int32 PointNo = 0; PointNo < points.Count; PointNo++)
      {
        if (PointNo > 0)
          Result += "|";

        Result += points[PointNo].ToString();       
      }

      return Result;
    }
    public static void FromString(string pointsData,Points points)
    {
      string[] Fields = pointsData.Split('|');
      if (Fields.Length == 0)
        return;

      for (Int32 PointNo = 0; PointNo < Fields.Length; PointNo++)
        points.Add(new Point(Fields[PointNo]));
    }

    public Points()
    {
    }
    public Points(string pointsData)
    {
      FromString(pointsData);
    }

    public new string ToString()
    {
      return ToString(this);
    }
    public void FromString(string pointsData)
    {
      Clear();
      FromString(pointsData,this);
    }
  }
}
