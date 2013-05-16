
package com.greatguide.backend.core;

import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.joda.time.DateTime;
import org.joda.time.format.DateTimeFormat;
import org.joda.time.format.DateTimeFormatter;

import android.graphics.Typeface;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

public class FormatUtil {
    public final static String SIMPLE_TIME_FORMAT = "HH:mm";
    public final static String SIMPLE_DATE_FORMAT = "dd MMM";
    public final static String DATE_FORMAT = "dd/MM/yyyy";
    public final static String DISTANCE_FORMAT = "#,###,###,##0.00km";
    
    private static DecimalFormat df = new DecimalFormat( DISTANCE_FORMAT );
    
    /**
     * @param time
     * @return
     */
    public static String formatTime( final long time )
    {
        final DateTimeFormatter dtf = DateTimeFormat.forPattern( SIMPLE_TIME_FORMAT );
        final DateTime dt = new DateTime( time ); //midnight
        return dtf.print( dt );
    }
    /**
     * @param time
     * @return
     */
    public static String formatSimpleDate( final long time )
    {
        final DateTimeFormatter dtf = DateTimeFormat.forPattern( SIMPLE_DATE_FORMAT );
        final DateTime dt = new DateTime( time ); //midnight
        return dtf.print( dt );
    }
    /**
     * @param time
     * @return
     */
    public static String formatDate( final Date date )
    {
        final DateTimeFormatter dtf = DateTimeFormat.forPattern( DATE_FORMAT );
        final DateTime dt = new DateTime( date ); //midnight
        return dtf.print( dt );
    }
    
    public static String formatDate( final Date date, final String pattern )
    {
        final DateTimeFormatter dtf = DateTimeFormat.forPattern( pattern );
        final DateTime dt = new DateTime( date ); //midnight
        return dtf.print( dt );
    }
    
    /**
     * @param year
     * @param monthOfYear
     * @param dayOfMonth
     * @return
     */
    public static String formatDate( final int year, final int monthOfYear, final int dayOfMonth )
    {
        final DateTimeFormatter dtf = DateTimeFormat.forPattern( DATE_FORMAT );
        final DateTime date = new DateTime( year, monthOfYear+1, dayOfMonth, 0, 0 ); //midnight
        return dtf.print( date );
    }
    
    public static DateTime parse( String date ) {
        final DateTimeFormatter dtf = DateTimeFormat.forPattern( DATE_FORMAT );
        return dtf.parseDateTime( date );
    }
    
    public static String formatDistance( final double distance ) {
        return df.format( distance );
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
            }
        }
    }

    public static String formatCurrency(double aCurrency) {
        String result = "R0.00";
        result = "R" + String.format("%.2f", aCurrency);
        return result;
    }

    public static Date parseDate(String aDate) throws Exception {
        SimpleDateFormat format = new SimpleDateFormat("yyyyMMdd");
        return format.parse(aDate);
    }
}
