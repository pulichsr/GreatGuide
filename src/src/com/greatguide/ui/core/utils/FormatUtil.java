/**
 * 
 */
package com.greatguide.ui.core.utils;

import java.text.SimpleDateFormat;
import java.util.Date;

import android.content.Context;
import android.graphics.Typeface;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;

/**
 * 
 * @since Dec 6, 2012
 */
public class FormatUtil {
    public final static String SIMPLE_TIME_FORMAT = "HH:mm";
    public final static String SIMPLE_DATE_FORMAT = "dd MMM";
    public final static String DATE_FORMAT = "dd-MM-yyyy";
    
    /**
     * @param time
     * @return
     */
    public static String formatTime( final long time )
    {
        final SimpleDateFormat sdf = new SimpleDateFormat(SIMPLE_TIME_FORMAT);
        return sdf.format( new Date(time) );
    }
    /**
     * @param time
     * @return
     */
    public static String formatSimpleDate( final long time )
    {
        final SimpleDateFormat sdf = new SimpleDateFormat(SIMPLE_DATE_FORMAT);
        return sdf.format( new Date(time) );
    }
    /**
     * @param time
     * @return
     */
    public static String formatDate( final Date date )
    {
        final SimpleDateFormat sdf = new SimpleDateFormat(DATE_FORMAT);
        return sdf.format( date );
    }
    
    /**
     * Set the TypeFace for all applicable controls
     * @param viewGroup parent view
     * @param typeFace the typeface
     */
    public static void setFont( final ViewGroup viewGroup, final Typeface typeFace ) {
        int count = viewGroup.getChildCount();
        View v;
        for ( int i=0; i<count; i++ ) {
            v = viewGroup.getChildAt( i );
            if ( v instanceof TextView ) {
                ((TextView)v).setTypeface( typeFace );
            } else if ( v instanceof RelativeLayout || v instanceof LinearLayout ) {
                setFont( (ViewGroup)v, typeFace );
            }
        }
    }
}
