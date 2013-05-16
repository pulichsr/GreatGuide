package com.greatguide.backend.route;

import android.content.Context;
import android.util.Log;

import com.greatguide.backend.core.MediaContent;
import com.greatguide.backend.location.MyLocation;
import com.greatguide.backend.route.Direction.Directions;


public class RouteLogic {
	private static RouteLogic _instance = null;

	public static synchronized RouteLogic getInstance() {
		if (_instance == null) {
			_instance = new RouteLogic();
		}

		return _instance;
	}

	private RouteLogic() {
	}

	public MediaContent Decide(Context aContext, MyLocation aCurrentLocation) {
		Directions moveDirection = Direction.FromHeading(aCurrentLocation.getHeadingValue());
		Log.d("RouteLogic", "Heading: " + aCurrentLocation.getHeading()  + " Direction: " + moveDirection);
		return null;
	}
}
