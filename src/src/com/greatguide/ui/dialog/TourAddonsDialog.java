package com.greatguide.ui.dialog;

import java.util.List;

import android.app.DialogFragment;
import android.graphics.Typeface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.TextView;

import com.greatguide.GreatGuideApplication;
import com.greatguide.R;
import com.greatguide.ui.adapter.ObjectArrayAdapter;
import com.greatguide.ui.core.utils.FormatUtil;
import com.greatguide.ui.domain.POI;

/**
 * 
 * @since Dec 28, 2012
 */
public class TourAddonsDialog extends DialogFragment {

    public final static String BUNDLE_STR_TOUR_NAME = "BUNDLE_STR_TOUR_NAME";
    public final static String BUNDLE_OBJ_POI_ADDONS = "BUNDLE_OBJ_POI_ADDONS";
    
    private ObjectArrayAdapter<POI, ViewGroup> listAdapter;
    /** maintains ListView item mapper*/
    ObjectArrayAdapter.ObjectMapper<POI, ViewGroup> mapper = new ObjectArrayAdapter.ObjectMapper<POI, ViewGroup>() {

        /**
         * {@inheritDoc }
         */
        @Override
        public void map( ViewGroup view, POI point ) {
            final TextView titleView = (TextView)view.findViewById( R.id.list_item_tour_addon_title );
            titleView.setText( point.getName() );
            final TextView descriptionView = (TextView)view.findViewById( R.id.list_item_tour_addon_detail );
            descriptionView.setText( point.getDescription() );
        }
        
    };
    
    private TourAddonsDialog() {
        super();
    }
    
    /**
     * @return a new instance
     */
    public final static TourAddonsDialog newInstance() {
        final TourAddonsDialog dlg = new TourAddonsDialog();
        dlg.setStyle( STYLE_NO_TITLE, R.style.GGDialog );
        return dlg;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.dlg_tour_addons, container, true );
        final GreatGuideApplication app = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = app.getTypefaceLight();
        FormatUtil.setFont( (ViewGroup)view, face );
        //
        // Arguments
        final Bundle args = super.getArguments();
        final String tourName = args.getString( BUNDLE_STR_TOUR_NAME );
        final List<POI> points = (List<POI>)args.getSerializable( BUNDLE_OBJ_POI_ADDONS );
        //
        final TextView tourNameView = (TextView)view.findViewById( R.id.dlg_tour_addon_tourname );
        tourNameView.setText( tourName );
        
        //
        listAdapter = new ObjectArrayAdapter<POI, ViewGroup>( this.getActivity().getApplicationContext(), R.layout.list_item_tour_addon, points, this.mapper);
        final ListView poiList = (ListView)view.findViewById( R.id.dlg_tour_addon_poi_list );
        poiList.setAdapter( listAdapter );
        FormatUtil.setFont( (ViewGroup)poiList, face );
        return view;
    }
    
    
}
