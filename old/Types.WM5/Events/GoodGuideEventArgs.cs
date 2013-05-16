using System;

namespace Nucleo.GoodGuide.Types.Events
{
  public class GoodGuideEventArgs : EventArgs
  {
    public GoodGuideEventArgs(string publisher,GoodGuideEvent eventData)
    {
      Publisher = publisher;
      PublishDtm = DateTime.Now;
      PublishTicks = PublishDtm.Ticks;
      EventData = eventData;
    }

    public string Publisher = string.Empty;
    public DateTime PublishDtm = DateTime.Now;
    public Int64 PublishTicks = DateTime.Now.Ticks;
    public GoodGuideEvent EventData = null;
  }
}
