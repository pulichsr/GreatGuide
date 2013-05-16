package com.greatguide.backend.core;

import android.app.Activity;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.location.Location;
import android.os.IBinder;
import android.os.RemoteException;
import com.greatguide.backend.core.IGGService.IGGService;

/**
 *
 * Author: Lennie De Villiers
 * Created: 27 Nov 2012
 */
public class ServiceManager implements IGGService {

    private IGGService _serviceInterface = null;
    private boolean _serviceCreated = false;

    private ServiceConnection _mConnection = new ServiceConnection()
    {
        public void onServiceConnected(ComponentName className, IBinder service) {
            _serviceInterface = IGGService.Stub.asInterface((IBinder)service);
        }

        public void onServiceDisconnected(ComponentName className) {
            _serviceInterface = null;
        }
    };
	
    public ServiceManager(Activity aActivity, boolean aPlaybackMode, String aLanguageCode) 
    {
    	final AppSettings settings = AppSettings.getInstance(aActivity);
    	settings.putPlaybackMode(aPlaybackMode);
    	settings.putLanguageCode(aLanguageCode);
        _serviceCreated = aActivity.bindService(new Intent(aActivity.getBaseContext(), GGService.class), _mConnection, Context.BIND_AUTO_CREATE);
    }
    
    public boolean isServiceCreated() {
        return _serviceCreated;
    }

    @Override
    public void playAudio() throws RemoteException {
        _serviceInterface.playAudio();
    }

    @Override
    public boolean isPlaying() throws RemoteException {
       return _serviceInterface.isPlaying();
    }

    @Override
    public void pause() throws RemoteException {
        _serviceInterface.pause();
    }

    @Override
    public void resume() throws RemoteException {
        _serviceInterface.resume();
    }

    @Override
    public void stop() throws RemoteException {
        _serviceInterface.stop();
    }

    @Override
    public void increaseVolume() throws RemoteException {
        _serviceInterface.increaseVolume();
    }

    @Override
    public void decreaseVolume() throws RemoteException {
        _serviceInterface.decreaseVolume();
    }

    @Override
    public void repeatForever() throws RemoteException {
        _serviceInterface.repeatForever();
    }

    @Override
    public void stopRepeatForever() throws RemoteException {
        _serviceInterface.stopRepeatForever();
    }

    @Override
    public IBinder asBinder() {
        return null;
    }

    public void destroy(Activity aActivity) throws Exception {
      this.destroy();
      aActivity.unbindService(_mConnection);
    }

	@Override
	public void destroy() throws RemoteException {
		_serviceInterface.destroy();
	}

	@Override
	public void sendLocation(Location aLocation) throws RemoteException {
		_serviceInterface.sendLocation(aLocation);
	}
}
