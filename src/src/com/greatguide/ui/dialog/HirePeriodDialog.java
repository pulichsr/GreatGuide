package com.greatguide.ui.dialog;

import java.util.Date;

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
public class HirePeriodDialog extends DialogFragment {

    private String rentalDate;
    private String details;

    /**
     * Constructor
     */
    public HirePeriodDialog() {
        super();
    }

    /**
     * Returns a new class instance
     * @param fromDate
     * @param toDate
     * @param details
     * @return
     */
    public final static HirePeriodDialog newInstance( final Date fromDate, final Date toDate,
            final String details ) {
        if ( fromDate == null || toDate == null ) {
            throw new IllegalArgumentException( "Invalid dates" );
        }
        HirePeriodDialog dlg = new HirePeriodDialog();
        dlg.setStyle( STYLE_NO_TITLE, R.style.GGDialog );
        dlg.rentalDate = FormatUtil.formatDate( fromDate ) +" - "+FormatUtil.formatDate( toDate );
        dlg.details = details;
        return dlg;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.dlg_hire_period, container, false );
        final GreatGuideApplication application = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = application.getTypefaceLight();
        FormatUtil.setFont( (ViewGroup)view, face );
        //
        contentRetriever stringRetriever = contentRetriever.getContentRetriever();
        final TextView label = (TextView)view.findViewById(R.id.dlg_hire_period_label);
        label.setText(stringRetriever.getString("label_activation_period"));
        //
        final TextView title = (TextView)view.findViewById( R.id.dlg_title_text );
        title.setTypeface( face );
        title.setText( FormatUtil.formatSimpleDate( System.currentTimeMillis() ) );
        //
        final TextView period = ( TextView )view.findViewById( R.id.dlg_hire_period );
        period.setText( this.rentalDate );
        //
        final TextView details = (TextView) view.findViewById( R.id.dlg_hire_period_details );
        details.setText( this.details );
        return view;
    }

}
