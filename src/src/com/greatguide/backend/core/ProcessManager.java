package com.greatguide.backend.core;

import java.io.File;

import android.content.Context;
import android.util.Log;

import com.greatguide.backend.device.StorageManager;
import com.greatguide.backend.route.parser.RouteLoader;

public final class ProcessManager {

	public ActionResult process(Context aContext) {
		return processRoute(aContext);
	}

	public ActionResult processRoute(Context aContext) {
		ActionResult result = null;
		try {
			ActionResult routeLocation = StorageManager.getInstance().getSDCardPath(aContext, "routes/def");
			if (routeLocation.isSuccessful()) {
				String path = routeLocation.getValue() + "load.xml";
				File route = new File(path);
				if (route.exists()) {
					result = new ActionResult();
					RouteLoader list = new RouteLoader();	    
					list.parse(aContext, route.getAbsolutePath());
					File processedFile = new File(routeLocation.getValue() + "load_processes_" + FormatUtil.formatDate(new java.util.Date(), "ddMMyyyhhmmss") + ".xml");
					route.renameTo(processedFile);
				}
			}
		}
		catch (Exception ex) {
			Log.e(this.getClass().getName(), "Unable to process route", ex);
			result = new ActionResult(ex);
		}
		return result;
	}
}
