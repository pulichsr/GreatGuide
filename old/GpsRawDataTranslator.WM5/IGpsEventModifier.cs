using System;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.GpsRawDataTranslator
{
  public interface IGpsEventModifier
  {
    Boolean Modify(GpsRawEvent rawEvent,GpsPositionEvent positionEvent);
  }
}
