using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class DestinationRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.Destination destination = (WrapperObjects.Destination)targetObject;

      destination.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      destination.Code = RecordReaderHelper.ReadString(dataReader, "CODE"); 
      destination.ShortDescription = RecordReaderHelper.ReadString(dataReader, "SHORT_DESCRIPTION");
      destination.LongDescription = RecordReaderHelper.ReadString(dataReader, "LONG_DESCRIPTION");
      destination.DestinationTypeId = RecordReaderHelper.ReadInt32(dataReader,"DESTINATION_TYPE_ID");
      destination.ClassificationId = RecordReaderHelper.ReadInt32(dataReader,"CLASSIFICATION_ID");
      destination.ArrivalZoneData = RecordReaderHelper.ReadString(dataReader, "ARRIVAL_ZONE_DATA");
		  destination.Address = RecordReaderHelper.ReadString(dataReader,"ADDRESS");
		  destination.TelephoneNo = RecordReaderHelper.ReadString(dataReader,"TELEPHONE_NO");
		  destination.MasterAreaId = RecordReaderHelper.ReadInt32(dataReader,"MASTER_AREA_ID");
      destination.ThemeId = RecordReaderHelper.ReadInt32(dataReader, "THEME_ID");
      destination.VersionNo = RecordReaderHelper.ReadInt32(dataReader, "VERSION_NO");
      destination.Recommendation = RecordReaderHelper.ReadInt16(dataReader, "RECOMMENDATION");
      destination.Location = RecordReaderHelper.ReadString(dataReader, "LOCATION");
		  destination.Condition = RecordReaderHelper.ReadString(dataReader,"CONDITION");
		  destination.Latitude = RecordReaderHelper.ReadDecimal(dataReader,"LATITUDE");
      destination.Longitude = RecordReaderHelper.ReadDecimal(dataReader, "LONGITUDE");
      destination.CellNo = RecordReaderHelper.ReadString(dataReader, "CELL_NO");
      destination.Image1Filename = RecordReaderHelper.ReadString(dataReader, "IMAGE1_FILENAME");
      destination.Image2Filename = RecordReaderHelper.ReadString(dataReader, "IMAGE2_FILENAME");
      destination.Comment1 = RecordReaderHelper.ReadString(dataReader, "COMMENT1");
      destination.Comment2 = RecordReaderHelper.ReadString(dataReader, "COMMENT2");
      destination.Comment3 = RecordReaderHelper.ReadString(dataReader, "COMMENT3");
      destination.Comment4 = RecordReaderHelper.ReadString(dataReader, "COMMENT4");
      destination.Booking = RecordReaderHelper.ReadString(dataReader, "BOOKING");
      destination.Language = RecordReaderHelper.ReadString(dataReader, "LANGUAGE");
      destination.Comment = RecordReaderHelper.ReadString(dataReader, "COMMENT");
		  destination.CollectionId = RecordReaderHelper.ReadInt32(dataReader,"COLLECTION_ID");
		  destination.ArrivalZoneType = RecordReaderHelper.ReadString(dataReader,"ARRIVAL_ZONE_TYPE");
      destination.ArrivalZoneMinLatitude = RecordReaderHelper.ReadDecimal(dataReader, "ARRIVAL_ZONE_MIN_LATITUDE");
      destination.ArrivalZoneMaxLatitude = RecordReaderHelper.ReadDecimal(dataReader, "ARRIVAL_ZONE_MAX_LATITUDE");
      destination.ArrivalZoneMinLongitude = RecordReaderHelper.ReadDecimal(dataReader, "ARRIVAL_ZONE_MIN_LONGITUDE");
      destination.ArrivalZoneMaxLongitude = RecordReaderHelper.ReadDecimal(dataReader, "ARRIVAL_ZONE_MAX_LONGITUDE");
		  destination.GridReferenceX = RecordReaderHelper.ReadInt32(dataReader,"GRID_REFERENCE_X");
      destination.GridReferenceY = RecordReaderHelper.ReadInt32(dataReader, "GRID_REFERENCE_Y");
      destination.IncludeInNearestSearch = RecordReaderHelper.ReadBoolean(dataReader, "INCLUDE_IN_NEAREST_SEARCH");
      destination.SearchKey = RecordReaderHelper.ReadString(dataReader, "SEARCH_KEY");
    }
  }
}


