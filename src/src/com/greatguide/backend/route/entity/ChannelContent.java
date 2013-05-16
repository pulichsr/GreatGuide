package com.greatguide.backend.route.entity;

import java.io.Serializable;
import java.util.List;

import android.content.Context;

import com.greatguide.backend.route.dao.DBManager;
import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "ChannelContent")
public class ChannelContent implements Serializable {

	private static final long serialVersionUID = 1769809922361541343L;
	             
	public static final String GPS_REGION_ID = "ID";     
	public static final String CHANNEL_CONTENT_ID = "CHANNEL_CONTENT_ID";    
           
	@DatabaseField(columnName = "ID", generatedId = false)
	private int _id;
	@DatabaseField(columnName = "GPS_REGION_ID", canBeNull = false)
	private int _gpsRegionID;
	@DatabaseField(columnName = "TRIGGER_TYPE", canBeNull = false)
	private String _triggerType;
	@DatabaseField(columnName = "DIRECTION_NUMBER", canBeNull = false)
	private int _directionNumber;
	@DatabaseField(columnName = "MAX_PRESENTED_COUNT", canBeNull = false)
	private int _maxPresentedCount;
	@DatabaseField(columnName = "PRIORITY", canBeNull = false)
	private int _priority;
	@DatabaseField(columnName = "ACTIVE_DAYS", canBeNull = false)
	private int _activeDays;
	@DatabaseField(columnName = "HEADING", canBeNull = true)
	private String _heading;
	@DatabaseField(columnName = "HEADING_VARIANCE", canBeNull = true)
	private String _headingVariance;
	@DatabaseField(columnName = "AUTO_PRESENT", canBeNull = false)
	private int _autoPresent;
	@DatabaseField(columnName = "IS_SEQUENCED", canBeNull = false)
	private int _isSequenced;
	
	private transient List<GpsRegion> _gpsRegionList;
	private transient List<ContentItem> _contentItemList;

	ChannelContent() {
	}

	public ChannelContent(int id, int gpsRegionID, String triggerType,
			int directionNumber, int maxPresentedCount, int priority,
			int activeDays, String heading, String headingVariance,
			int autoPresent, int isSequenced) {
		_id = id;
		_gpsRegionID = gpsRegionID;
		_triggerType = triggerType;
		_directionNumber = directionNumber;
		_maxPresentedCount = maxPresentedCount;
		_priority = priority;
		_activeDays = activeDays;
		_heading = heading;
		_headingVariance = headingVariance;
		_autoPresent = autoPresent;
		_isSequenced = isSequenced;
	}
	
    public Boolean isAutoPlay(){
		      
		Boolean autoPlayFalg = false;
		      
		if(_autoPresent == 1){   
			autoPlayFalg = true;
		}else{
			autoPlayFalg = false;
		}      
		   
		return autoPlayFalg;
	}
                
	public List<GpsRegion> getRegion(Context _aContext)
			throws Exception {
   
		if (_gpsRegionList == null) {
			// load the region by the channelContentID   
			_gpsRegionList = DBManager.getInstance().getRegion(_id, _aContext);
		}
		return _gpsRegionList;
	}   
	
	public List<ContentItem> getContentItem(Context _aContext)
			throws Exception {
       
		if (_contentItemList == null) {
			// load the contentitem by the channelContentID   
			_contentItemList = DBManager.getInstance().getContentItem(_id, _aContext);
		}   
		return _contentItemList;
	}    

	public String getHeading() {
		return _heading;
	}

	public void setHeading(String heading) {
		this._heading = heading;
	}

	public String getHeadingVariance() {
		return _headingVariance;
	}

	public void setHeadingVariance(String headingVariance) {
		this._headingVariance = headingVariance;
	}

	public int getId() {
		return _id;
	}

	public void setId(int id) {
		this._id = id;
	}

	public int getGpsRegionID() {
		return _gpsRegionID;
	}

	public void setGpsRegionID(int gpsRegionID) {
		this._gpsRegionID = gpsRegionID;
	}

	public String getTriggerType() {
		return _triggerType;
	}

	public void setTriggerType(String triggerType) {
		this._triggerType = triggerType;
	}

	public int getDirectionNumber() {
		return _directionNumber;
	}

	public void setDirectionNumber(int directionNumber) {
		this._directionNumber = directionNumber;
	}

	public int getMaxPresentedCount() {
		return _maxPresentedCount;
	}

	public void setMaxPresentedCount(int maxPresentedCount) {
		this._maxPresentedCount = maxPresentedCount;
	}

	public int getPriority() {
		return _priority;
	}

	public void setPriority(int priority) {
		this._priority = priority;
	}

	public int getActiveDays() {
		return _activeDays;
	}

	public void setActiveDays(int activeDays) {
		this._activeDays = activeDays;
	}

	public int getAutoPresent() {
		return _autoPresent;
	}

	public void setAutoPresent(int autoPresent) {
		this._autoPresent = autoPresent;
	}

	public int getIsSequenced() {
		return _isSequenced;
	}

	public void setIsSequenced(int isSequenced) {
		this._isSequenced = isSequenced;
	}

}
