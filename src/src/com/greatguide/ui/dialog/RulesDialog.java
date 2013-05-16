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
public class RulesDialog extends DialogFragment {

    private String details;
    private contentRetriever stringRetriever = contentRetriever.getContentRetriever();

    /**
     * Constructor
     */
    public RulesDialog() {
        super();
    }

    /**
     * Returns a new class instance
     * @param fromDate
     * @param toDate
     * @param details
     * @return
     */
    public final static RulesDialog newInstance( final String details ) {
        RulesDialog dlg = new RulesDialog();
        dlg.setStyle( STYLE_NO_TITLE, R.style.GGDialog );
        dlg.details = details;
        return dlg;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.dlg_rules, container, false );
        final GreatGuideApplication application = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = application.getTypefaceLight();
        FormatUtil.setFont( (ViewGroup)view, face );
        //
        final TextView title = (TextView)view.findViewById( R.id.dlg_title_text );
        title.setTypeface( face );
        title.setText(stringRetriever.getString("title_rules"));
        //
        final TextView details = (TextView) view.findViewById( R.id.dlg_rules_details );
        details.setText( this.details );
        //
        return view;
    }
}
