using System;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface ILogger
  {
    void Write(object sender,string text);
    void Write(object sender,string text, Exception exc);
  }
}
