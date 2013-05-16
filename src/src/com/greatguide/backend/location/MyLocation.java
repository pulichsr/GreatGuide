package com.greatguide.backend.location;

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
public class MyLocation extends Location implements IXMLLocation {

    private int _id = 0;
    private String _locationName = null;
    private Date _lastUpdateDate = null;

    public MyLocation()
    {
        super("mock");
    }

    public MyLocation(Location aLocation)
    {
        super(aLocation);
        this._lastUpdateDate = new Date();
    }

    public MyLocation(double aLatitude, double aLongitude) {
        super("");
        this.setLatitude(aLatitude);
        this.setLongitude(aLongitude);
    }

    public int getId() {
        return _id;
    }

    public void setId(int aId) {
        _id = aId;
    }

    public String getLocationName() {
        return _locationName != null ? _locationName : "Some location";
    }

    public void setLocationName(String _locationName) {
        this._locationName = _locationName;
    }

    /**
     *
     * Set the last date/time the coordinates was updated
     *
     * @param aDate
     */
    public void setLastUpdateDate(Date aDate)
    {
        this._lastUpdateDate = aDate;
    }

    /**
     *
     * Get the last date/time the coordinates was updated
     *
     * @return
     */
    public Date getLastUpdateDate()
    {
        return this._lastUpdateDate;
    }

    /**
     *
     * Get the last date/time the coordinate was updated for display purpose
     *
     * @return
     */
    public String getDisplayUpdateDate()
    {
        String result = null;
        try
        {
            // TODO: We might wanna read this date format from a settings file?
            DateFormat df = new SimpleDateFormat("dd/MM/yyyy hh:mm:ss");
            result = df.format(this._lastUpdateDate);
        }
        catch (Exception e) {
           // We don't care about errors here
        }
        return result;
    }

    @Override
    public String toString()
    {
        return "Lat: " + this.getLatitude() + " Long: " + this.getLongitude() ;
    }

    @Override
    public double getStartLatitude() {
        return 0D;
    }

    @Override
    public double getStartLongitude() {
        return 0D;
    }

    @Override
    public double getEndLatitude() {
        return 0D;
    }

    @Override
    public double getEndLongitude() {
        return 0D;
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

    /**
     *
     * Get the directions heading
     *
     * @return
     */
    public String getHeading()
    {
       return String.valueOf(super.getBearing());
    }

    public String getLatitudeForAudit()
    {
        return String.valueOf(this.getLatitude());
    }

    public String getLongitudeForAudit()
    {
        return String.valueOf(this.getLongitude());
    }

	public Location getLocation() {
		return (Location) this;
	}

	public float getHeadingValue() {
		return super.getBearing();
	}
}
