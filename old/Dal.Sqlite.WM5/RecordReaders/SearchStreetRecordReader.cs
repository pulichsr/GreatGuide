using System;
using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class SearchStreetRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.SearchStreet searchStreet = (WrapperObjects.SearchStreet)targetObject;

      searchStreet.Id = (Int32)RecordReaderHelper.ReadInt64(dataReader,"ID").Value;
      searchStreet.RegionId = (Int32)RecordReaderHelper.ReadInt64(dataReader, "REGION_ID").Value;
      searchStreet.SearchKey = RecordReaderHelper.ReadString(dataReader, "SEARCH_KEY"); 
      searchStreet.Name = RecordReaderHelper.ReadString(dataReader, "NAME"); 
      searchStreet.CollatedName = RecordReaderHelper.ReadString(dataReader, "COLLATED_NAME"); 
      searchStreet.RegionCollatedName = RecordReaderHelper.ReadString(dataReader, "REGION_COLLATED_NAME"); 
      searchStreet.Latitude = RecordReaderHelper.ReadDouble(dataReader, "LATITUDE").Value; 
      searchStreet.Longitude = RecordReaderHelper.ReadDouble(dataReader, "LONGITUDE").Value; 
      searchStreet.StreetNumbers = RecordReaderHelper.ReadString(dataReader, "STREET_NUMBERS"); 
    }
  }
}
