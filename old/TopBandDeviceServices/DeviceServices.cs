using System;
using System.Diagnostics;
using System.IO;
using Nucleo.GoodGuide.Types.Interfaces.Device;
using Nucleo.WinCe;

namespace Nucleo.GoodGuide.TopBandDeviceServices
{
  public class DeviceServices:
    IDeviceServices
  {
    public string FlashVersion
    {
      get { return ""; }
    }
    public UInt32 FmTransmitterChannel
    {
      get { return 0; }
      set { }
    }
    public Boolean FMTransmitterState
    {
      get { return false; }
      set { }
    }

    public void Initialise()
    {
      try
      {
        string osd = @"\ResidentFlash2\MyShell\OSD.exe";
        if (File.Exists(osd) == true)
          Process.Start(osd,string.Empty);
      }
      catch (Exception exc)
      {
      }
    }
    public void PowerOff()
    {
      try
      {
        ManualResetEvent resetEvent = new ManualResetEvent("PV_POWEROFF_SYSTEM", false);
        resetEvent.Set();
        resetEvent.Dispose();
      }
      catch (Exception exc)
      {        
      }
    }
    public void Suspend()
    {
      try
      {
        ManualResetEvent resetEvent = new ManualResetEvent("SHARE_POWERBUTTON_EVENT", false);
        resetEvent.Set();
        resetEvent.Dispose();
      }
      catch (Exception exc)
      {        
      }
    }
  }
}