/**
 * 
 */
package com.greatguide.ui;

import android.os.Bundle;
import android.widget.TextView;

import com.greatguide.R;
import com.greatguide.ui.contentRetriever.contentRetriever;
import com.greatguide.ui.core.BaseActivity;
import com.greatguide.ui.fragment.Title;

/**
 * 
 * @since Dec 5, 2012
 */
public class DirectionActivity extends BaseActivity  {

    /**
     * {@inheritDoc }
     */
    @Override
    protected void onCreate( Bundle savedInstanceState ) {
        super.onCreate( savedInstanceState );
        setContentView( R.layout.activity_direction );
    }
    
    
    /**
     * {@inheritDoc }
     */
    @Override
    protected void onResume() {
        super.onResume();
        Title title = (Title)super.getFragmentManager().findFragmentById( R.id.direction_title );
        title.setTitleText( stringRetriever.getString("title_direction") );
        title.setButton1Visible( false );
        title.setButton2Visible( true );
        title.setButton3Visible( true );
    }
}
