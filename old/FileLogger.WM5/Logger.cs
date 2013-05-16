using System;
using Nucleo.Diagnostics.Logging;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.FileLogger
{
  public class Logger: 
    ILogger
  {
    public const string FilenameParamName = "FileLogger.Filename";

    public Logger()
    {
      string filename = string.Format("{0}{1}.txt", Path.ExecutablePath, Path.ExecutableNameWithoutExtension);
      messageWriter = new FormattedMessageWriter(new FileMessageWriter(filename, false));
    }

    #region ILogger
    public void Write(object sender,string text)
    {
      string senderType = sender == null ? string.Empty : sender.GetType().Name;
      messageWriter.WriteLine(string.Format("{0}:{1}", senderType, text));
    }
    public void Write(object sender, string text, Exception exc)
    {
      string senderType = sender == null ? string.Empty : sender.GetType().Name;
      messageWriter.WriteLine(string.Format("{0}:{1}", senderType, ExceptionManager.MessageWithStackTrace(text,exc)));
    }

    #endregion

    private readonly IMessageWriter messageWriter;
  }
}