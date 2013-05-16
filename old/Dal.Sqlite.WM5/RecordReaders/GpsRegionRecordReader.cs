using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class GpsRegionRecordReader :
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.GpsRegion gpsRegion = (WrapperObjects.GpsRegion)targetObject;

      gpsRegion.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      gpsRegion.RegionType = RecordReaderHelper.ReadString(dataReader, "REGION_TYPE"); 
      gpsRegion.RegionData = RecordReaderHelper.ReadString(dataReader, "REGION_DATA"); 
      gpsRegion.MinLatitude = RecordReaderHelper.ReadDecimal(dataReader, "MIN_LATITUDE");
      gpsRegion.MaxLatitude = RecordReaderHelper.ReadDecimal(dataReader, "MAX_LATITUDE");
      gpsRegion.MinLongitude = RecordReaderHelper.ReadDecimal(dataReader, "MIN_LONGITUDE");
      gpsRegion.MaxLongitude = RecordReaderHelper.ReadDecimal(dataReader, "MAX_LONGITUDE");
      gpsRegion.ResetOnEntry = RecordReaderHelper.ReadBoolean(dataReader, "RESET_ON_ENTRY");
		  gpsRegion.MasterAreaId = RecordReaderHelper.ReadInt32(dataReader,"MASTER_AREA_ID");
      gpsRegion.ThemeId = RecordReaderHelper.ReadInt32(dataReader, "THEME_ID");
      gpsRegion.Name = RecordReaderHelper.ReadString(dataReader, "NAME");
    }
  }
}
