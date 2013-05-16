package com.greatguide.backend.location;

import android.app.Activity;
import android.content.Context;

public interface IMyLocationAction {

	public void getCoordinates(Context aContext);
	public boolean registerListener(IMyLocationListener aListener);
    public boolean isGPSEnabled();
    public void stop();
    public void resume();
    public void getSignalStrength(Activity aActivity, IGPSStateListener aListener);
    public void checkGPSEnabled(Activity aActivity, IGPSStateListener aListener);
}
