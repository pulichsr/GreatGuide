package com.greatguide.backend.core;

import java.io.Serializable;

import android.content.Context;

import com.greatguide.backend.device.StorageManager;

public class MediaContent implements Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = 3452640051223423479L;
	
	private MediaType mediaType;
	private String fileName;
	private boolean repeatForever;
	private int repeatPlayCount;
	private String languageCode;
	private transient Context context;
	
	
	public MediaContent()
	{}
	
	public MediaContent(Context aContext, String aLanguageCode) {
		context = aContext;
		languageCode = aLanguageCode;
		repeatPlayCount = 1;
	}
	
	public MediaType getMediaType() {
		return mediaType;
	}
	
	public void setMediaType(MediaType aType) {
		mediaType = aType;
	}
	
	public String getFileName()
	{
		return fileName;
	}
	
	public void setFileName(String aFileName) {
		fileName = aFileName;
	}
	
	public boolean isRepeatForever() {
		return repeatForever;
	}
	
	public void setRepeatForever(boolean aMedia) {
		repeatForever = aMedia;
	}
	
	public int getRepeatPlayCount() {
		return repeatPlayCount;
	}
	
	public void setRepeatPlayCount(int aRepeat) {
		repeatPlayCount = aRepeat;
	}
	
	public Context getContext() {
		return context;
	}
	
	public void setContext(Context aContext) {
		context = aContext;
	}
	
	public String getLanguageCode() {
		return languageCode;
	}
	
	public void setLanguageCode(String aLanguageCode) {
		languageCode = aLanguageCode;
	}
	
	public String getMediaFile() {
		
		String result = null;
		ActionResult media = StorageManager.getInstance().getSDCardPath(context, getAudioFolder());
        if (media.isSuccessful()) {
            result = media.getValue() + fileName;
        }		
		return result;
	}
	
	private String getAudioFolder() {
		return "routes/media/" + languageCode;
	}
	
	@Override
	public String toString() {
		StringBuilder result = new StringBuilder();
		result.append("Media Type: ").append(mediaType.toString()).append(", ");
		result.append("File: ").append(getMediaFile());		
		return result.toString();
	}
}
