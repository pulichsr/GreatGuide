
package com.greatguide.backend.maps;

public class MapPoint
{
	public double x, y;

	public MapPoint(double x, double y)
	{
		this.x = x;
		this.y = y;
	}

	public MapPoint()
	{
		this(0, 0);
	}

	@Override
	public String toString()
	{
		return "(" + Double.toString(x) + "," + Double.toString(y) + ")";
	}
}