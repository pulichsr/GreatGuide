package com.greatguide.ui.test.media;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import android.app.ListActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import com.greatguide.R;
import com.greatguide.backend.core.ActionResult;
import com.greatguide.backend.core.AuditTrail;
import com.greatguide.backend.device.StorageManager;
import com.greatguide.backend.media.IMediaPlayer;
import com.greatguide.backend.media.IMediaPlayerListener;
import com.greatguide.backend.media.MPlayer;

/**
 * @author sandeep pulichintala
 */
public class MediaTest extends ListActivity implements IMediaPlayerListener {

    private String _mediaPath = null;
    private List<String> songs = new ArrayList<String>();
    private IMediaPlayer mp = null;
    private Button btnPause = null;

    @Override
    public void onCreate(Bundle icicle) {
        try {
            super.onCreate(icicle);
            setContentView(R.layout.mp_songlist);
            mp = MPlayer.getInstance(this);

            AuditTrail.getInstance().log(this, "Getting media path");
            ActionResult media = StorageManager.getInstance().getSDCardPath(this, "media");
            if (media.isSuccessful()) {
                _mediaPath = media.getValue();
            }

            updateSongList();

            Button btnStop = (Button) findViewById(R.id. btnStop);
            btnStop.setOnClickListener(new OnClickListener() {
                public void onClick(View v) {
                    mp.stop();
                }
            });

            btnPause = (Button) findViewById(R.id.btnPause);
            btnPause.setOnClickListener(new OnClickListener() {
                public void onClick(View v) {
                    if (mp != null) {
                        mp.pause();
                    }
                }
            });

            Button btnVolumeIncrease = (Button) findViewById(R.id.btnVolumeIncrease);
            btnVolumeIncrease.setOnClickListener(new OnClickListener() {
                public void onClick(View v) {
                    mp.increaseVolume(MediaTest.this);
                }
            });

            Button btnVolumeDecrease = (Button) findViewById(R.id.btnVolumeDecrease);
            btnVolumeDecrease.setOnClickListener(new OnClickListener() {
                public void onClick(View v) {
                    if (mp != null) {
                        mp.decreaseVolume(MediaTest.this);
                    }
                }
            });

            Button btnPlayVideo = (Button) findViewById(R.id.btnPlayVideo);
            btnPlayVideo.setOnClickListener(new OnClickListener() {
                public void onClick(View v) {
                    if (mp != null) {
                        playVideo();
                    }
                }
            });

        } catch (NullPointerException e) {
            Log.e(MediaTest.class.getName(), "Received an exception", e);
        }
    }

    private void playVideo()
    {
        if (_mediaPath != null) {
            String media = _mediaPath + "video.mp4";
            AuditTrail.getInstance().log(this, "Playing: " + media);
            mp.play(media, this, R.id.videoView1);
        }
    }

    /**
     * update the songslist
     */
    private void updateSongList() {
        if (_mediaPath != null) {
            File home = new File(_mediaPath);
            if (home.listFiles(new MediaPlayerFilter()).length > 0) {
                for (File file : home.listFiles(new MediaPlayerFilter())) {
                    songs.add(file.getName());
                }

                ArrayAdapter<String> songList = new ArrayAdapter<String>(this, R.layout.mp_song_item, songs);
                setListAdapter(songList);
            }
        }
    }

    /**
     * called when we select the item on songslist
     */
    @Override
    protected void onListItemClick(ListView l, View v, int position, long id) {

        String media = _mediaPath + songs.get(position);
        AuditTrail.getInstance().log(this, "Playing: " + media);
        mp.play(media, this);

    }

    /**
     * call back method from MediaDelegationImpl to notify music is playing
     */
    public void OnAudioStartPlaying() {
        Log.i(MediaTest.class.getName(), "**** Music Playing");
    }

    /**
     * call back method from MediaDelegationImpl to notify music is stopped
     */
    public void OnAudioStopPlaying() {
        Log.i(MediaTest.class.getName(), "**** Music is stopped");
    }

    /**
     * call back method from MediaDelegationImpl to notify music is pause
     */
    public void onAudioPausePlaying() {
        btnPause.setText("Play");
        Log.i(MediaTest.class.getName(), "**** Music Pause");
    }

    /**
     * call back method from MediaDelegationImpl to notify music is resumed
     */
    public void onAudioResumePlaying() {
        btnPause.setText("Pause");
        Log.i(MediaTest.class.getName(), "**** Music Resume");
    }

    @Override
    public void onAudioFinishPlaying() {
        btnPause.setText("Finish playing");
    }

    @Override
    public void onAudioIncreaseVolume() {
        Log.i(MediaTest.class.getName(), "**** Music Increase Volume");
    }

    @Override
    public void onAudioDecreaseVolume() {
        Log.i(MediaTest.class.getName(), "**** Music Decrease Volume");
    }

    @Override
    public void onStopAudioRepeat() {
        Log.i(MediaTest.class.getName(), "**** Music Stop Repeat");
    }

    @Override
    public void onResumeAudioRepeat() {
        Log.i(MediaTest.class.getName(), "**** Music Resume Repeat");
    }

}
