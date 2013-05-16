package com.greatguide.backend.maps.prepare.xml;

import java.util.*;

import android.app.Activity;

import android.content.Context;
import android.util.Log;
import com.greatguide.backend.core.ErrorManagerAuditTrail;
import com.greatguide.backend.location.IXMLLocation;
import com.greatguide.backend.location.LocationBox;
import com.greatguide.backend.location.MyLocation;

/**
 * @author sandeep pulichintala
 * 
 */
public class XMLLocationReader {

	// XML node LocationBox keys
	final String KEY_ITEM = "map";
	final String KEY_ID = "id";
	final String KEY_NAME = "name";
	final String KEY_START_LATITUDE = "startlat";
	final String KEY_START_LONGITUDE = "startlong";
	final String KEY_END_LATITUDE = "endlat";
	final String KEY_END_LONGITUDE = "endlong";

	// XML node MyLocation keys
	final String KEY_LATITUDE = "lat";
	final String KEY_LONGITUDE = "long";   

	private List<IXMLLocation> locationsList = new ArrayList<IXMLLocation>();
	private List<IXMLLocation> locationsBoxList = new ArrayList<IXMLLocation>();

	private ArrayList<HashMap<String, String>> xmlLocationList = null;

	public List<IXMLLocation> getLocationBOXCoordinates(Context aContext,
			String xmlLayout) {
		return this.retrieveLocationBoxCoordinates(aContext, xmlLayout);
	}

	private List<IXMLLocation> retrieveLocationBoxCoordinates(
            Context aContext, String xmlLayout) {

		try {
			xmlLocationList = LocationList.getXml(aContext, xmlLayout);
		} catch (Exception e) {
            ErrorManagerAuditTrail.getInstance().log(aContext, e);
		}

		for (HashMap<String, String> locationsList : xmlLocationList) {

			// assign the values from xml file
			int locationID = Integer.parseInt(locationsList.get(KEY_ID));

			Double startLatitude = Double.parseDouble(locationsList
					.get(KEY_START_LATITUDE));
			Double startLongitude = Double.parseDouble(locationsList
					.get(KEY_START_LONGITUDE));
			Double endLatitude = Double.parseDouble(locationsList
					.get(KEY_END_LATITUDE));
			Double endLongitude = Double.parseDouble(locationsList
					.get(KEY_END_LONGITUDE));

			LocationBox locationBox = new LocationBox();
			locationBox.setId(locationID);
			locationBox.setLocationName(locationsList.get(KEY_NAME));
			locationBox.setStartLatitude(startLatitude);
			locationBox.setStartLongitude(startLongitude);
			locationBox.setEndLatitude(endLatitude);
			locationBox.setEndLongitude(endLongitude);

			locationsBoxList.add(locationBox);
		}

		return locationsBoxList;
	}

	public List<IXMLLocation> getMyLocationCoordinates(Context aContext,
			String xmlLayout) {
		return this.retrieveMyLocationCoordinates(aContext, xmlLayout);
	}

	private List<IXMLLocation> retrieveMyLocationCoordinates(Context aContext,
			String xmlLayout) {

		try {
			xmlLocationList = LocationList.getXml(aContext, xmlLayout);
		} catch (Exception e) {
			Log.e(XMLLocationReader.class.getName(),
					"Exception in reading xml file" + xmlLocationList);

		}

		for (HashMap<String, String> myLocationsList : xmlLocationList) {
    
			// assign the values from xml file
			int locationID = Integer.parseInt(myLocationsList.get(KEY_ID));
			Double latitude = Double.parseDouble(myLocationsList
					.get(KEY_LATITUDE));
			Double longitude = Double.parseDouble(myLocationsList
					.get(KEY_LONGITUDE));

			MyLocation newLocation = new MyLocation();

			newLocation.setId(locationID);
			newLocation.setLocationName(myLocationsList.get(KEY_NAME));
			newLocation.setLatitude(latitude);
			newLocation.setLongitude(longitude);
			newLocation.setTime(System.currentTimeMillis());

			locationsList.add(newLocation);
		}

		return locationsList;
	}

}
