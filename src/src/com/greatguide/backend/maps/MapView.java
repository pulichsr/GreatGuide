package com.greatguide.backend.maps;

import java.util.Collection;
import java.util.List;

import android.app.Activity;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Point;
import android.graphics.Rect;
import android.location.Location;
import android.os.Build;
import android.os.Handler;
import android.view.Display;
import android.view.MotionEvent;
import android.view.View;
import android.view.WindowManager;
import com.greatguide.backend.location.Marker;

public class MapView extends View
{
	// Needed to pass to View constructor
	protected Context _context;

	// MapView dimensions
	protected int _viewWidth, _viewHeight;

	// Provides us with _tiles
	protected ITilesProvider _tileProvider;

	// Handles calculations
	protected TilesManager _tileManager;

	// Different paints
	protected Paint _fontPaint;
	protected Paint _bitmapPaint = new Paint();
	protected Paint _circlePaint = new Paint();

	// The location of the view center in longitude, latitude
	protected MapPoint _seekLocation = new MapPoint(0, 0);
	// Location of the phone using Gps data
	protected Location _gpsLocation = null;
	// If true then _seekLocation will always match _gpsLocation
	protected boolean _autoFollow = true;
    private boolean _isStaticMap = false;

	// touch position values kept for panning\dragging
	protected MapPoint _lastTouchPos = new MapPoint(-1, -1);

    private List<Marker> _markerList = null;

    private IMapStateListener _stateListener;

	public MapView(Context context, String dbPath, Handler uiHandler, int _viewWidth, int _viewHeight)
	{
		super(context);
		this._context = context;

        _tileProvider = new TilesProvider(dbPath, uiHandler);

		// These values will be used later
		this._viewWidth = _viewWidth;
		this._viewHeight = _viewHeight;

		// Creating a TilesManager assuming that the tile size is 256*256.
		// You might want to pass tile size as a parameter or even calculate it
		// somehow
		_tileManager = new TilesManager(256, _viewWidth, _viewHeight);

		// Initializes paints
		initPaints();

		// Fetching _tiles from the tilesProvider
		fetchTiles();
	}

