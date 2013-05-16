using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ItineraryDayRecordReader :
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.ItineraryDay itineraryDay = (WrapperObjects.ItineraryDay)targetObject;

      itineraryDay.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      itineraryDay.ItineraryDat = RecordReaderHelper.ReadDateTime(dataReader, "ITINERARY_DAT");
      itineraryDay.Comment = RecordReaderHelper.ReadString(dataReader, "COMMENT");
    }
  }
}
  