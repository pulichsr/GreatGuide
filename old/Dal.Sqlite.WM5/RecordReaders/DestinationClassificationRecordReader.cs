using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class DestinationClassificationRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.DestinationClassification destinationClassification = (WrapperObjects.DestinationClassification)targetObject;

      destinationClassification.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      destinationClassification.DestinationTypeId = RecordReaderHelper.ReadInt32(dataReader,"DESTINATION_TYPE_ID");
      destinationClassification.Name = RecordReaderHelper.ReadString(dataReader, "NAME"); 
      destinationClassification.Description = RecordReaderHelper.ReadString(dataReader, "DESCRIPTION"); 
      destinationClassification.Code = RecordReaderHelper.ReadString(dataReader, "CODE"); 
    }
  }
}


