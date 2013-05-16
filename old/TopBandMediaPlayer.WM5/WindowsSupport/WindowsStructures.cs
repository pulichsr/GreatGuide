using System;
using System.Runtime.InteropServices;

namespace Nucleo.GoodGuide.TopbandMediaPlayer.WindowsSupport
{
  /// <summary>
  /// Contains data to be passed to another application by the WM_COPYDATA message. 
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct COPYDATASTRUCT
  {
    public int dwData;
    public int cbData;
    public IntPtr lpData;
  }

}
