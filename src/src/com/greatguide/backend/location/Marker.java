package com.greatguide.backend.location;

import android.graphics.Bitmap;
import android.location.Location;
import org.osmdroid.util.GeoPoint;

/**
 * Author: Lennie De Villiers
 */
public class Marker {
    private Location _location = null;
    private Bitmap _image = null;
    private String _title = null;
    private String _description = null;

    public Marker(Location aMarkerLocation)
    {
        _location = aMarkerLocation;
    }

    public Marker(Location aMarkerLocation, String aTitle, String aDescription)
    {
        _location = aMarkerLocation;
        _title = aTitle;
        _description = aDescription;
    }

    public Marker(String aLatitude, String aLongitude)
    {
        _location = new Location("");
        _location.setLatitude(Double.parseDouble(aLatitude));
        _location.setLongitude(Double.parseDouble(aLongitude));
    }

    public Marker(String aLatitude, String aLongitude, String aTitle, String aDescription) {
        _location = new Location("");
        _location.setLatitude(Double.parseDouble(aLatitude));
        _location.setLongitude(Double.parseDouble(aLongitude));
        _title = aTitle;
        _description = aDescription;
    }


    public void setBitmap(Bitmap aImage) {
        _image = aImage;
    }

    public double getLongitude() {
        double result = 0;
        if (_location != null) {
            result = _location.getLongitude();
        }
        return result;
    }

    public double getLatitude() {
        double result = 0;
        if (_location != null) {
            result = _location.getLatitude();
        }
        return result;
    }

    public Bitmap getImage() {
        return _image;
    }

    public float getAccuracy() {
        float result = 0;
        if (_location != null) {
            result = _location.getAccuracy();
        }
        return result;
    }

    public GeoPoint getPoint() {
        return _location != null ? new GeoPoint(this.getLatitude(), this.getLongitude()) : null;
    }

    public String getTitle() {
        return _title;
    }

    public String getDescription() {
        return _description;
    }
}
