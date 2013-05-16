package com.greatguide.backend.media;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.content.Context;
import android.media.AudioManager;
import android.media.MediaPlayer;
import android.widget.MediaController;
import android.widget.VideoView;

import com.greatguide.backend.core.ErrorManagerAuditTrail;

/**
 * @author sandeep pulichintala
 */
public class MPlayer extends Activity implements IMediaPlayer, MediaPlayer.OnCompletionListener {

    private StringBuffer _stringBuffer = null;
    private MediaPlayer _mediaPlayer = null;
    private List<IMediaPlayerListener> _listeners = new ArrayList<IMediaPlayerListener>();
    private VideoView _videoView = null;
    private MediaController _mediaControl = null;
    private AudioManager _audioManager = null;

    private static IMediaPlayer _mPlayer = null;

    public static synchronized IMediaPlayer getInstance(IMediaPlayerListener aListener) {
        if (_mPlayer == null) {
            _mPlayer = new MPlayer(aListener);
        }

        return _mPlayer;
    }

    private MPlayer(IMediaPlayerListener aListener) {
        this.registerListener(aListener);
    }

    /**
     * register the listener to receive the fired events
     */
    public boolean registerListener(IMediaPlayerListener aListener) {
        return _listeners.add(aListener);
    }

    /**
     * stop the music
     */
    public void stop() {
        if (_mediaPlayer != null) {
            if (_mediaPlayer.isPlaying()) {
                _mediaPlayer.pause();
                for (int i = 0; i < _listeners.size(); i++) {
                    _listeners.get(i).OnAudioStopPlaying();
                }
            }
        }
    }

    @Override
    public boolean isPlaying() {
        return _mediaPlayer != null && _mediaPlayer.isPlaying();
    }

    @Override
    public void play(String aMediaItem) {
        this.play(aMediaItem, null, 0);
    }

    @Override
    public void play(String aMediaIem, Activity aActivity)
    {
        this.play(aMediaIem, aActivity, 0);
    }

    @Override
    public void play(String aMediaIem, Activity aActivity, int aVideoViewId) {
        // Play video media
        if ((aMediaIem.toLowerCase().lastIndexOf(".mp4") != -1) || (aMediaIem.toLowerCase().lastIndexOf(".3gp") != -1)) {
            try
            {
                this.stopMusicPlaying();
                _videoView = (VideoView) aActivity.findViewById(aVideoViewId);
                _videoView.setVideoPath(aMediaIem);
                _mediaControl = new MediaController(aActivity);
                _mediaControl.setAnchorView(_videoView);
                _mediaControl.setMediaPlayer(_videoView);
                _videoView.setMediaController(_mediaControl);
                _videoView.requestFocus();
                _videoView.start();
            }
            catch(Exception e) {
                ErrorManagerAuditTrail.getInstance().log(aActivity, e);
            }

        } else {
            // Play music media
            try {
                if (_mediaPlayer == null || !_mediaPlayer.isPlaying()) {
                    this.stopVideoPlaying();
                    _mediaPlayer = new MediaPlayer();
                    _mediaPlayer.reset();
                    _mediaPlayer.setDataSource(aMediaIem);
                    _mediaPlayer.prepare();
                    _mediaPlayer.start();
                    _mediaPlayer.setOnCompletionListener(this);
                }
            } catch (Exception e) {
                ErrorManagerAuditTrail.getInstance().log(aActivity, e);
            }
        }

        for (int i = 0; i < _listeners.size(); i++) {
            _listeners.get(i).OnAudioStartPlaying();
        }
    }

    /**
     * pause and resume the music depend on button click
     */
    public void pause() {
        if (_mediaPlayer != null && _mediaPlayer.isPlaying()) {
            _mediaPlayer.pause();
            for (int i = 0; i < _listeners.size(); i++) {
                _listeners.get(i).onAudioPausePlaying();
            }
        }
    }

