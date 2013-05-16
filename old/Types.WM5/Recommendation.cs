using System;

namespace Nucleo.GoodGuide.Types
{
  public class Recommendation
  {
    public enum Recommendations
    {
      Undefined = 0,
      MustDo = 1,
      GreatIdea,
      IfTime
    }

    public static string ToString(Int16 recommendation)
    {
      return ToString((Recommendations)recommendation);
    }
    public static string ToString(Recommendations recommendation)
    {
      switch (recommendation)
      {
        case Recommendations.MustDo:
          return "Must do";
        case Recommendations.GreatIdea:
          return "Great idea";
        case Recommendations.IfTime:
          return "If time";
        default:
          return "";
      }
    }
    public static Recommendations IntToRecommendation(Int16 recommendation)
    {
      return (Recommendations)recommendation;
    }
    public static Int16 RecommendationToInt(Recommendations recommendation)
    {
      return (Int16)recommendation;
    }
  }
}
