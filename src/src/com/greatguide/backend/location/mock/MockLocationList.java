package com.greatguide.backend.location.mock;

import java.util.ArrayList;
import java.util.HashMap;

import android.content.Context;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

class MockLocationList {

	
	public static ArrayList<HashMap<String, String>> getXml(Context aContext) throws Exception{
			             
			// XML node keys
			final String KEY_ITEM = "location"; // parent node
			final String KEY_NAME = "locationname";   
			final String KEY_ID = "locationid";   
			final String KEY_LATITUDE = "longitude";    
			final String KEY_LONGITUDE = "latitude";   
			final String KEY_LAST_MODIFIED_DATE = "lastmodifieddate";
			
			ArrayList<HashMap<String, String>> locationList = new ArrayList<HashMap<String, String>>();
			
	        Document dom= LocationListXMLParser.getDomElement(aContext);
	        NodeList nl = dom.getElementsByTagName(KEY_ITEM);
	        
	        // looping through all item nodes <item>
	        for (int i = 0; i < nl.getLength(); i++) {
	        	
	            // creating new HashMap
	            HashMap<String, String> map = new HashMap<String, String>();
	            Element e = (Element) nl.item(i);
	               
	            // adding each child node to HashMap key => value
	           map.put(KEY_ID, LocationListXMLParser.getValue(e, KEY_ID));
	           map.put(KEY_NAME, LocationListXMLParser.getValue(e, KEY_NAME));
	           map.put(KEY_LATITUDE,LocationListXMLParser.getValue(e, KEY_LATITUDE));
	           map.put(KEY_LONGITUDE, LocationListXMLParser.getValue(e, KEY_LONGITUDE));
	           map.put(KEY_LAST_MODIFIED_DATE, LocationListXMLParser.getValue(e, KEY_LAST_MODIFIED_DATE));
	           
	            // adding HashList to ArrayList   
	           locationList.add(map);
	        }  
	        
			return locationList;
		}

	}

