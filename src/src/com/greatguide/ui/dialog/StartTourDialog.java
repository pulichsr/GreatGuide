package com.greatguide.ui.dialog;

import android.app.DialogFragment;
import android.graphics.Typeface;
import android.os.Bundle;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.greatguide.GreatGuideApplication;
import com.greatguide.R;

/**
 * 
 * @since Dec 22, 2012
 */
public class StartTourDialog extends DialogFragment {

    public final static String BUNDLE_STR_TOUR_NAME = "BUNDLE_STR_TOUR_NAME";
    public final static String BUNDLE_STR_DISTANCE_TO_START = "BUNDLE_STR_DISTANCE_TO_START";
    public final static String BUNDLE_STR_DISTANCE_TO_NEAREST_POINT = "BUNDLE_STR_DISTANCE_TO_NEAREST_POINT";
    public final static String BUNDLE_ARR_AT_THE_END = "BUNDLE_ARR_AT_THE_END";
    public final static String BUNDLE_STR_ADDITIONAL_INFO = "BUNDLE_STR_ADDITIONAL_INFO";
    /**
     * Constructor
     */
    private StartTourDialog() {
        super();
    }
    
    /**
     * @return a new instance
     */
    public final static StartTourDialog newInstance() {
        final StartTourDialog dlg = new StartTourDialog();
        dlg.setStyle( STYLE_NO_TITLE, R.style.GGDialog );
        return dlg;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.dlg_start_tour, container, true );
        final GreatGuideApplication app = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = app.getTypefaceLight();
        //
        // Arguments
        final Bundle args = super.getArguments();
        final String defaultValue = super.getResources().getString( R.string.undefined );
        final String tourName = args.getString( BUNDLE_STR_TOUR_NAME, defaultValue );
        final String start = args.getString( BUNDLE_STR_DISTANCE_TO_START, defaultValue );
        final String nearestPoint = args.getString( BUNDLE_STR_DISTANCE_TO_NEAREST_POINT, defaultValue );
        final String[] ends = args.getStringArray( BUNDLE_ARR_AT_THE_END );
        final String addInfo = args.getString( BUNDLE_STR_ADDITIONAL_INFO, defaultValue );
        //
        final TextView tvTourName = (TextView)view.findViewById( R.id.dlg_starttour_tourname );
        tvTourName.setText( tourName );
        tvTourName.setTypeface( face );

        final TextView tvTitle = (TextView)view.findViewById( R.id.dlg_starttour_title );
        tvTitle.setText( getResources().getString( R.string.tour_start_tile ) );
        tvTitle.setTypeface( face );

        final TextView tvStart = (TextView)view.findViewById( R.id.dlg_starttour_distance_to_start_details );
        tvStart.setText( start );
        tvStart.setTypeface( face );
        
        final TextView tvNearestPoint = (TextView)view.findViewById( R.id.dlg_starttour_distance_to_nearest_map_point_details );
        tvNearestPoint.setText( nearestPoint );
        tvNearestPoint.setTypeface( face );
        
        final TextView tvEndSteps = (TextView)view.findViewById( R.id.dlg_starttour_at_the_end_steps );
        tvEndSteps.setText( this.buildStringList( ends ) );
        tvEndSteps.setTypeface( face );
        
        final TextView tvTourNarrative = (TextView)view.findViewById( R.id.dlg_starttour_narrative );
        tvTourNarrative.setText( addInfo );
        tvTourNarrative.setTypeface( face );
        
        
        // 
        return view;
    }

    /**
     * @param array
     * @return
     */
    private CharSequence buildStringList( final String[] array) {
        StringBuffer builder = new StringBuffer();
        for ( String str : array ) {
            builder.append( "&#8226; " ).append( str ).append( "<br/>" );
        }
        
        return Html.fromHtml( builder.toString() );
    }
    
}
