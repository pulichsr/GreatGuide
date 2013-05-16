package com.greatguide.backend.location.playback;

import java.util.ArrayList;
import java.util.HashMap;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

import android.content.Context;

public class LocationList {

	public static ArrayList<HashMap<String, String>> getXml(String xmlLayout) throws Exception {

		// XML node PlayRouteLocation keys
		final String KEY_ITEM1 = "detail";  
		final String KEY_ITEM = "record";
		final String KEY_ID = "id";
		final String KEY_LATITUDE = "latitude";
		final String KEY_LONGITUDE = "longitude";
		final String KEY_HEADING = "heading";
		final String KEY_QUALITY = "quality";
		final String KEY_SPEED = "speed";
		final String KEY_SATILITE = "satellite";
		final String KEY_DELAY = "delay";

		ArrayList<HashMap<String, String>> playRouteList = new ArrayList<HashMap<String, String>>();

		Document dom = LocationListXMLParser.getDomElement(xmlLayout);
		NodeList nl = dom.getElementsByTagName(KEY_ITEM);
		NodeList n2 = dom.getElementsByTagName(KEY_ITEM1); 
		Element e1 = (Element) n2.item(0);     
		
		// looping through all item nodes <item>
		for (int i = 0; i < nl.getLength(); i++) {

			// creating new HashMap
			HashMap<String, String> map = new HashMap<String, String>();
			Element e = (Element) nl.item(i);

			// adding each child node to HashMap key => value
   
			map.put(KEY_ID, LocationListXMLParser.getValue(e, KEY_ID));
			map.put(KEY_HEADING, LocationListXMLParser.getValue(e, KEY_HEADING));
			map.put(KEY_QUALITY, LocationListXMLParser.getValue(e, KEY_QUALITY));
			map.put(KEY_SPEED, LocationListXMLParser.getValue(e, KEY_SPEED));
			map.put(KEY_SATILITE,
					LocationListXMLParser.getValue(e, KEY_SATILITE));
			map.put(KEY_LATITUDE,
					LocationListXMLParser.getValue(e, KEY_LATITUDE));
			map.put(KEY_LONGITUDE,
					LocationListXMLParser.getValue(e, KEY_LONGITUDE));    
			map.put(KEY_DELAY,
					LocationListXMLParser.getValue(e1, KEY_DELAY));

			// adding HashList to ArrayList
			playRouteList.add(map);    
		}

		return playRouteList;
	}

	

}
