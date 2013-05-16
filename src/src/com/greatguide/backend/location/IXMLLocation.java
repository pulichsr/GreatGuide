package com.greatguide.backend.location;

/**
 */
public interface IXMLLocation {

    public int getId();
    public String getLocationName();

    public double getStartLatitude();
    public double getStartLongitude();
    public double getEndLatitude();
    public double getEndLongitude();

    public double getLatitude();
    public double getLongitude();
}
