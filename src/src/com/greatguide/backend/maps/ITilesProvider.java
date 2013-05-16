package com.greatguide.backend.maps;

import android.graphics.Rect;

import java.util.Hashtable;

/**
 * Author: Lennie De Villiers
 */
public interface ITilesProvider {
    public Object getTilesLock();
    public void fetchTiles(Rect rect, int zoom);
    public Hashtable<String, MapTile> getTiles();
    public void close();
    public void clear();
    public void release();
    public void setEnableDownloadMap(boolean aDownloadMap);
    public void setMapStateListener(IMapStateListener aStateListener);
    public void setAsyncDownload(boolean aSyncDownload);
}
