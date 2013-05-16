using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class ItineraryDestination
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
    public Int32? ItineraryDayId;
    public Int32? DestinationId;
    public Int16? DestinationOrder;

    #endregion
  }

  public class ItineraryDestinations : List<ItineraryDestination>
  { 
  }
}
