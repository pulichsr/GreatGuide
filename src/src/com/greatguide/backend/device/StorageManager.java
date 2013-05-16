package com.greatguide.backend.device;

import java.io.File;

import android.content.Context;
import android.content.res.Resources;
import android.os.Environment;
import android.util.Log;

import com.greatguide.R;
import com.greatguide.backend.core.ActionResult;

/**
 * Original Author: Sandeep
 * 
 */
public class StorageManager {

    private static StorageManager _storageManager = null;

    public static synchronized StorageManager getInstance() {
        if (_storageManager == null) {
            _storageManager = new StorageManager();
        }

        return _storageManager;
    }

    private StorageManager() {
    }

    /**
     *
     *  get SD card path
     *
     * @param aContext
     * @param aPath
     * @return
     */
	public ActionResult getSDCardPath(Context aContext, String aPath) {

        ActionResult result = new ActionResult();
        try
        {
            Resources res = aContext.getResources();

            StringBuilder storageManagerPath = new StringBuilder();

            if (res.getString(R.string.sdcardpath).length() == 0) {
                storageManagerPath.append(Environment.getExternalStorageDirectory().getAbsolutePath() + "/GreatGuide");
            }
            else {
                storageManagerPath.append(res.getString(R.string.sdcardpath));
            }

            if (!createDirectory(storageManagerPath.toString()))
                throw new Exception("Unable to create main storage path: " + storageManagerPath.toString());

        	if (!storageManagerPath.toString().endsWith("/"))
        		storageManagerPath.append("/");

            if (aPath != null) {
            	storageManagerPath.append(aPath);
            }
            
            if (!createDirectory(storageManagerPath.toString()))
                throw new Exception("Unable to create sub storage path: " + storageManagerPath.toString());

            if (!storageManagerPath.toString().endsWith("/"))
                storageManagerPath.append("/");

            Log.i(StorageManager.class.getName(), "Storage Path: " + storageManagerPath.toString());

            result.setValue(storageManagerPath.toString());

        }
        catch(Exception ex)
        {
            result = new ActionResult(ex);
        }

		return result;
	}

    private boolean createDirectory(String aDirectory)
    {
        boolean result = true;
        File storageManagerDirectory = new File(aDirectory);
        if (!storageManagerDirectory.exists()) {
            storageManagerDirectory.mkdirs(); // directory is created
        }
        return result;
    }
}
