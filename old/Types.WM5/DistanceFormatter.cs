using System;

namespace Nucleo.GoodGuide.Types
{
  public static class DistanceFormatter
  {
    public const float MetersInMile = 1609.344f;

    public static string Format(Int32 distanceInMeter,Units units)
    {
      switch(units)
      {
        case Units.Imperial:
          return FormatImperial(distanceInMeter);
        case Units.Metric:
          return FormatMetric(distanceInMeter);
      }

      return FormatMetric(distanceInMeter);
    }
    public static string FormatMetric(Int32 distanceInMeter)
    {
      if (distanceInMeter < 1000)
        return string.Format("{0}m",((Int16)distanceInMeter));

      if (distanceInMeter < 10000)
        return string.Format("{0:f1}km", distanceInMeter / 1000);

      return string.Format("{0:f0}km", distanceInMeter / 1000);
    }
    public static string FormatImperial(Int32 distanceInMeter)
    {
      float DistanceInMile = distanceInMeter / MetersInMile;

      if (DistanceInMile < 100)
        return string.Format("{0:f1}mi", DistanceInMile);

      return string.Format("{0:f0}mi", DistanceInMile);
    }
  }
}
