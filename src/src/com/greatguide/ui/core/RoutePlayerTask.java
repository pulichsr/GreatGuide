package com.greatguide.ui.core;

import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import com.greatguide.backend.location.playback.PlayRoute;
import com.greatguide.backend.location.playback.PlayRouteLocation;

import java.util.List;
import java.util.TimerTask;

/**
 * Author: Lennie De Villiers
 */
public class RoutePlayerTask extends TimerTask {

    private List<PlayRouteLocation> _routePointList;
    private Handler _handler;
    private int _currentRoutePos;
    private int _currentRoutePosDisp;

    public RoutePlayerTask(PlayRoute aPlayRoute, Handler aUIHandler)
    {
        if (aPlayRoute.getRoutePointList() != null && aPlayRoute.getRoutePointList().size() > 0) {
            _routePointList = aPlayRoute.getRoutePointList();
            _currentRoutePos = 0;
            _currentRoutePosDisp = 1;
        }

        _handler =  aUIHandler;
    }

    @Override
    public void run() {
        if (_routePointList != null && _routePointList.size() > 0) {
            if (_currentRoutePos <= _routePointList.size() - 1) {
                PlayRouteLocation currentRoute = _routePointList.get(_currentRoutePos);
                Message msg = new Message();
                Bundle data = new Bundle();
                data.putDouble("lat", currentRoute.getLatitude());
                data.putDouble("long", currentRoute.getLongitude());
                data.putInt("currentPos", _currentRoutePosDisp);
                data.putInt("totalPos", _routePointList.size());
                data.putFloat("bearing", currentRoute.getBearing());
                msg.setData(data);
                _handler.sendMessage(msg);
                _currentRoutePos++;
                _currentRoutePosDisp++;
            }
        }
    }
}
