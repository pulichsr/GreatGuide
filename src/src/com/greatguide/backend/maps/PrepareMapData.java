package com.greatguide.backend.maps;

import android.app.Activity;

/**
 */
public class PrepareMapData {

    private Activity _activity;
    private int _width;
    private int _height;

    public PrepareMapData(Activity aActivity, int aWidth, int aHeight) {
        _activity = aActivity;
        _width = aWidth;
        _height = aHeight;
    }

    public Activity getActivity() {
        return _activity;
    }

    public void setActivity(Activity _activity) {
        this._activity = _activity;
    }

    public int getWidth() {
        return _width;
    }

    public void setWidth(int _width) {
        this._width = _width;
    }

    public int getHeight() {
        return _height;
    }

    public void setHeight(int _height) {
        this._height = _height;
    }
}
