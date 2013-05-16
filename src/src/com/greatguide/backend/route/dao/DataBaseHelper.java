package com.greatguide.backend.route.dao;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;

import com.greatguide.backend.core.ActionResult;
import com.greatguide.backend.device.StorageManager;
import com.j256.ormlite.android.apptools.OrmLiteSqliteOpenHelper;
import com.j256.ormlite.support.ConnectionSource;


public class DataBaseHelper {
	
	private static DataBaseHelper _dataBaseHelper = null;
	
	public static synchronized DataBaseHelper getInstance() {
        if (_dataBaseHelper == null) {
        	_dataBaseHelper = new DataBaseHelper();
        }

        return _dataBaseHelper;    
    }
	  
	 public ConnectionSource getConnection(Context aContext) throws Exception {
	        ActionResult databasePath = StorageManager.getInstance().getSDCardPath(aContext, null);
	        String dbPath = null;
	        if (databasePath.isSuccessful())   
	            dbPath = databasePath.getValue() + "GreatGuide.sqlitedb";   
     
	        OrmLiteSqliteOpenHelper database = new OrmLiteSqliteOpenHelper(aContext, dbPath, null, 1) {
	            @Override
	            public void onCreate(SQLiteDatabase sqLiteDatabase, ConnectionSource connectionSource) {
	                //To change body of implemented methods use File | Settings | File Templates.
	            }

	            @Override
	            public void onUpgrade(SQLiteDatabase sqLiteDatabase, ConnectionSource connectionSource, int i, int i2) {
	                //To change body of implemented methods use File | Settings | File Templates.
	            }
	        };
	        return database.getConnectionSource();
	    }

}
