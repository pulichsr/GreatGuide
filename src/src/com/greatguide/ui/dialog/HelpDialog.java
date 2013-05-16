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
public class HelpDialog extends DialogFragment {


    /**
     * Constructor
     */
    public HelpDialog() {
        super();
    }

    /**
     * Returns a new class instance
     * @param fromDate
     * @param toDate
     * @param details
     * @return
     */
    public final static HelpDialog newInstance( final String tag ) {
        HelpDialog dlg = new HelpDialog();
        dlg.setStyle( STYLE_NO_TITLE, R.style.GGDialog );
        return dlg;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.dlg_help, container, false );
        final GreatGuideApplication application = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = application.getTypefaceLight();
        FormatUtil.setFont( (ViewGroup)view, face );
        //
        contentRetriever stringRetriever = contentRetriever.getContentRetriever();
        //
        final TextView title = (TextView)view.findViewById( R.id.dlg_title_text );
        title.setTypeface( face );
        title.setText(stringRetriever.getString("title_help"));
        return view;
    }
}
