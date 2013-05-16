package com.greatguide.routerecorder;

import android.location.*;
import android.os.Bundle;
import android.app.Activity;
import android.content.Context;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

public class MainActivity extends Activity implements LocationListener {

    // Only detect GPS location every 10 seconds or 10 meters
    private final long LOCATION_DETECT_DELAY_SECONDS = 10000;
    private final float LOCATION_DETECT_DELAY_METERS = 10f;

	private LocationManager _locationManager;
	private TextView _txtFeedback;
	private Route _route;
    private boolean _stopped = false;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		_txtFeedback = (TextView) findViewById(R.id.textView1);
		
		Button btnStart = (Button) findViewById(R.id.btnStart);
		btnStart.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				Start();

			}
		});
		
		Button btnStop = (Button) findViewById(R.id.btnStop);
		btnStop.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				Stop();
				
			}
		});

        Button btnReset = (Button) findViewById(R.id.btnReset);
        btnReset.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                Reset();
            }
        });

		Button btnSave = (Button) findViewById(R.id.btnSave);
		btnSave.setOnClickListener(new View.OnClickListener() {
			
			@Override
			public void onClick(View v) {
				Save();
			}
		});
	}

	public void Start(){
        initGPS();
        _stopped = false;
	}
	
	public void Stop(){
        _stopped = true;
        _txtFeedback.setText("Ready To Start");
	}

	public void Reset(){
        if (_route != null) {
            ActionResult deleteResult =  _route.reset();
            if (!deleteResult.isSuccessful()) {
                reportError("Error reset route", deleteResult);
            }
            else {
                _txtFeedback.setText("Ready To Start");
            }
        }
    }
	
	public void Save() {
        if (_route != null) {
		    ActionResult saveResult = _route.saveRoute();
            if (!saveResult.isSuccessful()) {
                reportError("Error saving route", saveResult);
            }
            else {
                _txtFeedback.setText("Route saved");
            }
        }
	}

    private void initGPS() {
        try {
            _route = new Route(this);
            String provider = null;
            if (_locationManager == null) {

                _locationManager = (LocationManager) this
                        .getSystemService(Context.LOCATION_SERVICE);
                Criteria criteriaFilter = new Criteria();
                criteriaFilter.setAccuracy(Criteria.ACCURACY_FINE);
                provider = _locationManager.getBestProvider(criteriaFilter, true);
                if (provider != null) {
                    Location lastLocation = _locationManager.getLastKnownLocation(provider);
                    if (lastLocation != null) {
                        addRouteRecord(lastLocation);
                    }
                }
            }

            if (_locationManager != null && provider != null) {
                _locationManager.requestLocationUpdates(provider, LOCATION_DETECT_DELAY_SECONDS,
                        LOCATION_DETECT_DELAY_METERS, this);
            }
        } catch (Exception ex) {
            _txtFeedback.setText("Unable to init GPS");
            Log.e(this.getClass().getName(), "Error: " + ex);
        }
    }

    @Override
    public void onLocationChanged(Location location) {
        if (!_stopped) {
            if (location != null) {
                String feedbackText = "Lat: " + location.getLatitude() + " Long: " + location.getLongitude() + " Speed: " + location.getSpeed() + " Bearing: " + location.getBearing();
                _txtFeedback.setText(feedbackText);
                addRouteRecord(location);
            }
        }
    }

    private void addRouteRecord(Location aLocation) {
        GpsStatus status = _locationManager.getGpsStatus(null);
        int satellitesCount = 0;
        if (status != null) {
            satellitesCount = status.getMaxSatellites();
        }
        ActionResult recordResult = _route.addRecord(new MyLocation(aLocation, satellitesCount));
        if (!recordResult.isSuccessful()) {
            reportError("Error adding route record", recordResult);
        }
    }

    private void reportError(String aErrorMessage, ActionResult aErrorResult) {
        _txtFeedback.setText(aErrorMessage);
        _stopped = true;
        Log.e(this.getClass().getName(), "Error: " + aErrorResult.getErrorMessage());
    }

    @Override
    public void onStatusChanged(String provider, int status, Bundle extras) {
        // Not needed
    }

    @Override
    public void onProviderEnabled(String provider) {
        // Not needed
    }

    @Override
    public void onProviderDisabled(String provider) {
        // Not needed
    }
}
