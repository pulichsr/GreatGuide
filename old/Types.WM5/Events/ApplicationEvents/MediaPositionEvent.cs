using System;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class MediaPositionEvent : ApplicationEvent
  {
    public MediaPositionEvent()
    {
    }
    public MediaPositionEvent(short position)
    {
      this.position = position;
    }

    public short Position
    {
      get { return position; }
      set { position = value; }
    }

    private Int16 position = 0;
  }
}
