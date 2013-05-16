package com.greatguide.backend.location.playback;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import android.util.Log;

/**
 * @author sandeep pulichintala
 * 
 */
public class PlayRouteLocationAction {

	// XML node MyLocation keys
	final String KEY_ITEM = "record";
	final String KEY_ITEM1 = "detail";
	final String KEY_ID = "id";
	final String KEY_LATITUDE = "latitude";   
	final String KEY_LONGITUDE = "longitude";
	final String KEY_HEADING = "heading";
	final String KEY_QUALITY = "quality";
	final String KEY_SPEED = "speed";
	final String KEY_DELAY = "delay";
    final String KEY_SATILITE = "satellite";

	private List<PlayRouteLocation> _locationsList = new ArrayList<PlayRouteLocation>();
	private ArrayList<HashMap<String, String>> _xmlLocationList = null;

	public PlayRoute getPlayingRoute(String xmlLayout) {
		return this.getRoute(xmlLayout);
	}

	private PlayRoute getRoute(String xmlLayout) {

		try {
			_xmlLocationList = LocationList.getXml(xmlLayout);
		} catch (Exception e) {
			Log.e(PlayRouteLocationAction.class.getName(),
					"Exception in reading xml file" + _xmlLocationList);

		}

        int delay = 0;
		for (HashMap<String, String> myLocationsList : _xmlLocationList) {

			// assign the values from xml file
			Double latitude = Double.parseDouble(myLocationsList
					.get(KEY_LATITUDE));
			Double longitude = Double.parseDouble(myLocationsList   
					.get(KEY_LONGITUDE));      
			int id = Integer.parseInt(myLocationsList.get(KEY_ID));   
			String heading = myLocationsList.get(KEY_HEADING);
			String quality = myLocationsList.get(KEY_QUALITY);   
			float speed = Float.parseFloat(myLocationsList.get(KEY_SPEED));
			int numOfSatellites = Integer.parseInt(myLocationsList.get(KEY_SATILITE));
			delay = Integer.parseInt(myLocationsList.get(KEY_DELAY));

			PlayRouteLocation newLocation = new PlayRouteLocation(id, latitude, longitude, heading, quality, speed, numOfSatellites);
			_locationsList.add(newLocation);
		}

        PlayRoute playRoute = new PlayRoute(_locationsList, delay);
		return playRoute;
	}
}