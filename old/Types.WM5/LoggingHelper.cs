using System;

namespace Nucleo.GoodGuide.Types
{
  public class LoggingHelper
  {
    public event EventHandler<TextEventArgs> TextLogged;
    public event EventHandler<ErrorEventArgs> ErrorLogged;

    public void RegisterChild(LoggingHelper helper)
    {
      Guard.ArgumentNotNull(helper, "helper");

      helper.TextLogged += TextLoggedHandler;
      helper.ErrorLogged += ErrorLoggedHandler;
    }
    public void Write(object sender, string text)
    {
      OnLogText(sender,text);
    }
    public void Write(object sender, string text, Exception exc)
    {
      OnLogError(sender,text,exc);
    }

    private void TextLoggedHandler(object sender,TextEventArgs e)
    {
      OnLogText(sender,e.Text);
    }
    private void ErrorLoggedHandler(object sender,ErrorEventArgs e)
    {
      OnLogError(sender,e.Text,e.Exception);
    }

    private void OnLogText(object sender, string text)
    {
      if (TextLogged == null)
        return;

      TextEventArgs e = new TextEventArgs(text);
      TextLogged(sender, e);
    }
    private void OnLogError(object sender, string text, Exception exc)
    {
      if (ErrorLogged == null)
        return;

      ErrorEventArgs e = new ErrorEventArgs(text, exc);
      ErrorLogged(sender, e);
    }
  }
}
