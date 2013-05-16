package com.greatguide;

import android.app.Application;
import android.graphics.Typeface;

/**
 * 
 * @since Dec 17, 2012
 */
public class GreatGuideApplication extends Application {

    private Typeface typefaceLight;
    private Typeface typefaceThin;
    private int widthPx;
    private int heightPx;
    /**
     * {@inheritDoc }
     */
    @Override
    public void onCreate() {
        super.onCreate();
        this.typefaceLight = Typeface.createFromAsset( super.getAssets(), "fonts/roboto/Roboto-Light.ttf" );
        this.typefaceThin = Typeface.createFromAsset( super.getAssets(), "fonts/roboto/Roboto-Thin.ttf" );
        this.widthPx = super.getResources().getDisplayMetrics().widthPixels;
        this.heightPx = super.getResources().getDisplayMetrics().heightPixels;
    }
    
    /**
     * @return the 'Thin' typeface
     */
    public Typeface getTypefaceThin() {
        return this.typefaceThin;
    }
    /**
     * @return the 'Light' typeface
     */
    public Typeface getTypefaceLight() {
        return this.typefaceLight;
    }
    
    public int getWidthPx() {
        return Math.min( widthPx, heightPx );
    }

}
