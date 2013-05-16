package com.greatguide.ui.dialog;

import android.app.DialogFragment;
import android.content.DialogInterface;
import android.graphics.Typeface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.greatguide.GreatGuideApplication;
import com.greatguide.R;
import com.greatguide.ui.contentRetriever.contentRetriever;
import com.greatguide.ui.core.utils.FormatUtil;

/**
 * 
 * @since Dec 15, 2012
 */
public class ActivationDialog extends DialogFragment {


    private View theLayout;
    /**
     * Constructor
     */
    public ActivationDialog() {
        super();
    }

    /**
     * Returns a new class instance
     * @param fromDate
     * @param toDate
     * @param details
     * @return
     */
    public final static ActivationDialog newInstance( final String tag ) {
        ActivationDialog dlg = new ActivationDialog();
        dlg.setStyle( STYLE_NO_TITLE, R.style.GGDialog );
        dlg.setCancelable( false );
        return dlg;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        this.theLayout = inflater.inflate( R.layout.dlg_activate, container, false );
        final GreatGuideApplication application = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = application.getTypefaceLight();
        FormatUtil.setFont( (ViewGroup)this.theLayout, face );
        //
        contentRetriever stringRetriever = contentRetriever.getContentRetriever();
        //
        final TextView title = (TextView)this.theLayout.findViewById( R.id.dlg_title_text );
        title.setText( stringRetriever.getString( "title_activations" ) );
        title.setTypeface( face );
        this.theLayout.findViewById( R.id.dlg_close_btn ).setVisibility( View.GONE );
        
        final TextView label = (TextView)this.theLayout.findViewById( R.id.dlg_activation_label );
        label.setTypeface( face );
        label.setText(stringRetriever.getString("message_activations"));
        
        final View goButton = this.theLayout.findViewById( R.id.dlg_activiation_go_btn );
        goButton.setOnClickListener( new View.OnClickListener() {
            
            @Override
            public void onClick( View v ) {
                // TODO Auto-generated method stub
                ActivationDialog.this.dismiss();
            }
        } );
        return this.theLayout;
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public void onCancel( DialogInterface dialog ) {
        // TODO Auto-generated method stub
        super.onCancel( dialog );
    }
    
    
    
    
}
