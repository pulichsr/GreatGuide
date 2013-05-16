package com.greatguide.ui.temp;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Timer;

import org.osmdroid.DefaultResourceProxyImpl;
import org.osmdroid.ResourceProxy;
import org.osmdroid.tileprovider.tilesource.TileSourceFactory;
import org.osmdroid.util.GeoPoint;
import org.osmdroid.views.MapController;
import org.osmdroid.views.MapView;
import org.osmdroid.views.overlay.ItemizedIconOverlay;
import org.osmdroid.views.overlay.OverlayItem;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Point;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.greatguide.R;
import com.greatguide.backend.core.LocationUtils;
import com.greatguide.backend.core.RouteAuditTrail;
import com.greatguide.backend.location.IGPSStateListener;
import com.greatguide.backend.location.IMyLocationAction;
import com.greatguide.backend.location.IMyLocationListener;
import com.greatguide.backend.location.Marker;
import com.greatguide.backend.location.MyLocation;
import com.greatguide.backend.location.SignalStrength;
import com.greatguide.backend.location.playback.PlayRoute;
import com.greatguide.ui.core.BaseActivity;
import com.greatguide.ui.core.RoutePlayerTask;

/**
 * Author: Lennie De Villiers
 * Created: 05 Dec 2012
 *
 */
public class TourUI extends BaseActivity implements IMyLocationListener, IGPSStateListener {

    private final int DEFAULT_ZOOM_LEVEL = 18;
    private final int MIN_ZOOM_LEVEL = 15;
    private final int MAX_ZOOM_LEVEL = 18;

    private IMyLocationAction _location = null;

    private MapView _mapView;
    private MapController _mapController;
    private ArrayList<Marker> _markerList;

    private RoutePlayerTask _routePlayer;
    private Timer _routeTimer;

    private Handler _playerHandler = new Handler() {
        public void handleMessage(android.os.Message msg) {
            Bundle bundle = msg.getData();
            TourUI.this.playRouteLocation(bundle);
        }
    };
    
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setup(this, "en");
        
        setContentView(R.layout.osm_map);

        _mapView = (MapView) findViewById(R.id.mapview);

        _mapView.setBuiltInZoomControls(false);
        _mapView.setMultiTouchControls(false);
        _mapView.setUseDataConnection(false);
        _mapView.setClickable(true);
        _mapView.setTileSource(TileSourceFactory.MAPQUESTOSM);
        _mapController = _mapView.getController();
        _mapController.setZoom(DEFAULT_ZOOM_LEVEL);
        // Hard code coordinates to Lennie's house (23 Pinewood Ave, Goodwood)
        //_mapController.setCenter(new GeoPoint(-33.89724498616154, 18.535552031830058));
        writeZoomLevel();

        _markerList = new ArrayList<Marker>();
        DefaultResourceProxyImpl defaultResourceProxyImpl = new DefaultResourceProxyImpl(this);
        MyItemizedIconOverlay myItemizedIconOverlay = new MyItemizedIconOverlay(getOverlayItems(), null, defaultResourceProxyImpl);
        _mapView.getOverlays().add(myItemizedIconOverlay);

