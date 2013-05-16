using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.WinCe;
using Nucleo.Windows.Forms.DynamicForms;

namespace Nucleo.GoodGuide.CarApplication
{
  static class Program
  {
    [MTAThread]
    static void Main()
    {
      #region Create logger
      logger = LoggerFactory.Create();
      if (logger == null)
      {
        MessageBox.Show("No loggers found");
        return;
      }
      #endregion

      #region Hide Taskbar
      Taskbar.Hide();
      #endregion

#if !DEBUG

      #region Show splash form
      frmSplash = new frmSplash();
      frmSplash.Show();
      Application.DoEvents();
      #endregion

      firstUse = FirstUse.IsFirstUse;
      if (firstUse)
      {
        #region FirstUse
        frmLanguageSelection = new frmSelectLanguage();
        frmLanguageSelection.ShowDialog();
        SelectedCulture.Culture = frmLanguageSelection.Culture;
        #endregion
      }

      frmDisclaimer = new frmDisclaimer(SelectedCulture.Culture);
      frmDisclaimer.Show();
      Application.DoEvents();
#endif
      try
      {
        Thread.CurrentThread.Name = "UiThread";
        CarApplication.Instance.InitialisationCompleted += Instance_InitialisationCompleted;
        CarApplication.Instance.Initialise(logger);

        System.Windows.Forms.Application.Run(frmCarApplication);
        CarApplication.Instance.Finalise();
        Nucleo.WinCe.Taskbar.Show();
      }
      catch (Exception exc)
      {
        MessageBox.Show(ExceptionManager.MessageWithStackTrace(exc));

        try
        {
          MessageBox.Show(ExceptionManager.MessageWithStackTrace("Crash",exc));
          string filename = string.Format("{0}Crashlog {1}.txt", Nucleo.Path.ExecutablePath,DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
          TextWriter CrashLogWriter = new StreamWriter(filename,false);
          CrashLogWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
          CrashLogWriter.WriteLine(ExceptionManager.MessageWithStackTrace("Applciation crash",exc));
          CrashLogWriter.Flush();
          CrashLogWriter.Close();
        }
        catch (Exception excCrashlog)
        {
        }

        try
        {
          Process P = Process.Start(string.Format("{0}ResetApplication.exe", Nucleo.Path.ExecutablePath), string.Empty);
          if (P == null)
            Nucleo.WinCe.System.SoftReset();
        }
        catch
        {
          Nucleo.WinCe.System.SoftReset();
        }
      }
    }
    
    private static void Instance_InitialisationCompleted(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(null, ">> Instance_InitialisationCompleted");

      if (frmDisclaimer != null)
        frmDisclaimer.BringToFront();

      frmCarApplication = new frmCarApplication();
      CarApplication.Instance.SetNavigatorMapControl(frmCarApplication.map);
      CarApplication.Instance.StartupSequence();

      // Have to load itinerary after StartupSequence because StartupSequence clears FirstUse
      CarApplication.Instance.LoadItineraryFromCard();

#if !DEBUG
      frmWelcome = new frmWelcome();
      frmWelcome.ShowDialog();
      frmWelcome.Dispose();

      frmDisclaimer.Close();
      frmDisclaimer.Dispose();

      if (firstUse)
      {
        CarApplication.Instance.Language = LanguageHelper.GetLanguage(SelectedCulture.Culture);

        frmHowItWorks = new frmHowItWorks();
        frmHowItWorks.ShowDialog();
        frmHowItWorks.Dispose();
      }

      FormDefinition formDefinition = DynamicFormManager.FormDefinitions[CarApplication.ItineraryFormName];
      if (formDefinition != null)
      {
        frmItinerary frmItinerary = new frmItinerary(false);
        frmItinerary.BuildPresentation(formDefinition, null);
        frmItinerary.ShowDialog();
        frmItinerary.Dispose();
      }
#endif  
    }

    private static Form frmSplash;
    private static frmSelectLanguage frmLanguageSelection;
    private static Form frmDisclaimer;
    private static Form frmWelcome;
    private static Form frmHowItWorks;
    private static frmCarApplication frmCarApplication;
    private static ILogger logger;
    private static Boolean firstUse;
  }
}

