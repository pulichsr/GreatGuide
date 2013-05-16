using System;
using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class SearchRegionRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.SearchRegion searchRegion = (WrapperObjects.SearchRegion)targetObject;

      searchRegion.Id = (Int32)RecordReaderHelper.ReadInt64(dataReader,"ID").Value;
      searchRegion.ParentId = (Int32)RecordReaderHelper.ReadInt64(dataReader,"PARENT_ID").Value;
      searchRegion.SearchKey = RecordReaderHelper.ReadString(dataReader, "SEARCH_KEY"); 
      searchRegion.Name = RecordReaderHelper.ReadString(dataReader, "NAME"); 
      searchRegion.CollatedName = RecordReaderHelper.ReadString(dataReader, "COLLATED_NAME");
      searchRegion.Level = (Int32)RecordReaderHelper.ReadInt64(dataReader, "LEVEL").Value;
      searchRegion.Latitude = RecordReaderHelper.ReadDouble(dataReader, "LATITUDE").Value;
      searchRegion.Longitude = RecordReaderHelper.ReadDouble(dataReader, "LONGITUDE").Value;
    }
  }
}
