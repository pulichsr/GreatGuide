package com.greatguide.ui.test.service;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;
import com.greatguide.R;
import com.greatguide.ui.core.BaseActivity;

/**
 * Author: Lennie De Villiers
 * Created: 27 Nov 2012
 */
public class ServiceTest extends BaseActivity {

    @Override
    public void onCreate(Bundle icicle)
    {
        super.onCreate(icicle);
        setContentView(R.layout.test_service);
        setup(this, "en");

        TextView tv  =(TextView) findViewById(R.id.ServiceTestStatus);
        tv.setText("Active language: " + _language.getActiveLanguage() + " - " + _language.getActiveLanguageDescription());

        Button btnPlay = (Button) findViewById(R.id.btnServiceTestPlay);
        btnPlay.setText(_language.getString("AudioPlay"));
        btnPlay.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try {
                    _serviceManager.playAudio();
                }
                catch(Exception e)
                {}
            }
        });

        Button btnStop = (Button) findViewById(R.id.btnServiceTestStop);
        btnStop.setText(_language.getString("AudioStop"));
        btnStop.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
               try {
                   _serviceManager.stop();
               }
               catch (Exception e)
               {}
            }
        });

        final Button btnPause = (Button) findViewById(R.id.btnServiceTestPause);
        btnPause.setText(_language.getString("AudioPause"));
        btnPause.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try {
                    if (_serviceManager.isPlaying()) {
                        btnPause.setText(_language.getString("AudioResume"));
                        _serviceManager.pause();
                    } else {
                        btnPause.setText(_language.getString("AudioPause"));
                        _serviceManager.resume();
                    }
                }
                catch (Exception e)
                {}
            }
        });

        Button btnIncreaseVolume = (Button) findViewById(R.id.btnServiceTestIncreaseVolume);
        btnIncreaseVolume.setText(_language.getString("AudioIncreaseVolume"));
        btnIncreaseVolume.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try {
                    _serviceManager.increaseVolume();
                } catch (Exception e) {
                }
            }
        });

        Button btnDecreaseVolume = (Button) findViewById(R.id.btnServiceTestDecreaseVolume);
        btnDecreaseVolume.setText(_language.getString("AudioDecreaseVolume"));
        btnDecreaseVolume.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try
                {
                    _serviceManager.decreaseVolume();
                }
                catch (Exception e)
                {}
            }
        });

        final Button btnServiceTestRepeat = (Button) findViewById(R.id.btnServiceTestRepeat);
        btnServiceTestRepeat.setText(_language.getString("AudioStopRepeat"));
        btnServiceTestRepeat.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                try
                {
                    String value = (String) btnServiceTestRepeat.getText();
                    if (value.equalsIgnoreCase(_language.getString("AudioStopRepeat"))) {
                        btnServiceTestRepeat.setText(_language.getString("AudioResumeRepeat"));
                        _serviceManager.stopRepeatForever();
                    }
                    else {
                        btnServiceTestRepeat.setText(_language.getString("AudioStopRepeat"));
                        _serviceManager.repeatForever();
                    }
                }
                catch (Exception e)
                {}
            }
        });
    }

    @Override
    public void handleAll(String aAction, String aEvent)
    {
        TextView tv  =(TextView) findViewById(R.id.ServiceTestStatus);
        StringBuilder st = new StringBuilder();
        st.append("Action: ").append(aAction).append(" ");
        st.append("Event: ").append(aEvent);
        tv.setText(st.toString());
    }

    @Override
    public void OnAudioStopPlaying() {
        Toast.makeText(this, ServiceTest.class.getName() + ".OnAudioStopPlaying",  Toast.LENGTH_SHORT).show();
    }
}
