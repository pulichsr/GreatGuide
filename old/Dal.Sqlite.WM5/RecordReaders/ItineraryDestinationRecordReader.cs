using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ItineraryDestinationRecordReader :
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.ItineraryDestination itineraryDestination = (WrapperObjects.ItineraryDestination)targetObject;

      itineraryDestination.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      itineraryDestination.ItineraryDayId = RecordReaderHelper.ReadInt32(dataReader, "ITINERARY_DAY_ID");
      itineraryDestination.DestinationId = RecordReaderHelper.ReadInt32(dataReader, "DESTINATION_ID");
      itineraryDestination.DestinationOrder = RecordReaderHelper.ReadInt16(dataReader, "DESTINATION_ORDER");
    }
  }
}
  