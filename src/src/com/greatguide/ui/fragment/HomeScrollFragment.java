/**
 * 
 */
package com.greatguide.ui.fragment;

import android.app.Fragment;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
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
import com.greatguide.ui.core.utils.FormatUtil;

/**
 * 
 * @since Dec 12, 2012
 */
public class HomeScrollFragment extends Fragment {

    private int fragWidth;
    private int fragHeight;
    private String currDate;
    private BroadcastReceiver receiverTimeTicks;

    /**
     * Default constructor
     */
    public HomeScrollFragment() {
        super();
    }
    //-------------------------------------------------------------------------
    // PRIVATE METHODS HERE
    //-------------------------------------------------------------------------
    /**
     * Set date in the Date tile 
     */
    private void setDateTile( ) {
        
        final long currTime = System.currentTimeMillis();
        final String dateText = FormatUtil.formatSimpleDate( currTime );
        if ( !dateText.equals( this.currDate )) {
            final TextView dateTextView = (TextView)getView().findViewById(R.id.home_date);
            dateTextView.setText( dateText );
        }
    }
    
    /**
     * Register Receiver for TimeTicks
     * @return the {@code BroadcastReceiver}
     */
    private BroadcastReceiver registerTimeTicks() {
        // BroadcastReceiver for TimeTicks
        final BroadcastReceiver br = new BroadcastReceiver() {
            /**
             * {@inheritDoc }
             */
            @Override
            public void onReceive( Context context, Intent intent ) {
                HomeScrollFragment.this.setDateTile();
            }
            
        };
        getActivity().registerReceiver( br , new IntentFilter( Intent.ACTION_TIME_TICK ) );
        return br;
    }

    //-------------------------------------------------------------------------
    // PUBLIC METHODS HERE
    //-------------------------------------------------------------------------
    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.fragment_home_scrollview, container, false );
        // Set typeface
        final GreatGuideApplication application = (GreatGuideApplication)super.getActivity().getApplication();
        final Typeface face = application.getTypefaceLight();
        final TextView toursTextView = (TextView)view.findViewById(R.id.home_tours_tile);
        final TextView introTextView = (TextView)view.findViewById(R.id.home_intro_tile);
        final TextView helpTextView = (TextView)view.findViewById(R.id.home_help_tile);
        final TextView returnsTextView = (TextView)view.findViewById(R.id.home_returns_tile);
        final TextView dateTextView = (TextView)view.findViewById(R.id.home_date);
        final TextView hireperiodlabelTextView = (TextView)view.findViewById(R.id.home_hire_period_label);
        final TextView hireperiodTextView = (TextView)view.findViewById(R.id.home_hire_period);
        toursTextView.setTypeface( face );
        introTextView.setTypeface( face );
        helpTextView.setTypeface( face );
        returnsTextView.setTypeface( face );
        dateTextView.setTypeface( face );
        hireperiodlabelTextView.setTypeface( face );
        hireperiodTextView.setTypeface( face );
        //
        final ViewTreeObserver vto = view.getViewTreeObserver();
        vto.addOnGlobalLayoutListener( new ViewTreeObserver.OnGlobalLayoutListener() {

            @Override
            public void onGlobalLayout() {
                
                fragWidth = view.getWidth();
                fragHeight = view.getHeight();
                if ( fragWidth == 0 ) return;
                // // Log.d( "HOME", "Fragment {w="+view.getWidth()+",h="+view.getHeight()+'}' );
                // Set Tile Sizes in relation to the fragment size
                final View hire = view.findViewById( R.id.home_hiredate_tile );
                final LayoutParams paramsHireTile = hire.getLayoutParams();
                paramsHireTile.height = (int)(fragHeight * 0.30);
                paramsHireTile.width = (int)(fragWidth * 0.80);
                hire.setLayoutParams( paramsHireTile );
                //((TextView)hire).setTypeface( face );
                //
                final View tours = view.findViewById( R.id.home_tours_tile );
                final LayoutParams paramsToursTile = tours.getLayoutParams();
                paramsToursTile.height = (int)(fragHeight * 0.30);
//                paramsToursTile.width  <----- Anchored to R.id.home_hiredate_tile
                tours.setLayoutParams( paramsToursTile );
                ((TextView)tours).setTypeface( face );
                //
                final View bottomTiles = view.findViewById( R.id.home_bottom_tiles );
                final LayoutParams paramsBottomTiles = bottomTiles.getLayoutParams();
                paramsBottomTiles.height = (int)(fragHeight * 0.33);
//                paramsBottomTiles.width  <----- Anchored to R.id.home_hiredate_tile
                bottomTiles.setLayoutParams( paramsBottomTiles );
                //
                final View rulesTile = view.findViewById( R.id.home_rules_tile );
                final LayoutParams paramsRulesTile = rulesTile.getLayoutParams();
                paramsRulesTile.width = paramsHireTile.height; // <---- Set width as height
                rulesTile.setLayoutParams( paramsRulesTile );
                ((TextView)rulesTile).setTypeface( face );

                final View setupTile = view.findViewById( R.id.home_setup_tile );
                final LayoutParams paramsSetupTile = setupTile.getLayoutParams();
                paramsSetupTile.width = paramsHireTile.height; // <---- Set width as height
                setupTile.setLayoutParams( paramsSetupTile );
                ((TextView)setupTile).setTypeface( face );

                view.getViewTreeObserver().removeGlobalOnLayoutListener( this );
            }
        } );
        return view;
    }
    /**
     * {@inheritDoc }
     */
    @Override
    public void onResume() {
        super.onResume();
        this.setDateTile( );
        this.receiverTimeTicks = this.registerTimeTicks();
    }
    /**
     * {@inheritDoc }
     */
    @Override
    public void onStop() {
        if ( this.receiverTimeTicks != null ) {
            getActivity().unregisterReceiver( this.receiverTimeTicks );
        }
        super.onStop();
    }
    
    
}
