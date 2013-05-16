using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class RecentDestination
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (VisitedDat == null)
        result.AddValidationError("VisitedDat is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public DateTime? VisitedDat;
    public Int32? DestinationId;
    public decimal? Latitude;
    public decimal? Longitude;
    public string Name;

    #endregion
  }

  public class RecentDestinations : List<RecentDestination>
  { 
  }
}
