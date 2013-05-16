package com.greatguide.backend.route.entity;

import java.io.Serializable;
import java.util.List;

import android.content.Context;

import com.greatguide.backend.route.dao.DBManager;
import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

@DatabaseTable(tableName = "ChannelGroup")  
public class ChannelGroup implements Serializable {

	private static final long serialVersionUID = 4198700006418005663L;

	public static final String CHANNEL_GROUP_ID = "CHANNEL_GROUP_ID";      
	   
	@DatabaseField(columnName = "ID",generatedId = false)
	private int _id;
	@DatabaseField(columnName = "NAME", canBeNull = false)
	private String _name;   
	
	private transient List<Channel> _channelList;
	private transient List<MasterArea> _masterAreaList;  
	private transient List<ContentItem> _contentItemList;
	   
	ChannelGroup() {  
    }
	                              
	public ChannelGroup(int id, String name) {
		_id = id;
		_name = name;
	}           
	                                  
	public List<Channel> getChannel(Context _aContext) throws Exception {
          
		if (_channelList == null) {        
			// load the channelList by the channelgroup id    
			_channelList = DBManager.getInstance().getChannel(_id,_aContext); 
		}     
		      
		return _channelList;
	}
	
	public List<ContentItem> getContentItem(Context _aContext)
			throws Exception {
           
		if (_contentItemList == null) {
			// load the contentitem by the channelgroupid      
			_contentItemList = DBManager.getInstance().getContentItemByGroupID(_id, _aContext);
		}   
		return _contentItemList;
	}    
	   
	public List<MasterArea> getMasterArea(Context _aContext) throws Exception {
        
		if (_masterAreaList == null) {        
			// load the channelList by the channelgroup id    
			_masterAreaList = DBManager.getInstance().getMasterArea(_id,_aContext); 
		}     
		         
		return _masterAreaList;
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
