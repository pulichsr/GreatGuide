package com.greatguide.ui.core;

import java.lang.reflect.Method;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.graphics.Point;
import android.media.AudioManager;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.WindowManager;

import android.widget.Toast;

import com.greatguide.R;
import com.greatguide.backend.core.ActionResult;
import com.greatguide.backend.core.ErrorManagerAuditTrail;
import com.greatguide.backend.core.GGService;
import com.greatguide.backend.core.MediaContent;
import com.greatguide.backend.core.ProcessManager;
import com.greatguide.backend.core.ServiceManager;
import com.greatguide.ui.IServiceEventListener;
import com.greatguide.ui.contentRetriever.contentRetriever;
import com.greatguide.ui.multilang.LangRetriever;

/**
 * Author: Lennie De Villiers
 * Created: 01 Dec 2012
 */
public abstract class BaseActivity extends Activity implements IServiceEventListener {

	private IServiceEventListener _serviceListener;

	protected ServiceManager _serviceManager;
	protected LangRetriever _language;

	protected int _width, _height;

	protected contentRetriever stringRetriever;

	private BroadcastReceiver _audioStateEventReceiver = new BroadcastReceiver() {

		@Override
		public void onReceive(Context context, Intent itent) {

			String action = itent.getAction();
			String event = itent.getStringExtra("Event");

			// Call handleAll
			try
			{
				Class[] paramString = new Class[2];
				paramString[0] = String.class;
				paramString[1] = String.class;

				Method handleAllMethod = BaseActivity.this._serviceListener.getClass().getMethod("handleAll", paramString);
				handleAllMethod.invoke(BaseActivity.this._serviceListener, action, event);
			}
			catch(Exception ex)
			{
				ErrorManagerAuditTrail.getInstance().log(BaseActivity.this, ex);
			}

			// Call specific event
			try
			{
				Method eventMethod = BaseActivity.this._serviceListener.getClass().getMethod(event);
				eventMethod.invoke(BaseActivity.this._serviceListener);
			}
			catch (Exception ex)
			{
				ErrorManagerAuditTrail.getInstance().log(BaseActivity.this, ex);
			}
		}
	};

	private BroadcastReceiver _showMediaReceiver = new BroadcastReceiver() {

		@Override
		public void onReceive(Context context, Intent intent) {
			MediaContent mediaContent = (MediaContent) intent.getSerializableExtra(GGService.INTENT_MEDIA_CONTENT);
			if (mediaContent != null) {
				mediaContent.setContext(BaseActivity.this);
				BaseActivity.this.showMedia(mediaContent);
			}
		}

	};

	protected void setup(IServiceEventListener aServiceListener, String aLanguage)
	{
		_serviceListener = aServiceListener;
		_language = new LangRetriever(this, aLanguage);
	}

	protected void connectToService(boolean aPlaybackMode)
	{
		try {
			_serviceManager = new ServiceManager(this, aPlaybackMode, _language.getActiveLanguage());
			IntentFilter filter = new IntentFilter(GGService.AUDIO_STATE_EVENT);
			registerReceiver(_audioStateEventReceiver, filter);

			filter = new IntentFilter(GGService.SHOW_MEDIA);
			registerReceiver(_showMediaReceiver, filter);
		}
		catch (Exception ex) {
			ErrorManagerAuditTrail.getInstance().log(this, ex);
			displayMessage(getString(R.string.route_link_to_service));
		}
	}

	protected void showMedia(MediaContent mediaContent) {
		// TODO: Implement this to show the media base upon
		// Call getType() to return MediaType enum that indicate if its a IMAGE or a VIDEO
		Log.d("BaseActivity", "showMedia: " + mediaContent.toString());
	}


	/**
	 * {@inheritDoc }
	 */
	 @Override
	 protected void onCreate( Bundle savedInstanceState ) {
		 super.onCreate( savedInstanceState );
		 super.overridePendingTransition( 0, 0 );
		 // go full screen
		 WindowManager.LayoutParams attrs = super.getWindow().getAttributes();
		 attrs.flags |= WindowManager.LayoutParams.FLAG_FULLSCREEN;
		 super.getWindow().setAttributes(attrs);

		 // Get screen width + height
		 Point size = new Point();
		 WindowManager w = getWindowManager();
		 w.getDefaultDisplay().getSize(size);
		 _width = size.x;
		 _height = size.y;

		 contentRetriever.initializeRetriever(getApplicationContext());
		 stringRetriever = contentRetriever.getContentRetriever();

		 ActionResult processResult = new ProcessManager().process(this);
		 if (processResult != null) {
			 if (processResult.isSuccessful()) {
				 displayMessage(this.getString(R.string.route_process_successful));
			 }
			 else {
				 ErrorManagerAuditTrail.getInstance().log(this, new Exception(processResult.getErrorMessage()));
				 displayMessage(this.getString(R.string.route_process_successful));
			 }
		 }
	 }

