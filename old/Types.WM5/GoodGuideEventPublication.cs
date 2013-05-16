using System;
using System.Reflection;
using Nucleo.Events;
using Nucleo.GoodGuide.Types.Events;

namespace Nucleo.GoodGuide.Types
{
  public class GoodGuideEventPublication : EventPublication
  {
    public GoodGuideEventPublication(EventBroker eventBroker, object publisher, Type publisherType, EventInfo eventInfo, Boolean asyncEvent, string topic)
      : base(eventBroker,publisher,publisherType,eventInfo,asyncEvent,topic)
    {    
    }

    protected override Delegate CreateDelegate()
    {
      return new EventHandler<GoodGuideEventArgs>(ForegroundEventHandler);
    }
  }
}
