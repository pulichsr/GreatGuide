/**
 * 
 */
package com.greatguide.backend.route.entity;

import java.io.Serializable;
import java.util.List;

import android.content.Context;

import com.greatguide.backend.route.dao.DBManager;
import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

/**
 * @author sandeep
 * 
 */

@DatabaseTable(tableName = "MasterArea")
public class MasterArea implements Serializable {
	    
	private static final long serialVersionUID = 1L;
	
	public static final String MASTER_AREA_ID = "MASTER_AREA_ID";

	@DatabaseField(columnName = "ID", generatedId = false)
	private int _id;
	@DatabaseField(columnName = "NAME", canBeNull = false)
	private String _name;
	@DatabaseField(columnName = "CHANNEL_GROUP_ID", canBeNull = false)
	private int _channelGroupID;
	@DatabaseField(columnName = "CONTENT_BASE_PATH", canBeNull = false)
	private String _contentBasePath;
	@DatabaseField(columnName = "REGION_DATA", canBeNull = false)
	private String _regionData;
	@DatabaseField(columnName = "MIN_LATITUDE", canBeNull = false)
	private Double _minLatitude;
	@DatabaseField(columnName = "MAX_LATITUDE", canBeNull = false)
	private Double _maxLatitude;
	@DatabaseField(columnName = "MIN_LONGITUDE", canBeNull = false)
	private Double _minLongitude;
	@DatabaseField(columnName = "MAX_LONGITUDE", canBeNull = false)
	private Double _maxLongitude;
	@DatabaseField(columnName = "REGION_TYPE", canBeNull = false)
	private String _regionType;
	
	private transient List<GpsRegion> _gpsRegionList;
   
	MasterArea() {

	}

	public MasterArea(int id, String name, int channelGroupID,
			String contentBasePath, String regionData, Double minLatitude,
			Double maxLatitude, Double minLongitude, Double maxLongitude,
			String regionType) {

		_id = id;
		_name = name;
		_channelGroupID = channelGroupID;
		_contentBasePath = contentBasePath;
		_regionData = regionData;
		_minLatitude = minLatitude;
		_maxLatitude = maxLatitude;
		_minLongitude = minLongitude;
		_maxLongitude = maxLongitude;
		_regionType = regionType;

	}  
      
	public List<GpsRegion> getRegionByMasterAreaID(Context _aContext) throws Exception {
  
		if (_gpsRegionList == null) {
			// load the region  by the masterAreaID
		   	_gpsRegionList = DBManager.getInstance().getRegionByMasterAreaID(_id,_aContext); 
		}         
		return _gpsRegionList;
	}
              
	public int getId() {
		return _id;
	}

	public void setId(int id) {
		this._id = id;
	}

	public String getName() {
		return _name;
	}

	public void setName(String name) {
		this._name = name;
	}

	public int getChannelGroupID() {
		return _channelGroupID;
	}

	public void setChannelGroupID(int channelGroupID) {
		this._channelGroupID = channelGroupID;
	}

	public String getContentBasePath() {
		return _contentBasePath;
	}

	public void setContentBasePath(String contentBasePath) {
		this._contentBasePath = contentBasePath;
	}

	public String getRegionData() {
		return _regionData;
	}

	public void setRegionData(String regionData) {
		this._regionData = regionData;
	}

	public Double getMinimunLatitude() {
		return _minLatitude;
	}

	public void setMinimunLatitude(Double minimunLatitude) {
		this._minLatitude = minimunLatitude;
	}

	public Double getMaxLatitude() {
		return _maxLatitude;
	}

	public void setMaxLatitude(Double maxLatitude) {
		this._maxLatitude = maxLatitude;
	}

	public Double getMinLongitude() {
		return _minLongitude;
	}

	public void setMinLongitude(Double minLongitude) {
		this._minLongitude = minLongitude;
	}

	public Double getMaxLongitude() {
		return _maxLongitude;
	}

	public void setMaxLongitude(Double maxLongitude) {
		this._maxLongitude = maxLongitude;
	}

	public String getRegionType() {
		return _regionType;
	}

	public void setRegionType(String regionType) {
		this._regionType = regionType;
	}

}
