using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ThemeRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.Theme theme = (WrapperObjects.Theme)targetObject;

      theme.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      theme.Name = RecordReaderHelper.ReadString(dataReader, "NAME"); 
      theme.Description = RecordReaderHelper.ReadString(dataReader, "DESCRIPTION"); 
    }
  }
}
