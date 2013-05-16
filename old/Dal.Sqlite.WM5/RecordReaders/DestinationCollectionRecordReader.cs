using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class DestinationCollectionRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.DestinationCollection destinationCollection = (WrapperObjects.DestinationCollection)targetObject;

      destinationCollection.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      destinationCollection.Name = RecordReaderHelper.ReadString(dataReader, "NAME"); 
      destinationCollection.Code = RecordReaderHelper.ReadString(dataReader, "CODE"); 
    }
  }
}


