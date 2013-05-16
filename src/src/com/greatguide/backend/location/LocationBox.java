package com.greatguide.backend.location;

/**
 * Author: Lennie De Villiers
 */
public class LocationBox implements IXMLLocation {

    private int id = 0;
    private String _locationName = null;

    private double _startLatitude = 0D;
    private double _startLongitude = 0D;

    private double _endLatitude = 0D;
    private double _endLongitude = 0D;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getLocationName() {
        return _locationName;
    }

    public void setLocationName(String locationName) {
        this._locationName = locationName;
    }

    public double getStartLatitude() {
        return _startLatitude;
    }

    public void setStartLatitude(double startLatitude) {
        this._startLatitude = startLatitude;
    }

    public double getStartLongitude() {
        return _startLongitude;
    }

    public void setStartLongitude(double startLongitude) {
        this._startLongitude = startLongitude;
    }

    public double getEndLatitude() {
        return _endLatitude;
    }

    public void setEndLatitude(double endLatitude) {
        this._endLatitude = endLatitude;
    }

    public double getEndLongitude() {
        return _endLongitude;
    }

    @Override
    public double getLatitude() {
        return 0;
    }

    @Override
    public double getLongitude() {
        return 0;
    }

    public void setEndLongitude(double endLongitude) {
        this._endLongitude = endLongitude;
    }
}
