package com.greatguide.ui.multilang;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import org.xmlpull.v1.XmlPullParser;

import android.content.Context;
import android.content.res.AssetManager;
import android.content.res.Resources;

import com.greatguide.R;
import com.greatguide.backend.core.ErrorManagerAuditTrail;

/**
 * Author: Ivan Kruger
 */
public class LangRetriever {

    private String _languageCode = "en";
    private List<Language> _languagesList;
    private StringDatabaseHandler _languageDatabase;
    private Context _context;

    /**
     * Contruct new language and default to ENGLISH
     *
     * @param aContext
     */
    public LangRetriever (Context aContext){
       this(aContext, "en");
    }

    /**
     *
     * Construct new language and give it the language code
     *
     * @param aContext
     * @param aLanguage
     */
    public LangRetriever (Context aContext, String aLanguage){

        _context = aContext;

        // Get languages from XML file
        Resources res = aContext.getResources();
        List<String> languageCodeList = Arrays.asList(res.getStringArray(R.array.language_code));
        List<String> languageDescriptionList = Arrays.asList(res.getStringArray(R.array.language_description));

        // Populate list of languages
        _languagesList = new ArrayList<Language>();
        for(int i = 0; i < languageCodeList.size(); i++) {
           _languagesList.add(new Language(languageCodeList.get(i), languageDescriptionList.get(i)));
        }

        // Set active language
        if (isLangInList(aLanguage)){
            _languageCode = aLanguage;
        }
        else
          throw new LanguageNotFound(aLanguage);

        _languageDatabase = new StringDatabaseHandler(aContext);

        populateStringDatabase();
    }

    private void populateStringDatabase(){

        try{

            _languageDatabase.purge();

            AssetManager xmlFileManager = _context.getAssets();

            String fileName = "res/xml/strings_" + _languageCode + ".xml";

            XmlPullParser xpp = xmlFileManager.openXmlResourceParser(fileName);
            int eventType = xpp.getEventType();
            while (eventType != XmlPullParser.END_DOCUMENT)
            {
               if (eventType == XmlPullParser.START_TAG)
                {
                    String name = xpp.getName();

                    if (name.equals("string")){

                        name = xpp.getAttributeValue(0);

                        xpp.next();

                        String string = xpp.getText();

                        _languageDatabase.addString(name, string);
                    }

                }
                eventType = xpp.next();
            }

        }
        catch (Exception e){
            ErrorManagerAuditTrail.getInstance().log(_context, e);
        }
    }

    /**
     *
     * Check if the given language code is in the available language list
     *
     * @param aLanguageCode
     * @return
     */
    public boolean isLangInList(String aLanguageCode){
        boolean result = false;
        for(Language currentLanguage: _languagesList) {
            if (currentLanguage.getCode().equalsIgnoreCase(aLanguageCode)) {
                result = true;
                break;
            }
        }

        return result;
    }

    /**
     *
     * Get list of languages
     *
     * @return
     */
    public List<Language> getListOfLangs(){
        return _languagesList;
    }

    /**
     *
     * Set the selected language
     *
     * @param aLanguageCode
     * @return
     */
    public boolean setLang(String aLanguageCode){

        boolean result = false;
        if (isLangInList(aLanguageCode)){
            _languageCode = aLanguageCode;
            populateStringDatabase();
            result = true;
        }

        return result;
    }

    /**
     *
     * Get the current active language by code
     *
     * @return
     */
    public String getActiveLanguage(){
        return _languageCode;
    }

    /**
     *
     * Get the description for the active language
     *
     * @return
     */
    public String getActiveLanguageDescription()
    {
        String result = null;
        for(Language currentLanguage: _languagesList) {
            if (currentLanguage.getCode().equalsIgnoreCase(_languageCode)) {
                result = currentLanguage.getDescription();
                break;
            }
        }
        return result;
    }

    /**
     *
     * Lookup language translation item and return the item translated to in the specific language
     *
     * @param aLanguageItemKey
     * @return
     */
    public String getString(String aLanguageItemKey){
        return _languageDatabase.getString(aLanguageItemKey);
    }
}
