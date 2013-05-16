package com.greatguide.backend.media;

import android.app.Activity;
import android.content.Context;

public interface IMediaPlayer {

    public boolean registerListener(IMediaPlayerListener aListener);

    public void stop();

    public boolean isPlaying();

    public void pause();

    public void resume();

    public void play(String aMediaItem);

    public void play(String aMediaItem, Activity aActivity);

    public void play(String aMediaItem, Activity aActivity, int aVideoViewId);

    public void repeatForever();

    public void stopRepeatForever();

    public void increaseVolume(Context aContext);
    public void decreaseVolume(Context aContext);
}
