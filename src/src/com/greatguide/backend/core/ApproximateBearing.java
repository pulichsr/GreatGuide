package com.greatguide.backend.core;

/**
 *
 *
 * Calculate bearing in degrees
 *
 * Original Author: Sandeep
 * Author: Lennie
 */
class ApproximateBearing
{
    private double _result = 0D;

	public ApproximateBearing(double aLatitudeOrigin, double aLongitudeOrigin, double aLatitudeDestination, double aLongitudeDestination)
	{
        Angle angle = new Angle();
	    double _latitudeOrigin = angle.DegreesToRadians(aLatitudeOrigin);
	    double _latitudeDestination = angle.DegreesToRadians(aLatitudeDestination);

	    double dLongitude = angle.DegreesToRadians(aLongitudeDestination - aLongitudeOrigin);

	    double y = Math.sin(dLongitude) * Math.cos(_latitudeDestination);
	    double x = Math.cos(_latitudeOrigin) * Math.sin(_latitudeDestination) - Math.sin(_latitudeOrigin) * Math.cos(_latitudeDestination) * Math.cos(dLongitude);

	    _result = Math.atan2(y, x);
	    _result = angle.RadiansToDegrees(_result);
	    _result = (_result + 360) % 360;
	}

    /**
     *
     * Get result in degrees
     *
     * @return
     */
	public double getResult()
    {
        return _result;
    }

    private class Angle
    {
        public double RadiansToDegrees(double angleInRadians)
        {
            return angleInRadians * 180 / Math.PI;
        }
        public double DegreesToRadians(double angleInDegrees)
        {
            return angleInDegrees * Math.PI / 180;
        }
    }
}
