namespace Nucleo.GoodGuide.Types
{
  public class AudioSettings
  {
    public enum Content
    {
      NavigationOnly,
      CommentaryOnly,
      NavigationAndCommentary,
      None
    }
    public enum Source
    {
      SpeakerOnly,
      RadioOnly
    }

    public static string ToString(Content content)
    {
      switch(content)
      {
        case Content.NavigationOnly:
          return "Navigation";
        case Content.CommentaryOnly:
          return "Commentary";
        case Content.NavigationAndCommentary:
          return "Navigation & commentary";
        case Content.None:
          return "None";
      }

      return "Undefined";
    }
    public static string ToString(Source source)
    {
      switch (source)
      {
        case Source.RadioOnly:
          return "Radio";
        case Source.SpeakerOnly:
          return "Speaker";
      }

      return "Undefined";
    }
  }
}
