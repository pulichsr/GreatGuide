package com.greatguide.ui.dialog;

import android.app.DialogFragment;
import android.graphics.Typeface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.greatguide.GreatGuideApplication;
import com.greatguide.R;
import com.greatguide.ui.core.utils.FormatUtil;
import com.greatguide.ui.contentRetriever.contentRetriever;

/**
 * 
 * @since Dec 15, 2012
 */
public class ReturnsDialog extends DialogFragment {

    private String hotelName;
    private String hotelAddress;
    private String metroDetails;
    private String busDetails;
    private contentRetriever stringRetriever = contentRetriever.getContentRetriever();
    /**
     * Constructor
     */
    public ReturnsDialog() {
        super();
    }

    /**
     * Returns a new class instance
     * @param hotelName the hotel name
     * @param hotelAddress the hotel address
     * @param metroDetails closest train station details
     * @param busDetails closest bus station details
     * @return a new instance of this class
     */
    public final static ReturnsDialog newInstance( final String hotelName, final String hotelAddress,
            final String metroDetails, final String busDetails ) {
        ReturnsDialog dlg = new ReturnsDialog();
        dlg.setStyle( STYLE_NO_TITLE, R.style.GGDialog );
        dlg.hotelName = hotelName;
        dlg.hotelAddress = hotelAddress;
        dlg.metroDetails = metroDetails;
        dlg.busDetails = busDetails;
        return dlg;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.dlg_returns, container, false );
        final GreatGuideApplication application = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = application.getTypefaceLight();
        FormatUtil.setFont( (ViewGroup)view, face );
        //
        final TextView title = (TextView)view.findViewById( R.id.dlg_title_text );
        title.setTypeface( face );
        title.setText(stringRetriever.getString("title_returns"));
        //
        final TextView hotel = (TextView)view.findViewById( R.id.dlg_returns_hotel_name );
        hotel.setText( this.hotelName );
//        hotel.setTypeface( face );
        //
        final TextView hotelAdd = (TextView)view.findViewById( R.id.dlg_returns_hotel_address );
        hotelAdd.setText( this.hotelAddress );
//        hotelAdd.setTypeface( face );
        //
        final TextView metroDetailsView = (TextView)view.findViewById( R.id.dlg_returns_metro_details );
        metroDetailsView.setText( this.metroDetails );
//        metroDetailsView.setTypeface( face );
        //
        final TextView busDetailsView = (TextView)view.findViewById( R.id.dlg_returns_bus_details );
        busDetailsView.setText( this.metroDetails );
//        busDetailsView.setTypeface( face );
        //
        return view;
    }
    
}
