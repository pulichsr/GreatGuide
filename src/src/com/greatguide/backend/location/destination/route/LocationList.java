package com.greatguide.backend.location.destination.route;

import java.util.ArrayList;
import java.util.HashMap;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

import android.content.Context;

class LocationList {

	public static ArrayList<HashMap<String, String>> getXml(Context aContext,
			String xmlLayout) throws Exception {

			// XML node MyLocation keys   
		final String KEY_ITEM = "route";
		final String KEY_LATITUDE = "latitude";
		final String KEY_LONGITUDE = "longitude";     
   
		ArrayList<HashMap<String, String>> locationList = new ArrayList<HashMap<String, String>>();

		Document dom = LocationListXMLParser.getDomElement(aContext, xmlLayout);
		NodeList nl = dom.getElementsByTagName(KEY_ITEM);

		// looping through all item nodes <item>
		for (int i = 0; i < nl.getLength(); i++) {

			// creating new HashMap
			HashMap<String, String> map = new HashMap<String, String>();
			Element e = (Element) nl.item(i);
			              
				// adding each child node to HashMap key => value
			
				map.put(KEY_LATITUDE,
						LocationListXMLParser.getValue(e, KEY_LATITUDE));
				map.put(KEY_LONGITUDE,
						LocationListXMLParser.getValue(e, KEY_LONGITUDE));
		
			// adding HashList to ArrayList
			locationList.add(map);
		}
    
		return locationList;
	}
           
}
