using System;

namespace Nucleo.GoodGuide.Types
{
  public class Constants
  {
    public const float Pi = (float)System.Math.PI;
    public const float EarthRadius = 6356750; // meters

    public const float AngularToDistanceFactor = (float)(Pi / 180.0 * EarthRadius);
    public const float DistanceToAngularFactor = (float)(180.0 / Pi / EarthRadius);
  }
}
