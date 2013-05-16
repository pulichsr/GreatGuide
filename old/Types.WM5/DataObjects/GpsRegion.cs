using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class GpsRegion
  {
    public const string EntryTrigger = "E";
    public const string ExitTrigger = "X";
    public const string WhileInTrigger = "W";
    public const string AnywhereTrigger = "A";

    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (Id == null)
        result.AddValidationError("Id is undefined");

      if (string.IsNullOrEmpty(RegionType) == true)
        result.AddValidationError("RegionType is undefined");

      if (MinLatitude == null)
        result.AddValidationError("MinLatitude is undefined");

      if (MaxLatitude == null)
        result.AddValidationError("MaxLatitude is undefined");

      if (MinLongitude == null)
        result.AddValidationError("MinLongitude is undefined");

      if (MaxLongitude == null)
        result.AddValidationError("MaxLongitude is undefined");

      if (string.IsNullOrEmpty(RegionData) == true)
        result.AddValidationError("RegionData is undefined");

      if (MasterAreaId == null)
        result.AddValidationError("MasterAreaId is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public Int32? Id;
    public string RegionType;
    public string RegionData;
    public decimal? MinLatitude;
    public decimal? MaxLatitude;
    public decimal? MinLongitude;
    public decimal? MaxLongitude;
    public bool ResetOnEntry;
    public Int32? MasterAreaId;
    public Int32? ThemeId;

    public string Name;
    public string Description;

    #endregion
  }

  public class GpsRegions : List<GpsRegion>
  { 
    public GpsRegion FindById(Int32 id)
    {
      for (Int32 regionNo = 0; regionNo < Count; regionNo++)
        if (this[regionNo].Id.Value == id)
          return this[regionNo];

      return null;
    }
  }
}
