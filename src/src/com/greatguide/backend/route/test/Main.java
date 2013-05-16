package com.greatguide.backend.route.test;

import android.app.Activity;
import android.os.Bundle;

import com.greatguide.R;

public class Main extends Activity {


	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main);
		/*
		TextView myXmlContent = (TextView) findViewById(R.id.xml_tv);
		String stringXmlContent = "";

		try {

			
			stringXmlContent += " \n \n  Routes XML Reading ...: \n"; 
			
			LocationList list = new LocationList();
			    
			list.readXMLANDWriteToDB(this, "V1_8b_PR12_48b_Sharjah_6Jan13.xml");
			    
   
			myXmlContent.setText(stringXmlContent);   
		} catch (Exception e) {       
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		*/
	}

}