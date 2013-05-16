package com.greatguide.ui.fragment;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Timer;

import org.osmdroid.ResourceProxy;
import org.osmdroid.util.GeoPoint;
import org.osmdroid.views.MapController;
import org.osmdroid.views.MapView;
import org.osmdroid.views.overlay.ItemizedIconOverlay;
import org.osmdroid.views.overlay.OverlayItem;

import android.app.Fragment;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Point;
import android.graphics.Typeface;
import android.os.Bundle;
import android.os.Handler;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.greatguide.GreatGuideApplication;
import com.greatguide.R;
import com.greatguide.backend.core.LocationUtils;
import com.greatguide.backend.core.RouteAuditTrail;
import com.greatguide.backend.location.IGPSStateListener;
import com.greatguide.backend.location.IMyLocationAction;
import com.greatguide.backend.location.IMyLocationListener;
import com.greatguide.backend.location.Marker;
import com.greatguide.backend.location.MyLocation;
import com.greatguide.backend.location.SignalStrength;
import com.greatguide.ui.core.RoutePlayerTask;
import com.greatguide.ui.core.utils.FormatUtil;

/**
 *
 * @since Dec 20, 2012
 */
public class TourFragment extends Fragment implements IMyLocationListener, IGPSStateListener {

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

            if (!markerList.isEmpty()) {

                for (Marker currentMarker : markerList) {

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
    final static public String BUNDLE_ARG_STR_TOUR_NAME = "BUNDLE_ARG_STR_TOUR_NAME";
    final static public String BUNDLE_ARG_STR_DESCRIPTION = "BUNDLE_ARG_STR_DESCRIPTION";
    final static public String BUNDLE_ARG_STR_DETAILS = "BUNDLE_ARG_STR_DETAILS";
    final static public String BUNDLE_ARG_STR_NARRATIVE = "BUNDLE_ARG_STR_NARRATIVE";

    final static public String BUNDLE_ARG_LIST_POINTS_OF_INTEREST = "BUNDLE_ARG_LIST_POINTS_OF_INTEREST";
    /**
     * @param container
     * @param tourDescription
     */
    private final static void setTourDescription( final LinearLayout container, final String tourDescription ) {
        TextView descriptionLabel = (TextView)container.findViewById( R.id.tour_description );
        descriptionLabel.setText( tourDescription );
    }
    /**
     * @param container
     * @param tourDetails
     */
    private final static void setTourDetails( final LinearLayout container, final String tourDetails ) {
        TextView detailsLabel = (TextView)container.findViewById( R.id.tour_details );
        detailsLabel.setText( tourDetails );
    }
    /**
     * @param container
     * @param tourName
     */
    private final static void setTourName( final LinearLayout container, final String tourName ) {
        TextView tourNameLabel = (TextView)container.findViewById( R.id.tour_tourname );
        tourNameLabel.setText( tourName );
    }
    /**
     * @param container
     * @param tourNarrative
     */
    private final static void setTourNarrative( final LinearLayout container, final String tourNarrative ) {
        TextView narrativeLabel = (TextView)container.findViewById( R.id.tour_narrative );
        narrativeLabel.setText( tourNarrative );
    }
    private List<Marker> markerList;

    private RoutePlayerTask _routePlayer;

    private Timer _routeTimer;

    private IMyLocationAction _location = null;

    private MapController mapController;

    private MapView _mapView;
    private Handler _playerHandler = new Handler() {
        public void handleMessage(android.os.Message msg) {
            Bundle bundle = msg.getData();
            TourFragment.this.playRouteLocation(bundle);
        }
    };
    /**
     * Constructor
     */
    public TourFragment() {
        super();
    }
    public boolean downloadMap() {
        return getResources().getString(R.string.downloadMap).equalsIgnoreCase("true") ? true : false;
    }

    private void drawMarkers(MyLocation aLocation) {

        if (aLocation != null) {

            markerList = new ArrayList<Marker>();

            Marker myPositionMarker = new Marker(aLocation, "My Location", "My Location");
            myPositionMarker.setBitmap(BitmapFactory.decodeResource(getResources(), R.drawable.my_location_marker));
            markerList.add(myPositionMarker);

            // Show my destination
            Marker destinationMarker = LocationUtils.getDestinationMarker(super.getActivity());
            if (destinationMarker != null) {
                destinationMarker.setBitmap(BitmapFactory.decodeResource(getResources(), R.drawable.destination_marker));
                markerList.add(destinationMarker);
            }
        }
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public void failedToGetMyLocation( MyLocation aLastLocationFound, List<MyLocation> aLocationsList ) {
        // TODO Auto-generated method stub

    }

    public int getDefaultZoomLevel()
    {
       return Integer.parseInt(getResources().getString(R.string.mapDefaultZoomLevel));
    }

    private List<OverlayItem> getOverlayItems() {

        List<OverlayItem> result = new ArrayList<OverlayItem>();
        for(Marker currentMarker: markerList) {
            OverlayItem destination = new OverlayItem( currentMarker.getTitle(), currentMarker.getDescription(),
                    currentMarker.getPoint() );
            result.add( destination );
        }
        return result;
    }

    private void gotoGpsLocation(double longitude, double latitude) {
        GeoPoint locGeoPoint = new GeoPoint(latitude, longitude);
        mapController.setCenter(locGeoPoint);
        _mapView.invalidate();
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final Bundle args = super.getArguments();
        final String tourName = args.getString( BUNDLE_ARG_STR_TOUR_NAME );
        final String tourDescription = args.getString( BUNDLE_ARG_STR_DESCRIPTION );
        final String tourDetails = args.getString( BUNDLE_ARG_STR_DETAILS );
        final String tourNarrative = args.getString( BUNDLE_ARG_STR_NARRATIVE );
        /**
         * Don't attached to root (i.e attachToRoot == false) because we'll be dynamically
         * attaching this fragment to the container
         */
        final View view = inflater.inflate( R.layout.fragment_tour, container, false );
        /**
         * Set Typeface
         */
        final GreatGuideApplication application = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = application.getTypefaceLight();
        final LinearLayout contentContainer = (LinearLayout)view.findViewById( R.id.tour_content_container );
        FormatUtil.setFont( contentContainer, face );
        //
        // Set Fragment width (80% of Window)
        //
        final GreatGuideApplication app = (GreatGuideApplication)super.getActivity().getApplication();
        final LayoutParams lp = view.getLayoutParams();
        lp.width = (int)(app.getWidthPx() * 0.7);
        view.setLayoutParams( lp );

        final LinearLayout buttons = (LinearLayout)view.findViewById( R.id.tour_buttons );
        final LayoutParams lpButtons = buttons.getLayoutParams();
        lpButtons.height = lp.width/2;
        lpButtons.width = lpButtons.height;
        buttons.setLayoutParams( lpButtons );
        
        final View mapView = view.findViewById( R.id.tour_map );
        mapView.setTag( args );
        final LayoutParams lpMapView = mapView.getLayoutParams();
        lpMapView.width = lp.width;
        lpMapView.height = lpMapView.width;
        mapView.setLayoutParams( lpMapView );
        //
        // Set Fragment Content
        //
        TourFragment.setTourName( contentContainer, tourName );
        TourFragment.setTourDescription( contentContainer, tourDescription );
        TourFragment.setTourDetails( contentContainer, tourDetails );
        TourFragment.setTourNarrative( contentContainer, tourNarrative );
        //
        final View tourStartButton = view.findViewById( R.id.tour_start_bar );
        tourStartButton.setTag( args );
        final View hilitesButton = view.findViewById( R.id.tour_highlights_tile );
        hilitesButton.setTag( args );
        //
        // MAP
//        this._mapView = (MapView)view.findViewById( R.id.tour_map );
//        final LayoutParams lpMap = _mapView.getLayoutParams();
//        lpMap.height = lp.width;
//        _mapView.setLayoutParams( lpMap );
//
//        _mapView.setBuiltInZoomControls(true);
//        _mapView.setMultiTouchControls(true);
//        _mapView.setUseDataConnection(downloadMap());
//        _mapView.setClickable(true);
//        _mapView.setTileSource(TileSourceFactory.MAPQUESTOSM);
//        this.mapController = _mapView.getController();
//        mapController.setZoom( getDefaultZoomLevel() );
//        // Hard code coordinates to Lennie's house (23 Pinewood Ave, Goodwood)
//        //_mapController.setCenter(new GeoPoint(-33.89724498616154, 18.535552031830058));
//        //writeZoomLevel();
//
//        this.markerList = new ArrayList<Marker>();
//        DefaultResourceProxyImpl defaultResourceProxyImpl = new DefaultResourceProxyImpl(super.getActivity());
//        MyItemizedIconOverlay myItemizedIconOverlay = new MyItemizedIconOverlay(getOverlayItems(), null, defaultResourceProxyImpl);
//        _mapView.getOverlays().add(myItemizedIconOverlay);
        return view;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public void onResume() {
//
//        PlayRoute route = LocationUtils.getPlayableRoute(super.getActivity());
//        if (route != null) {
//        	_mapView.setUseDataConnection(true);
//            _routePlayer = new RoutePlayerTask(route, _playerHandler);
//            _routeTimer = new Timer();
//            _routeTimer.schedule(_routePlayer, route.getDelay(), route.getDelay());
//        } else {
//
//            if (_location == null) {
//                _location = LocationUtils.getLocation(super.getActivity().getBaseContext(), this);
//                _location.checkGPSEnabled(super.getActivity(), this);
//                _location.getSignalStrength(super.getActivity(), this);
//            } else {
//                _location.resume();
//            }
//        }
        super.onResume();
    }

    private void playRouteLocation(Bundle aBundle) {
        Toast.makeText(super.getActivity(), "Play Route:  " + aBundle.getInt("currentPos") + " / " + aBundle.getInt("totalPos"), Toast.LENGTH_LONG).show();
        receiveMyLocation(new MyLocation(aBundle.getDouble("lat"), aBundle.getDouble("long")), null, false, new java.util.Date());
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public void receiveGPSStatus( boolean flag ) {
        // TODO Auto-generated method stub

    }

    /**
     * {@inheritDoc }
     */
    @Override
    public void receiveMyLocation(MyLocation aCurrentLocation, List<MyLocation> aLocationsList, boolean aCache, Date aLastUpdatedDate) {
        if (aCurrentLocation != null) {
            //Toast.makeText(super.getActivity(), "New coordinate, " + aCurrentLocation, Toast.LENGTH_LONG).show(); // debug
            gotoGpsLocation(aCurrentLocation.getLongitude(), aCurrentLocation.getLatitude());
            drawMarkers(aCurrentLocation);
            RouteAuditTrail.getInstance().log(super.getActivity(), aCurrentLocation, "My current location");
        }
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public void receiveSignalStrength( SignalStrength signalStrength ) {
        // TODO Auto-generated method stub

    }



}
