using System;

namespace Nucleo.GoodGuide.Types.Configuration
{
  [Serializable]
  public class TripRecorderConfiguration
  {
    public string TestTripName
    {
      get { return testTripName; }
      set { testTripName = value; }
    }

    private string testTripName;
  }
}