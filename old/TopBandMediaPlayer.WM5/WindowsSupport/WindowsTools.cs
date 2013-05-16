using System;
using System.Runtime.InteropServices;

namespace Nucleo.GoodGuide.TopbandMediaPlayer.WindowsSupport
{
  public class cMsgStrings
  {
    const int LMEM_FIXED = 0x0000;
    const int LMEM_ZEROINIT = 0x0040;
    const int LPTR = (LMEM_FIXED | LMEM_ZEROINIT);

    [DllImport("coredll.dll")]
    public static extern IntPtr LocalAlloc(int flag, int size);
    [DllImport("coredll.dll")]
    public static extern IntPtr LocalFree(IntPtr p);
    [DllImport("coredll.dll")]
    public static extern int SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    //Create wrappers for the memory API's similar to
    //Marshal.AllocHGlobal and Marshal.FreeHGlobal

    public static IntPtr AllocHGlobal(int cb)
    {
      IntPtr hMemory = new IntPtr();
      hMemory = LocalAlloc(LPTR, cb);
      return hMemory;
    }

    public static void FreeHGlobal(IntPtr hMemory)
    {
      if (hMemory != IntPtr.Zero)
        LocalFree(hMemory);
    }

    public static void SendMsgString(IntPtr hWndDest, string sScript)
    {
      COPYDATASTRUCT oCDS = new COPYDATASTRUCT();
      oCDS.cbData = (sScript.Length + 1) * 2;
      oCDS.lpData = LocalAlloc(0x40, oCDS.cbData);
      Marshal.Copy(sScript.ToCharArray(), 0, oCDS.lpData, sScript.Length);
      oCDS.dwData = 1;
      IntPtr lParam = AllocHGlobal(oCDS.cbData);
      Marshal.StructureToPtr(oCDS, lParam, false);
      SendMessageW(hWndDest, (int)WindowsMessages.WM_COPYDATA, IntPtr.Zero, lParam);
      LocalFree(oCDS.lpData);
      FreeHGlobal(lParam);
    }

    public static string GetMsgString(IntPtr lParam)
    {
      COPYDATASTRUCT st = (COPYDATASTRUCT)Marshal.PtrToStructure(lParam, typeof(COPYDATASTRUCT));
      string str = Marshal.PtrToStringUni(st.lpData);
      return str;
    }
  }
}