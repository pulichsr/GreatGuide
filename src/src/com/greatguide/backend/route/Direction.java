package com.greatguide.backend.route;

public class Direction {
	public enum Directions
	{
		North,
		NorthEast,
		East,
		SouthEast,
		South,
		SouthWest,
		West,
		NorthWest
	}

	public static String ToShortString(int aDirection)
	{
		return ToShortString(aDirection);
	}

	public static String ToShortString(Directions direction)
	{
		switch (direction)
		{
		case North:
			return "N";
		case NorthEast:
			return "NE";
		case East:
			return "E";
		case SouthEast:
			return "SE";
		case South:
			return "S";
		case SouthWest:
			return "SW";
		case West:
			return "W";
		case NorthWest:
			return "NW";
		default:
			return "";
		}
	}

	public static Directions IntToDirection(int aDirection)
	{
		return Directions.values()[aDirection];
	}
	
	public static Directions FromHeading(float aHeading)
	{
		aHeading = (short)(aHeading % 360);
		if ((aHeading >= 22.5) && (aHeading < 67.5))
			return Directions.NorthEast;
		if ((aHeading >= 67.5) && (aHeading < 112.5))
			return Directions.East;
		if ((aHeading >= 112.5) && (aHeading < 157.5))
			return Directions.SouthEast;
		if ((aHeading >= 157.5) && (aHeading < 202.5))
			return Directions.South;
		if ((aHeading >= 202.5) && (aHeading < 247.5))
			return Directions.SouthWest;
		if ((aHeading >= 247.5) && (aHeading < 292.5))
			return Directions.West;
		if ((aHeading >= 292.5) && (aHeading < 337.5))
			return Directions.NorthWest;

		return Directions.North;
	}
}
