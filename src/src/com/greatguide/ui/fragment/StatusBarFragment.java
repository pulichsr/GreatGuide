/**
 * 
 */
package com.greatguide.ui.fragment;

import android.annotation.TargetApi;
import android.app.Fragment;
import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.database.ContentObserver;
import android.graphics.drawable.Drawable;
import android.media.AudioManager;
import android.os.BatteryManager;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.greatguide.R;
import com.greatguide.ui.core.utils.FormatUtil;

/**
 * The {@code Header} fragment is responsible for handling the interactions with the Header
 * @since Dec 5, 2012
 */
@TargetApi( 11 )
public class StatusBarFragment extends Fragment {

    private View theLayout;
    private BroadcastReceiver receiverTimeTicks;
    private BroadcastReceiver receiverBatteryStatus;
    private AudioManager audioManager;
    private ContentObserver volumeChangeObsever;
    /**
     * 
     */
    public StatusBarFragment() {
        super();
        // TODO Auto-generated constructor stub
    }
    //-------------------------------------------------------------------------
    // PRIVATE METHODS HERE
    //-------------------------------------------------------------------------
    /**
     * Set header 'Time' textview 
     */
    private void setTime( final TextView timeView ) {
        final long currTime = System.currentTimeMillis();
        final String timeText = FormatUtil.formatTime( currTime );
        timeView.setText( timeText );
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
                final TextView timeView = (TextView)getView().findViewById( R.id.header_time );
                StatusBarFragment.this.setTime( timeView );
            }
            
        };
        getActivity().registerReceiver( br , new IntentFilter( Intent.ACTION_TIME_TICK ) );
        return br;
    }
    
    /**
     * Register for battery status
     * @return {@code BroadcastReceiver} for battery status changes
     */
    private BroadcastReceiver registerBatteryStatus() {
        // BroadcastReceiver for TimeTicks
        final BroadcastReceiver br = new BroadcastReceiver() {
            /**
             * {@inheritDoc }
             */
            @Override
            public void onReceive( Context context, Intent intent ) {
                StatusBarFragment.this.updateBatteryStatusDrawable( intent );
            }
            
        };
        final Intent batteryStatus = getActivity().registerReceiver( br , new IntentFilter( Intent.ACTION_BATTERY_CHANGED ) );
        this.updateBatteryStatusDrawable( batteryStatus );
        return br;
    }
    
    /**
     * Updates the battery status icon according to {@code batteryStatus}
     * @param batteryStatus maintains the battery status
     */
    private void updateBatteryStatusDrawable( final Intent batteryStatus ) {
        final int level = batteryStatus.getIntExtra( BatteryManager.EXTRA_LEVEL, -1 );
        final int scale = batteryStatus.getIntExtra( BatteryManager.EXTRA_SCALE, -1 );
        final float batteryPct = level / (float)scale;
//        Log.d( "Battery Status", "%="+batteryPct);
        // Charging or not
        final int status = batteryStatus.getIntExtra( BatteryManager.EXTRA_STATUS, -1 );
        final boolean isCharging = status == BatteryManager.BATTERY_STATUS_CHARGING ||
                status == BatteryManager.BATTERY_STATUS_FULL;
        //
        final ImageView icon = (ImageView)getView().findViewById( R.id.header_battery );
        int drawable = R.drawable.stat_sys_battery_unknown;
        
        if ( isCharging && false ) {
            //TODO Do charging animation
        } else {
            // NOT changing
            if ( batteryPct < 0.05 ) {
                drawable = R.drawable.stat_sys_battery_0;
            } else if ( batteryPct < 0.1 ) {
                drawable = R.drawable.stat_sys_battery_10;
            } else if ( batteryPct < 0.2 ) {
                drawable = R.drawable.stat_sys_battery_20;
            } else if ( batteryPct < 0.4 ) {
                drawable = R.drawable.stat_sys_battery_40;
            } else if ( batteryPct < 0.6 ) {
                drawable = R.drawable.stat_sys_battery_60;
            } else if ( batteryPct < 0.8 ) {
                drawable = R.drawable.stat_sys_battery_80;
            } else {
                drawable = R.drawable.stat_sys_battery_100;
            } 
        }
        
        icon.setImageDrawable( getResources().getDrawable( drawable ) );
    }
    
    private void registerVolumnChange() {
        this.volumeChangeObsever = new ContentObserver( new Handler() ) {

            /**
             * {@inheritDoc }
             */
            @Override
            public void onChange( boolean selfChange ) {
                super.onChange( selfChange );
                Log.d("StatusBar", "Here");
                if ( StatusBarFragment.this.audioManager != null ) {
                    updateVolumeStatusIcon();
                }
            }
            
        };
        super.getActivity().getContentResolver().registerContentObserver(
                android.provider.Settings.System.getUriFor(android.provider.Settings.System.VOLUME_SETTINGS[AudioManager.STREAM_MUSIC]),
                false, this.volumeChangeObsever);
    }
    //-------------------------------------------------------------------------
    // PUBLIC METHODS HERE
    //-------------------------------------------------------------------------
    /**
     * {@inheritDoc }
     */
    @Override
    public void onCreate( Bundle savedInstanceState ) {
        super.onCreate( savedInstanceState );
        this.audioManager = (AudioManager)super.getActivity().getSystemService( Context.AUDIO_SERVICE );
    }
    
    /**
     * {@inheritDoc }
     */
    @Override
    public void onResume() {
        super.onResume();
        // Register for TIME TICKS
        this.receiverTimeTicks = this.registerTimeTicks();
        this.receiverBatteryStatus = this.registerBatteryStatus();
        this.registerVolumnChange();
    }
    
    /**
     * {@inheritDoc }
     */
    @Override
    public void onStop() {
        super.onStop();
        // Unregister TIME TICK receiver intent
        if ( receiverTimeTicks != null ) {
            // unregister broadcast receiver
            getActivity().unregisterReceiver( receiverTimeTicks );
        }
        if ( this.receiverBatteryStatus != null ) {
            // unregister broadcast receiver
            getActivity().unregisterReceiver( this.receiverBatteryStatus );
        }
        if ( this.volumeChangeObsever != null ) {
            super.getActivity().getContentResolver().unregisterContentObserver( this.volumeChangeObsever );
        }
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        theLayout = inflater.inflate( R.layout.fragment_statusbar, container, false );
        final TextView timeView = (TextView)theLayout.findViewById( R.id.header_time );
        this.setTime( timeView );
        this.updateVolumeStatusIcon();
        return theLayout;
    }
    
    private void updateVolumeStatusIcon() {
        final ImageView volumeImage = (ImageView)theLayout.findViewById( R.id.header_volume );
        final Drawable volumnDrawable = this.getVolumeStatusIcon();
        if ( volumnDrawable != null && volumeImage != null ) volumeImage.setImageDrawable( volumnDrawable );
    }
    
    /**
     * @return
     */
    private Drawable getVolumeStatusIcon( ) {
        
        final int maxLevel = this.audioManager.getStreamMaxVolume( AudioManager.STREAM_MUSIC );
        final int currentLevel = this.audioManager.getStreamVolume( AudioManager.STREAM_MUSIC );
        final double percentLevel = (double)((double)currentLevel/(double)maxLevel); 
        if ( percentLevel == 0 ) {
            //TODO no icon available
        } else if ( percentLevel > 0 && percentLevel <= 0.2 ) {
            return super.getActivity().getResources().getDrawable( R.drawable.stat_sys_volumn_20 );
        } else if ( percentLevel > 0.2 && percentLevel <= 0.4 ) {
            return super.getActivity().getResources().getDrawable( R.drawable.stat_sys_volumn_40 );
        } else if ( percentLevel > 0.4 && percentLevel <= 0.6 ) {
            return super.getActivity().getResources().getDrawable( R.drawable.stat_sys_volumn_60 );
        } else if ( percentLevel > 0.6 && percentLevel <= 0.8 ) {
            return super.getActivity().getResources().getDrawable( R.drawable.stat_sys_volumn_80 );
        } else if ( percentLevel > 0.8 && percentLevel <= 1.0 ) {
            return super.getActivity().getResources().getDrawable(  R.drawable.stat_sys_volumn_100 );
        }
        return null;
    }
    
}
