package com.greatguide.ui.fragment;

import android.app.Fragment;
import android.graphics.Typeface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.view.ViewTreeObserver;
import android.widget.TextView;

import com.greatguide.GreatGuideApplication;
import com.greatguide.R;
import com.greatguide.ui.SetupActivity;
import com.greatguide.ui.contentRetriever.contentRetriever;

/**
 * 
 * @since Jan 10, 2013
 */
public class SetupFragment extends Fragment {

    private int fragWidth;
    private int fragHeight;
    private contentRetriever stringRetriever;
    /**
     * 
     */
    public SetupFragment() {
        super();
        // TODO Auto-generated constructor stub
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.fragment_setup, container, false );
        
        final GreatGuideApplication application = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = application.getTypefaceLight();
        
        final SetupActivity activity = (SetupActivity)super.getActivity();
        
        contentRetriever.initializeRetriever( activity );
        stringRetriever = contentRetriever.getContentRetriever();
        
        final ViewTreeObserver vto = view.getViewTreeObserver();
        vto.addOnGlobalLayoutListener( new ViewTreeObserver.OnGlobalLayoutListener() {

            @Override
            public void onGlobalLayout() {
                
                fragWidth = view.getWidth();
                fragHeight = view.getHeight();
                if ( fragWidth == 0 ) return;
                // Set Tile Sizes in relation to the fragment size
                final View activateDeviceTile = view.findViewById( R.id.setup_activate_device_tile );
                final LayoutParams paramsActivateDeviceTile = activateDeviceTile.getLayoutParams();
                paramsActivateDeviceTile.height = (int)(fragHeight * 0.30);
                paramsActivateDeviceTile.width = (int)(fragWidth * 0.80);
                activateDeviceTile.setLayoutParams( paramsActivateDeviceTile );
                ((TextView)activateDeviceTile).setTypeface( face );
                ((TextView)activateDeviceTile).setText( stringRetriever.getString( "button_activate" ) );
                //
                final View resetToursTile = view.findViewById( R.id.setup_reset_tours_tile );
                final LayoutParams paramsToursTile = resetToursTile.getLayoutParams();
                paramsToursTile.height = (int)(fragHeight * 0.30);
                paramsToursTile.width = (int)(fragWidth * 0.80);
                resetToursTile.setLayoutParams( paramsToursTile );
                ((TextView)resetToursTile).setTypeface( face );
                ((TextView)resetToursTile).setText( stringRetriever.getString( "button_reset_tours" ) );
                //
                final View deviceSetupTiles = view.findViewById( R.id.setup_device_setup_tile );
                final LayoutParams paramsDeviceSetupTiles = deviceSetupTiles.getLayoutParams();
                paramsDeviceSetupTiles.height = (int)(fragHeight * 0.30);
                paramsDeviceSetupTiles.width = (int)(fragWidth * 0.80);
                deviceSetupTiles.setLayoutParams( paramsDeviceSetupTiles );
                ((TextView)deviceSetupTiles).setTypeface( face );
                ((TextView)deviceSetupTiles).setText( stringRetriever.getString( "button_device_setup" ) );

                view.getViewTreeObserver().removeGlobalOnLayoutListener( this );
            }
        } );
        
        return view;
    }
    
    

}
