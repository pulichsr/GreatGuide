using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class RecentDestinationRecordReader :
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.RecentDestination recentDestination = (WrapperObjects.RecentDestination)targetObject;

      recentDestination.VisitedDat = RecordReaderHelper.ReadDateTime(dataReader, "VISITED_DAT");
      recentDestination.DestinationId = RecordReaderHelper.ReadInt32(dataReader, "DESTINATION_ID");
      recentDestination.Latitude = RecordReaderHelper.ReadDecimal(dataReader, "LATITUDE");
      recentDestination.Longitude = RecordReaderHelper.ReadDecimal(dataReader, "LONGITUDE");
      recentDestination.Name = RecordReaderHelper.ReadString(dataReader, "NAME");
    }
  }
}
  
