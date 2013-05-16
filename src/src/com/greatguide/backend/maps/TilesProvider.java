package com.greatguide.backend.maps;

import java.util.ArrayList;
import java.util.Hashtable;

import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Rect;
import android.os.Handler;
import com.greatguide.backend.maps.web.IDownloadTaskFinishedCallback;
import com.greatguide.backend.maps.web.TileDownloadTask;
import com.greatguide.backend.maps.web.WebTilesProvider;

public class TilesProvider implements ITilesProvider, IDownloadTaskFinishedCallback
{
    WebTilesProvider _webProvider;

    // The database that holds the map
    protected SQLiteDatabase _tilesDB;

    // Tiles will be stored here, the index\key will be in this format x:y
    protected Hashtable<String, MapTile> _tiles = new Hashtable<String, MapTile>();

    // An object to use with synchronized to lock _tiles hashtable
    public Object _tilesLock = new Object();

    // A handler from the outside to be informed of new downloaded _tiles
    // Used to redraw the map view whenever a new tile arrives
    Handler _newTileHandler;

    private boolean _downloadMap = true;

    private IMapStateListener _stateListener;

    public TilesProvider(String dbPath, Handler _newTileHandler)
    {
        /*
           *  Create WebTileProvider with max number of thread equal to five
           *  We also pass this class as a DownloadTaskFinishedCallback
           *  This way when the web provider downloads a tile we get it
           *  and insert it into the database and the hashtable
           */
        _webProvider = new WebTilesProvider(5, this);

        // This time we are opening the database as read\write
        _tilesDB = SQLiteDatabase.openDatabase(dbPath, null, SQLiteDatabase.OPEN_READWRITE);

        // This handler is to be notified when a new tile is downloaded
        // and available for rendering
        this._newTileHandler = _newTileHandler;
    }

    @Override
    public Object getTilesLock() {
        return _tilesLock;
    }

    // Updates the _tiles in the hashtable
    public void fetchTiles(Rect rect, int zoom)
    {
        // We are using a separate object here for synchronizing
        // Using the hashtable _tiles will cause problems when we swap the
        // pointers temp and _tiles
        synchronized (_tilesLock)
        {
            // Max tile index for x and y
            int maxIndex = (int) Math.pow(2, zoom) - 1;

            // First we create a list containing the index of each tile inside
            // the rectangle rect, we're expecting to find these _tiles in memory
            // or
            // in the database
            ArrayList<String> expectedTiles = new ArrayList<String>();
            for (int x = rect.left; x <= rect.right; x++)
            {
                // Ignore _tiles with invalid index
                if (x < 0 || x > maxIndex) continue;
                for (int y = rect.top; y <= rect.bottom; y++)
                {
                    if (y < 0 || y > maxIndex) continue;
                    expectedTiles.add(x + ":" + y);
                }
            }

            // Perpare the query for the database
            String query = "SELECT x,y,image FROM tiles WHERE x >= " + rect.left + " AND x <= " + rect.right + " AND y >= " + rect.top
                    + " AND y <=" + rect.bottom + " AND z == " + (17 - zoom);

            // query should be something like:
            // SELECT x,y,image FROM _tiles WHERE x>=0 AND x<=4 AND y>=2 AND
            // y<=6
            // AND
            // z==6

            Cursor cursor;
            cursor = _tilesDB.rawQuery(query, null);

            // Now cursor contains a table with these columns
            /*
                * x(int)	y(int)	image(byte[])
                */

            // Prepare an empty hash table to fill with the _tiles we fetched
            Hashtable<String, MapTile> temp = new Hashtable<String, MapTile>();

            // Loop through all the rows(_tiles) of the table returned by the
            // query
            // MUST call moveToFirst
            if (cursor.moveToFirst())
            {
                do
                {
                    // Getting the index of this mapTile
                    int x = cursor.getInt(0);
                    int y = cursor.getInt(1);

                    // Try to get this mapTile from the hashtable we have
                    MapTile mapTile = _tiles.get(x + ":" + y);

                    // If This is a new mapTile, we didn't fetch it in the
                    // previous
                    // fetchTiles call.
                    if (mapTile == null)
                    {
                        // Get the binary image data from the third cursor
                        // column
                        byte[] img = cursor.getBlob(2);

                        // Create a bitmap (expensive operation)
                        Bitmap tileBitmap = BitmapFactory.decodeByteArray(img, 0, img.length);

                        // Create the new mapTile
                        mapTile = new MapTile(x, y, tileBitmap);
                    }

                    // The object "mapTile" should now be ready for rendering

                    // Add the mapTile to the temp hashtable
                    temp.put(x + ":" + y, mapTile);
                }
                while (cursor.moveToNext()); // Move to next tile in the
                // query

                // The hashtable "_tiles" is now outdated,
                // so clear it and set it to the new hashtable temp.

                /*
                     * Swapping here sometimes creates an exception if we use
                     * _tiles for synchronizing
                     */
                _tiles.clear();
                _tiles = temp;
            }

            // Remove the _tiles we have from the ones to download
            for (MapTile t : _tiles.values())
            {
                expectedTiles.remove(t.x + ":" + t.y);
            }

            // Download the _tiles we couldn't find
            if (_downloadMap) {
                for (String string : expectedTiles)
                {
                    int x = 0, y = 0;
                    String[] nums = string.split(":");
                    x = Integer.parseInt(nums[0]);
                    y = Integer.parseInt(nums[1]);
                    _webProvider.downloadTile(x, y, zoom);
                }
            }
            else
            {
                if (_stateListener != null && expectedTiles != null && expectedTiles.size() > 0) {
                    _stateListener.noMapsAvailable(expectedTiles);
                }
            }
        }
    }

