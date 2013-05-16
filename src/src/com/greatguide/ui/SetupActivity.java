package com.greatguide.ui;

import android.os.Bundle;

import com.greatguide.R;
import com.greatguide.ui.contentRetriever.contentRetriever;
import com.greatguide.ui.core.BaseActivity;
import com.greatguide.ui.fragment.Title;

/**
 * 
 * @since Jan 9, 2013
 */
public class SetupActivity extends BaseActivity {

    
    /**
     * {@inheritDoc }
     */
    @Override
    protected void onCreate( Bundle savedInstanceState ) {
        super.onCreate( savedInstanceState );
        setContentView( R.layout.activity_setup );
        
    }
    /**
     * {@inheritDoc }
     */
    @Override
    protected void onResume() {
        super.onResume();
        Title title = ( Title ) super.getFragmentManager().findFragmentById( R.id.setup_title );
        title.setTitleText(stringRetriever.getString("title_setup"));
        title.setButton1Visible( false );
        title.setButton2Visible( true );
        title.setButton3Visible( true );
    }
}
