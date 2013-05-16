using System;
using System.Runtime.InteropServices;

namespace Nucleo.GoodGuide.TopbandMediaPlayer.WindowsSupport
{
  public static class WindowsMethods
  {
    [DllImport("coredll.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

    public static int GetWindowId_Int(string className, string windowName)
    {
      IntPtr handle = FindWindow(className, windowName);
      return handle.ToInt32();
    }

    public static IntPtr GetWindowId_IntPtr(string className, string windowName)
    {
      IntPtr handle = FindWindow(className, windowName);
      return handle;
    }

    [DllImport("coredll.dll", EntryPoint = "PostMessage")]
    public static extern int PostMessage(int hWnd, int Msg, int wParam, int lParam);
  }
}
