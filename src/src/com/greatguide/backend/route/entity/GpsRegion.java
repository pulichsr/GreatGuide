package com.greatguide.backend.route.entity;

import java.io.Serializable;
import java.util.List;

import android.content.Context;

import com.greatguide.backend.route.dao.DBManager;
import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

  
@DatabaseTable(tableName = "GpsRegion")
public class GpsRegion implements Serializable {
	
	private static final long serialVersionUID = -9022027376406564922L;
	
	public static final String GPS_REGION_ID = "GPS_REGION_ID";    
	
	@DatabaseField(columnName = "ID",generatedId = false)
	private int _id;     
	@DatabaseField(columnName = "MASTER_AREA_ID", canBeNull = false)
	private int _masterAreaId;  
	@DatabaseField(columnName = "THEME_ID", canBeNull = false)
	private int _themeId;      
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
	@DatabaseField(columnName = "RESET_ONENTRY", canBeNull = false)
	private String _resetOnEntry;   
	
	private transient List<ChannelContent> _channelContentList;
	   
	GpsRegion(){
	}      
	
	public GpsRegion(int id,int masterAreaId,int themeId,String regionData,Double minLatitude,Double maxLatitude,
			Double minLongitude,Double maxLongitude,String regionType,String resetOnEntry){
		_id = id;  
		_masterAreaId = masterAreaId;
		_themeId = themeId;
		_regionData = regionData;
		_minLatitude = minLatitude;
		_maxLatitude = maxLatitude;
		_minLongitude = minLongitude;
		_maxLongitude = maxLongitude;       
		_regionType = regionType;
		_resetOnEntry = resetOnEntry;   
		
	} 
	
	public List<ChannelContent> getChannelContent(Context _aContext)
			throws Exception {
       
		if (_channelContentList == null) {   
			// load the channelcontent by the regionid   
			_channelContentList = DBManager.getInstance().getChannelContent(_id, _aContext);
		}   
		return _channelContentList;
	} 
	   
	 
	public int getId() {
		return _id;
	}

	public void setId(int id) {
		this._id = id;
	}

	public int getMasterAreaId() {
		return _masterAreaId;
	}

	public void setMasterAreaId(int masterAreaId) {
		this._masterAreaId = masterAreaId;
	}
   
	public int getThemeId() {
		return _themeId;
	}

	public void setThemeId(int themeId) {
		this._themeId = themeId;
	}

	public String getRegionData() {
		return _regionData;
	}

	public void setRegionData(String regionData) {
		this._regionData = regionData;
	}

	public Double getMinLatitude() {
		return _minLatitude;
	}

	public void setMinLatitude(Double minLatitude) {
		this._minLatitude = minLatitude;
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

	public String getResetOnEntry() {
		return _resetOnEntry;
	}

	public void setResetOnEntry(String resetOnEntry) {
		this._resetOnEntry = resetOnEntry;
	}

	

}
