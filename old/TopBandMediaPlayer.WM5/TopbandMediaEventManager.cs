using System;
using Microsoft.WindowsCE.Forms;

namespace Nucleo.GoodGuide.TopbandMediaPlayer
{
  // Derive MessageWindow to respond to
  // Windows messages and to notify the
  // form when they are received.
  public class TopbandMediaEventManager : MessageWindow
  {
    public event EventHandler AudioStarted;
    public event EventHandler AudioStopped;
    public event EventHandler AudioReachEnd;
    public event EventHandler<ProgressEventArgs> AudioPositionChanged;
    public event EventHandler VideoStarted;
    public event EventHandler VideoStopped;
    public event EventHandler VideoReachEnd;
    public event EventHandler<ProgressEventArgs> VideoPositionChanged;

    // Override the default WndProc behavior to examine messages.
    protected override void WndProc(ref Message msg)
    {
      switch (msg.Msg)
      {
        // If message is of interest, invoke the method on the form that
        // functions as a callback to perform actions in response to the message.
        case (int)TopbandMediaPlayerManager.WM_TUOBANG_AUDIO_STARTED:
          if (AudioStarted != null)
            AudioStarted(this, new EventArgs());
          break;
        case (int)TopbandMediaPlayerManager.WM_TUOBANG_AUDIO_STOPPED:
          if (AudioStopped != null)
            AudioStopped(this, new EventArgs());
          break;
        case (int)TopbandMediaPlayerManager.WM_TUOBANG_AUDIO_REACH_END:
          if (AudioReachEnd != null)
            AudioReachEnd(this, new EventArgs());
          break;
        case (int)TopbandMediaPlayerManager.WM_TUOBANG_AUDIO_POSITION:
          if (AudioPositionChanged != null)
            AudioPositionChanged(this, new ProgressEventArgs("", (short)msg.WParam.ToInt32()));
          break;
        case (int)TopbandMediaPlayerManager.WM_TUOBANG_VIDEO_STARTED:
          if (VideoStarted != null)
            VideoStarted(this, new EventArgs());
          break;
        case (int)TopbandMediaPlayerManager.WM_TUOBANG_VIDEO_STOPPED:
          if (VideoStopped != null)
            VideoStopped(this, new EventArgs());
          break;
        case (int)TopbandMediaPlayerManager.WM_TUOBANG_VIDEO_REACH_END:
          if (VideoReachEnd != null)
            VideoReachEnd(this, new EventArgs());
          break;
        case (int)TopbandMediaPlayerManager.WM_TUOBANG_VIDEO_POSITION:
          if (VideoPositionChanged != null)
            VideoPositionChanged(this, new ProgressEventArgs("", (short)msg.WParam.ToInt32()));
          break;
      }
      // Call the base WndProc method
      // to process any messages not handled.
      base.WndProc(ref msg);
    }
  }
}
