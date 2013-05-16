package com.greatguide.backend.location;

import android.graphics.Bitmap;

public class SignalStrength {
   
	private float _accuracy;
	
	public SignalStrength(float _accuracy){
		this._accuracy = _accuracy;
	}

	public float getScale(){
		
		float scale = 0;
		
		if (_accuracy < 100)
			scale = 100;
		else if (_accuracy > 100 && _accuracy < 500)
			scale = 500;
		else if (_accuracy > 500 && _accuracy < 1000)
			scale = 1000;
		
		return scale;
	}
	   
	public float getAccuracy(){
		return this._accuracy;
	}

	public float getSignalStrength(){
		      
		float result =	(this.getAccuracy() / this.getScale() * 100);
		return result;
		   
	}

    public Bitmap getStatusIcon()
    {
        // TODO: Need to implement this once we get assets
        /* Rules:
        Green: +- 50 meter
        Yellow: +- 100 - 500 meter
        Red: 1000+ meter
        Red/Cross: No GPS (GPS disabled)
        */
        return null;
    }
}
