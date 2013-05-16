using System;

namespace Nucleo.GoodGuide.Types
{
  public class GeoRegion
  {
    public static Boolean IsInsideRegion(
      double minLatitude,double maxLatitude,
      double minLongitude,double maxLongitude,
      double latitude, double longitude)
    {
      // Check bounding region. THIS DOES NOT YET TAKE ACCOUNT OF WRAPPING OVER HEMISPHERE BOUNDARIES
      if (latitude < minLatitude)
        return false;
      if (latitude > maxLatitude)
        return false;
      if (longitude < minLongitude)
        return false;
      if (longitude > maxLongitude)
        return false;

      return true;
    }
  }
}
