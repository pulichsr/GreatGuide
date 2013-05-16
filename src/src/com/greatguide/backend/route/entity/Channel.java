package com.greatguide.backend.route.entity;

import java.io.Serializable;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;


@DatabaseTable(tableName = "CHANNEL")
public class Channel implements Serializable{   
	
	private static final long serialVersionUID = -6180192085753558236L;

	public static final String CHANNEL_GROUP_ID = "CHANNEL_GROUP_ID";  
	   
	@DatabaseField(columnName = "ID",generatedId = false)
	private int _id;   
	@DatabaseField(columnName = "CHANNEL_GROUP_ID", canBeNull = false)
	private int _channelGroupID;   
	@DatabaseField(columnName = "CONTENT_PATH", canBeNull = false)
	private String _contentPath;
	@DatabaseField(columnName = "LANGUAGE", canBeNull = false)
	private String _language;      
	
	Channel(){
	}   

	public Channel(int id,int channelGroupID,String contentPath,String language){
		_id = id;
		_channelGroupID = channelGroupID;
		_contentPath = contentPath;
		_language = language;
		
	}      
        
	
	      
	public int getid() {
		return _id;   
	}

   
	public void setid(int id) {
		this._id = id;
	}


	public int getchannelGroupID() {
		return _channelGroupID;
	}


	public void setchannelGroupID(int channelGroupID) {
		this._channelGroupID = channelGroupID;
	}


	public String getContentPath() {
		return _contentPath;
	}


	public void setContentPath(String contentPath) {
		this._contentPath = contentPath;
	}


	public String getLanguage() {
		return _language;
	}
   

	public void setLanguage(String language) {
		this._language = language;
	}


}
