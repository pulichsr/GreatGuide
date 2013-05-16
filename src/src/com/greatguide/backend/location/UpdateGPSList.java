package com.greatguide.backend.location;

import java.util.ArrayList;
import java.util.List;

class UpdateGPSList {

    /**
     *
     * Update Location list
     *
     * @param aLocationsList
     * @param aCurrentLocation
     * @return
     */
	public static List<MyLocation> updateList(List<MyLocation> aLocationsList,
                                              MyLocation aCurrentLocation) {

		List<MyLocation> result = new ArrayList<MyLocation>();
        result.add(aCurrentLocation);
        if (aLocationsList != null && aLocationsList.size() > 0) {
            int i = 0;
            for(MyLocation currentLocation: aLocationsList) {
                result.add(currentLocation);
                i++;
                if (i >= 9)
                    break;
            }
        }

		return result;
	}
}
