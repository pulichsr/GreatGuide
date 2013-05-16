using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class DestinationConverter
  {
    public static DataObjects.Destination ToDataOject(DestinationDataset.DestinationRow row)
    {
      DataObjects.Destination dataObject = new DataObjects.Destination();
      dataObject.Address = row.sAddress;
      dataObject.ArrivalZoneData = row.sArrivalZoneData;
      dataObject.ArrivalZoneMaxLatitude = row.IsArrivalZoneMaxLatitudeNull() ? new decimal?() : (decimal)row.ArrivalZoneMaxLatitude;
      dataObject.ArrivalZoneMaxLongitude = row.IsArrivalZoneMaxLongitudeNull() ? new decimal?() : (decimal)row.ArrivalZoneMaxLongitude;
      dataObject.ArrivalZoneMinLatitude = row.IsArrivalZoneMinLatitudeNull() ? new decimal?() : (decimal)row.ArrivalZoneMinLatitude;
      dataObject.ArrivalZoneMinLongitude = row.IsArrivalZoneMinLongitudeNull() ? new decimal?() : (decimal)row.ArrivalZoneMinLongitude;
      dataObject.ArrivalZoneType = row.sArrivalZoneType;
      dataObject.Booking = row.sBooking;
      dataObject.CellNo = row.CellNo;
      dataObject.ClassificationId = row.IsClassificationIdNull() ? new Int32?() : row.ClassificationId;
      dataObject.Code = row.sCode;
      dataObject.CollectionId = row.IsCollectionIdNull() ? new Int32?() : row.CollectionId;
      dataObject.Comment = row.sComment;
      dataObject.Comment1 = row.sComment1;
      dataObject.Comment2 = row.sComment2;
      dataObject.Comment3 = row.sComment3;
      dataObject.Comment4 = row.sComment4;
      dataObject.Condition = row.sCondition;
      dataObject.DestinationTypeId = row.IsDestinationTypeIdNull() ? new Int32?() : row.DestinationTypeId;
      dataObject.GridReferenceX = row.IsGridReferenceXNull() ? new Int32?() : row.GridReferenceX;
      dataObject.GridReferenceY = row.IsGridReferenceYNull() ? new Int32?() : row.GridReferenceY;
      dataObject.Id = row.IsIdNull() ? new Int32?() : row.Id;
      dataObject.Image1Filename = row.sImage1Filename;
      dataObject.Image2Filename = row.sImage2Filename;
      dataObject.IncludeInNearestSearch = row.IsIncludeInNearestSearchNull() ? false : row.IncludeInNearestSearch;
      dataObject.Language = row.sLanguage;
      dataObject.Latitude = row.IsLatitudeNull() ? new decimal?() : (decimal)row.Latitude;
      dataObject.Location = row.sLocation;
      dataObject.LongDescription = row.sLongDescription;
      dataObject.Longitude = row.IsLongitudeNull() ? new decimal?() : (decimal)row.Longitude;
      dataObject.MasterAreaId = row.IsMasterAreaIdNull() ? new Int32?() : row.MasterAreaId;
      dataObject.Recommendation = row.IsRecommendationNull() ? new Int16?() : row.Recommendation;
      dataObject.ShortDescription = row.sShortDescription;
      dataObject.TelephoneNo = row.sTelephoneNo;
      dataObject.ThemeId = row.IsThemeIdNull() ? new Int32?() : row.ThemeId;
      dataObject.VersionNo = row.IsVersionNoNull() ? new Int32?() : row.VersionNo;
      dataObject.SearchKey = row.sSearchKey;
    
      return dataObject;
    }
  }
}