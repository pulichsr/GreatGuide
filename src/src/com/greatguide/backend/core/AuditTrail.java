package com.greatguide.backend.core;

import java.io.FileWriter;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Locale;

import android.app.Activity;

import com.greatguide.backend.device.StorageManager;

public class AuditTrail {

    private static AuditTrail _auditTrail = null;

    public static synchronized AuditTrail getInstance() {
        if (_auditTrail == null) {
            _auditTrail = new AuditTrail();
        }

        return _auditTrail;
    }

    private AuditTrail() {
    }


    /**
     *
     * Log audit trail
     *
     * @param aActivity
     * @param aString
     * @return
     */
	public ActionResult log(Activity aActivity, String aString)  {

        ActionResult result = new ActionResult();


        try{
            ActionResult auditLocation = StorageManager.getInstance().getSDCardPath(aActivity, "audits");
            if (!auditLocation.isSuccessful())
                auditLocation = StorageManager.getInstance().getSDCardPath(aActivity, "");

            if (!auditLocation.isSuccessful())
                throw new Exception(auditLocation.getExceptionDetail());

		    FileWriter writer = new FileWriter(auditLocation.getValue() + "audit_"+ getDate("dd_MM_yyyy") + ".csv", true);


            writer.append(aString);
	    	writer.append(',');
		    writer.append(getDate("dd/MM/yyyy"));
		    writer.append(',');
		    writer.append(getTime("HH:mm:ss"));
		    writer.append('\n');

		    writer.flush();
		    writer.close();

        }catch(Exception e){
        	result = new ActionResult(e);
        	ErrorManagerAuditTrail.getInstance().log(aActivity, e);
       }    

		return result;
	}

    private String getDate(String aDateFormat)
    {
        Calendar cal = Calendar.getInstance();
        SimpleDateFormat dateFormatter = new SimpleDateFormat(aDateFormat, Locale.ENGLISH);
        return dateFormatter.format(cal.getTime());
    }

    private String getTime(String aTimeFormat)
    {
        Calendar cal = Calendar.getInstance();
        SimpleDateFormat timeFormatter = new SimpleDateFormat(aTimeFormat,Locale.ENGLISH);
        return timeFormatter.format(cal.getTime()) ;
    }
}
