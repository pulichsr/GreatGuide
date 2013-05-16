package com.greatguide.backend.maps.web;

import java.util.HashSet;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class WebTilesProvider implements IDownloadTaskFinishedCallback
{
    // Max number of active download threads
    // A large number of threads might get
    // you blocked from the _tiles server.
    final int _threadsCount;

    // Keeping track of current non-finished tasks
    // to avoid downloading a tile more than once
    HashSet<String> _pendingRequests = new HashSet<String>();

    ExecutorService _threadPool; // Handles requests

    // A callback to be called by finished\failed tasks
    IDownloadTaskFinishedCallback _uiCallBackHandler;

    private boolean _asyncDownload = true;

    public WebTilesProvider(int threadsCount, IDownloadTaskFinishedCallback _uiCallBackHandler)
    {
        this._threadsCount = threadsCount;

        if (_asyncDownload) {
		    _threadPool = Executors.newFixedThreadPool(threadsCount);
        }

        this._uiCallBackHandler = _uiCallBackHandler;
    }

    public void downloadTile(int x, int y, int z)
    {
        // Get the url in the right format
        String url = formatUrl(x, y, z);

        // Whenever using the HashSet _pendingRequests we must
        // make sure that no other thread is using it, we do that by
        // using synchronized on the set whenever a code block uses the set
        synchronized (_pendingRequests)
        {
            // If tile isn't being downloaded then add it
            if (!_pendingRequests.contains(url))
            {
                _pendingRequests.add(url);

                // Create a new task and execute it in a separate thread
                TileDownloadTask task = new TileDownloadTask(url, this, x, y, z);
                if (_asyncDownload) {
                	    _threadPool.execute(task);
                }
                else {
                    task.run();
                }
            }
        }
    }

    String formatUrl(int x, int y, int z)
    {
        // Here we're using open street map _tiles, you can replace it with the
        // server you want
        // Just make sure you have the right to download the _tiles
        // Also note the zxy order for the _tiles!
        String result = String.format("http://a.tile.openstreetmap.org/%s/%s/%s.png", z, x, y);

        return result;
    }

    // This function should be called when the TilesProvider has
    // received and processed the tile, it should be called even when the
    // download fails, otherwise
    // the request will be stuck in pendingRequest without actually being
    // executed!
    // leaving the tile blank.
    private void removeRequestFromPending(String url)
    {
        // Making sure no other thread is using the set
        synchronized (_pendingRequests)
        {
            _pendingRequests.remove(url);
        }
    }

    // Called by a TileDownloadTask when finished
    @Override
    public synchronized void handleDownload(TileDownloadTask task)
    {
        int state = task.getState();

        // If downloaded successfully
        if (state == TileDownloadTask.TASK_COMPLETE)
        {
            // Pass the task to the TilesProvider
            if (_uiCallBackHandler != null) _uiCallBackHandler.handleDownload(task);
        }
        else if (state == TileDownloadTask.TASK_FAILED)
        {
            // Do nothing!!
        }

        // It's necessary to remove the request from pending list
        // We only remove it when we are done with it, otherwise the MapView
        // could request the tile while it's being inserted in the database for
        // example.
        // This way we make sure we download the tile only once.
        removeRequestFromPending(task.getUrl());
    }

    // Hopefully kills the active download tasks and clears all pending tasks
    public void cancelDownloads()
    {
        if (_threadPool != null) {
            _threadPool.shutdownNow();
        }

        synchronized (_pendingRequests)
        {
            _pendingRequests.clear();
        }

        // Cannot reuse ExecutorService after calling shutdownNow
        // Create a new executor
        if (_threadPool != null) {
		    _threadPool = Executors.newFixedThreadPool(_threadsCount);
        }
    }

    public void setAsyncDownload(boolean aSyncDownload) {
        _asyncDownload =  aSyncDownload;
    }
}