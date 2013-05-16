using System;
using Microsoft.WindowsCE.Forms;

namespace Nucleo.GoodGuide.Types
{
  public class EventWindow : MessageWindow
  {
    public delegate void MsgProcessedEventHandler(ref Message msg);

    public event MsgProcessedEventHandler MediaComplete;
    public const Int32 WM_USER = 0x400;

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);

      if (m.Msg == WM_USER)
      {
        if (MediaComplete != null)
          MediaComplete(ref m);
      }
    }

  }
}