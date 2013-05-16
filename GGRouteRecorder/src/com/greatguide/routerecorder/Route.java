package com.greatguide.routerecorder;

import java.io.*;

import android.content.Context;
import android.location.Location;
import android.os.*;
import android.util.Log;

public class Route {
	
	private File _XmloutFile;
	private FileOutputStream _fOut;
	private OutputStreamWriter _writer;
	private StringBuilder _master = new StringBuilder();
	private int id = 1;
	
	public Route(Context aContext){
		
		try {
            ActionResult storageLocation = StorageManager.getInstance().getSDCardPath(aContext, "");
            if (!storageLocation.isSuccessful())
                throw new Exception(storageLocation.getExceptionDetail());

			_XmloutFile = new File(storageLocation.getValue() + "playback.xml");
			_XmloutFile.createNewFile();
			
			_fOut = new FileOutputStream(_XmloutFile);
			_writer = new OutputStreamWriter(_fOut);
			
			_master.append("<route>\n");
            _master.append("<detail>\n");
            _master.append("<delay>10</delay>\n");
            _master.append("</detail>\n");

		
		} catch (Exception e){
			Log.e(Route.class.getName(), e.getMessage());
        }
	}
	
	public ActionResult addRecord(MyLocation aLocation){
        ActionResult result = new ActionResult();
		try{
			StringBuilder elements = new StringBuilder();

            elements.append("<record>\n");

            elements.append("<id>" + id + "</id>\n");
            elements.append("<latitude>").append(aLocation.getLatitude()).append("</latitude>\n");
            elements.append("<longitude>").append(aLocation.getLongitude()).append("</longitude>\n");
            elements.append("<heading>").append(aLocation.getBearing()).append("</heading>\n");
            elements.append("<quality>").append(aLocation.getSignalStrength().getQuality()).append("</quality>\n");
            elements.append("<speed>").append(aLocation.getSpeed()).append("</speed>\n");
            elements.append("<satellite>").append(aLocation.getNumberOfSatellite()).append("</satellite>\n");

            elements.append("</record>\n");

			_master.append(elements.toString());
			
			id++;
			
					
		}catch (Exception e){
	        result = new ActionResult(e);
		}

        return result;
	}

	public ActionResult saveRoute(){

        ActionResult result = new ActionResult();

		try{
            if (_master != null) {
			    _master.append("</route>");
			    _writer.write(_master.toString());
                _master = null;
			    _writer.flush();
			    _fOut.close();
			    _writer.close();
            }
		}catch (Exception e){
			 result = new ActionResult(e);
		}
        return result;
	}

    public ActionResult reset() {
        ActionResult result = new ActionResult();
        if (_XmloutFile != null && _XmloutFile.exists()) {
            result = new ActionResult(_XmloutFile.delete(), "Unable to delete route");
        }
        return result;
    }
}
