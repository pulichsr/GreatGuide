package com.greatguide.ui.contentRetriever;


import android.graphics.BitmapFactory;
import android.os.Environment;
import com.greatguide.ui.multilang.LangRetriever;
import com.greatguide.ui.multilang.Language;

import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Bitmap;

import java.io.File;
import java.util.List;

/**
 * Author: Ivan Kruger
 */

public class contentRetriever {

    private static LangRetriever langRetriever;
    private static Context _context = null;

    private final String VIDEOPATH = Environment.getExternalStorageDirectory().getAbsolutePath() + "/GreatGuide/vids/";
    private final String IMAGEPATH = Environment.getExternalStorageDirectory().getAbsolutePath() + "/GreatGuide/images/";

    public static final String PREFERENCE = "prefs";



    private contentRetriever(){

        SharedPreferences Prefs = _context.getSharedPreferences(PREFERENCE, 0);

        langRetriever = new LangRetriever(_context ,Prefs.getString("Language", "en"));

    }

    private static class ContentManager {
        public static final contentRetriever CONTENT_RETRIEVER = new contentRetriever();
    }

    public static contentRetriever getContentRetriever(){

        if (_context == null){
            return null;
        }

        return ContentManager.CONTENT_RETRIEVER;
    }

    public static void initializeRetriever(Context aContext){
        _context = aContext;
    }

    public String getString(String name){
        return langRetriever.getString(name);
    }

    public boolean isLangInList(String aLanguageCode){
        return langRetriever.isLangInList(aLanguageCode);
    }

    public List<Language> getListOfLangs(){
        return langRetriever.getListOfLangs();
    }

    public boolean setLang(String aLanguageCode){
        return langRetriever.setLang(aLanguageCode);
    }

    public String getActiveLanguage(){
        return langRetriever.getActiveLanguage();
    }

    public String getActiveLanguageDescription(){
        return langRetriever.getActiveLanguageDescription();
    }

    public String getVideoFile(String vidName){
        if (doesVideoFileExist(vidName)){
            return VIDEOPATH + vidName + "_" + langRetriever.getActiveLanguage();
        }else{
            return "";
        }
    }

    public boolean doesVideoFileExist(String vidName){
        File vid = _context.getFileStreamPath(VIDEOPATH + vidName + "_" + langRetriever.getActiveLanguage());

        return vid.exists();
    }

    public Bitmap getImage(String imgName){
        if (doesImageExist(imgName)){
            return BitmapFactory.decodeFile(IMAGEPATH + imgName);
        }else{
            return Bitmap.createBitmap(10,10, Bitmap.Config.ALPHA_8);
        }
    };

    public boolean doesImageExist(String imgName){
        File img = _context.getFileStreamPath(IMAGEPATH + imgName);

        return img.exists();
    };

}
