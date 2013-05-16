package com.greatguide.backend.location.playback;

import android.location.Location;

/**
 *
 * Author: Lennie De Villiers
 * Created Date: 24 Nov 2012
 *
 */
public class PlayRouteLocation extends Location {

    private int _id = 0;
    private String _quality = null;
    private int _numOfSatellites = 0;

    public PlayRouteLocation(int aId, double aLatitude, double aLongitude, String aHeading, String aQuality, float aSpeed, int aNumOfSatellites)
    {
        super("this");
        _id = aId;
        this.setLatitude(aLatitude);
        this.setLongitude(aLongitude);
        this.setSpeed(aSpeed);
        this.setBearing(Float.parseFloat(aHeading));
        _quality = aQuality;
        _numOfSatellites = aNumOfSatellites;
    }

	public String getQuality() {
		return _quality;
	}

	/**
	 * 
	 * @param quality
	 */
	public void setQuality(String quality) {
		this._quality = quality;
	}

	public int getNumOfSatellites() {
		return _numOfSatellites;
	}

	public void setNumOfSatellites(int numOfSatellites) {
		this._numOfSatellites = numOfSatellites;
	}

    public int getId() {
        return _id;
    }

    public void setId(int aId) {
        _id = aId;
    }

    @Override
    public String toString()
    {
        return "Lat: " + this.getLatitude() + " Long: " + this.getLongitude() ;
    }
}