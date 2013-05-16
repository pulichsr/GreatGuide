using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ItineraryRecordReader :
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.Itinerary itinerary = (WrapperObjects.Itinerary)targetObject;

      itinerary.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      itinerary.FirstName = RecordReaderHelper.ReadString(dataReader, "FIRST_NAME");
      itinerary.LastName = RecordReaderHelper.ReadString(dataReader, "LAST_NAME"); 
      itinerary.Title = RecordReaderHelper.ReadString(dataReader, "TITLE"); 
      itinerary.GracePeriod = RecordReaderHelper.ReadInt16(dataReader, "GRACE_PERIOD");
      itinerary.BookingReference = RecordReaderHelper.ReadString(dataReader, "BOOKING_REFERENCE"); 
      itinerary.GeoFenceData = RecordReaderHelper.ReadString(dataReader, "GEO_FENCE_DATA"); 
      itinerary.ArrivalDat = RecordReaderHelper.ReadDateTime(dataReader, "ARRIVAL_DAT");
      itinerary.DepartureDat = RecordReaderHelper.ReadDateTime(dataReader, "DEPARTURE_DAT");
      itinerary.Branding1 = RecordReaderHelper.ReadString(dataReader, "BRANDING1");
      itinerary.Branding2 = RecordReaderHelper.ReadString(dataReader, "BRANDING2");
      itinerary.Branding3 = RecordReaderHelper.ReadString(dataReader, "BRANDING3");
      itinerary.Culture = RecordReaderHelper.ReadString(dataReader, "CULTURE");
    }
  }
}
  