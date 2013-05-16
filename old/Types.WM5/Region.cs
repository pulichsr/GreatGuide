using System;

namespace Nucleo.GoodGuide.Types
{
  public class Region
  {
    public const string EntryTrigger = "E";
    public const string ExitTrigger = "X";
    public const string WhileInTrigger = "W";
    public const string AnywhereTrigger = "A";

    public Region()
    {
    }
    public Region(Int32 id, float minLatitude, float maxLatitude, float minLongitude, float maxLongitude,Boolean resetOnEntry)
    {
      Id = id;
      MinLatitude = minLatitude;
      MaxLatitude = maxLatitude;
      MinLongitude = minLongitude;
      MaxLongitude = maxLongitude;
      ResetOnEntry = resetOnEntry;
    }

    public virtual Boolean IsInsideRegion(float latitude,float longitude)
    {
      return false;
    }

    public Int32 Id = 0;
    public float MinLatitude = float.MaxValue;
    public float MaxLatitude = float.MinValue;
    public float MinLongitude = float.MaxValue;
    public float MaxLongitude = float.MinValue;
    public Boolean ResetOnEntry = false;
  }
}
