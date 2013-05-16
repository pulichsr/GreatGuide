package com.greatguide.ui.dialog;

import android.app.DialogFragment;
import android.graphics.Typeface;
import android.os.Bundle;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.TextView;

import com.greatguide.GreatGuideApplication;
import com.greatguide.R;

/**
 * 
 * @since Dec 22, 2012
 */
public class TourMapDialog extends DialogFragment {

    public final static String BUNDLE_STR_TOUR_NAME = "BUNDLE_STR_TOUR_NAME";
    public final static String BUNDLE_STR_DISTANCE_TO_START = "BUNDLE_STR_DISTANCE_TO_START";
    public final static String BUNDLE_STR_DISTANCE_TO_NEAREST_POINT = "BUNDLE_STR_DISTANCE_TO_NEAREST_POINT";
    public final static String BUNDLE_ARR_AT_THE_END = "BUNDLE_ARR_AT_THE_END";
    public final static String BUNDLE_STR_ADDITIONAL_INFO = "BUNDLE_STR_ADDITIONAL_INFO";
    
    private GreatGuideApplication app;
    /**
     * Constructor
     */
    private TourMapDialog() {
        super();
    }
    
    /**
     * @return a new instance
     */
    public final static TourMapDialog newInstance() {
        final TourMapDialog dlg = new TourMapDialog();
        dlg.setStyle( STYLE_NO_TITLE, R.style.GGDialog );
        return dlg;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.dlg_tour_map, container, false );
        this.app = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = app.getTypefaceLight();
        //
        // Arguments
        final Bundle args = super.getArguments();
        
        // 
        return view;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public void onViewCreated( View view, Bundle savedInstanceState ) {
        //
        // Set Fragment width (80% of Window)
        //
//        final LayoutParams lp = view.getLayoutParams();
//        lp.width = (int)(this.app.getWidthPx() * 0.7);
//        view.setLayoutParams( lp );
//        
//        final View mapView = view.findViewById( R.id.dlg_tour_map );
//        final LayoutParams lpMapView = mapView.getLayoutParams();
//        lpMapView.width = lp.width;
//        lpMapView.height = lpMapView.width;
//        mapView.setLayoutParams( lpMapView );
//        super.onViewCreated( view, savedInstanceState );
    }
    
}
