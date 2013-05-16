using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class DestinationCollectionMemberRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.DestinationCollectionMember destinationCollectionMember = (WrapperObjects.DestinationCollectionMember)targetObject;

      destinationCollectionMember.CollectionId = RecordReaderHelper.ReadInt32(dataReader,"COLLECTION_ID");
      destinationCollectionMember.DestinationId = RecordReaderHelper.ReadInt32(dataReader,"DESTINATION_ID");
    }
  }
}


