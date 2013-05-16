using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class MasterArea
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (Id == null)
        result.AddValidationError("Id is undefined");

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

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public Int32? Id;
    public string Name;
    public string Description;
    public decimal? MinLatitude;
    public decimal? MaxLatitude;    
    public decimal? MinLongitude;
    public decimal? MaxLongitude;
    public string ContentBasePath;
    public string RegionData;

    public string RegionType;

    #endregion
  }

  public class MasterAreas: List<MasterArea>
  { 
  }
}
