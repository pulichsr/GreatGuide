
namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class LanguageEvent : ApplicationEvent
  {
    public LanguageEvent(Language language)
    {
      this.language = language;
    }

    public Language Language
    {
      get { return language; }
      set { language = value; }
    }

    private Language language;
  }
}
