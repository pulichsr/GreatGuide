package com.greatguide.ui;

import com.greatguide.R;
import android.os.Bundle;
import com.greatguide.ui.core.BaseActivity;

/**
 * 
 * @since Dec 5, 2012
 */
public class WelcomeActivity extends BaseActivity {

    /**
     * {@inheritDoc }
     */
    @Override
    protected void onCreate( Bundle savedInstanceState ) {
        super.onCreate( savedInstanceState );
        setContentView( R.layout.activity_welcome );
    }

}
