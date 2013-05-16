using System.Collections.Generic;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.Reflection;

namespace Nucleo.GoodGuide.Types
{
  public class LoggerFactory
  {
    public static ILogger Create()
    {
      List<ILogger> loggers = Loader.LoadFromPath<ILogger>(Nucleo.Path.ExecutablePath, ".ggl");
      if (loggers.Count > 0)
      {
        return loggers[0];
      }
      else
      {
        return null;
      }
    }
  }
}