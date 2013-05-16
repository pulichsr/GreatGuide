using System;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.Types
{
  public class LoggingServices
  {
    public LoggingServices(ILogger logger)
    {
      this.logger = logger;
    }

    public Boolean Enabled
    {
      get { return enabled; }
      set { enabled = value; }
    }

    public void RegisterHelper(LoggingHelper helper)
    {
      helper.TextLogged += LogTextHandler;
      helper.ErrorLogged += LogErrorHandler;
    }
    public void UnregisterHelper(LoggingHelper helper)
    {
      helper.TextLogged -= LogTextHandler;
      helper.ErrorLogged -= LogErrorHandler;
    }
    private void LogTextHandler(object sender, TextEventArgs e)
    {
      if (enabled == false)
        return;

      string text = string.Format("{0}|{1}", sender.GetType().FullName, e.Text);
      logger.Write(sender,text);
    }
    private void LogErrorHandler(object sender, ErrorEventArgs e)
    {
      if (enabled == false)
        return;

      string text = string.Format("{0}|{1}", sender.GetType().FullName, e.Text);
      logger.Write(sender, e.Text,e.Exception);
    }

    private readonly ILogger logger;
    private volatile Boolean enabled = false;
  }
}
