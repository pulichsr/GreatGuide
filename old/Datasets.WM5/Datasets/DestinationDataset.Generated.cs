//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;


namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class DestinationDataset
  {
    public partial class DestinationRow
    {
		#region String properties
		public string sId
		{
			get
			{
			  if (IsNull(tableDestination.IdColumn) == true)
			    return string.Empty;
			  else
			    return Id.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.IdColumn] = DBNull.Value;
			  else
			    Id = Convert.ToInt32(value.Trim());
			}
		}
		public string sCode
		{
			get
			{
			  if (IsNull(tableDestination.CodeColumn) == true)
			    return string.Empty;
			  else
			    return Code;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.CodeColumn] = DBNull.Value;
			  else
			    Code = value.Trim();
			}
		}
		public string sShortDescription
		{
			get
			{
			  if (IsNull(tableDestination.ShortDescriptionColumn) == true)
			    return string.Empty;
			  else
			    return ShortDescription;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ShortDescriptionColumn] = DBNull.Value;
			  else
			    ShortDescription = value.Trim();
			}
		}
		public string sLongDescription
		{
			get
			{
			  if (IsNull(tableDestination.LongDescriptionColumn) == true)
			    return string.Empty;
			  else
			    return LongDescription;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.LongDescriptionColumn] = DBNull.Value;
			  else
			    LongDescription = value.Trim();
			}
		}
		public string sDestinationTypeId
		{
			get
			{
			  if (IsNull(tableDestination.DestinationTypeIdColumn) == true)
			    return string.Empty;
			  else
			    return DestinationTypeId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.DestinationTypeIdColumn] = DBNull.Value;
			  else
			    DestinationTypeId = Convert.ToInt32(value.Trim());
			}
		}
		public string sClassificationId
		{
			get
			{
			  if (IsNull(tableDestination.ClassificationIdColumn) == true)
			    return string.Empty;
			  else
			    return ClassificationId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ClassificationIdColumn] = DBNull.Value;
			  else
			    ClassificationId = Convert.ToInt32(value.Trim());
			}
		}
		public string sArrivalZoneData
		{
			get
			{
			  if (IsNull(tableDestination.ArrivalZoneDataColumn) == true)
			    return string.Empty;
			  else
			    return ArrivalZoneData;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ArrivalZoneDataColumn] = DBNull.Value;
			  else
			    ArrivalZoneData = value.Trim();
			}
		}
		public string sAddress
		{
			get
			{
			  if (IsNull(tableDestination.AddressColumn) == true)
			    return string.Empty;
			  else
			    return Address;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.AddressColumn] = DBNull.Value;
			  else
			    Address = value.Trim();
			}
		}
		public string sTelephoneNo
		{
			get
			{
			  if (IsNull(tableDestination.TelephoneNoColumn) == true)
			    return string.Empty;
			  else
			    return TelephoneNo;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.TelephoneNoColumn] = DBNull.Value;
			  else
			    TelephoneNo = value.Trim();
			}
		}
		public string sImage1Filename
		{
			get
			{
			  if (IsNull(tableDestination.Image1FilenameColumn) == true)
			    return string.Empty;
			  else
			    return Image1Filename;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.Image1FilenameColumn] = DBNull.Value;
			  else
			    Image1Filename = value.Trim();
			}
		}
		public string sMasterAreaId
		{
			get
			{
			  if (IsNull(tableDestination.MasterAreaIdColumn) == true)
			    return string.Empty;
			  else
			    return MasterAreaId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.MasterAreaIdColumn] = DBNull.Value;
			  else
			    MasterAreaId = Convert.ToInt32(value.Trim());
			}
		}
		public string sThemeId
		{
			get
			{
			  if (IsNull(tableDestination.ThemeIdColumn) == true)
			    return string.Empty;
			  else
			    return ThemeId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ThemeIdColumn] = DBNull.Value;
			  else
			    ThemeId = Convert.ToInt32(value.Trim());
			}
		}
		public string sVersionNo
		{
			get
			{
			  if (IsNull(tableDestination.VersionNoColumn) == true)
			    return string.Empty;
			  else
			    return VersionNo.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.VersionNoColumn] = DBNull.Value;
			  else
			    VersionNo = Convert.ToInt32(value.Trim());
			}
		}
		public string sRecommendation
		{
			get
			{
			  if (IsNull(tableDestination.RecommendationColumn) == true)
			    return string.Empty;
			  else
			    return Recommendation.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.RecommendationColumn] = DBNull.Value;
			  else
			    Recommendation = Convert.ToInt16(value.Trim());
			}
		}
		public string sCondition
		{
			get
			{
			  if (IsNull(tableDestination.ConditionColumn) == true)
			    return string.Empty;
			  else
			    return Condition;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ConditionColumn] = DBNull.Value;
			  else
			    Condition = value.Trim();
			}
		}
		public string sLocation
		{
			get
			{
			  if (IsNull(tableDestination.LocationColumn) == true)
			    return string.Empty;
			  else
			    return Location;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.LocationColumn] = DBNull.Value;
			  else
			    Location = value.Trim();
			}
		}
		public string sDestinationTypeCode
		{
			get
			{
			  if (IsNull(tableDestination.DestinationTypeCodeColumn) == true)
			    return string.Empty;
			  else
			    return DestinationTypeCode;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.DestinationTypeCodeColumn] = DBNull.Value;
			  else
			    DestinationTypeCode = value.Trim();
			}
		}
		public string sClassificationCode
		{
			get
			{
			  if (IsNull(tableDestination.ClassificationCodeColumn) == true)
			    return string.Empty;
			  else
			    return ClassificationCode;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ClassificationCodeColumn] = DBNull.Value;
			  else
			    ClassificationCode = value.Trim();
			}
		}
		public string sLatitude
		{
			get
			{
			  if (IsNull(tableDestination.LatitudeColumn) == true)
			    return string.Empty;
			  else
			    return Latitude.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.LatitudeColumn] = DBNull.Value;
			  else
			    Latitude = Convert.ToSingle(value.Trim());
			}
		}
		public string sLongitude
		{
			get
			{
			  if (IsNull(tableDestination.LongitudeColumn) == true)
			    return string.Empty;
			  else
			    return Longitude.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.LongitudeColumn] = DBNull.Value;
			  else
			    Longitude = Convert.ToSingle(value.Trim());
			}
		}
		public string sCellNo
		{
			get
			{
			  if (IsNull(tableDestination.CellNoColumn) == true)
			    return string.Empty;
			  else
			    return CellNo;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.CellNoColumn] = DBNull.Value;
			  else
			    CellNo = value.Trim();
			}
		}
		public string sImage2Filename
		{
			get
			{
			  if (IsNull(tableDestination.Image2FilenameColumn) == true)
			    return string.Empty;
			  else
			    return Image2Filename;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.Image2FilenameColumn] = DBNull.Value;
			  else
			    Image2Filename = value.Trim();
			}
		}
		public string sComment1
		{
			get
			{
			  if (IsNull(tableDestination.Comment1Column) == true)
			    return string.Empty;
			  else
			    return Comment1;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.Comment1Column] = DBNull.Value;
			  else
			    Comment1 = value.Trim();
			}
		}
		public string sComment2
		{
			get
			{
			  if (IsNull(tableDestination.Comment2Column) == true)
			    return string.Empty;
			  else
			    return Comment2;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.Comment2Column] = DBNull.Value;
			  else
			    Comment2 = value.Trim();
			}
		}
		public string sComment3
		{
			get
			{
			  if (IsNull(tableDestination.Comment3Column) == true)
			    return string.Empty;
			  else
			    return Comment3;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.Comment3Column] = DBNull.Value;
			  else
			    Comment3 = value.Trim();
			}
		}
		public string sComment4
		{
			get
			{
			  if (IsNull(tableDestination.Comment4Column) == true)
			    return string.Empty;
			  else
			    return Comment4;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.Comment4Column] = DBNull.Value;
			  else
			    Comment4 = value.Trim();
			}
		}
		public string sBooking
		{
			get
			{
			  if (IsNull(tableDestination.BookingColumn) == true)
			    return string.Empty;
			  else
			    return Booking;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.BookingColumn] = DBNull.Value;
			  else
			    Booking = value.Trim();
			}
		}
		public string sLanguage
		{
			get
			{
			  if (IsNull(tableDestination.LanguageColumn) == true)
			    return string.Empty;
			  else
			    return Language;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.LanguageColumn] = DBNull.Value;
			  else
			    Language = value.Trim();
			}
		}
		public string sComment
		{
			get
			{
			  if (IsNull(tableDestination.CommentColumn) == true)
			    return string.Empty;
			  else
			    return Comment;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.CommentColumn] = DBNull.Value;
			  else
			    Comment = value.Trim();
			}
		}
		public string sCollectionId
		{
			get
			{
			  if (IsNull(tableDestination.CollectionIdColumn) == true)
			    return string.Empty;
			  else
			    return CollectionId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.CollectionIdColumn] = DBNull.Value;
			  else
			    CollectionId = Convert.ToInt32(value.Trim());
			}
		}
		public string sIncludeInNearestSearch
		{
			get
			{
			  if (IsNull(tableDestination.IncludeInNearestSearchColumn) == true)
			    return string.Empty;
			  else
			    return IncludeInNearestSearch.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.IncludeInNearestSearchColumn] = DBNull.Value;
			  else
			    IncludeInNearestSearch = Convert.ToBoolean(value.Trim());
			}
		}
		public string sArrivalZoneType
		{
			get
			{
			  if (IsNull(tableDestination.ArrivalZoneTypeColumn) == true)
			    return string.Empty;
			  else
			    return ArrivalZoneType;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ArrivalZoneTypeColumn] = DBNull.Value;
			  else
			    ArrivalZoneType = value.Trim();
			}
		}
		public string sArrivalZoneMinLatitude
		{
			get
			{
			  if (IsNull(tableDestination.ArrivalZoneMinLatitudeColumn) == true)
			    return string.Empty;
			  else
			    return ArrivalZoneMinLatitude.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ArrivalZoneMinLatitudeColumn] = DBNull.Value;
			  else
			    ArrivalZoneMinLatitude = Convert.ToSingle(value.Trim());
			}
		}
		public string sArrivalZoneMaxLatitude
		{
			get
			{
			  if (IsNull(tableDestination.ArrivalZoneMaxLatitudeColumn) == true)
			    return string.Empty;
			  else
			    return ArrivalZoneMaxLatitude.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ArrivalZoneMaxLatitudeColumn] = DBNull.Value;
			  else
			    ArrivalZoneMaxLatitude = Convert.ToSingle(value.Trim());
			}
		}
		public string sArrivalZoneMinLongitude
		{
			get
			{
			  if (IsNull(tableDestination.ArrivalZoneMinLongitudeColumn) == true)
			    return string.Empty;
			  else
			    return ArrivalZoneMinLongitude.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ArrivalZoneMinLongitudeColumn] = DBNull.Value;
			  else
			    ArrivalZoneMinLongitude = Convert.ToSingle(value.Trim());
			}
		}
		public string sArrivalZoneMaxLongitude
		{
			get
			{
			  if (IsNull(tableDestination.ArrivalZoneMaxLongitudeColumn) == true)
			    return string.Empty;
			  else
			    return ArrivalZoneMaxLongitude.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.ArrivalZoneMaxLongitudeColumn] = DBNull.Value;
			  else
			    ArrivalZoneMaxLongitude = Convert.ToSingle(value.Trim());
			}
		}
		public string sGridReferenceX
		{
			get
			{
			  if (IsNull(tableDestination.GridReferenceXColumn) == true)
			    return string.Empty;
			  else
			    return GridReferenceX.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.GridReferenceXColumn] = DBNull.Value;
			  else
			    GridReferenceX = Convert.ToInt32(value.Trim());
			}
		}
		public string sGridReferenceY
		{
			get
			{
			  if (IsNull(tableDestination.GridReferenceYColumn) == true)
			    return string.Empty;
			  else
			    return GridReferenceY.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableDestination.GridReferenceYColumn] = DBNull.Value;
			  else
			    GridReferenceY = Convert.ToInt32(value.Trim());
			}
		}
      public string sSearchKey
      {
        get
        {
          if (IsNull(tableDestination.SearchKeyColumn) == true)
            return string.Empty;
          else
            return SearchKey;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestination.SearchKeyColumn] = DBNull.Value;
          else
            SearchKey = value.Trim();
        }
      }
    #endregion
 
    }
  }
}
		  
