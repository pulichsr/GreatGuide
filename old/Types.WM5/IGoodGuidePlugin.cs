using System;

namespace Nucleo.GoodGuide.Types
{
  public interface IGoodGuidePlugin
  {
    string Status { get;}

    Boolean IsLoggingEnabled { get;set;}

    void Initialise(NamedParameters initialisationParameters);
    void Finalise();
  }
}