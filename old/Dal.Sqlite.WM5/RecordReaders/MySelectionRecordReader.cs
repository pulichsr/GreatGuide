using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class MySelectionRecordReader :
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.MySelection mySelection = (WrapperObjects.MySelection)targetObject;

      mySelection.DestinationId = RecordReaderHelper.ReadInt32(dataReader, "DESTINATION_ID");
    }
  }
}
  