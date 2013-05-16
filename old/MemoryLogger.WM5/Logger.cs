using System;
using Nucleo.Diagnostics.Logging;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GooodGuide.MemoryLogger
{
  public class Logger: 
    ILogger
  {
    public Logger()
    {
      memoryMessageWriter = new MemoryMessageWriter(100000);
      messageWriter = new FormattedMessageWriter(memoryMessageWriter);
    }
    ~Logger()
    {
      string filename = string.Format("{0}{1}.{2}.txt", Path.ExecutablePath, Path.ExecutableNameWithoutExtension,DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));

      FileMessageWriter writer = new FileMessageWriter(filename, false);
      foreach (string logEntry in memoryMessageWriter.Entries)
        writer.WriteLine(logEntry);
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
      messageWriter.WriteLine(string.Format("{0}:{1}", senderType, ExceptionManager.MessageWithStackTrace(text, exc)));
    }

    #endregion

    private readonly MemoryMessageWriter memoryMessageWriter;
    private readonly IMessageWriter messageWriter;
  }
}