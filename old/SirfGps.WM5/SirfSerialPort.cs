using System;
using System.Runtime.InteropServices;

namespace Nucleo.GoodGuide.SirfGps
{
  public sealed class SirfSerialPort
  {
    public SirfSerialPort(string portName, int baudRate)
    {
      this.portName = portName;
      this.baudRate = baudRate;
    }

    [DllImport("TestDll.dll")]
    private static extern bool OpenPort(string lpFileName, int BaudRate);
    public void Open()
    {
      lock (lockObject)
      {
        OpenPort(portName, baudRate);
        isOpen = true;
      }
    }

    [DllImport("TestDll.dll")]
    private static extern void ClosePort();
    public void Close()
    {
      lock (lockObject)
      {
        ClosePort();
        isOpen = false;
      }
    }

    [DllImport("TestDll.dll")]
    private static extern int ReadPort(ref byte prtStr, int len);
    public int Read(byte[] data, ref uint maxByteCount)
    {
      lock (lockObject)
      {
        return ReadPort(ref data[0], Convert.ToInt32(maxByteCount));
      }
    }

    public bool IsOpen 
    {
      get { return isOpen; }
    }

    private bool isOpen = false;
    private readonly string portName;
    private readonly int baudRate;
    private readonly object lockObject = new object();
  }
}