package com.greatguide.backend.location;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.content.ContentResolver;
import android.content.Context;

import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.provider.Settings;

/**
 * Original Author: sandeep pulichintala
 * Author: Lennie De Villiers
 * 24 Nov 2012
 */
public class MyLocationAction implements IMyLocationAction {

    // Only detect GPS location every 10 seconds or 10 meters
    private final long LOCATION_DETECT_DELAY_SECONDS = 30000;
    private final float LOCATION_DETECT_DELAY_METERS = 10f;

    private List<IMyLocationListener> _listeners = new ArrayList<IMyLocationListener>();
	private LocationManager _gpslocManager = null;
	private LocationListener _gpslocListener = null;
	private List<MyLocation> _locationsList = new ArrayList<MyLocation>();
    private Boolean _gpsEnabled = false;
    private Location _lastLocation = null;
    private boolean _stopped = false;

    public MyLocationAction(Context aContext, IMyLocationListener aListener) {
        _listeners.add(aListener);
        this.retrieveCoordinatesFromHardware(aContext);
    }

	public void getCoordinates(Context aContext) {
		this.retrieveCoordinatesFromHardware(aContext);
	}

    private void retrieveCoordinatesFromHardware(Context aContext) {

        _gpslocListener = new GPSLocationListener();
        String provider = null;

        if (_gpslocManager == null) {

            _gpslocManager = (LocationManager) aContext
                    .getSystemService(Context.LOCATION_SERVICE);
            Criteria criteriaFilter = new Criteria();
            criteriaFilter.setAccuracy(Criteria.ACCURACY_FINE);
            provider = _gpslocManager.getBestProvider(criteriaFilter, true);
            if (provider != null) {
                _lastLocation = _gpslocManager.getLastKnownLocation(provider);
                if (_lastLocation != null) {
                    for (int i = 0; i < _listeners.size(); i++) {
                        MyLocation loc = new MyLocation(_lastLocation);
                        _listeners.get(i).receiveMyLocation(loc, _locationsList, false, loc.getLastUpdateDate());
                    }
                }
            }
        }

        if (_gpslocManager != null && provider != null) {
            _gpslocManager.requestLocationUpdates(provider, LOCATION_DETECT_DELAY_SECONDS,
                    LOCATION_DETECT_DELAY_METERS, _gpslocListener);

            _gpsEnabled = _gpslocManager.isProviderEnabled(provider);
            if (_gpsEnabled == false) {
                if (_locationsList != null && _locationsList.size() > 0) {
                    failedToGetMyLocation(_locationsList.get(0), _locationsList);
                } else {
                    failedToGetMyLocation(null, null);
                }
            }
        } else {
            failedToGetMyLocation(null, null);
        }
    }

    private void failedToGetMyLocation(MyLocation aLastLocationFound, List<MyLocation> aLocationsList) {
        for (int i = 0; i < _listeners.size(); i++) {
            _listeners.get(i).failedToGetMyLocation(aLastLocationFound, aLocationsList);
        }
    }

    private class GPSLocationListener implements LocationListener {

            public void onLocationChanged(Location newLocation) {

                if (!_stopped) {
                    if (newLocation != null) {
                        MyLocation currentLocation = new MyLocation(newLocation);
                        _locationsList = UpdateGPSList.updateList(_locationsList, currentLocation);
                        for (int i = 0; i < _listeners.size(); i++) {
                            _listeners.get(i).receiveMyLocation(currentLocation, _locationsList, false, currentLocation.getLastUpdateDate());
                        }
                    } else {
                        if (_locationsList != null && _locationsList.size() > 0) {
                            for (int i = 0; i < _listeners.size(); i++) {
                                _listeners.get(i).receiveMyLocation(_locationsList.get(0), _locationsList, true, _locationsList.get(0).getLastUpdateDate());
                            }
                        }
                        else {
                            for (int i = 0; i < _listeners.size(); i++) {
                                _listeners.get(i).failedToGetMyLocation(null, null);
                            }
                        }
                    }
                }
            }

		public void onProviderDisabled(String provider) {

		}

		public void onProviderEnabled(String provider) {

		}

		public void onStatusChanged(String provider, int status, Bundle extras) {

		}
	}

	/**
	 * register the listener to receive the fired events
	 */
	public boolean registerListener(IMyLocationListener aListener) {
		return _listeners.add(aListener);
	}

    public boolean isGPSEnabled()
    {
        return _gpsEnabled;
    }

    @Override
    public void stop() {
        _stopped = true;
    }

    @Override
    public void resume() {
        _stopped = false;
        if (_lastLocation != null) {
            for (int i = 0; i < _listeners.size(); i++) {
                MyLocation loc = new MyLocation(_lastLocation);
                _listeners.get(i).receiveMyLocation(loc, _locationsList, false, loc.getLastUpdateDate());
            }
        }
    }

    /**
     * get the GreatGuide GPS Strength
     */
    public void getSignalStrength(Activity aActivity, IGPSStateListener aListener) {

        String provider = null;

        if (_gpslocManager == null) {

            _gpslocManager = (LocationManager) aActivity
                    .getSystemService(Context.LOCATION_SERVICE);
            Criteria criteriaFilter = new Criteria();
            criteriaFilter.setAccuracy(Criteria.ACCURACY_FINE);
            provider = _gpslocManager.getBestProvider(criteriaFilter, true);
            Location lastLocation = _gpslocManager.getLastKnownLocation(provider);

            if(lastLocation != null){
                if (aListener != null) {
                    MyLocation loc = new MyLocation(lastLocation);
                    SignalStrength signalStrength =loc.getSignalStrength();
                    aListener.receiveSignalStrength(signalStrength);
                }
            }
        }

    }

    /**
     * get the GreatGuide GPS ON/OFF
     *
     * @param aActivity
     * @param aListener
     *
     * @return
     */
    public void checkGPSEnabled(Activity aActivity, IGPSStateListener aListener) {

        ContentResolver contentResolver = aActivity.getBaseContext()
                .getContentResolver();
        boolean gpsEnabled = Settings.Secure.isLocationProviderEnabled(contentResolver,
                LocationManager.GPS_PROVIDER);

        if (aListener != null) {
            aListener.receiveGPSStatus(gpsEnabled);
        }
    }
}

