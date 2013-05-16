package com.greatguide.backend.core;

import java.io.File;
import java.io.FileWriter;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Locale;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.util.Log;

import com.greatguide.backend.device.StorageManager;

public class ErrorManagerAuditTrail {

	private static ErrorManagerAuditTrail _auditTrail = null;

	public static synchronized ErrorManagerAuditTrail getInstance() {
		if (_auditTrail == null) {
			_auditTrail = new ErrorManagerAuditTrail();
		}

		return _auditTrail;
	}

	private ErrorManagerAuditTrail() {
	}

	/**
	 * 
	 * Log errors
	 * 
	 * @param aContext
	 * @param aException
	 * @return
	 */
	public ActionResult log(Context aContext, Exception aException) {

		ActionResult result = new ActionResult();   
        Log.e(aContext.getClass().getName(), aException.getMessage());
        aException.printStackTrace();

		try {
			ActionResult auditLocation = StorageManager.getInstance()
					.getSDCardPath(aContext, "errors");
			if (!auditLocation.isSuccessful())
				auditLocation = StorageManager.getInstance().getSDCardPath(
						aContext, "");

			if (!auditLocation.isSuccessful())
				throw new Exception(auditLocation.getExceptionDetail());

			File storageManagerDirectory = new File(auditLocation.getValue()
					+ "error_audit_" + getDate("dd_MM_yyyy") + ".csv");

            // create the header for csv file
             
			if (!storageManagerDirectory.exists()) {
				
				FileWriter writer = new FileWriter(auditLocation.getValue()
						+ "error_audit_" + getDate("dd_MM_yyyy") + ".csv", true);
				
				writer.write("DATE");
				writer.write(',');
				writer.write("TIME");
				writer.write(',');
				writer.write("APPLICATION VERSION");
				writer.write(',');
				writer.write("ANDROID VERSION NUMBER");
				writer.write(',');
				writer.write("ERROR MESSAGE");    
				writer.write(',');   
				writer.write("FULL STACK TRACE");     
				writer.write('\n');
				writer.flush();  
				writer.close();        
				  
			}
			
			//append the data to the csv file
			FileWriter writer1 = new FileWriter(auditLocation.getValue()
					+ "error_audit_" + getDate("dd_MM_yyyy") + ".csv", true);  
			
			StringWriter sw = new StringWriter();
			aException.printStackTrace(new PrintWriter(sw));
			String exceptionAsString = sw.toString();
                         
			writer1.append(getDate("dd/MM/yyyy"));
			writer1.append(',');    
			writer1.append(getTime("HH:mm:ss"));    
			writer1.append(',');
			writer1.append(String.valueOf(getAppVersionNumber(aContext)));
			writer1.append(',');
			writer1.append(String.valueOf(getAndroidVersionNumber()));       
			writer1.append(',');
			writer1.append(aException.getMessage());
			writer1.append(',');          
			   
			writer1.append(exceptionAsString);     
			writer1.append('\n');   

			writer1.flush();
			writer1.close();    
			

		} catch (Exception e) {
			result = new ActionResult(e);
			Log.e(ErrorManagerAuditTrail.class.getName(),
					"Failed to write CSV File:  " + e);

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
	
	/**
	 * get the android version number
	 * @return
	 */
	private int getAndroidVersionNumber()
    {
        try   
        {
        	return android.os.Build.VERSION.SDK_INT;
        }
        catch (Exception e)
        {
        	 return 0;     
        }
    }
    
	/**
	 * get the Android Application Version Number
	 * @param aContext
	 * @return
	 */
	private String getAppVersionNumber(Context aContext)
    {
        try   
        {
            PackageInfo packageInfo = aContext.getPackageManager().getPackageInfo(aContext.getPackageName(),0);
        	return packageInfo.versionName;

        }
        catch (Exception e)
        {
            return "-";
        }
    }
}
