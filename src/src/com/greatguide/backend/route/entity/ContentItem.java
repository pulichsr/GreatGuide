package com.greatguide.backend.route.entity;

import java.io.Serializable;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

      
@DatabaseTable(tableName = "ContentItem")
public class ContentItem implements Serializable{
	
	private static final long serialVersionUID = 6936624413741919486L;
	
	@DatabaseField(columnName = "ID",generatedId = false)      
	private int _id;     
	@DatabaseField(columnName = "CHANNEL_CONTENT_ID", canBeNull = false)
	private int _channelContentId;  
	@DatabaseField(columnName = "CHANNEL_GROUP_ID", canBeNull = false)
	private int _channelGroupId;  
	@DatabaseField(columnName = "CONTENT_TYPE_CODE", canBeNull = false)
	private String _contentTypeCode;      
	@DatabaseField(columnName = "FILE_NAME", canBeNull = false)
	private String _filename; 
	   
	ContentItem(){   
		
	}
	
	public ContentItem(int id,int channelContentId,int channelGroupId,String contentTypeCode,String filename){
		_id = id;  
		_channelContentId = channelContentId;
		_channelGroupId = channelGroupId;
		_contentTypeCode = contentTypeCode;
		_filename = filename;   
	} 
	
	public Boolean isVideo(){
		
		Boolean isVideoFalg = false;
		if(_contentTypeCode.equals("V")){
			isVideoFalg = true;
		}else{
			isVideoFalg = false;
		}
		return isVideoFalg;
	}  
	           
	public int getId() {
		return _id;
	}
	public void setId(int id) {
		this._id = id;
	}
	public int getChannelContentId() {
		return _channelContentId;
	}
	public void setChannelContentId(int channelContentId) {
		this._channelContentId = channelContentId;
	}
	public int getChannelGroupId() {
		return _channelGroupId;
	}
	public void setChannelGroupId(int channelGroupId) {
		this._channelGroupId = channelGroupId;
	}
	public String getContentTypeCode() {
		return _contentTypeCode;
	}
	public void setContentTypeCode(String contentTypeCode) {
		this._contentTypeCode = contentTypeCode;
	}
	public String getFilename() {
		return _filename;
	}
	public void setFilename(String filename) {
		this._filename = filename;
	}   
   
}  