    public void resume()
    {
        if (_mediaPlayer != null && !_mediaPlayer.isPlaying()) {
            if (_mediaPlayer != null) {
                _mediaPlayer.start();
                for (int i = 0; i < _listeners.size(); i++) {
                    _listeners.get(i).onAudioResumePlaying();
                }
            }
        }
    }

    /**
     * Get total duration of file
     */
    public String getTotalDuration() {
        int durationInMillis = _mediaPlayer.getDuration();
        return this.getTimeDuration(durationInMillis);

    }

    /**
     * Get total duration of file played so far
     */
    public String getTotalDurationPlayed() {
        int durationInMillis = _mediaPlayer.getCurrentPosition();
        return this.getTimeDuration(durationInMillis);

    }

    /**
     * Get total duration of file left to play
     */
    public String getTotalDurationLeftToPlay() {
        int durationInMillis = _mediaPlayer.getDuration() - _mediaPlayer.getCurrentPosition();
        return this.getTimeDuration(durationInMillis);

    }

    /**
     * @param durationInMillis
     * @return
     */
    private String getTimeDuration(int durationInMillis) {

        final int HOUR = 60 * 60 * 1000;
        final int MINUTE = 60 * 1000;
        final int SECOND = 1000;

        int durationHour = (int) (durationInMillis / HOUR);
        int durationMint = (int) ((durationInMillis % HOUR) / MINUTE);
        int durationSec = (int) ((durationInMillis % MINUTE) / SECOND);

        if (durationHour > 0) {
            _stringBuffer.append(String.format("%02d", durationHour))
                    .append(":").append(String.format("%02d", durationMint))
                    .append(":").append(String.format("%02d", durationSec));
        } else {
            _stringBuffer.append(String.format("%02d", durationMint))
                    .append(":").append(String.format("%02d", durationSec));

        }

        return _stringBuffer.toString();
    }

    @Override
    public void repeatForever() {
        if (_mediaPlayer != null) {
            _mediaPlayer.setLooping(true);
            for (int i = 0; i < _listeners.size(); i++) {
                _listeners.get(i).onResumeAudioRepeat();
            }
        }
    }

    @Override
    public void stopRepeatForever() {
        if (_mediaPlayer != null) {
            _mediaPlayer.setLooping(false);
            for (int i = 0; i < _listeners.size(); i++) {
                _listeners.get(i).onStopAudioRepeat();
            }
        }
    }

    @Override
    public void increaseVolume(Context aContext) {
        if (_audioManager == null) {
            _audioManager = (AudioManager) aContext.getSystemService(Context.AUDIO_SERVICE);
        }

        _audioManager.adjustVolume(AudioManager.ADJUST_RAISE, AudioManager.FLAG_PLAY_SOUND);

        for (int i = 0; i < _listeners.size(); i++) {
            _listeners.get(i).onAudioIncreaseVolume();
        }
    }

    @Override
    public void decreaseVolume(Context aContext) {
        if (_audioManager == null) {
            _audioManager = (AudioManager) aContext.getSystemService(Context.AUDIO_SERVICE);
        }

        _audioManager.adjustVolume(AudioManager.ADJUST_LOWER, AudioManager.FLAG_PLAY_SOUND);

        for (int i = 0; i < _listeners.size(); i++) {
            _listeners.get(i).onAudioDecreaseVolume();
        }
    }

    public void stopMusicPlaying() {

        if (_mediaPlayer != null && _mediaPlayer.isPlaying()) {
            _mediaPlayer.stop();
        }
    }

    public void stopVideoPlaying() {

        if (_videoView != null && _videoView.isPlaying()) {
            _videoView.stopPlayback();
        }
    }

    @Override
    public void onCompletion(MediaPlayer mediaPlayer) {
        for (int i = 0; i < _listeners.size(); i++) {
            _listeners.get(i).onAudioFinishPlaying();
        }
    }
}
