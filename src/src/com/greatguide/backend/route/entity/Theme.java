package com.greatguide.backend.route.entity;

import java.io.Serializable;
import java.util.List;

import android.content.Context;

import com.greatguide.backend.route.dao.DBManager;
import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "Theme")
public class Theme implements Serializable {    
	   
	private static final long serialVersionUID = -1709119195061003765L;

	public static final String THEME_ID = "THEME_ID";
            
	@DatabaseField(columnName = "ID",generatedId = false)
	private int _id;
	@DatabaseField(columnName = "NAME", canBeNull = false)
	private String _name;  
	private transient List<GpsRegion> _gpsRegionList;
	   
	Theme() {
    }
    
	public Theme(int id, String name) {
		_id = id;
		_name = name;
	}
	
	public List<GpsRegion> getRegion(Context _aContext) throws Exception {
		  
		if (_gpsRegionList == null) {
			// load the region  by the masterAreaID
		   	_gpsRegionList = DBManager.getInstance().getRegionByThemeID(_id,_aContext); 
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

	
}