	 @Override
	 public void onDestroy()
	 {
		 super.onDestroy();
		 if (_serviceManager != null && _serviceManager.isServiceCreated()) {
			 try
			 {
				 _serviceManager.destroy(this);
			 }
			 catch (Exception ex) {}
			 unregisterReceiver(_audioStateEventReceiver);
			 unregisterReceiver(_showMediaReceiver);
		 }
	 }

	 @Override
	 public void OnAudioStartPlaying() {
		 Log.i(BaseActivity.class.getName(), "OnAudioStartPlaying");
	 }

	 @Override
	 public void OnAudioStopPlaying() {
		 Log.i(BaseActivity.class.getName(), "OnAudioStopPlaying");
	 }

	 @Override
	 public void onAudioPausePlaying() {
		 Log.i(BaseActivity.class.getName(), "onAudioPausePlaying");
	 }

	 @Override
	 public void onAudioResumePlaying() {
		 Log.i(BaseActivity.class.getName(), "onAudioResumePlaying");
	 }

	 @Override
	 public void onAudioFinishPlaying() {
		 Log.i(BaseActivity.class.getName(), "onAudioFinishPlaying");
	 }

	 @Override
	 public void onAudioIncreaseVolume() {
		 Log.i(BaseActivity.class.getName(), "onAudioIncreaseVolume");
	 }

	 @Override
	 public void onAudioDecreaseVolume() {
		 Log.i(BaseActivity.class.getName(), "onAudioDecreaseVolume");
	 }

	 @Override
	 public void onStopAudioRepeat() {
		 Log.i(BaseActivity.class.getName(), "onStopAudioRepeat");
	 }

	 @Override
	 public void onResumeAudioRepeat() {
		 Log.i(BaseActivity.class.getName(), "onResumeAudioRepeat");
	 }

	 @Override
	 public void handleAll(String aAction, String aEvent) {
		 Log.i(BaseActivity.class.getName(), "handleAll: " + aAction + " - " + aEvent);
	 }

	 /**
	  * Click handler for the Header 'Volume' textview.
	  * @param view the 'volume' textview
	  */
	 public final void onVolumeClicked( final View view ) {
		 final AudioManager audio = (AudioManager)super.getSystemService( Context.AUDIO_SERVICE );
		 audio.adjustStreamVolume( AudioManager.STREAM_MUSIC, AudioManager.ADJUST_SAME, AudioManager.FLAG_SHOW_UI );
	 }

	 @Override
	 public boolean onKeyDown(int keyCode, KeyEvent event) {
		 if (keyCode == KeyEvent.KEYCODE_VOLUME_DOWN) {
			 Log.i(BaseActivity.class.getName(), "Physical volume down button is pressed ");
			 this.volumeDownButtonPress();
		 }else if(keyCode == KeyEvent.KEYCODE_VOLUME_UP){
			 Log.i(BaseActivity.class.getName(), "Physical volume up button is pressed ");
			 this.volumeUpButtonPress();
		 }
		 return super.onKeyDown(keyCode, event);
	 }

	 protected void displayMessage(CharSequence aErrorMessage) {
		 AlertDialog alertDialog = new AlertDialog.Builder(this).create();
		 alertDialog.setTitle(super.getString(R.string.app_name));
		 alertDialog.setMessage(aErrorMessage);
		 alertDialog.setButton(DialogInterface.BUTTON_POSITIVE, "OK", new DialogInterface.OnClickListener() {
			 public void onClick(DialogInterface dialog, int which) {
				 return;
			 } });
		 alertDialog.show();
	 }

	 /**
	  * {@inheritDoc }
	  */
	 @Override
	 public void onBackPressed() {
		 super.onBackPressed();
		 super.overridePendingTransition( 0, 0 );
	 }


	 public void volumeDownButtonPress()
	 {
		 Toast.makeText(this,"Volume Down pressed", Toast.LENGTH_SHORT).show();
	 }

	 public void volumeUpButtonPress()
	 {
		 Toast.makeText(this,"Volume Up pressed", Toast.LENGTH_SHORT).show();
	 }

	 public void onHomeButtonClicked( final View view ) {

	 }
	 public void onSearchButtonClicked( final View view ) {

	 }
	 public void onOptionsButtonClicked( final View view ) {

	 }
}
