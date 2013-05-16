package com.greatguide.backend.core;

import android.app.Activity;
import android.content.Context;
import android.location.Location;
import com.greatguide.R;
import com.greatguide.backend.device.StorageManager;
import com.greatguide.backend.location.*;
import com.greatguide.backend.location.destination.route.DestinationRouteLocation;
import com.greatguide.backend.location.mock.MockMyLocationAction;
import com.greatguide.backend.location.playback.PlayRoute;
import com.greatguide.backend.location.playback.PlayRouteLocationAction;

import java.io.File;
import java.util.List;

/**
 *
 * Author: Lennie De Villiers
 * Created: 21 Nov 2012
 */
public class LocationUtils {

    private final static float KILOMETERS_IN_METERS = 1000f;
    private final static float AVERAGE_WALKING_SPEED = 1.4f; //as seen on google

    /**
     *
     * Calculate the distance between two start + end latitude and longitude coordinates
     * Return distance in meters
     *
     * @param aStartLat
     * @param aStartLong
     * @param aEndLat
     * @param aEndLong
     * @return
     */
    public static float calculateDistance(double aStartLat, double aStartLong, double aEndLat, double aEndLong) {

        float[] distance = new float[3]; //magic number used for debugging perposes

        Location.distanceBetween(aStartLat, aStartLong, aEndLat, aEndLong ,distance);

        return (distance[0]);
    }


    /***
     *
     * Calculate the average walking speed in seconds
     * This is the average speed it will take a person to walk from two start + end latitude and longitude coordinates
     *
     * @param aStartLat
     * @param aStartLong
     * @param aEndLat
     * @param aEndLong
     * @return
     */
    public static float calculateAverageWalkingSpeedInSeconds(double aStartLat, double aStartLong, double aEndLat, double aEndLong)
    {
        float distance = calculateDistance(aStartLat, aStartLong, aEndLat, aEndLong);
        float timeInSeconds = (distance /  AVERAGE_WALKING_SPEED);
        return timeInSeconds;
    }


    /***
     *
     * Calculate the average walking speed in minutes;
     * This is the average speed it will take a person to walk from two start + end latitude and longitude coordinates
     *
     * @param aStartLat
     * @param aStartLong
     * @param aEndLat
     * @param aEndLong
     * @return
     */
    public static float calculateAverageWalkingSpeedInMinutes(double aStartLat, double aStartLong, double aEndLat, double aEndLong)
    {
        float timeInSeconds = calculateAverageWalkingSpeedInSeconds(aStartLat, aStartLong, aEndLat, aEndLong);
       return timeInSeconds / 60;
    }

    /**
     *
     * Convert meters into kilometers
     *
     * @param aMeters
     * @return
     */
    public static float metersToKilometers(double aMeters)
    {
          return (float) aMeters / KILOMETERS_IN_METERS;
    }

    public static IMyLocationAction getLocation(Context aContext, IMyLocationListener aListener) {
        IMyLocationAction result;
        boolean mockGPSEnabled =  aContext.getResources().getString(R.string.mockGPSEnabled).equalsIgnoreCase("true");
        if (mockGPSEnabled) {
            result = new MockMyLocationAction(aContext, aListener);
        }
        else {
            result = new MyLocationAction(aContext, aListener);
        }

        return result;
    }

    public static Marker getDestinationMarker(Activity aActivity) {
        Marker result = null;
        DestinationRouteLocation route = new DestinationRouteLocation();
        List<MyLocation> locationList = route.getMyLocationCoordinates(aActivity, "routes.xml");
        if (locationList != null && locationList.size() > 0) {
           MyLocation myLocation = locationList.get(0);
           Location destinationLocation = new Location("Destination");
            destinationLocation.setLatitude(myLocation.getLatitude());
            destinationLocation.setLongitude(myLocation.getLongitude());
           result = new Marker(destinationLocation, "Destination", "Destination");
        }
        return result;
    }

    public static PlayRoute getPlayableRoute(Activity aActivity) {
        PlayRoute result = null;
        ActionResult routeLocation = StorageManager.getInstance().getSDCardPath(aActivity, "routes");
        if (routeLocation.isSuccessful()) {
            String path = routeLocation.getValue() + "playback.xml";
            File route = new File(path);
            if (route.exists()) {
                result = new PlayRouteLocationAction().getPlayingRoute(path);
            }
        }
        return result;
    }
}
