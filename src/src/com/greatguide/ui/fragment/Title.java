/**
 * 
 */
package com.greatguide.ui.fragment;

import android.app.Fragment;
import android.graphics.Typeface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.greatguide.GreatGuideApplication;
import com.greatguide.R;

/**
 * 
 * @since Dec 10, 2012
 */
public class Title extends Fragment {

    private TextView titleTextView;
    /**
     * Default constructor
     */
    public Title() {
        super();
    }

    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        final View view = inflater.inflate( R.layout.fragment_title, container, false );
        this.titleTextView = (TextView)view.findViewById( R.id.page_title );
        //
        TextView tv = (TextView)view.findViewById(R.id.page_title);
        final GreatGuideApplication application = (GreatGuideApplication)super.getActivity().getApplication();
        Typeface face = application.getTypefaceThin();
        tv.setTypeface(face);
        //
        return view;
    }
    
    /**
     * Invoked when Button 1 is clicked
     * @param view Button 1
     */
    public void onButton1Clicked( final View view ) {
        
    }
    /**
     * Invoked when Button 2 is clicked
     * @param view Button 2
     */
    public void onButton2Clicked( final View view ) {
        
    }
    /**
     * Invoked when Button 3 is clicked
     * @param view Button 3
     */
    public void onButton3Clicked( final View view ) {
        
    }
    
    /**
     * @param title the String to set as the title
     */
    public void setTitleText( final String title ) {
        this.titleTextView.setText( title );
    }
    
    /**
     * Sets the visibility of Button 1
     * @param visible <code>true</code> if visible, else hidden
     */
    public void setButton1Visible( final boolean visible ) {
        final View button = getView().findViewById( R.id.page_button_1 );
        if ( visible ) {
            button.setVisibility( View.VISIBLE );
        } else {
            button.setVisibility( View.GONE );
        }
    }
    /**
     * Sets the visibility of Button 2
     * @param visible <code>true</code> if visible, else hidden
     */
    public void setButton2Visible( final boolean visible ) {
        final View button = getView().findViewById( R.id.page_button_2 );
        if ( visible ) {
            button.setVisibility( View.VISIBLE );
        } else {
            button.setVisibility( View.GONE );
        }
    }
    /**
     * Sets the visibility of Button 3
     * @param visible <code>true</code> if visible, else hidden
     */
    public void setButton3Visible( final boolean visible ) {
        final View button = getView().findViewById( R.id.page_button_3 );
        if ( visible ) {
            button.setVisibility( View.VISIBLE );
        } else {
            button.setVisibility( View.GONE );
        }
    }
    
    public void setButton1OnClickListener( final View.OnClickListener listener ) {
        getView().findViewById( R.id.page_button_1 ).setOnClickListener( listener );
    }
    public void setButton2OnClickListener( final View.OnClickListener listener ) {
        getView().findViewById( R.id.page_button_2 ).setOnClickListener( listener );
    }
    public void setButton3OnClickListener( final View.OnClickListener listener ) {
        getView().findViewById( R.id.page_button_3 ).setOnClickListener( listener );
    }
    

}
