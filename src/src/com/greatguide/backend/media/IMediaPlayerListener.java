package com.greatguide.backend.media;

public interface IMediaPlayerListener {

    public void OnAudioStartPlaying();
    public void OnAudioStopPlaying();

    public void onAudioPausePlaying();
    public void onAudioResumePlaying();

    public void onAudioFinishPlaying();

    public void onAudioIncreaseVolume();
    public void onAudioDecreaseVolume();

    public void onStopAudioRepeat();
    public void onResumeAudioRepeat();
}
