package com.greatguide.backend.route.parser;

import java.util.List;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

import android.content.Context;
import android.util.Log;

import com.greatguide.backend.route.dao.DataBaseHelper;
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
import com.j256.ormlite.table.TableUtils;

public class RouteLoader {

	private ConnectionSource connectionSource = null;

	public void parse(Context aContext, String xmlLayout)
			throws Exception {

		// XML node MasterArea keys
		final String KEY_ITEM = "MasterAreaRow";
		final String KEY_ID = "Id";
		final String KEY_NAME = "Name";
		final String KEY_CHANNEL_GROUPID = "ChannelGroupId";
		final String KEY_CONTENT_BASEPATH = "ContentBasePath";
		final String KEY_REGIONDATA = "RegionData";
		final String KEY_MIN_LATITUDE = "MinLatitude";
		final String KEY_MAX_LATITUDE = "MaxLatitude";
		final String KEY_MIN_LONGITUDE = "MinLongitude";
		final String KEY_MAX_LONGITUDE = "MaxLongitude";
		final String KEY_REGION_TYPE = "RegionType";

		// xml node ThemeRow keys
		final String KEY_ITEM1 = "ThemeRow";
		final String KEY_THEME_ID = "Id";
		final String KEY_THEME_NAME = "Name";

		// XML node ChannelRow keys
		final String KEY_ITEM2 = "ChannelRow";
		final String KEY_CHANNEL_ID = "Id";
		final String KEY_CHANNEL_GROUP_ID = "ChannelGroupId";
		final String KEY_CONTENTPATH = "ContentPath";
		final String KEY_LANGUAGE = "Language";

		// XML node ChannelGroupRow keys

		final String KEY_ITEM3 = "ChannelGroupRow";
		final String KEY_CHANNEL_GROUP_ROW_ID = "Id";
		final String KEY_CHANNEL_GROUP_NAME = "Name";

		// XML node ChannelContentRow keys
		final String KEY_ITEM4 = "ChannelContentRow";
		final String KEY_CHANNEL_CONTENT_ID = "Id";
		final String KEY_CHANNEL_GPSREGION_ID = "GpsRegionId";
		final String KEY_TRIGGER_TYPE = "TriggerType";
		final String KEY_DIRECTION_NUMBER = "DirectionNumber";
		final String KEY_MAXPRESENTED_COUNT = "MaxPresentedCount";
		final String KEY_CHANNEL_PRIORITY = "Priority";
		final String KEY_CHANNEL_ACTIVEDAYS = "ActiveDays";
		final String KEY_HEADING = "Heading";
		final String KEY_HEADING_VARIANCE = "HeadingVariance";
		final String KEY_CHANNEL_AUTOPRESENT = "AutoPresent";
		final String KEY_CHANNEL_SEQUENCE = "IsSequenced";

		// XML node ContentItemRow keys
		final String KEY_ITEM5 = "ContentItemRow";
		final String KEY_CONTENT_ID = "Id";
		final String KEY_CHANNEL_CONTENTITEM_ID = "ChannelContentId";
		final String KEY_CONTENT_GROUP_ID = "ChannelGroupId";
		final String KEY_CONTENT_TYPE_CODE = "ContentTypeCode";
		final String KEY_CONTENT_FILENAME = "Filename";

		// XML node GpsRegionRow keys
		final String KEY_ITEM6 = "GpsRegionRow";
		final String KEY_GPSREGION_ID = "Id";
		final String KEY_MASTER_AREA_ID = "MasterAreaId";
		final String KEY_GPSREGION_THEME_ID = "ThemeId";
		final String KEY_GPSREGION_DATA = "RegionData";
		final String KEY_GPSREGION_MINLATITUDE = "MinLatitude";
		final String KEY_GPSREGION_MAXLATITUDE = "MaxLatitude";
		final String KEY_GPSREGION_MINLONGITUDE = "MinLongitude";
		final String KEY_GPSREGION_MAXLONGITUDE = "MaxLongitude";
		final String KEY_GPSREGION_REGIONTYPE = "RegionType";
		final String KEY_GPSREGION_RESETONENTRY = "ResetOnEntry";

		// get the database connction
		if (connectionSource == null)
			connectionSource = DataBaseHelper.getInstance().getConnection(aContext);

		Document dom = RouteLoaderXMLParser.getDomElement(aContext, xmlLayout);
		NodeList n = dom.getElementsByTagName(KEY_ITEM);

		Dao<MasterArea, String> masterAreaDao = DaoManager.createDao(
				connectionSource, MasterArea.class);

		if (!masterAreaDao.isTableExists()) {
			TableUtils.createTable(connectionSource, MasterArea.class);
		}

		Dao<Theme, String> themeDao = DaoManager.createDao(
				connectionSource, Theme.class);
		
		if (!themeDao.isTableExists()) {
			TableUtils.createTable(connectionSource, Theme.class);
		}

		Dao<Channel, String> channelDao = DaoManager.createDao(
				connectionSource, Channel.class);
		
		if (!channelDao.isTableExists()) {
			TableUtils.createTable(connectionSource, Channel.class);
		}

		Dao<ChannelGroup, String> channelGroupDao = DaoManager.createDao(
				connectionSource, ChannelGroup.class);

		if (!channelGroupDao.isTableExists()) {
			TableUtils.createTable(connectionSource, ChannelGroup.class);
		}

		Dao<ChannelContent, String> channelContentDao = DaoManager
				.createDao(connectionSource, ChannelContent.class);

		if (!channelContentDao.isTableExists()) {
			TableUtils.createTable(connectionSource, ChannelContent.class);
		}
		
		Dao<ContentItem, String> contentItemDao = DaoManager.createDao(
				connectionSource, ContentItem.class);

		if (!contentItemDao.isTableExists()) {
			TableUtils.createTable(connectionSource, ContentItem.class);
		}
		
		Dao<GpsRegion, String> gpsRegionDao = DaoManager.createDao(
				connectionSource, GpsRegion.class);

		if (!gpsRegionDao.isTableExists()) {
			TableUtils.createTable(connectionSource, GpsRegion.class);
		}
		
		// looping through all item nodes <item>
		for (int i = 0; i < n.getLength(); i++) {

			Element e = (Element) n.item(i);

			int id = parseInt(RouteLoaderXMLParser.getValue(e, KEY_ID));

			String name = RouteLoaderXMLParser.getValue(e, KEY_NAME);

			int channelGroupID = parseInt(RouteLoaderXMLParser.getValue(e, KEY_CHANNEL_GROUPID));
			
			String contentBasePath = RouteLoaderXMLParser.getValue(e,
					KEY_CONTENT_BASEPATH);

			String regionData = RouteLoaderXMLParser.getValue(e,
					KEY_REGIONDATA);

			Double minimunLatitude = parseDouble(RouteLoaderXMLParser
					.getValue(e, KEY_MIN_LATITUDE));

			Double maxLatitude = parseDouble(RouteLoaderXMLParser
					.getValue(e, KEY_MAX_LATITUDE));

			Double minLongitude = parseDouble(RouteLoaderXMLParser
					.getValue(e, KEY_MIN_LONGITUDE));

			Double maxLongitude = parseDouble(RouteLoaderXMLParser
					.getValue(e, KEY_MAX_LONGITUDE));

			String regionType = RouteLoaderXMLParser.getValue(e,
					KEY_REGION_TYPE);

			MasterArea masterArea = new MasterArea(id, name, channelGroupID,
					contentBasePath, regionData, minimunLatitude, maxLatitude,
					minLongitude, maxLongitude, regionType);

			// //call this method if you want to drop the table
			// TableUtils.dropTable(connectionSource, MasterArea.class, true);
			// //call this method if you want to create the table
			//	TableUtils.createTable(connectionSource, MasterArea.class);
			// // persist the masterArea object to the database
			masterAreaDao.create(masterArea);

			List<MasterArea> masterAreaList = masterAreaDao.queryForAll();

			for (MasterArea masterArea1 : masterAreaList) {
				Log.i(this.getClass().getName(), "MasterArea data:"
						+ masterArea1.getId());   
				List<GpsRegion> gpsRegionList = masterArea1.getRegionByMasterAreaID(aContext);
				for (GpsRegion gpsRegion1 : gpsRegionList) {
					Log.i(this.getClass().getName(), "GpsRegion data:"
							+ gpsRegion1.getRegionData());       
				}     

			}
		}

		NodeList n1 = dom.getElementsByTagName(KEY_ITEM1);

		// looping through all item nodes <item>
		for (int i = 0; i < n1.getLength(); i++) {

			Element e = (Element) n1.item(i);

			int id = parseInt(RouteLoaderXMLParser.getValue(e, KEY_THEME_ID));

			String name = RouteLoaderXMLParser.getValue(e, KEY_THEME_NAME);

			Theme theme = new Theme(id, name);

			// //call this method if you want to drop the table
			// TableUtils.dropTable(connectionSource, Theme.class, true);
			// //call this method if you want to create the table
			//	TableUtils.createTable(connectionSource, Theme.class);
			// persist the themeArea object to the database
			themeDao.create(theme);      

			List<Theme> themeList = themeDao.queryForAll();

			for (Theme theme1 : themeList) {
				Log.i(this.getClass().getName(), "Theme data:" + theme1.getId());

				List<GpsRegion> gpsRegionList = theme1.getRegion(aContext);
				for (GpsRegion gpsRegion1 : gpsRegionList) {
					Log.i(this.getClass().getName(), "GpsRegion data:"
							+ gpsRegion1.getRegionData());       
				}  
			}

		}

		NodeList n2 = dom.getElementsByTagName(KEY_ITEM2);

		// looping through all item nodes <item>
		for (int i = 0; i < n2.getLength(); i++) {

			Element e = (Element) n2.item(i);

			int id = parseInt(RouteLoaderXMLParser.getValue(e, KEY_CHANNEL_ID));
			int channelGroupID = parseInt(RouteLoaderXMLParser.getValue(e, KEY_CHANNEL_GROUP_ID));
			String contentPath = RouteLoaderXMLParser.getValue(e,
					KEY_CONTENTPATH);
			String language = RouteLoaderXMLParser.getValue(e, KEY_LANGUAGE);

			Channel channel = new Channel(id, channelGroupID, contentPath,
					language);

			// //call this method if you want to drop the table
			// TableUtils.dropTable(connectionSource, Channel.class, true);
			// //call this method if you want to create the table
			//	TableUtils.createTable(connectionSource, Channel.class);   
			// persist the channel object to the database
			channelDao.create(channel);

			List<Channel> channelList = channelDao.queryBuilder().where()
					.eq("ID", id).query();          

			for (Channel channel1 : channelList) {
				Log.i(this.getClass().getName(), "Channel data:" + channel1.getid());
			}

		}   

		NodeList n3 = dom.getElementsByTagName(KEY_ITEM3);

		// looping through all item nodes <item>
		for (int i = 0; i < n3.getLength(); i++) {

			Element e = (Element) n3.item(i);

			int id = parseInt(RouteLoaderXMLParser.getValue(e, KEY_CHANNEL_GROUP_ROW_ID));

			String name = RouteLoaderXMLParser.getValue(e,
					KEY_CHANNEL_GROUP_NAME);

			ChannelGroup channelGroup = new ChannelGroup(id, name);

			// //call this method if you want to drop the table
			// TableUtils.dropTable(connectionSource, ChannelGroup.class, true);
			// //call this method if you want to create the table
			//	TableUtils.createTable(connectionSource, ChannelGroup.class);
			// // persist the channelGroup object to the database
			channelGroupDao.create(channelGroup);

			List<ChannelGroup> channelGroupList = channelGroupDao.queryForAll();

			for (ChannelGroup channelGroup1 : channelGroupList) {
				Log.i(this.getClass().getName(), "ChannelGroup data:"
						+ channelGroup1.getId());      
				List<Channel> channelList = channelGroup1.getChannel(aContext);
				for (Channel Channel : channelList) {   
					Log.i(this.getClass().getName(), "Channel data:"    
							+ Channel.getchannelGroupID());              
				}  

				List<ContentItem> contentItemList = channelGroup1.getContentItem(aContext);
				for (ContentItem contentItem1 : contentItemList) {   
					Log.i(this.getClass().getName(), "ContentItem data:"    
							+ contentItem1.getChannelContentId());              
				} 

				List<MasterArea> masterAreaList = channelGroup1.getMasterArea(aContext);
				for (MasterArea masterArea1 : masterAreaList) {   
					Log.i(this.getClass().getName(), "MasterArea data:"    
							+ masterArea1.getChannelGroupID());           
				}  

			}   

		}

		NodeList n4 = dom.getElementsByTagName(KEY_ITEM4);

		// looping through all item nodes <item>
		for (int i = 0; i < n4.getLength(); i++) {

			Element e = (Element) n4.item(i);

			int id = parseInt(RouteLoaderXMLParser.getValue(e,
					KEY_CHANNEL_CONTENT_ID));
			int gpsRegionID = parseInt(RouteLoaderXMLParser.getValue(
					e, KEY_CHANNEL_GPSREGION_ID));
			String triggerType = RouteLoaderXMLParser.getValue(e,
					KEY_TRIGGER_TYPE);
			int directionNumber = parseInt(RouteLoaderXMLParser
					.getValue(e, KEY_DIRECTION_NUMBER));
			int maxPresentedCount = parseInt(RouteLoaderXMLParser
					.getValue(e, KEY_MAXPRESENTED_COUNT));
			int priority = parseInt(RouteLoaderXMLParser.getValue(e,
					KEY_CHANNEL_PRIORITY));
			int activeDays = parseInt(RouteLoaderXMLParser.getValue(e,
					KEY_CHANNEL_ACTIVEDAYS));
			String heading = RouteLoaderXMLParser.getValue(e, KEY_HEADING);
			String headingVariance = RouteLoaderXMLParser.getValue(e,
					KEY_HEADING_VARIANCE);
			int autoPresent = parseInt(RouteLoaderXMLParser.getValue(
					e, KEY_CHANNEL_AUTOPRESENT));
			int isSequenced = parseInt(RouteLoaderXMLParser.getValue(
					e, KEY_CHANNEL_SEQUENCE));

			ChannelContent channelContent = new ChannelContent(id, gpsRegionID,
					triggerType, directionNumber, maxPresentedCount, priority,
					activeDays, heading, headingVariance, autoPresent,
					isSequenced);

			// //call this method if you want to drop the table
			// TableUtils.dropTable(connectionSource, ChannelContent.class,
			// true);
			// //call this method if you want to create the table
			//	TableUtils.createTable(connectionSource, ChannelContent.class);
			// // persist the channelContent object to the database
			channelContentDao.create(channelContent);

			List<ChannelContent> channelContentList = channelContentDao
					.queryBuilder().where().eq("ID", id).query();

			for (ChannelContent channelContent1 : channelContentList) {
				Log.i(this.getClass().getName(), "ChannelContent data:"
						+ channelContent1.isAutoPlay());        
				//				List<GpsRegion> gpsRegionList = channelContent1.getRegion(aContext);
				//				for (GpsRegion gpsRegion1 : gpsRegionList) {
				//					Log.i(this.getClass().getName(), "GpsRegion data:"
				//							+ gpsRegion1.getRegionData());       
				//				}      

				List<ContentItem> contentItemList = channelContent1.getContentItem(aContext);
				for (ContentItem contentItem1 : contentItemList) {   
					Log.i(this.getClass().getName(), "ContentItem data:"
							+ contentItem1.getChannelContentId());          
				}              
			}
		}

		NodeList n5 = dom.getElementsByTagName(KEY_ITEM5);

		// looping through all item nodes <item>
		for (int i = 0; i < n5.getLength(); i++) {

			Element e = (Element) n5.item(i);

			int id = parseInt(RouteLoaderXMLParser.getValue(e,
					KEY_CONTENT_ID));
			int channelContentId = parseInt(RouteLoaderXMLParser
					.getValue(e, KEY_CHANNEL_CONTENTITEM_ID));
			int channelGroupId = parseInt(RouteLoaderXMLParser
					.getValue(e, KEY_CONTENT_GROUP_ID));
			String contentTypeCode = RouteLoaderXMLParser.getValue(e,
					KEY_CONTENT_TYPE_CODE);
			String filename = RouteLoaderXMLParser.getValue(e,
					KEY_CONTENT_FILENAME);

			ContentItem contentItem = new ContentItem(id, channelContentId,
					channelGroupId, contentTypeCode, filename);

			// //call this method if you want to drop the table
			// TableUtils.dropTable(connectionSource, ContentItem.class, true);
			// //call this method if you want to create the table
			//	TableUtils.createTable(connectionSource, ContentItem.class);
			// // persist the contentItem object to the database
			contentItemDao.create(contentItem);   

			List<ContentItem> contentItemList = contentItemDao.queryBuilder().where()
					.eq("ID", id).query();

			for (ContentItem contentItem1 : contentItemList) {
				Log.i(this.getClass().getName(), "ContentItem data:"
						+ contentItem1.isVideo());             

			}   

		}

		NodeList n6 = dom.getElementsByTagName(KEY_ITEM6);

		// looping through all item nodes <item>
		for (int i = 0; i < n6.getLength(); i++) {

			Element e = (Element) n6.item(i);

			int id = parseInt(RouteLoaderXMLParser.getValue(e,
					KEY_GPSREGION_ID));
			int masterAreaId = parseInt(RouteLoaderXMLParser.getValue(
					e, KEY_MASTER_AREA_ID));
			int themeId = parseInt(RouteLoaderXMLParser.getValue(e,
					KEY_GPSREGION_THEME_ID));
			String regionData = RouteLoaderXMLParser.getValue(e,
					KEY_GPSREGION_DATA);
			Double minLatitude = parseDouble(RouteLoaderXMLParser
					.getValue(e, KEY_GPSREGION_MINLATITUDE));
			Double maxLatitude = parseDouble(RouteLoaderXMLParser
					.getValue(e, KEY_GPSREGION_MAXLATITUDE));
			Double minLongitude = parseDouble(RouteLoaderXMLParser
					.getValue(e, KEY_GPSREGION_MINLONGITUDE));
			Double maxLongitude = parseDouble(RouteLoaderXMLParser
					.getValue(e, KEY_GPSREGION_MAXLONGITUDE));
			String regionType = RouteLoaderXMLParser.getValue(e,
					KEY_GPSREGION_REGIONTYPE);
			String resetOnEntry = RouteLoaderXMLParser.getValue(e,
					KEY_GPSREGION_RESETONENTRY);

			GpsRegion gpsRegion = new GpsRegion(id, masterAreaId, themeId,
					regionData, minLatitude, maxLatitude, minLongitude,
					maxLongitude, regionType, resetOnEntry);

			// //call this method if you want to drop the table
			// TableUtils.dropTable(connectionSource, GpsRegion.class, true);
			// //call this method if you want to create the table
			//	TableUtils.createTable(connectionSource, GpsRegion.class);

			// persist the gpsRegion object to the database
			gpsRegionDao.create(gpsRegion);   

			List<GpsRegion> gpsRegionList = gpsRegionDao.queryBuilder().where()
					.eq("ID", id).query();
			for (GpsRegion gpsRegion1 : gpsRegionList) {     
				Log.i(this.getClass().getName(), "GpsRegion data:" + gpsRegion1.getId());
				List<ChannelContent> contentItemList = gpsRegion1.getChannelContent(aContext);   
				for (ChannelContent channelContent1 : contentItemList) {   
					Log.i(this.getClass().getName(), "Channel Content data:"
							+ channelContent1.getId());          
				}  
			}

		}   
	}

	private Double parseDouble(String aValue) {
		double result = 0D;
		try
		{
			result = Double.parseDouble(aValue);
		}
		catch (Exception ex) {}
		return result;
	}
	
	private int	parseInt(String aValue) {
		int result = 0;
		try
		{
			result = Integer.parseInt(aValue);
		}
		catch (Exception ex) {}
		return result;
	}
}
