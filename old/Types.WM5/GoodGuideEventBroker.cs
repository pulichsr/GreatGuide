using System;
using System.Reflection;
using Nucleo.Events;

namespace Nucleo.GoodGuide.Types
{
  public class GoodGuideEventBroker : EventBroker
  {
    protected override EventPublication CreatePublication(EventBroker eventBroker, object publisher, Type publisherType, EventInfo eventInfo, bool asyncEvent, string topic)
    {
      return new GoodGuideEventPublication(eventBroker,publisher,publisherType,eventInfo,asyncEvent,topic);
    }
  }
}
