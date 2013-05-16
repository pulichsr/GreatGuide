using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class MasterAreaRecordReader :
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.MasterArea masterArea = (WrapperObjects.MasterArea)targetObject;

      masterArea.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      masterArea.Name = RecordReaderHelper.ReadString(dataReader, "NAME");
      masterArea.Description = RecordReaderHelper.ReadString(dataReader, "DESCRIPTION");
      masterArea.MinLatitude = RecordReaderHelper.ReadDecimal(dataReader, "MIN_LATITUDE");
      masterArea.MaxLatitude = RecordReaderHelper.ReadDecimal(dataReader, "MAX_LATITUDE");
      masterArea.MinLongitude = RecordReaderHelper.ReadDecimal(dataReader, "MIN_LONGITUDE");
      masterArea.MaxLongitude = RecordReaderHelper.ReadDecimal(dataReader, "MAX_LONGITUDE");
      masterArea.ContentBasePath = RecordReaderHelper.ReadString(dataReader, "CONTENT_BASE_PATH"); 
      masterArea.RegionData = RecordReaderHelper.ReadString(dataReader, "REGION_DATA"); 
      masterArea.RegionType = RecordReaderHelper.ReadString(dataReader, "REGION_TYPE"); 
    }
  }
}
