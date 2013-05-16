/**
 * 
 */
package com.greatguide.ui.fragment;

import java.io.Serializable;
import java.util.List;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import android.util.Log;
import android.view.GestureDetector;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.HorizontalScrollView;

import com.greatguide.R;
import com.greatguide.ui.domain.POI;

/**
 * 
 * @since Dec 12, 2012
 */
public class ToursScrollFragment extends Fragment {

    private final static float CONFIG_MAX_FLING_SPEED = 8000;
    private GestureDetector gestureDetector;
    private HorizontalScrollView scrollView;
    private int countTourFragments = 0;
    /**
     * Default constructor
     */
    public ToursScrollFragment() {
        super();
    }
    //-------------------------------------------------------------------------
    // PRIVATE METHODS HERE
    //-------------------------------------------------------------------------
    

    //-------------------------------------------------------------------------
    // PUBLIC METHODS HERE
    //-------------------------------------------------------------------------
    /**
     * {@inheritDoc }
     */
    @Override
    public View onCreateView( LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState ) {
        this.countTourFragments = 0;
        final View view = inflater.inflate( R.layout.fragment_tours_scrollview, container, false );
        this.scrollView = (HorizontalScrollView)view.findViewById( R.id.fragment_tours_scroll );
        this.scrollView.setSmoothScrollingEnabled( true );
        //
        // Register for gesture events
        this.gestureDetector = new GestureDetector( this.getActivity().getApplicationContext(), new GestureDetector.OnGestureListener() {
            /** Maintains the index of the visible TourFragment*/
            private int visibleTour = 0;
            
            @Override
            public boolean onSingleTapUp( MotionEvent e ) {
                // TODO Auto-generated method stub
                return false;
            }
            
            @Override
            public void onShowPress( MotionEvent e ) {
                // TODO Auto-generated method stub
                
            }
            
            @Override
            public boolean onScroll( MotionEvent e1, MotionEvent e2, float distanceX, float distanceY ) {
                return true;
            }
            
            @Override
            public void onLongPress( MotionEvent e ) {
                // TODO Auto-generated method stub
                
            }
            
            @Override
            public boolean onFling( MotionEvent e1, MotionEvent e2, float velocityX, float velocityY ) {
                if ( Math.abs( velocityX ) > ToursScrollFragment.CONFIG_MAX_FLING_SPEED ) {
                    //threshold for flings..stay put
                    int fragWidth = (int)(ToursScrollFragment.this.scrollView.getRight() * 0.8);
                    ToursScrollFragment.this.scrollView.smoothScrollTo( ( (this.visibleTour) * fragWidth ), 0 );
                    return true;
                }
                if ( velocityX < 0 && ( this.visibleTour < (ToursScrollFragment.this.countTourFragments - 1) ) ) {
                    //left-swipe
                    this.visibleTour++;
                    int fragWidth = (int)(ToursScrollFragment.this.scrollView.getRight() * 0.8);
                    ToursScrollFragment.this.scrollView.smoothScrollTo( ( (this.visibleTour) * fragWidth ), 0 );
                    return true;
                } else if ( velocityX > 0 && this.visibleTour > 0) {
                    //right-swipe
                    this.visibleTour--;
                    int fragWidth = (int)(ToursScrollFragment.this.scrollView.getRight() * 0.8);
                    ToursScrollFragment.this.scrollView.smoothScrollTo( ( (this.visibleTour) * fragWidth ), 0 );
                    return true;
                }
                return false;
            }
            
            @Override
            public boolean onDown( MotionEvent e ) {
                // TODO Auto-generated method stub
                return false;
            }
            
        } );
        //
        // Listen out for touch events
        scrollView.setOnTouchListener( new View.OnTouchListener() {
            
            @Override
            public boolean onTouch( View v, MotionEvent event ) {
                return ToursScrollFragment.this.gestureDetector.onTouchEvent( event );
            }
        });
        //
        // View to attach
        return view;
    }
    
    /**
     * Invoked to add/append a new {@code TourFragment} to the scrollview
     * @param tourName the name of the tour
     */
    public void addTour( final String tourName, final String tourDescription, final String tourDetails, final String tourNarrative, final List<POI> points ) {
        final FragmentTransaction ft = super.getFragmentManager().beginTransaction();
        final Bundle args = new Bundle();
        args.putString( TourFragment.BUNDLE_ARG_STR_TOUR_NAME, tourName );
        args.putString( TourFragment.BUNDLE_ARG_STR_DESCRIPTION, tourDescription );
        args.putString( TourFragment.BUNDLE_ARG_STR_DETAILS, tourDetails );
        args.putString( TourFragment.BUNDLE_ARG_STR_NARRATIVE, tourNarrative );
        args.putSerializable( TourFragment.BUNDLE_ARG_LIST_POINTS_OF_INTEREST, (Serializable)points );
        final TourFragment tour = new TourFragment();
        tour.setArguments( args );
        ft.add( R.id.fragment_tours_container, tour, tourName );
        ft.commit();
        this.countTourFragments++; //increment the number of TourFragments this container holds
    }
    
    
}
