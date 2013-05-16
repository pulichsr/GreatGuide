using System;
using System.Collections.Generic;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class CurrentRegionsEvent : ApplicationEvent
  {
    public List<uint> CurrentRegions
    {
      get { return currentRegions; }
    }

    private readonly List<UInt32> currentRegions = new List<uint>();
  }
}


