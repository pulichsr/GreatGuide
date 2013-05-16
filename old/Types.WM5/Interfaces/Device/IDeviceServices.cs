using System;

namespace Nucleo.GoodGuide.Types.Interfaces.Device
{
  public interface IDeviceServices
  {
    string FlashVersion { get;}
    UInt32 FmTransmitterChannel{get;set;}
    Boolean FMTransmitterState{get;set;}

    void Initialise();
    void PowerOff();
    void Suspend();
  }
}