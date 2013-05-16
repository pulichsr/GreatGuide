using System;

namespace Nucleo.GoodGuide.Types.Configuration
{
  [Serializable]
  public class GpsConfiguration
  {
    public short GpsTimeOffset
    {
      get { return gpsTimeOffset; }
      set { gpsTimeOffset = value; }
    }
    public short FixQualityThreshold
    {
      get { return fixQualityThreshold; }
      set { fixQualityThreshold = value; }
    }
    public int BaudRate
    {
      get { return baudRate; }
      set { baudRate = value; }
    }
    public string PortName
    {
      get { return portName; }
      set { portName = value; }
    }
    public bool SetSystemTimeOnFix
    {
      get { return setSystemTimeOnFix; }
      set { setSystemTimeOnFix = value; }
    }

    private Int16 gpsTimeOffset;
    private Int16 fixQualityThreshold;
    private Int32 baudRate;
    private string portName;
    private Boolean setSystemTimeOnFix;
  }
}