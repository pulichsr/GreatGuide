package com.greatguide.backend.location.playback;

import java.io.File;
import java.io.FileInputStream;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;

import android.content.Context;
import com.greatguide.backend.core.ActionResult;
import com.greatguide.backend.device.StorageManager;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

import android.util.Log;

class LocationListXMLParser {

    public static Document getDomElement(String xmlLayout) {
        Document dom = null;    
        try {
            File file = new File(xmlLayout);

            // create an input stream to be read by the stream reader.
            FileInputStream fis = new FileInputStream(file);
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            DocumentBuilder builder = factory.newDocumentBuilder();
            dom = builder.parse(fis);
        } catch (Exception e) {
            Log.e(LocationListXMLParser.class.getName(), "Exception Ocuured : " + e);
        }

        // return DOM
        return dom;
    }

    public static String getValue(Element item, String str) {
        NodeList n = item.getElementsByTagName(str);
        return getElementValue(n.item(0));
    }

    public static final String getElementValue(Node elem) {
        Node child;
        if (elem != null) {
            if (elem.hasChildNodes()) {
                for (child = elem.getFirstChild(); child != null; child = child
                        .getNextSibling()) {
                    if (child.getNodeType() == Node.TEXT_NODE) {
                        return child.getNodeValue();
                    }
                }
            }
        }
        return "";  
    }

}