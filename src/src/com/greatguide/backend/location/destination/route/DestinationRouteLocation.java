package com.greatguide.backend.location.destination.route;

import java.util.*;

import android.app.Activity;

import android.util.Log;
import com.greatguide.backend.core.ErrorManagerAuditTrail;
import com.greatguide.backend.location.MyLocation;

/**
 * @author sandeep pulichintala
 * 
 */
public class DestinationRouteLocation {

	// XML node MyLocation keys       
	final String KEY_ITEM = "route";
	final String KEY_LATITUDE = "latitude";   
	final String KEY_LONGITUDE = "longitude";     
	

	private List<MyLocation> locationsList = new ArrayList<MyLocation>();
	private ArrayList<HashMap<String, String>> xmlLocationList = null;
	
	public List<MyLocation> getMyLocationCoordinates(Activity aActivity,
			String xmlLayout) {
		return this.retrieveMyLocationCoordinates(aActivity, xmlLayout);
	}

	private List<MyLocation> retrieveMyLocationCoordinates(Activity aActivity,
			String xmlLayout) {

		try {
			xmlLocationList = LocationList.getXml(aActivity, xmlLayout);
		} catch (Exception e) {
            ErrorManagerAuditTrail.getInstance().log(aActivity, e);
		}

		for (HashMap<String, String> myLocationsList : xmlLocationList) {
    
			// assign the values from xml file
			Double latitude = Double.parseDouble(myLocationsList
					.get(KEY_LATITUDE));
			Double longitude = Double.parseDouble(myLocationsList
					.get(KEY_LONGITUDE));
    
			MyLocation newLocation = new MyLocation();

			newLocation.setLatitude(latitude);
			newLocation.setLongitude(longitude);
			newLocation.setTime(System.currentTimeMillis());

			locationsList.add(newLocation);
		}

		return locationsList;
	}

}
