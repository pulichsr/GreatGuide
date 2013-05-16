using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class ItineraryDay
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (Id == null)
        result.AddValidationError("Id is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2
    
    public Int32? Id;
		public DateTime? ItineraryDat;
		public string Comment;
    
    #endregion
  }

  public class ItineraryDays : List<ItineraryDay>
  { 
  }
}
