/**
 *
 */
package com.greatguide.ui;

import java.util.ArrayList;
import java.util.List;

import android.app.FragmentTransaction;
import android.os.Bundle;
import android.view.View;

import com.greatguide.R;
import com.greatguide.ui.core.BaseActivity;
import com.greatguide.ui.dialog.StartTourDialog;
import com.greatguide.ui.dialog.TourAddonsDialog;
import com.greatguide.ui.dialog.TourMapDialog;
import com.greatguide.ui.domain.POI;
import com.greatguide.ui.fragment.Title;
import com.greatguide.ui.fragment.TourFragment;
import com.greatguide.ui.fragment.ToursScrollFragment;
import com.greatguide.ui.contentRetriever.contentRetriever;

/**
 * The {@code ToursActivity} class is responsible for presenting tours.
 * @since Dec 5, 2012
 */
public class ToursActivity extends BaseActivity {

    /**
     * {@inheritDoc }
     */
    @Override
    protected void onCreate( Bundle savedInstanceState ) {
        super.onCreate( savedInstanceState );
        setContentView( R.layout.activity_tours );

        contentRetriever stringRetriever = contentRetriever.getContentRetriever();
        //
        Title title = ( Title ) super.getFragmentManager().findFragmentById( R.id.tours_title );
        title.setTitleText(stringRetriever.getString("title_tours"));
        title.setButton1Visible( true );
        title.setButton2Visible( true );
        title.setButton3Visible( true );
        //
        List<POI> points = new ArrayList<POI>();
        points.add( new POI( 1, "POI 1", "When Samsung came swinging with allegations of OLED patent infringements and corporate theft, LG promptly counter-sued.") );
        points.add( new POI( 2, "POI 2", "Samsung then escalated by broadening its list of patent complaints, forcing LG to do what any dignified electronics brawler") );
        points.add( new POI( 3, "POI 3", "The device at stake today is the unsuspecting Galaxy Note 10.1, which has no direct rival among LG's current product range") );
        //
        this.addTour( "Table Mountain", "The iconic mountain", "16 km (120min)",
                "Android introduced fragments in Android 3.0 (API level 11), primarily to support more dynamic and flexible UI designs on large screens, such as tablets. Because a tablet's screen is much larger than that of a handset, there's more room to combine and interchange UI components. Fragments allow such designs without the need for you to manage complex changes to the view hierarchy. By dividing the layout of an activity into fragments, you become able to modify the activity's appearance at runtime and preserve those changes in a back stack that's managed by the activity.",
                points);
        this.addTour( "Cape Point", "Where you can see where the two oceans meet", "180 km (6hrs)",
                "For example, a news application can use one fragment to show a list of articles on the left and another fragment to display an article on the right both fragments appear in one activity, side by side, and each fragment has its own set of lifecycle callback methods and handle their own user input events. Thus, instead of using one activity to select an article and another activity to read the article, the user can select an article and read it all within the same activity, as illustrated in the tablet layout in figure 1",
                points);
        this.addTour( "V&A Waterfront", "Blah Blah Blah", "180 km (6hrs)",
                "For example, a news application can use one fragment to show a list of articles on the left and another fragment to display an article on the right both fragments appear in one activity, side by side, and each fragment has its own set of lifecycle callback methods and handle their own user input events. Thus, instead of using one activity to select an article and another activity to read the article, the user can select an article and read it all within the same activity, as illustrated in the tablet layout in figure 1",
                points);
    }

    /**
     * {@inheritDoc }
     */
    @Override
    protected void onResume() {
        super.onResume();
    }

    /**
     * Add a {@code TourFragment} to the {@code ToursScrollFragment}
     * @param tourName the Name of the tour
     * @param tourDescription the tour description
     * @param tourDetails the tour particulars
     * @param tourNarrative the tour narrative
     */
    private void addTour( final String tourName, final String tourDescription, final String tourDetails, final String tourNarrative, final List<POI> points ) {
        final ToursScrollFragment tours = (ToursScrollFragment)super.getFragmentManager().findFragmentById( R.id.tours_content );
        tours.addTour( tourName, tourDescription, tourDetails, tourNarrative, points );
    }

    /**
     * Invoked when the 'StartTourClicked' button is clicked
     * @param view the button
     */
    public void onStartTourClicked( final View view ) {
        final FragmentTransaction ft = super.getFragmentManager().beginTransaction();
        final StartTourDialog dlg = StartTourDialog.newInstance();
        final Bundle tag = (Bundle)view.getTag();

        final Bundle args = new Bundle();
        args.putString( StartTourDialog.BUNDLE_STR_TOUR_NAME, tag.getString( TourFragment.BUNDLE_ARG_STR_TOUR_NAME ) );
        args.putString( StartTourDialog.BUNDLE_STR_DISTANCE_TO_START, "Undefined" );
        args.putString( StartTourDialog.BUNDLE_STR_DISTANCE_TO_NEAREST_POINT, "Undefined" );
        args.putStringArray( StartTourDialog.BUNDLE_ARR_AT_THE_END, new String[]{ "Matthew", "Mark", "Luke", "John"} );
        args.putString( StartTourDialog.BUNDLE_STR_ADDITIONAL_INFO, tag.getString( TourFragment.BUNDLE_ARG_STR_NARRATIVE ) );
        dlg.setArguments( args );
        dlg.show( ft, null );
    }

    /**
     * Invoked when the 'onTourHilitesClicked' button is clicked
     * @param view the button
     */
    public void onTourHilitesClicked( final View view ) {
        final FragmentTransaction ft = super.getFragmentManager().beginTransaction();
        final TourAddonsDialog dlg = TourAddonsDialog.newInstance();
        final Bundle tag = (Bundle)view.getTag();
        
        final Bundle args = new Bundle();
        args.putString( TourAddonsDialog.BUNDLE_STR_TOUR_NAME, tag.getString( TourFragment.BUNDLE_ARG_STR_TOUR_NAME ) );
        args.putSerializable( TourAddonsDialog.BUNDLE_OBJ_POI_ADDONS, tag.getSerializable( TourFragment.BUNDLE_ARG_LIST_POINTS_OF_INTEREST ) );
        
        dlg.setArguments( args );
        dlg.show( ft, null );
    }
    
    /**
     * Invoked when the Tour map is clicked
     * @param view
     */
    public void onTourMapClicked( final View view ) {
        final FragmentTransaction ft = super.getFragmentManager().beginTransaction();
        final TourMapDialog dlg = TourMapDialog.newInstance();
        final Bundle tag = (Bundle)view.getTag();
        final Bundle args = new Bundle();
        dlg.setArguments( args );
        dlg.show( ft, null );
    }
}