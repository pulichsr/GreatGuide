using System;

namespace Nucleo.GoodGuide.Types.Interfaces.OperatingSystemServices
{
  public interface IOperatingSystemServices
  {
    UInt16 Volume { get;set;}
    void PlaySound(string filename);

    void SetTime(DateTime dateTime);
  }
}