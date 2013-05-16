package com.greatguide.backend.core;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

import android.app.Service;
import android.content.Intent;
import android.location.Location;
import android.os.IBinder;
import android.os.RemoteException;
import android.util.Log;

import com.greatguide.backend.core.IGGService.IGGService;
import com.greatguide.backend.location.IMyLocationAction;
import com.greatguide.backend.location.IMyLocationListener;
import com.greatguide.backend.location.MyLocation;
import com.greatguide.backend.media.IMediaPlayer;
import com.greatguide.backend.media.IMediaPlayerListener;
import com.greatguide.backend.media.MPlayer;
import com.greatguide.backend.route.RouteLogic;

/**
 *
 * Author: Lennie De Villiers
 * 27 Nov 2012
 */
public class GGService extends Service implements IMyLocationListener, IMediaPlayerListener {

	public static final String AUDIO_STATE_EVENT = "GGService";
	public static final String SHOW_MEDIA = "GGService.Show";
	public static final String INTENT_MEDIA_CONTENT = "INTENT_MEDIA_CONTENT";

	private MediaContent _mediaContent = null;
	private IMediaPlayer _audioPlayer = null;
	private IMyLocationAction _location = null;
	private String _languageCode;
	private boolean _playbackMode;
	
	private final IGGService.Stub _serviceBinder = new IGGService.Stub() {

		@Override
		public void playAudio() throws RemoteException {
			if (_mediaContent != null && _mediaContent.getMediaType() == MediaType.AUDIO) {            	
				_audioPlayer.play(_mediaContent.getMediaFile());
				if (_mediaContent.isRepeatForever()) {
					_audioPlayer.repeatForever();
				}                
			}
		}

		@Override
		public boolean isPlaying() throws RemoteException {
			return _audioPlayer != null && _audioPlayer.isPlaying();
		}

		@Override
		public void pause() throws RemoteException {
			if (_audioPlayer != null) {
				_audioPlayer.pause();
			}
		}

		@Override
		public void resume() throws RemoteException {
			if (_audioPlayer != null) {
				_audioPlayer.resume();
			}
		}

		@Override
		public void stop() throws RemoteException {
			if (_audioPlayer != null) {
				_audioPlayer.stop();
			}
		}

		@Override
		public void increaseVolume() throws RemoteException {
			if (_audioPlayer != null) {
				_audioPlayer.increaseVolume(GGService.this);
			}
		}

		@Override
		public void decreaseVolume() throws RemoteException {
			if (_audioPlayer != null) {
				_audioPlayer.decreaseVolume(GGService.this);
			}
		}

		@Override
		public void repeatForever() throws RemoteException {
			if (_audioPlayer != null) {
				_audioPlayer.repeatForever();
			}
		}

		@Override
		public void stopRepeatForever() throws RemoteException {
			if (_audioPlayer != null) {
				_audioPlayer.stopRepeatForever();
			}
		}

		@Override
		public void destroy() throws RemoteException {
			if (_audioPlayer != null) {
				_audioPlayer.stop();
				_audioPlayer = null;
			}
			destroyLocationListener();
		}

		@Override
		public void sendLocation(Location aLocation) throws RemoteException {
			Log.d("GGService", "sendLocation");
			MyLocation myLocation = new MyLocation(aLocation);
			receiveMyLocation(myLocation, null, true, new java.util.Date());
		}
	};
	
	@Override
	public IBinder onBind(Intent intent) {
		return _serviceBinder;
	}

	private void destroyLocationListener() {
		if (_location != null) {
			_location.stop();
			_location = null;
		}
	}

	@Override
	public void onCreate() {
		final AppSettings settings = AppSettings.getInstance(this);
		_languageCode = settings.getLanguageCode();
		_playbackMode = settings.getPlaybackMode();
		Log.d("GGService", "Language Code: " + _languageCode);
		Log.d("GGService", "Playback Mode: " + _playbackMode);
		if (!_playbackMode) {
			initLocationListener();
		}		
		if (_audioPlayer == null) {
			_audioPlayer = MPlayer.getInstance(GGService.this);
		}
	}

	private void initLocationListener() {
		if (_location == null) {
			_location = LocationUtils.getLocation(GGService.this, GGService.this);
		}
	}

	@Override
	public void receiveMyLocation(MyLocation aCurrentLocation, List<MyLocation> aLocationsList, boolean aCache, Date aLastUpdatedDate) {
		Log.d("GGService", "Location: " + aCurrentLocation.toString());
		/*
		_mediaContent = new MediaContent(this, _languageCode);
		_mediaContent.setMediaType(MediaType.AUDIO);
		_mediaContent.setFileName("love.mp3");
		if (_mediaContent != null && _mediaContent.getMediaType() == MediaType.AUDIO) {
			try {
				_serviceBinder.playAudio();
			}
			catch (Exception ex) {}
		}
		else {
			Intent  i = new Intent ();
			i.setAction(SHOW_MEDIA);
			i.putExtra(INTENT_MEDIA_CONTENT , (Serializable) _mediaContent);
			sendBroadcast(i);
		}*/
		
		_mediaContent = RouteLogic.getInstance().Decide(this, aCurrentLocation);
		if (_mediaContent != null && _mediaContent.getMediaType() == MediaType.AUDIO) {
			try {
				_serviceBinder.playAudio();
			}
			catch (Exception ex) {}
		}
		else {
			Intent  i = new Intent ();
			i.setAction(SHOW_MEDIA);
			i.putExtra(INTENT_MEDIA_CONTENT , (Serializable) _mediaContent);
			sendBroadcast(i);
		}
		
	}

	@Override
	public void failedToGetMyLocation(MyLocation aLastLocationFound, List<MyLocation> aLocationsList) {
		//To change body of implemented methods use File | Settings | File Templates.
	}

	@Override
	public void OnAudioStartPlaying() {
		broadcastAction("OnAudioStartPlaying");
	}

	@Override
	public void OnAudioStopPlaying() {
		broadcastAction("OnAudioStopPlaying");
	}

	@Override
	public void onAudioPausePlaying() {
		broadcastAction("onAudioPausePlaying");
	}

	@Override
	public void onAudioResumePlaying() {
		broadcastAction("onAudioResumePlaying");
	}

	@Override
	public void onAudioFinishPlaying() {
		broadcastAction("onAudioFinishPlaying");
	}

	@Override
	public void onAudioIncreaseVolume() {
		broadcastAction("onAudioIncreaseVolume");
	}

	@Override
	public void onAudioDecreaseVolume() {
		broadcastAction("onAudioDecreaseVolume");
	}

	@Override
	public void onStopAudioRepeat() {
		broadcastAction("onStopAudioRepeat");
	}

	@Override
	public void onResumeAudioRepeat() {
		broadcastAction("onResumeAudioRepeat");
	}

	private void broadcastAction(String aEvent)
	{
		Intent  i = new Intent (AUDIO_STATE_EVENT);
		i.putExtra("Event", aEvent);
		sendBroadcast(i);
	}
}
