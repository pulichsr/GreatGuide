package com.greatguide.backend.location;

import java.util.Date;
import java.util.List;

public interface IMyLocationListener {
	
	public void receiveMyLocation(MyLocation aCurrentLocation, List<MyLocation> aLocationsList, boolean aCache, Date aLastUpdatedDate);
	
	public void failedToGetMyLocation(MyLocation aLastLocationFound, List<MyLocation> aLocationsList);
}   
              