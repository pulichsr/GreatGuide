package com.greatguide.backend.route.dao;

import java.util.List;

import android.content.Context;

import com.greatguide.backend.route.entity.Channel;
import com.greatguide.backend.route.entity.ChannelContent;
import com.greatguide.backend.route.entity.ChannelGroup;
import com.greatguide.backend.route.entity.ContentItem;
import com.greatguide.backend.route.entity.GpsRegion;
import com.greatguide.backend.route.entity.MasterArea;
import com.greatguide.backend.route.entity.Theme;
import com.j256.ormlite.dao.Dao;
import com.j256.ormlite.dao.DaoManager;
import com.j256.ormlite.support.ConnectionSource;

public class DBManager {

	private static DBManager _dbManager = null;
	private ConnectionSource connectionSource;

	public static synchronized DBManager getInstance() {
		if (_dbManager == null) {
			_dbManager = new DBManager();
		}

		return _dbManager;
	}

	/**
	 * 
	 * @param _id
	 * @param _aContext
	 * @return
	 * @throws Exception
	 */

	public List<GpsRegion> getRegionByMasterAreaID(int _id, Context _aContext)
			throws Exception {

		if (connectionSource == null)
			connectionSource = DataBaseHelper.getInstance().getConnection(
					_aContext);
		Dao<GpsRegion, Integer> _gpsRegionDao = DaoManager.createDao(
				connectionSource, GpsRegion.class);
		List<GpsRegion> _gpsRegionList = _gpsRegionDao.queryBuilder().where()
				.eq(MasterArea.MASTER_AREA_ID, _id).query();

		return _gpsRegionList;
	}

	public List<GpsRegion> getRegion(int _id, Context _aContext)
			throws Exception {

		if (connectionSource == null)
			connectionSource = DataBaseHelper.getInstance().getConnection(
					_aContext);
		Dao<GpsRegion, Integer> gpsRegionDao = DaoManager.createDao(
				connectionSource, GpsRegion.class);
		List<GpsRegion> gpsRegionList = gpsRegionDao.queryBuilder().where()
				.eq(ChannelContent.GPS_REGION_ID, _id).query();

		return gpsRegionList;
	}

	/**
	 * 
	 * @param _id
	 * @param _aContext
	 * @return
	 * @throws Exception
	 */
	public List<GpsRegion> getRegionByThemeID(int _id, Context _aContext)
			throws Exception {

		if (connectionSource == null)
			connectionSource = DataBaseHelper.getInstance().getConnection(
					_aContext);
		Dao<GpsRegion, Integer> gpsRegionDao = DaoManager.createDao(
				connectionSource, GpsRegion.class);
		List<GpsRegion> gpsRegionList = gpsRegionDao.queryBuilder().where()
				.eq(Theme.THEME_ID, _id).query();

		return gpsRegionList;
	}

	/**
	 * 
	 * @param _id
	 * @param _aContext
	 * @return
	 * @throws Exception
	 */
	public List<Channel> getChannel(int _id, Context _aContext)
			throws Exception {

		if (connectionSource == null)
			connectionSource = DataBaseHelper.getInstance().getConnection(
					_aContext);
		Dao<Channel, Integer> channelDao = DaoManager.createDao(
				connectionSource, Channel.class);
		List<Channel> channelList = channelDao.queryBuilder().where()
				.eq(ChannelGroup.CHANNEL_GROUP_ID, _id).query();

		return channelList;   
	}
	    
	/**    
	 * 
	 * @param _id
	 * @param _aContext
	 * @return
	 * @throws Exception
	 */
	public List<MasterArea> getMasterArea(int _id, Context _aContext)
			throws Exception {

		if (connectionSource == null)
			connectionSource = DataBaseHelper.getInstance().getConnection(
					_aContext);
		Dao<MasterArea, Integer> masterAreaDao = DaoManager.createDao(
				connectionSource, MasterArea.class);   
		List<MasterArea> masterAreaList = masterAreaDao.queryBuilder().where()
				.eq(ChannelGroup.CHANNEL_GROUP_ID, _id).query();

		return masterAreaList;   
	}
	
	/**
	 * 
	 * @param _id
	 * @param _aContext
	 * @return
	 * @throws Exception
	 */
	public List<ContentItem> getContentItem(int _id, Context _aContext)
			throws Exception {
                              
		if (connectionSource == null)
			connectionSource = DataBaseHelper.getInstance().getConnection(
					_aContext);    
		Dao<ContentItem, Integer> contentItemDao = DaoManager.createDao(
				connectionSource, ContentItem.class);   
		List<ContentItem> contentItemList = contentItemDao.queryBuilder().where()
				.eq(ChannelContent.CHANNEL_CONTENT_ID, _id).query();
          
		return contentItemList;   
	}
	
	/**
	 * 
	 * @param _id
	 * @param _aContext
	 * @return
	 * @throws Exception
	 */
	public List<ContentItem> getContentItemByGroupID(int _id, Context _aContext)
			throws Exception {
                              
		if (connectionSource == null)
			connectionSource = DataBaseHelper.getInstance().getConnection(
					_aContext);    
		Dao<ContentItem, Integer> contentItemDao = DaoManager.createDao(
				connectionSource, ContentItem.class);   
		List<ContentItem> contentItemList = contentItemDao.queryBuilder().where()
				.eq(ChannelGroup.CHANNEL_GROUP_ID, _id).query();
             
		return contentItemList;   
	}
	
	
	/**
	 * 
	 * @param _id
	 * @param _aContext
	 * @return
	 * @throws Exception
	 */
	public List<ChannelContent> getChannelContent(int _id, Context _aContext)
			throws Exception {
                              
		if (connectionSource == null)
			connectionSource = DataBaseHelper.getInstance().getConnection(
					_aContext);    
		Dao<ChannelContent, Integer> channelContentDao = DaoManager.createDao(
				connectionSource, ChannelContent.class);   
		List<ChannelContent> contentItemList = channelContentDao.queryBuilder().where()
				.eq(GpsRegion.GPS_REGION_ID, _id).query();
                
		return contentItemList;       
	}                 
                                   

}   
