using System;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public class StartupSequenceController
  {
    public StartupSequenceController(Form frmSplash,Form frmLanguageSelection,Form frmDisclaimer,Form frmWelcome,Form frmHowItWorks,Form frmCarApplication)
    {
//      this.frmSplash = frmSplash;
//      this.frmLanguageSelection = frmLanguageSelection;
//      this.frmDisclaimer = frmDisclaimer;
      this.frmWelcome = frmWelcome;
      this.frmHowItWorks = frmHowItWorks;
      this.frmCarApplication = frmCarApplication;

//      this.frmSplash.TopMost = true;
//      this.frmLanguageSelection.TopMost = true;
//      this.frmDisclaimer.TopMost = true;
      this.frmWelcome.TopMost = true;
      this.frmHowItWorks.TopMost = true;

//      this.frmSplash.Closed += frmSplash_Closed;
//      this.frmLanguageSelection.Closed += frmLanguageSelection_Closed;
//      this.frmDisclaimer.Closed += frmDisclaimer_Closed;
      this.frmWelcome.Closed += frmWelcome_Closed;
      this.frmHowItWorks.Closed += frmHowItWorks_Closed;
      this.frmCarApplication.Activated += frmCarApplication_Activated;
    }

    /// <summary>
    /// This assumes that the FirstUse has already been determined
    /// </summary>
    public void Start()
    {
      CarApplication.Instance.WriteLog(this, ">> Start");
    }

    #region Show & Close methods
    //private void ShowSplash()
    //{
    //  CarApplication.Instance.WriteLog(this, ">> ShowSplash");

    //  frmSplash.Show();
    //  isSplashShowing = true;
    //  hasSplashShowed = true;

    //  Application.DoEvents();
    //}
    //private void CloseSplash()
    //{
    //  CarApplication.Instance.WriteLog(this, ">> CloseSplash");

    //  frmSplash.Close();

    //  Application.DoEvents();
    //}

    //private void ShowLanguageSelection()
    //{
    //  CarApplication.Instance.WriteLog(this, ">> ShowLanguageSelection");

    //  frmLanguageSelection.Show();
    //  isLanguageSelectionShowing = true;

    //  Application.DoEvents();
    //}

    //private void ShowDisclaimer()
    //{
    //  CarApplication.Instance.WriteLog(this, ">> ShowDisclaimer");

    //  frmDisclaimer.Show();
    //  isDisclaimerShowing = true;

    //  Application.DoEvents();
    //}
    //private void CloseDisclaimer()
    //{
    //  CarApplication.Instance.WriteLog(this, ">> CloseDisclaimer");

    //  frmDisclaimer.Close();

    //  Application.DoEvents();
    //}

    private void ShowWelcome()
    {
      CarApplication.Instance.WriteLog(this, ">> ShowWelcome");

      frmWelcome.Show();
      isWelcomeShowing = true;

      Application.DoEvents();
    }
    private void ShowHowItWorks()
    {
      CarApplication.Instance.WriteLog(this, ">> ShowHowItWorks");

      frmHowItWorks.Show();
      isHowItWorksShowing = true;

      Application.DoEvents();
    }
    #endregion

    #region Form event handlers
    //private void frmSplash_Closed(object sender, EventArgs e)
    //{
    //  CarApplication.Instance.WriteLog(this, ">> frmSplash_Closed");

    //  isSplashShowing = false;
    //}
    //private void frmLanguageSelection_Closed(object sender, EventArgs e)
    //{
    //  CarApplication.Instance.WriteLog(this, ">> frmLanguageSelection_Closed");

    //  isLanguageSelectionShowing = false;
    //  hasLanguageSelectionShowed = true;

    //  if (isInitialised == false)
    //  {
    //    CarApplication.Instance.WriteLog(this, " isInitialised == false: Show Disclaimer");
    //    ShowDisclaimer();
    //  }
    //  else
    //  {
    //    CarApplication.Instance.WriteLog(this, " isInitialised == true: Show Welcome");
    //    ShowWelcome();
    //  }
    //}
    //private void frmDisclaimer_Closed(object sender, EventArgs e)
    //{
    //  CarApplication.Instance.WriteLog(this, ">> frmDisclaimerClosed");

    //  isDisclaimerShowing = false;
    //  hasDisclaimerShowed = true;
    //}
    private void frmWelcome_Closed(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(this, ">> frmWelcomeClosed");

      isWelcomeShowing = false;
      hasWelcomeShowed = true;

      frmCarApplication.Activate();
    }
    private void frmHowItWorks_Closed(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(this, ">> frmHowItWorksClosed");

      isHowItWorksShowing = false;
      hasHowItWorksShowed = true;
    }
    private void frmCarApplication_Activated(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(this, ">> frmCarApplication_Activated");

      #region hasHowItWorksShowed
      if (hasHowItWorksShowed)
      {
        return;
      }
      #endregion

      #region hasWelcomeShowed
      if (hasWelcomeShowed)
      {
        if (FirstUse.IsFirstUse)
          ShowHowItWorks();
        else
          return;
      }
      else
      {
        ShowWelcome();
      }
      #endregion
    }
    #endregion

    #region Fields
//    private readonly Form frmSplash;
//    private readonly Form frmLanguageSelection;
//    private readonly Form frmDisclaimer;
    private readonly Form frmWelcome;
    private readonly Form frmHowItWorks;
    private readonly Form frmCarApplication;

    private Boolean isInitialised = false;

//    private Boolean isSplashShowing = false;
//    private Boolean hasSplashShowed = false;

//    private Boolean isLanguageSelectionShowing = false;
//    private Boolean hasLanguageSelectionShowed = false;

//    private Boolean isDisclaimerShowing = false;
//    private Boolean hasDisclaimerShowed = false;

    private Boolean isWelcomeShowing = false;
    private Boolean hasWelcomeShowed = false;

    private Boolean isHowItWorksShowing = false;
    private Boolean hasHowItWorksShowed = false;
    #endregion
  }
}
