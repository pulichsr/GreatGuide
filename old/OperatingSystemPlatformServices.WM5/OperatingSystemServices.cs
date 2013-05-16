using System;
using Nucleo.GoodGuide.Types.Interfaces.OperatingSystemServices;

namespace Nucleo.GoodGuide.OperatingSystemServices
{
  public class OperatingSystemServices : IOperatingSystemServices
  {
    public UInt16 Volume
    {
      get { return WinCe.Sound.DeviceVolume; }
      set { WinCe.Sound.DeviceVolume = value; }
    }
    public void PlaySound(string filename)
    {
      WinCe.Sound.PlaySound(filename);
    }

    public void SetTime(DateTime dateTime)
    {
      Nucleo.WinCe.System.SetLocalTime(dateTime);
    }

  }
}