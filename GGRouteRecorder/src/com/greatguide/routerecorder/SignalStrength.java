package com.greatguide.routerecorder;

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

	public float getQuality(){
		      
		float result =	(this.getAccuracy() / this.getScale() * 100);
		return result;
		   
	}
}
