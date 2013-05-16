package com.greatguide.backend.location.mock;


import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

import android.app.Activity;

import android.content.Context;
import android.util.Log;
import com.greatguide.backend.core.ErrorManagerAuditTrail;
import com.greatguide.backend.location.*;

/**
 * @author sandeep pulichintala
 * 
 */
public class MockMyLocationAction implements IMyLocationAction {

    private final float DEFAULT_ACCURACY = 200f;

	// XML node keys
    private final String KEY_NAME = "locationname";
    private final String KEY_LATITUDE = "longitude";
	private final String KEY_LONGITUDE = "latitude";
	private final String KEY_LAST_MODIFIED_DATE = "lastmodifieddate";

	private List<IMyLocationListener> listeners = new ArrayList<IMyLocationListener>();
	private List<MyLocation> locationsList = new ArrayList<MyLocation>();
	private Date lastUpdatedDate = new Date();
	private ArrayList<HashMap<String, String>> xmlLocationList = null;

    public MockMyLocationAction(Context aContext, IMyLocationListener aListener)
    {
        listeners.add(aListener);
        this.retrieveMockCoordinates(aContext);
    }

    public void getCoordinates(Context aContext) {
		this.retrieveMockCoordinates(aContext);
	}

	private void retrieveMockCoordinates(Context aContext) {

        try {
            xmlLocationList = MockLocationList.getXml(aContext);
        } catch (Exception e) {
            ErrorManagerAuditTrail.getInstance().log(aContext, e);
        }

        for (HashMap<String, String> gpsLocationsList : xmlLocationList) {

            // assign the values from xml file
            Double latitude = Double.parseDouble(gpsLocationsList.get(KEY_LATITUDE));
            Double longitude = Double.parseDouble(gpsLocationsList.get(KEY_LONGITUDE));

            SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MM-yyyy hh:mm:ss",Locale.ENGLISH);
            try {
                lastUpdatedDate = dateFormat.parse(gpsLocationsList.get(KEY_LAST_MODIFIED_DATE));
            } catch (ParseException e) {
            }

            MyLocation newLocation = new MyLocation();

            newLocation.setLocationName(gpsLocationsList.get(KEY_NAME));
            newLocation.setLastUpdateDate(lastUpdatedDate);
            newLocation.setLatitude(latitude);
            newLocation.setLongitude(longitude);
            newLocation.setTime(System.currentTimeMillis());

            locationsList.add(newLocation);
        }

		if (locationsList.size() > 0) {
            for (int i = 0; i < listeners.size(); i++) {
				listeners.get(i).receiveMyLocation(locationsList.get(0), locationsList, false, locationsList.get(0).getLastUpdateDate());
			}   
		}else{
            for (int i = 0; i < listeners.size(); i++) {
                listeners.get(i).failedToGetMyLocation(null, null);
            }
        }
	}

	/**
	 * register the listener to receive the fired events
	 */
	public boolean registerListener(IMyLocationListener aListener) {
		return listeners.add(aListener);
	}

    @Override
    public boolean isGPSEnabled() {
        return true;
    }

    @Override
    public void stop() {
        // We do nothing here since its only mocking
    }

    @Override
    public void resume() {
        // We do nothing here since its only mocking
    }

    @Override
    public void getSignalStrength(Activity aActivity, IGPSStateListener aListener) {
        if (aListener != null) {
            aListener.receiveSignalStrength(new SignalStrength(DEFAULT_ACCURACY));
        }
    }

    @Override
    public void checkGPSEnabled(Activity aActivity, IGPSStateListener aListener) {
        if (aListener != null)  {
            aListener.receiveGPSStatus(true);
        }
    }
}
