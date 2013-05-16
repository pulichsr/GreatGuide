package com.greatguide.ui;

import java.util.Date;

import android.app.FragmentTransaction;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;

import android.widget.TextView;
import com.greatguide.R;
import com.greatguide.ui.core.BaseActivity;
import com.greatguide.ui.dialog.ActivationDialog;
import com.greatguide.ui.dialog.HelpDialog;
import com.greatguide.ui.dialog.HirePeriodDialog;
import com.greatguide.ui.dialog.ReturnsDialog;
import com.greatguide.ui.dialog.RulesDialog;
import com.greatguide.ui.fragment.Title;
import com.greatguide.ui.contentRetriever.contentRetriever;


/**
 * The {@code HomeActivity} is responsible ...
 * 
 * @since Nov 30, 2012
 */
public class HomeActivity extends BaseActivity {

    private contentRetriever stringRetriever;

    /**
     * {@inheritDoc }
     */
    @Override
    protected void onCreate( Bundle savedInstanceState ) {
        super.onCreate( savedInstanceState );
        setContentView( R.layout.activity_home );

        SharedPreferences Prefs = getPreferences(0);

        SharedPreferences.Editor editor = Prefs.edit();

        editor.putString("Language", "en");
        editor.commit();

        contentRetriever.initializeRetriever(getApplicationContext());
        stringRetriever = contentRetriever.getContentRetriever();

        nameButtons();
    }

    private void nameButtons(){
        TextView button = (TextView) findViewById(R.id.home_hire_period_label);

        button.setText(stringRetriever.getString("label_activation_period"));

        button = (TextView) findViewById(R.id.home_rules_tile);

        button.setText(stringRetriever.getString("button_rules"));

        button = (TextView) findViewById(R.id.home_tours_tile);

        button.setText(stringRetriever.getString("button_tours"));

        button = (TextView) findViewById(R.id.home_intro_tile);

        button.setText(stringRetriever.getString("button_intro_vid"));

        button = (TextView) findViewById(R.id.home_help_tile);

        button.setText(stringRetriever.getString("button_help"));

        button = (TextView) findViewById(R.id.home_returns_tile);

        button.setText(stringRetriever.getString("button_returns"));

        button = (TextView) findViewById(R.id.home_setup_tile);

        button.setText(stringRetriever.getString("button_setup"));

    }

    /**
     * {@inheritDoc }
     */
    @Override
    protected void onResume() {
        super.onResume();
        Title title = ( Title ) super.getFragmentManager().findFragmentById( R.id.home_title );
        title.setTitleText(stringRetriever.getString("title_home"));
        title.setButton1Visible( false );
        title.setButton2Visible( false );
        title.setButton3Visible( true );
        
        title.setButton3OnClickListener( new View.OnClickListener() {
            
            @Override
            public void onClick( View v ) {
                // TODO Auto-generated method stub
                final Intent intent = new Intent( HomeActivity.this, DirectionActivity.class );
                startActivity( intent );
                finish();
            }
        } );
        
        showActiviationDialog();
    }

    /**
     * Invoked to show the 'Hire Period' dialog
     * 
     * @param view
     *            the view that was clicked
     */
    public void onHirePeriodClicked( final View view ) {
        final FragmentTransaction ft = super.getFragmentManager().beginTransaction();
        final HirePeriodDialog dlg = HirePeriodDialog
                .newInstance(
                        new Date(),
                        new Date(),
                        stringRetriever.getString("content_activation_period") );
        dlg.show( ft, "HirePeriodDialog" );
    }
    /**
     * Invoked when the 'Rules' tile is clicked
     * @param view the 'Rules' tile
     */
    public void onRulesClicked( final View view ) {
        final FragmentTransaction ft = super.getFragmentManager().beginTransaction();
        final RulesDialog dlg = RulesDialog
                .newInstance(
                        stringRetriever.getString("content_rules") );
        dlg.show( ft, "RulesDialog" );
    }
    /**
     * Invoked when the 'Help' tile is clicked
     * @param view the 'Help' tile
     */
    public void onHelpClicked( final View view ) {
        final FragmentTransaction ft = super.getFragmentManager().beginTransaction();
        final HelpDialog dlg = HelpDialog.newInstance( "TAG");
        dlg.show( ft, "RulesDialog" );
    }
    /**
     * Invoked when the 'Returns' tile is clicked
     * @param view the 'Returns' tile
     */
    public void onReturnsClicked( final View view ) {
        final FragmentTransaction ft = super.getFragmentManager().beginTransaction();
        final ReturnsDialog dlg = ReturnsDialog.newInstance( "Hotel Central",
                "V289 Minano Central", "Centralle Station - (Victoria Lane)", "Centralle Station\n\rBus No's 10/23/45/103/112");
        dlg.show( ft, "RulesDialog" );
    }
    
    /**
     * Invoked when the 'Tours' tile is clicked
     * @param view the 'Tours' tile
     */
    public void onToursClicked( final View view ) {
        final Intent intent = new Intent( this, ToursActivity.class );
        startActivity( intent );
    }
    
    /**
     * @param view
     */
    public void onSetupClicked( final View view ) {
        final Intent intent = new Intent( this, SetupActivity.class );
        startActivity( intent );
    }
    
    /**
     * 
     */
    private void showActiviationDialog() {
        final FragmentTransaction ft = super.getFragmentManager().beginTransaction();
        final ActivationDialog dlg = ActivationDialog.newInstance( "" );
        dlg.show( ft, "ActivationDialog" );
    }
}
