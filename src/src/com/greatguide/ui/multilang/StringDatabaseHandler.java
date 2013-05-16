package com.greatguide.ui.multilang;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;
import com.greatguide.backend.core.ErrorManagerAuditTrail;

/**
 * Author: Ivan Kruger
 */
class StringDatabaseHandler extends SQLiteOpenHelper {

    private static final int DATABASE_VERSION = 1;
    private static final String DATABASE_NAME = "stringsDatabase";
    private static final String TABLE_STRINGS = "strings";
    private static final String KEY_ID = "id";
    private static final String KEY_NAME = "name";
    private static final String KEY_STRING = "string";

    private Context _context;

    public StringDatabaseHandler(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        _context = context;
    }

    // Creating Tables
    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_STRINGS_TABLE = "CREATE TABLE " + TABLE_STRINGS + "("
                + KEY_ID + " INTEGER PRIMARY KEY," + KEY_NAME + " TEXT,"
                + KEY_STRING + " TEXT" + ")";
        db.execSQL(CREATE_STRINGS_TABLE);
    }

    // Upgrading database
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // Drop older table if existed
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_STRINGS);

        // Create tables again
        onCreate(db);
    }

    public void addString(String name, String value) {

        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();

        values.put(KEY_NAME, name);
        values.put(KEY_STRING, value);

        long bob = db.insert(TABLE_STRINGS, null, values);

        db.close();
    }

    public String getString(String name) {

        try{
            SQLiteDatabase db = this.getReadableDatabase();
            String query = "SELECT " + KEY_STRING + " FROM " + TABLE_STRINGS + " WHERE " + KEY_NAME + " = \'" + name + "\'";

            Cursor cursor = db.rawQuery(query,null);

            if (cursor == null){
                return null;
            } else{
                cursor.moveToFirst();
                return cursor.getString(0);
            }

        }
        catch(Exception e){
            ErrorManagerAuditTrail.getInstance().log(_context, e);
            return "";
        }


    }

    public void purge(){
        SQLiteDatabase db = this.getWritableDatabase();

        // Drop older table if existed
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_STRINGS);

        // Create tables again
        onCreate(db);
    }
}
