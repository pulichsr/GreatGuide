package com.greatguide.ui.temp;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import com.greatguide.R;
import com.greatguide.backend.core.ActionResult;
import com.greatguide.backend.core.AuditTrail;
import com.greatguide.backend.maps.PrepareMapData;
import com.greatguide.backend.maps.PrepareMapsDataAction;
import com.greatguide.ui.core.BaseActivity;

/**
 */
public class tempMainUI extends BaseActivity {

    private PrepareMapsDataAction _prepareMapDataAction;

    private Handler _uiHandler = new Handler() {
        // This is executed on the UI thread
        public void handleMessage(android.os.Message msg) {
            Bundle bundle = msg.getData();
            setStatusText(bundle.getString("status"));
        }
    };

    /**
     * {@inheritDoc }
     */
    @Override
    protected void onCreate( Bundle savedInstanceState ) {
        super.onCreate( savedInstanceState );
        setContentView( R.layout.temp_main_screen );

        Button btnTour = (Button) findViewById(R.id.btnTour);
        btnTour.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try {
                    Intent intent = new Intent(tempMainUI.this, TourUI.class);
                    startActivity(intent);
                }
                catch(Exception e)
                {}
            }
        });

        Button btnPrepareMapData = (Button) findViewById(R.id.btnPrepareMapData);
        btnPrepareMapData.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try {
                    _prepareMapDataAction = new PrepareMapsDataAction(_uiHandler);
                    _prepareMapDataAction.execute(new PrepareMapData[] {new PrepareMapData(tempMainUI.this, _width, _height) } );
                }
                catch(Exception ex)
                {
                    ex.printStackTrace();
                    Log.e(PrepareMapsDataAction.class.getName(), ex.getMessage());
                    AuditTrail.getInstance().log(tempMainUI.this, new ActionResult(ex).getErrorMessage());
                    setStatusText("Error occurred, please see stack trace.");
                }
            }
        });
    }

    private void setStatusText(String aText) {
        TextView status = (TextView) findViewById(R.id.statusDescription);
        status.setText(aText);
    }

    @Override
    public void onDestroy()
    {
        super.onDestroy();
        if (_prepareMapDataAction != null) {
            _prepareMapDataAction.release();
            _prepareMapDataAction = null;
        }
    }

    @Override
    public void onPause()
    {
        super.onPause();
    }
}