        Button tourUIZoomIn = (Button) findViewById(R.id.tourUIZoomIn);
        tourUIZoomIn.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try {
                    int zoomLevel = _mapView.getZoomLevel() + 1;
                    Log.i(TourUI.this.getClass().getName(), "In - zoomLevel: " + zoomLevel);
                    if (zoomLevel <= MAX_ZOOM_LEVEL) {
                        _mapController.setZoom(zoomLevel);
                    }
                    else {
                        Toast.makeText(TourUI.this, "Not allowed to zoom in anymore. Max allowed: " + MAX_ZOOM_LEVEL, Toast.LENGTH_LONG).show();
                    }
                    TourUI.this.writeZoomLevel();
                }
                catch(Exception e)
                {}
            }
        });

        Button tourUIZoomOut = (Button) findViewById(R.id.tourUIZoomOut);
        tourUIZoomOut.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try {
                    int zoomLevel = _mapView.getZoomLevel() - 1;
                    Log.i(TourUI.this.getClass().getName(), "Out - zoomLevel: " + zoomLevel);
                    if (zoomLevel >= MIN_ZOOM_LEVEL) {
                        _mapController.setZoom(zoomLevel);
                    }
                    else {
                        Toast.makeText(TourUI.this, "Not allowed to zoom out anymore. Max allowed: " + MIN_ZOOM_LEVEL, Toast.LENGTH_LONG).show();
                    }
                    TourUI.this.writeZoomLevel();
                }
                catch(Exception e)
                {}
            }
        });

    } // end onCreate()

    private void writeZoomLevel()
    {
        Log.i(this.getClass().getName(), "writeZoomLevel: " + _mapView.getZoomLevel());
        TextView zoomLevel = (TextView) findViewById(R.id.zoomLevel);
        zoomLevel.setText("Zoom Level: " + _mapView.getZoomLevel());
    }

    @Override
    public void onResume() {
        PlayRoute route = LocationUtils.getPlayableRoute(this);
        if (route != null) {
        	connectToService(true);
            _routePlayer = new RoutePlayerTask(route, _playerHandler);
            _routeTimer = new Timer();
            _routeTimer.schedule(_routePlayer, route.getDelay(), route.getDelay());
        } else {
        	connectToService(false);
            if (_location == null) {
                _location = LocationUtils.getLocation(this, this);
                _location.checkGPSEnabled(this, this);
                _location.getSignalStrength(this, this);
            } else {
                _location.resume();
            }
        }
        super.onResume();
    }

	@Override
    protected void onPause()
    {
        if (_location != null) {
            _location.stop();
        }

        if (_routeTimer != null)
        {
            _routeTimer.cancel();
        }

        super.onPause();
    }

    private void playRouteLocation(Bundle aBundle) {
        Toast.makeText(this, "Play Route:  " + aBundle.getInt("currentPos") + " / " + aBundle.getInt("totalPos"), Toast.LENGTH_LONG).show();
        MyLocation myLocation = new MyLocation(aBundle.getDouble("lat"), aBundle.getDouble("long"));
        myLocation.setBearing(aBundle.getFloat("bearing"));
        receiveMyLocation(myLocation, null, false, new java.util.Date());
        try {
        	_serviceManager.sendLocation(myLocation.getLocation());
        }
        catch (Exception ex) {}
    }
    
    @Override
    public void receiveMyLocation(MyLocation aCurrentLocation, List<MyLocation> aLocationsList, boolean aCache, Date aLastUpdatedDate) {
        if (aCurrentLocation != null) {
            //Toast.makeText(this, "New coordinate, " + aCurrentLocation, Toast.LENGTH_LONG).show(); // debug
            gotoGpsLocation(aCurrentLocation.getLongitude(), aCurrentLocation.getLatitude());
            drawMarkers(aCurrentLocation);
            RouteAuditTrail.getInstance().log(this, aCurrentLocation, "My current location");
        }
    }

    private void gotoGpsLocation(double longitude, double latitude) {
        GeoPoint locGeoPoint = new GeoPoint(latitude, longitude);
        _mapController.setCenter(locGeoPoint);
        _mapView.invalidate();
    }

    @Override
    public void failedToGetMyLocation(MyLocation aLastLocationFound, List<MyLocation> aLocationsList) {
        if (aLastLocationFound != null) {
            Toast.makeText(this, "Fail coordinate, " + aLastLocationFound, Toast.LENGTH_LONG).show(); // debug
            gotoGpsLocation(aLastLocationFound.getLongitude(), aLastLocationFound.getLatitude());
            drawMarkers(aLastLocationFound);
            RouteAuditTrail.getInstance().log(this, aLastLocationFound, "My current location");
        }
    }

    private void drawMarkers(MyLocation aLocation) {

        if (aLocation != null) {

            _markerList = new ArrayList<Marker>();

            Marker myPositionMarker = new Marker(aLocation, "My Location", "My Location");
            myPositionMarker.setBitmap(BitmapFactory.decodeResource(getResources(), R.drawable.my_location_marker));
            _markerList.add(myPositionMarker);

            // Show my destination
            Marker destinationMarker = LocationUtils.getDestinationMarker(this);
            if (destinationMarker != null) {
                destinationMarker.setBitmap(BitmapFactory.decodeResource(getResources(), R.drawable.destination_marker));
                _markerList.add(destinationMarker);
            }
        }
    }

    public List<OverlayItem> getOverlayItems() {

      List<OverlayItem> result = new ArrayList<OverlayItem>();
      for(Marker currentMarker: _markerList) {
          OverlayItem destination = new OverlayItem(currentMarker.getTitle(), currentMarker.getDescription(), currentMarker.getPoint());
          result.add(destination);
      }
      return result;
    }

    @Override
    public void receiveSignalStrength(SignalStrength signalStrength) {
        // Here you call:
        // signalStrength.getStatusIcon();
        // This will return a Bitmap for you as the status icon
        // for now this is not implemented
        Toast.makeText(this, "Signal Strength Percentage: " + signalStrength.getSignalStrength() + "%", Toast.LENGTH_LONG).show(); // temporary
    }

    @Override
    public void receiveGPSStatus(boolean flag) {
        if (flag == false) {
            Toast.makeText(this, "GPS not enabled", Toast.LENGTH_LONG).show();
        }
    }

    private class MyItemizedIconOverlay extends ItemizedIconOverlay<OverlayItem> {

        public MyItemizedIconOverlay(
                List<OverlayItem> pList,
                org.osmdroid.views.overlay.ItemizedIconOverlay.OnItemGestureListener<OverlayItem> pOnItemGestureListener,
                ResourceProxy pResourceProxy) {
            super(pList, pOnItemGestureListener, pResourceProxy);
        }

        @Override
        public void draw(Canvas canvas, MapView mapview, boolean arg2) {
            super.draw(canvas, mapview, arg2);

            if (!_markerList.isEmpty()) {

                for (Marker currentMarker : _markerList) {

                    GeoPoint in = currentMarker.getPoint();

                    Point out = new Point();
                    mapview.getProjection().toPixels(in, out);

                    Bitmap bm = currentMarker.getImage();
                    canvas.drawBitmap(bm,
                            out.x - bm.getWidth() / 2,
                            out.y - bm.getHeight() / 2,
                            null);
                }
            }
        }
    }
}