	@Override
	protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec)
	{
		// Setting width,height that was passed in the constructor as the view's
		// dimensions
		setMeasuredDimension(_viewWidth, _viewHeight);
	}

	void initPaints()
	{
		// Font paint is used to draw text
		_fontPaint = new Paint();
		_fontPaint.setColor(Color.DKGRAY);
		_fontPaint.setShadowLayer(1, 1, 1, Color.BLACK);
		_fontPaint.setTextSize(20);

		// Used to draw a semi-transparent circle at the phone's gps location
		_circlePaint.setARGB(70, 170, 170, 80);
		_circlePaint.setAntiAlias(true);
	}

	void fetchTiles()
	{
		// Update tilesManager to have the center of the view as its location
		_tileManager.setLocation(_seekLocation.x, _seekLocation.y);

		// Get the visible _tiles indices as a Rect
		Rect visibleRegion = _tileManager.getVisibleRegion();

		// Tell _tiles provider what _tiles we need and which zoom level.
		// The _tiles will be stored inside the tilesProvider.
		// We can get those _tiles later when drawing the view
		_tileProvider.fetchTiles(visibleRegion, _tileManager.getZoom());
	}

	@Override
	protected void onDraw(Canvas canvas)
	{
		// Clear the view to grey
		canvas.drawARGB(255, 100, 100, 100);

		/*
		 * To draw the map we need to find the position of the pixel representing the center of the view.
		 * We need the position to be relative to the full world map, lets call this pixel position "pix"
		 * pix.x will range from 0 to (2^zoom)*tileSize-1, same for pix.y		
		 * To draw anything on the map we subtract pix from the original position
		 * It's just like dragging the map so that the pixel representing the gps location gets into the center of the view 		 
		*/

		// In a square world map,
		// we need to know pix location as two values from 0.0 to 1.0
		MapPoint pixRatio = TilesManager.calcRatio(_seekLocation.x, _seekLocation.y);

		// Full world map width in pixels
		int mapWidth = _tileManager.mapSize() * 256;
		Point pix = new Point((int) (pixRatio.x * mapWidth), (int) (pixRatio.y * mapWidth));

		/*
		 * Subtracting pix from each tile position will result in pix being drawn at the top left corner of the view 
		 * To drag it to the center we add (_viewWidth/2, _viewHeight/2) to the final result
		 * pos.x = pos.x - pix.x + _viewWidth/2f
		 * pos.x = pox.x - (pix.x - _viewWidth/2f)
		 * ---> offset.x =  (pix.x - _viewWidth/2f)
		 * same for offset.y
		 */

		Point offset = new Point((int) (pix.x - _viewWidth / 2f), (int) (pix.y - _viewHeight / 2f));
		// offset is now ready to use

		// Drawing _tiles in a separate function to make the code more readable
		drawTiles(canvas, offset);

		// Draw the marker that pinpoints the user's location
		drawMarker(canvas, offset);
	}

	void drawTiles(Canvas canvas, Point offset)
	{
		/* 
		 * We use the same object in the TilesProvider when drawing
		 * This is necessary to make sure no one changes the available _tiles
		 * while the MapView is being rendered
		 */
		synchronized (_tileProvider.getTilesLock())
		{
			// Get _tiles from the Hashtable inside tilesProvider
			Collection<MapTile> tilesList = _tileProvider.getTiles().values();

			// x,y are the calculated offset

			// Go trough all the available _tiles
			for (MapTile mapTile : tilesList)
			{
				// We act as if we're drawing a map of the whole world at a
				// specific
				// zoom level
				// The top left corner of the map occupies the pixel (0,0) of
				// the
				// view
				int tileSize = _tileManager.getTileSize();
				long tileX = mapTile.x * tileSize;
				long tileY = mapTile.y * tileSize;

				// Subtract offset from the previous calculations
				long finalX = tileX - offset.x;
				long finalY = tileY - offset.y;

				// Draw the bitmap of the _tiles using a simple paint
				canvas.drawBitmap(mapTile.img, finalX, finalY, _bitmapPaint);
			}
		}
	}

	void drawMarker(Canvas canvas, Point offset)
	{
        if (_markerList != null && _markerList.size() > 0) {
            for(Marker aCurrentMarker: _markerList) {
			    // Get marker position in pixels as if we're going to draw it on a
			    // world map where the top left corner of the map occupies the (0,0)
			    // pixel of the view
			    Point markerPos = _tileManager.lonLatToPixelXY(aCurrentMarker.getLongitude(), aCurrentMarker.getLatitude());

			    // Add offset to the marker position
			    int markerX = markerPos.x - offset.x;
			    int markerY = markerPos.y - offset.y;

			    // Draw the marker and make sure you draw the center of the marker
			    // at the marker location

                Bitmap positionMarker = aCurrentMarker.getImage();
			    canvas.drawBitmap(positionMarker, markerX - positionMarker.getWidth() / 2, markerY - positionMarker.getHeight() / 2,
                    _bitmapPaint);

			    // Around the marker we will draw a circle representing the accuracy
			    // of the gps fix
			    // We first calculate its radius

			    // Calculate how many meters one pixel represents
			    float ground = (float) _tileManager.calcGroundResolution(aCurrentMarker.getLatitude());

			    // Location.getAccuracy() returns the accuracy in meters.
			    float rad = aCurrentMarker.getAccuracy() / ground;

			    canvas.drawCircle(markerX, markerY, rad, _circlePaint);

			    // Just drawing location info
			    int pen = 1;
                /* We don't want this
	    		canvas.drawText("lon:" + _gpsLocation.getLongitude(), 0, 20 * pen++, _fontPaint);
		    	canvas.drawText("lat:" + _gpsLocation.getLatitude(), 0, 20 * pen++, _fontPaint);
			    canvas.drawText("alt:" + _gpsLocation.getAltitude(), 0, 20 * pen++, _fontPaint);  */
			    canvas.drawText("Zoom:" + _tileManager.getZoom(), 0, 20 * pen++, _fontPaint);
            }
        }
	}

	@Override
	public boolean onTouchEvent(MotionEvent event)
	{
		int action = event.getAction();
        if (!_isStaticMap) {
		    if (action == MotionEvent.ACTION_DOWN)
		    {
			    // Keep touch position for later use (dragging)
			    _lastTouchPos.x = (int) event.getX();
			    _lastTouchPos.y = (int) event.getY();

			    return true;
		    }
		    else if (action == MotionEvent.ACTION_MOVE)
		    {
			    _autoFollow = false;

			    MapPoint current = new MapPoint(event.getX(), event.getY());

			    // Find how many pixels the users finger moved in both x and y
			    MapPoint diff = new MapPoint(current.x - _lastTouchPos.x, current.y - _lastTouchPos.y);

			    // In a full wolrd map, get the position of the center of the view
			    // in pixels
			    Point pixels1 = _tileManager.lonLatToPixelXY(_seekLocation.x, _seekLocation.y);

			    // Subtract diff from that position
			    Point pixels2 = new Point(pixels1.x - (int) diff.x, pixels1.y - (int) diff.y);

			    // Reconvert the final result to longitude, latitude point
			    MapPoint newSeek = _tileManager.pixelXYToLonLat((int) pixels2.x, (int) pixels2.y);

			    // Finally move the center of the view to the new location
			    _seekLocation = newSeek;

			    // Refresh the view
			    fetchTiles();
			    invalidate(); // Causes the view to redraw itself

			    // Prepare for the next drag event
			    _lastTouchPos.x = current.x;
			    _lastTouchPos.y = current.y;

			    return true;
		    }
        }
		return super.onTouchEvent(event);
	}

    public void placeTheMarkers()
    {
        fetchTiles();
        invalidate();
    }

	public void zoomIn()
	{
        if (_tileManager.allowedToZoomIn()) {
		    _tileManager.zoomIn();
		    onMapZoomChanged();
        }
	}

	public void zoomOut()
	{
        if (_tileManager.allowedToZoomOut()) {
		    _tileManager.zoomOut();
		    onMapZoomChanged();
        }
	}

    protected void onMapZoomChanged()
	{
		_tileProvider.clear();
		fetchTiles();
		invalidate();
	}

    public void setZoom(int zoom)
    {
        setZoom(zoom, true);
    }

    public void setZoom(int zoom, boolean zoomLevelChange)
	{
		_tileManager.setZoom(zoom);
        if (zoomLevelChange) {
		    onMapZoomChanged();
        }
	}

    public void gotoGpsLocation(double longitude, double latitude) {
        _seekLocation.x = longitude;
        _seekLocation.y = latitude;
        fetchTiles();
        invalidate();
        postInvalidate();
    }

    public void setStaticMap(boolean aStaticMap)
    {
        _isStaticMap = aStaticMap;
    }

    public void setMarkers(List<Marker> aMarkList) {
        _markerList = aMarkList;
    }

    public void setMapStateListener(IMapStateListener aStateListener) {
        _stateListener = aStateListener;
        _tileManager.setMapStateListener(aStateListener);
        _tileProvider.setMapStateListener(aStateListener);
    }

    public void setEnableDownloadMap(boolean aFlag) {
        if (_tileProvider != null) {
            _tileProvider.setEnableDownloadMap(aFlag);
        }
    }

    public void release() {
        if (_tileProvider != null) {
            _tileProvider.release();
        }
    }

    public void setAsyncDownload(boolean aSyncDownload) {
        if (_tileProvider != null) {
            _tileProvider.setAsyncDownload(aSyncDownload);
        }
    }
}