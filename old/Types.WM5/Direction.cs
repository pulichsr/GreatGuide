using System;

namespace Nucleo.GoodGuide.Types
{
  public class Direction
  {
    public enum Directions
    {
      North = 0,
      NorthEast,
      East,
      SouthEast,
      South,
      SouthWest,
      West,
      NorthWest
    }

    public static string ToShortString(Int16 direction)
    {
      return ToShortString((Directions)direction);
    }
    public static string ToShortString(Directions direction)
    {
      switch (direction)
      {
        case Directions.North:
          return "N";
        case Directions.NorthEast:
          return "NE";
        case Directions.East:
          return "E";
        case Directions.SouthEast:
          return "SE";
        case Directions.South:
          return "S";
        case Directions.SouthWest:
          return "SW";
        case Directions.West:
          return "W";
        case Directions.NorthWest:
          return "NW";
        default:
          return "";
      }
    }
    public static Directions IntToDirection(Int16 direction)
    {
      return (Directions)direction;
    }
    public static Int16 DirectionToInt(Directions direction)
    {
      return (Int16)direction;
    }

    public static Directions FromHeading(Int16 heading)
    {
      heading = (short)(heading % 360);
      if ((heading >= 22.5) && (heading < 67.5))
        return Directions.NorthEast;
      if ((heading >= 67.5) && (heading < 112.5))
        return Directions.East;
      if ((heading >= 112.5) && (heading < 157.5))
        return Directions.SouthEast;
      if ((heading >= 157.5) && (heading < 202.5))
        return Directions.South;
      if ((heading >= 202.5) && (heading < 247.5))
        return Directions.SouthWest;
      if ((heading >= 247.5) && (heading < 292.5))
        return Directions.West;
      if ((heading >= 292.5) && (heading < 337.5))
        return Directions.NorthWest;

      return Directions.North;
    }
  }
}
