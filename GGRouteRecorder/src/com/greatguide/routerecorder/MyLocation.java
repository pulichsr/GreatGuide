package com.greatguide.routerecorder;

import android.location.Location;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 *
 * Author: Lennie De Villiers
 * Created Date: 24 Nov 2012
 *
 */
public class MyLocation extends Location {

    private int _numberOfsatellite = 0;

    public MyLocation()
    {
        super("mock");
    }

    public MyLocation(Location aLocation, int aNumberOfSatellite)
    {
        super(aLocation);
        _numberOfsatellite = aNumberOfSatellite;
    }

    public int getNumberOfSatellite() {
        return _numberOfsatellite;
    }

    @Override
    public String toString()
    {
        return "Lat: " + this.getLatitude() + " Long: " + this.getLongitude() ;
    }

    /**
     *
     * Get the signal strength
     *
     * @return
     */
    public SignalStrength getSignalStrength() {

        SignalStrength result = new SignalStrength(this.getAccuracy());
        return result;
    }
}
