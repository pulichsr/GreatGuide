using System;

namespace Nucleo.GoodGuide.Types
{
  public class Arc
  {
    public static Boolean SpansAngle(Int16 lowerLimit, Int16 upperLimit, Int16 angle)
    {
      if (lowerLimit < 0)
      {
        upperLimit += System.Math.Abs(lowerLimit);
        angle = (Int16)((angle + System.Math.Abs(lowerLimit)) % 360);
        lowerLimit += System.Math.Abs(lowerLimit);
      }
      else      
      {
        upperLimit -= System.Math.Abs(lowerLimit);
        angle -= System.Math.Abs(lowerLimit);
        lowerLimit -= System.Math.Abs(lowerLimit);
      }

      return (angle >= lowerLimit) && (angle <= upperLimit);
    }
  }
}
