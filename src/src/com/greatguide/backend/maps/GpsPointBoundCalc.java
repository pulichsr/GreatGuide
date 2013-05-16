package com.greatguide.backend.maps;

/**
 *Author: Ivan Kruger
 *Date: 06/12/2012
 */

import java.util.List;
import java.util.ArrayList;
import com.greatguide.backend.location.MyLocation;

public class GpsPointBoundCalc {

    private Double offsetMeters = 100.0/100000;
    private boolean isNegative = false;

    public GpsPointBoundCalc(){}

    public void setOffsetMeters(Double d){offsetMeters = d/100000;}

    public double getOffsetMeters(){return offsetMeters;}

    public List<MyLocation> coordinatesInBox(double startLat,double startLong,double endLat,double endLong){

        List<MyLocation> result = new ArrayList<MyLocation>();

        double deltaLat = startLat - endLat;
        double deltaLong = startLong - endLong;

        double latLoc = startLat;
        double lonLoc = startLong;

        int numLatLoc = (int)(deltaLat / offsetMeters);
        int numLonLoc = (int)(deltaLong / offsetMeters);

        if(numLatLoc < 0){numLatLoc = numLatLoc * -1; isNegative = true;}
        if(numLonLoc < 0){numLonLoc = numLonLoc * -1; isNegative = true;}

        MyLocation p;

        for(int x = numLatLoc; x >= 0;x-- ){

            if(isNegative){
                latLoc = (latLoc - offsetMeters);
            }else{
                latLoc = (latLoc + offsetMeters);
            }

            for(int y = numLonLoc; y >= 0; y--){
                if(isNegative) {
                    lonLoc = (lonLoc - offsetMeters);
                }else{
                    lonLoc = (lonLoc + offsetMeters);
                }

                p = new MyLocation();

                p.setLatitude(latLoc);
                p.setLongitude(lonLoc);

                result.add(p);
            }
        }

        return result;

    }

}
