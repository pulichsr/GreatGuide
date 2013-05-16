package com.greatguide.backend.maps.prepare.xml;

import java.util.ArrayList;
import java.util.HashMap;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

import android.content.Context;

class LocationList {

	public static ArrayList<HashMap<String, String>> getXml(Context aContext,
			String xmlLayoutFile) throws Exception {

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

		ArrayList<HashMap<String, String>> locationList = new ArrayList<HashMap<String, String>>();

		Document dom = LocationListXMLParser.getDomElement(aContext, xmlLayoutFile);
		NodeList nl = dom.getElementsByTagName(KEY_ITEM);

		// looping through all item nodes <item>
		for (int i = 0; i < nl.getLength(); i++) {

			// creating new HashMap
			HashMap<String, String> map = new HashMap<String, String>();
			Element e = (Element) nl.item(i);
			              
			if (xmlLayoutFile.indexOf("GPSMyLocation.xml") > -1) {
				// adding each child node to HashMap key => value
				map.put(KEY_ID, LocationListXMLParser.getValue(e, KEY_ID));
				map.put(KEY_NAME, LocationListXMLParser.getValue(e, KEY_NAME));
				map.put(KEY_LATITUDE,
						LocationListXMLParser.getValue(e, KEY_LATITUDE));
				map.put(KEY_LONGITUDE,
						LocationListXMLParser.getValue(e, KEY_LONGITUDE));
			} else if(xmlLayoutFile.indexOf("GPSLocationBox.xml") > -1){
				// adding each child node to HashMap key => value
				map.put(KEY_ID, LocationListXMLParser.getValue(e, KEY_ID));
				map.put(KEY_NAME, LocationListXMLParser.getValue(e, KEY_NAME));
				map.put(KEY_START_LATITUDE,
						LocationListXMLParser.getValue(e, KEY_START_LATITUDE));
				map.put(KEY_START_LONGITUDE,
						LocationListXMLParser.getValue(e, KEY_START_LONGITUDE));
				map.put(KEY_END_LATITUDE,
						LocationListXMLParser.getValue(e, KEY_END_LATITUDE));
				map.put(KEY_END_LONGITUDE,
						LocationListXMLParser.getValue(e, KEY_END_LONGITUDE));
			} else{
				//no xml find out
			}

			// adding HashList to ArrayList
			locationList.add(map);
		}

		return locationList;
	}
           
}
