package com.greatguide.backend.core;

import android.content.Context;
import android.content.SharedPreferences;

public class AppSettings {
    
    public static final Object UNSPECIFIED = null;
    public static final String UNSPECIFIED_BLANK = "";
    
    private static final String BASE = "com.greatguide";
    private static final String PREFERENCE_BASE = BASE + ".preference";
	private static final String PREFERENCE_PLAYBACK_MODE = PREFERENCE_BASE + ".playback.mode";
	private static final String PREFERENCE_LANGUAGE_CODE = PREFERENCE_BASE + ".language.code";
    
    private final Context appContext;
    private final String preferenceName;
    private final SharedPreferences preference;
    private final SharedPreferences.Editor edit;

    /**
     * Constructor
     * @param context
     */
    private AppSettings( final Context context ) {
        super();
        this.appContext = context;
        this.preferenceName = PREFERENCE_BASE;
        preference = this.appContext.getSharedPreferences(this.preferenceName, 0);
        edit = preference.edit();
    }
    
    /**
     * Returns an instance of {@code AppSettings}
     * @param context a context to operate on
     * @return an new instance
     */
    public final static AppSettings getInstance( final Context context ) {
        return new AppSettings( context );
    }
    
    /**
     * Saves the value associated with the specified key
     * @param key identifies a preference
     * @param value the preference value
     * @return true is successfully saved
     */
    private boolean save(String key, String value) {
        return edit.putString(key,  value).commit();
    }
    
    private boolean save(String key, long value) {
        return edit.putLong( key, value ).commit();
    }

    private boolean save(String key, boolean value) {
        return edit.putBoolean( key, value ).commit();
    }
    
    private boolean remove(String key) {
        return edit.remove(key).commit();
    }

	public void putPlaybackMode(boolean aPlaybackMode) {
		save(PREFERENCE_PLAYBACK_MODE, aPlaybackMode);
	}

	public void putLanguageCode(String aLanguageCode) {
		save(PREFERENCE_LANGUAGE_CODE, aLanguageCode);
	}

	public String getLanguageCode() {
		return this.preference.getString(PREFERENCE_LANGUAGE_CODE, "en");
	}

	public boolean getPlaybackMode() {
		return this.preference.getBoolean(PREFERENCE_PLAYBACK_MODE, false);
	}
}
