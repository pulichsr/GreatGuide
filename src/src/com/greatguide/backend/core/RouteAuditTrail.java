package com.greatguide.backend.core;

import java.io.File;
import java.io.FileWriter;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Locale;

import android.app.Activity;

import com.greatguide.backend.device.StorageManager;
import com.greatguide.backend.location.MyLocation;

public class RouteAuditTrail {

	private static RouteAuditTrail _auditTrail = null;

	public static synchronized RouteAuditTrail getInstance() {
		if (_auditTrail == null) {
			_auditTrail = new RouteAuditTrail();
		}

		return _auditTrail;
	}

	private RouteAuditTrail() {
	}

	/**
	 * 
	 * Log audit trail
	 * 
	 * @param aActivity
	 * @param aString
	 * @return
	 */
	public ActionResult log(Activity aActivity, MyLocation aMyLocation, String aString) {

		ActionResult result = new ActionResult();

		try {
			ActionResult auditLocation = StorageManager.getInstance()
					.getSDCardPath(aActivity, "audits");
			if (!auditLocation.isSuccessful())
				auditLocation = StorageManager.getInstance().getSDCardPath(
						aActivity, "");

			if (!auditLocation.isSuccessful())
				throw new Exception(auditLocation.getExceptionDetail());

			File storageManagerDirectory = new File(auditLocation.getValue()
					+ "routes_audit_" + getDate("dd_MM_yyyy") + ".csv");

			if (!storageManagerDirectory.exists()) {
				
				FileWriter writer = new FileWriter(auditLocation.getValue()
						+ "routes_audit_" + getDate("dd_MM_yyyy") + ".csv", true);
				
				writer.write("NAME");
				writer.write(',');
				writer.write("HEADING");
				writer.write(',');
				writer.write("LATITUDE");
				writer.write(',');
				writer.write("LONGITUDE");
				writer.write(',');
				writer.write("DATE");
				writer.write(',');   
				writer.write("TIME");
				writer.write('\n');
				writer.flush();  
				writer.close();        
				  
			}

			FileWriter writer1 = new FileWriter(auditLocation.getValue()
					+ "routes_audit_" + getDate("dd_MM_yyyy") + ".csv", true);    

			writer1.append(aString);
			writer1.append(',');
			writer1.append(aMyLocation.getHeading());
			writer1.append(',');
			writer1.append(aMyLocation.getLatitudeForAudit());
			writer1.append(',');
			writer1.append(aMyLocation.getLongitudeForAudit());
			writer1.append(',');
			writer1.append(getDate("dd/MM/yyyy"));
			writer1.append(',');
			writer1.append(getTime("HH:mm:ss"));
			writer1.append('\n');

			
			writer1.flush();
			writer1.close();    
			

		} catch (Exception e) {
			result = new ActionResult(e);
			ErrorManagerAuditTrail.getInstance().log(aActivity, e);
		}

		return result;
	}

	private String getDate(String aDateFormat) {
		Calendar cal = Calendar.getInstance();
		SimpleDateFormat dateFormatter = new SimpleDateFormat(aDateFormat,
				Locale.ENGLISH);
		return dateFormatter.format(cal.getTime());
	}

	private String getTime(String aTimeFormat) {
		Calendar cal = Calendar.getInstance();
		SimpleDateFormat timeFormatter = new SimpleDateFormat(aTimeFormat,
				Locale.ENGLISH);
		return timeFormatter.format(cal.getTime());
	}

}
