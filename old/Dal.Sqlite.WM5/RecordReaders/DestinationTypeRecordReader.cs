using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class DestinationTypeRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.DestinationType destinationType = (WrapperObjects.DestinationType)targetObject;

      destinationType.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      destinationType.Name = RecordReaderHelper.ReadString(dataReader, "NAME"); 
      destinationType.Description = RecordReaderHelper.ReadString(dataReader, "DESCRIPTION"); 
      destinationType.Code = RecordReaderHelper.ReadString(dataReader, "CODE"); 
      destinationType.Comment1Label = RecordReaderHelper.ReadString(dataReader, "COMMENT1_LABEL"); 
      destinationType.Comment2Label = RecordReaderHelper.ReadString(dataReader, "COMMENT2_LABEL"); 
      destinationType.Comment3Label = RecordReaderHelper.ReadString(dataReader, "COMMENT3_LABEL"); 
      destinationType.Comment4Label = RecordReaderHelper.ReadString(dataReader, "COMMENT4_LABEL"); 
      destinationType.IconResourceName = RecordReaderHelper.ReadString(dataReader, "ICON_RESOURCE_NAME"); 
    }
  }
}


