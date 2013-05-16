using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class Itinerary
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
    public string FirstName;
    public string LastName;
    public string Title;
    public Int16? GracePeriod;
    public string BookingReference;
    public string GeoFenceData;
    public DateTime? ArrivalDat;
    public DateTime? DepartureDat;
    public string Branding1;
    public string Branding2;
    public string Branding3;
    public string Culture;

    #endregion
  }

  public class Itineraries : List<Itinerary>
  {
  }
}
