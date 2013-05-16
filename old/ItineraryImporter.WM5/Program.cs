using System;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.ItineraryImporter
{
  static class Program
  {
    [MTAThread]
    static void Main(string[] args)
    {
      #region Validate arguments
      if ((args.Length < 1) || (string.IsNullOrEmpty(args[0]) == true))
      {
        MessageBox.Show("Missing commandline argument. Usage: ItineraryImporter <booking reference>");
        return;
      }
      #endregion

      #region Create context
      ImporterContext context;
      try
      {
        context = new ImporterContext();
      }
      catch (Exception exc)
      {
        MessageBox.Show(ExceptionManager.MessageWithStackTrace("Initialisation error",exc));
        return;
      }
      #endregion

      System.Windows.Forms.Application.Run(new frmMain(context,args[0]));
    }
  }
}