    // Gets the hashtable where the _tiles are stored
    public Hashtable<String, MapTile> getTiles()
    {
        return _tiles;
    }

    public void close()
    {
        // If fetchTiles is used after closing it will not work, it will throw
        // an exception
        _tilesDB.close();
    }

    public void clear()
    {
        // Make sure no other thread is using the hashtable before clearing it
        synchronized (_tilesLock)
        {
            _tiles.clear();
        }

        // Cancel all download operations
        _webProvider.cancelDownloads();
    }

    // Called by the WebTilesProvider when a tile was downloaded successfully
    // Also note that it's marked as synchronized to make sure that we only
    // handle one
    // finished task at a time, since the WebTilesProvider will call this method
    // whenever
    // a task is finished
    @Override
    public synchronized void handleDownload(TileDownloadTask task)
    {
        byte[] tile = task.getFile();
        int x = task.getX();
        int y = task.getY();

        // Log.d("TAG", "Downloaded " + x + ":" + y);

        // Insert tile into database as an array of bytes
        insertTileToDB(x, y, 17 - task.getZ(), tile);

        // Creating bitmaps may throw OutOfMemoryError
        try
        {
            Bitmap bm = BitmapFactory.decodeByteArray(tile, 0, tile.length);
            MapTile t = new MapTile(x, y, bm);

            // Add the new tile to our _tiles memory cache
            synchronized (_tilesLock)
            {
                _tiles.put(x + ":" + y, t);
            }

            // Here we inform who ever interested that we have a new tile
            // ready to be rendered!
            // The handler is in the MapAppActivity and sending it a message
            // will cause it to redraw the MapView
            if (_newTileHandler != null) _newTileHandler.sendEmptyMessage(0);
        }
        catch (OutOfMemoryError e)
        {
            // At least we got the tile as byte array and saved it in the
            // database
        }
    }

    // Marked as synchronized to prevent to insert operations at the same time
    synchronized void insertTileToDB(int x, int y, int z, byte[] tile)
    {
        ContentValues vals = new ContentValues();
        vals.put("x", x);
        vals.put("y", y);
        vals.put("z", z);
        vals.put("image", tile);
        _tilesDB.insert("tiles", null, vals);
    }

    public void release() {
        this.close();
        this.clear();
    }

    public void setEnableDownloadMap(boolean aDownloadMap) {
        _downloadMap = aDownloadMap;
    }

    public void setMapStateListener(IMapStateListener aStateListener) {
        _stateListener = aStateListener;
    }

    @Override
    public void setAsyncDownload(boolean aSyncDownload) {
        if (_webProvider != null) {
            _webProvider.setAsyncDownload(aSyncDownload);
        }
    }
}