using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class ItineraryConverter
  {
    public static DataObjects.Itinerary ToDataOject(ItineraryDataset.ItineraryRow row)
    {
      DataObjects.Itinerary dataObject = new DataObjects.Itinerary();
      dataObject.ArrivalDat = row.IsArrivalDatNull() ? new DateTime?() : row.ArrivalDat;
      dataObject.BookingReference = row.sBookingReference;
      dataObject.Branding1 = row.sBranding1;
      dataObject.Branding2 = row.sBranding2;
      dataObject.Branding3 = row.sBranding3;
      dataObject.Culture = row.sCulture;
      dataObject.DepartureDat = row.IsDepartureDatNull() ? new DateTime?() : row.DepartureDat;
      dataObject.FirstName = row.sFirstName;
      dataObject.GeoFenceData = row.sGeofenceData;
      dataObject.GracePeriod = row.IsGracePeriodNull() ? new Int16?() : row.GracePeriod;
      dataObject.Id = row.IsIdNull() ? new int?() : row.Id;
      dataObject.LastName = row.sLastName;
      dataObject.Title = row.sTitle;
    
      return dataObject;
    }
  }
}