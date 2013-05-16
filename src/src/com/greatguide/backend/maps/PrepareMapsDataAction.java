package com.greatguide.backend.maps;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import com.greatguide.backend.core.ActionResult;
import com.greatguide.backend.core.AuditTrail;
import com.greatguide.backend.device.StorageManager;
import com.greatguide.backend.location.IXMLLocation;
import com.greatguide.backend.location.LocationBox;
import com.greatguide.backend.location.MyLocation;
import com.greatguide.backend.maps.prepare.xml.XMLLocationReader;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

/**
 */
public class PrepareMapsDataAction extends AsyncTask<PrepareMapData, Void, ActionResult> {

    private Activity _activity;
    private MapView _mapView;
    private Handler _uiDataFeedback;

    public PrepareMapsDataAction(Handler aUIDataFeedback)
    {
        _uiDataFeedback = aUIDataFeedback;
    }

    private Handler newTileHandler = new Handler() {
        public void handleMessage(android.os.Message msg) {
            /* might do this later on
            Bundle bundle = msg.getData();
            int downloadTilesCount = bundle.getInt("downloadTilesCount");
            if (_uiDataFeedback != null) {
                 if (downloadTilesCount <= 0) {
                     sendFeedback("Done preparing map data");
                 }
                 else {
                     sendFeedback("Tiles Left To Download: " + downloadTilesCount);
                 }
            }
            */
            // Ask the _mapView to redraw itself
            if (_mapView != null) {
                _mapView.invalidate();
            }
        }
    };

    private void sendFeedback(String aStatusMessage) {
        Message msg =  new Message();
        Bundle bundle = new Bundle();

        bundle.putString("status", aStatusMessage);
        msg.setData(bundle);
        _uiDataFeedback.sendMessage(msg);
    }

    private ActionResult prepareData(Activity aActivity, int aDisplayWidth, int aDisplayHeight) {
        ActionResult result = new ActionResult();

        _activity = aActivity;

        ActionResult xmlFiles = StorageManager.getInstance().getSDCardPath(_activity, "routes");
        String path = null;
        if (xmlFiles.isSuccessful())
            path = xmlFiles.getValue();

        String boxLocationFile = path + "GPSLocationBox.xml";
        String myLocationFile = path + "GPSMyLocation.xml";

        int totalBoxLocationCount = 0;
        int totalNormalLocationCount = 0;

        XMLLocationReader reader = new XMLLocationReader();
        List<IXMLLocation> boxLocationList = new ArrayList<IXMLLocation>();
        List<IXMLLocation> normalLocationList = new ArrayList<IXMLLocation>();

        if (new File(boxLocationFile).exists()) {
            boxLocationList = reader.getLocationBOXCoordinates(aActivity, boxLocationFile);
            totalBoxLocationCount = boxLocationList.size();
        }
        if (new File(myLocationFile).exists()) {
            normalLocationList = reader.getMyLocationCoordinates(aActivity, myLocationFile);
            totalNormalLocationCount = normalLocationList.size();
        }

        if (totalBoxLocationCount > 0 || totalNormalLocationCount > 0) {

            sendFeedback("Identifying map tiles");

            ActionResult mapDatabase = StorageManager.getInstance().getSDCardPath(_activity, "routes");
            String dbPath = null;
            if (mapDatabase.isSuccessful())
                dbPath = mapDatabase.getValue() + "CptTest.sqlitedb";

            GpsPointBoundCalc boundCalc = new GpsPointBoundCalc();
            _mapView = new MapView(_activity, dbPath, newTileHandler, aDisplayWidth, aDisplayHeight);
            _mapView.setStaticMap(true);
            _mapView.setEnableDownloadMap(true);
            _mapView.setAsyncDownload(false);

            try {
                for (IXMLLocation currentLocation : boxLocationList) {
                        List<MyLocation> coordinateList = boundCalc.coordinatesInBox(currentLocation.getStartLatitude(), currentLocation.getStartLongitude(), currentLocation.getEndLatitude(), currentLocation.getEndLongitude());
                        if (coordinateList != null && coordinateList.size() > 0) {
                            int coordinatesCount = coordinateList.size();
                            sendFeedback("Box Locations - Left To Download: " + coordinatesCount + "/" + totalBoxLocationCount);
                            for (MyLocation currentCoordinateLocation : coordinateList) {
                                for (int i = 15; i < 18; i++) {
                                    _mapView.setZoom(i, false);
                                    _mapView.gotoGpsLocation(currentCoordinateLocation.getLongitude(), currentCoordinateLocation.getLatitude());
                                }
                                coordinatesCount--;
                                sendFeedback("Box Locations - Left To Download: " + coordinatesCount + "/" + totalBoxLocationCount);
                            }
                        }
                        totalBoxLocationCount--;
                }

                for (IXMLLocation currentLocation : normalLocationList) {
                        sendFeedback("Locations - Left To Download: " + totalNormalLocationCount);
                        for (int i = 15; i < 18; i++) {
                            _mapView.setZoom(i, false);
                            _mapView.gotoGpsLocation(currentLocation.getLongitude(), currentLocation.getLatitude());
                        }
                        totalNormalLocationCount--;
                        sendFeedback("Locations - Left To Download: " + totalNormalLocationCount);
                }

            } catch (Exception ex) {
                ex.printStackTrace();
                Log.e(PrepareMapsDataAction.class.getName(), ex.getMessage() + ex.getStackTrace());
                result = new ActionResult(ex);
                AuditTrail.getInstance().log(aActivity, result.getErrorMessage());
                sendFeedback("Error occurred, please see stack trace.");
            }
            finally {
                release();
            }
        }

        if (result.isSuccessful()) {
            sendFeedback("Done with map preparation.");
        }

        return result;
    }

    @Override
    protected ActionResult doInBackground(PrepareMapData... prepareMapDatas) {
        return this.prepareData(prepareMapDatas[0].getActivity(), prepareMapDatas[0].getWidth(), prepareMapDatas[0].getHeight());
    }

    public void release() {
        if (_mapView != null) {
            _mapView.release();
            _mapView = null;
        }
    }
}
