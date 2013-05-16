using System;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.NullLogger
{
  public class Logger: 
    ILogger
  {
    #region ILogger
    public void Write(object sender,string text)
    {
    }
    public void Write(object sender, string text, Exception exc)
    {
    }

    #endregion

  }
}