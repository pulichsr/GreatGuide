//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToXmlDataTable template, $Rev: 1h $
//========================================================================
using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.Datasets.XmlDatasets
{
  public partial class DestinationXmlDataTableDecoder : XmlDataTableDecoder
  {
    public DestinationXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationDataset.DestinationDataTable Table = new DestinationDataset.DestinationDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationDataset.DestinationDataTable Table = (DestinationDataset.DestinationDataTable)ATable;
      return Table.NewDestinationRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      DestinationDataset.DestinationRow Row = (DestinationDataset.DestinationRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "Code":
          Row.sCode = AContent;
          break;
        case "ShortDescription":
          Row.sShortDescription = AContent;
          break;
        case "LongDescription":
          Row.sLongDescription = AContent;
          break;
        case "DestinationTypeId":
          Row.DestinationTypeId = Convert.ToInt32(AContent);
          break;
        case "ClassificationId":
          Row.ClassificationId = Convert.ToInt32(AContent);
          break;
        case "ArrivalZoneData":
          Row.sArrivalZoneData = AContent;
          break;
        case "Address":
          Row.sAddress = AContent;
          break;
        case "TelephoneNo":
          Row.sTelephoneNo = AContent;
          break;
        case "Image1Filename":
          Row.sImage1Filename = AContent;
          break;
        case "MasterAreaId":
          Row.MasterAreaId = Convert.ToInt32(AContent);
          break;
        case "ThemeId":
          Row.ThemeId = Convert.ToInt32(AContent);
          break;
        case "VersionNo":
          Row.VersionNo = Convert.ToInt32(AContent);
          break;
        case "Recommendation":
          Row.Recommendation = Convert.ToInt16(AContent);
          break;
        case "Condition":
          Row.sCondition = AContent;
          break;
        case "Location":
          Row.sLocation = AContent;
          break;
        case "DestinationTypeCode":
          Row.sDestinationTypeCode = AContent;
          break;
        case "ClassificationCode":
          Row.sClassificationCode = AContent;
          break;
        case "Latitude":
          Row.sLatitude = AContent;
          break;
        case "Longitude":
          Row.sLongitude = AContent;
          break;
        case "CellNo":
          Row.sCellNo = AContent;
          break;
        case "Image2Filename":
          Row.sImage2Filename = AContent;
          break;
        case "Comment1":
          Row.sComment1 = AContent;
          break;
        case "Comment2":
          Row.sComment2 = AContent;
          break;
        case "Comment3":
          Row.sComment3 = AContent;
          break;
        case "Comment4":
          Row.sComment4 = AContent;
          break;
        case "Booking":
          Row.sBooking = AContent;
          break;
        case "Language":
          Row.sLanguage = AContent;
          break;
        case "Comment":
          Row.sComment = AContent;
          break;
        case "CollectionId":
          Row.CollectionId = Convert.ToInt32(AContent);
          break;
        case "IncludeInNearestSearch":
          if (AContent == "1")
            Row.IncludeInNearestSearch = true;
          else
            Row.IncludeInNearestSearch = false;
          break;
        case "ArrivalZoneType":
          Row.sArrivalZoneType = AContent;
          break;
        case "ArrivalZoneMinLatitude":
          Row.sArrivalZoneMinLatitude = AContent;
          break;
        case "ArrivalZoneMaxLatitude":
          Row.sArrivalZoneMaxLatitude = AContent;
          break;
        case "ArrivalZoneMinLongitude":
          Row.sArrivalZoneMinLongitude = AContent;
          break;
        case "ArrivalZoneMaxLongitude":
          Row.sArrivalZoneMaxLongitude = AContent;
          break;
        case "GridReferenceX":
          Row.GridReferenceX = Convert.ToInt32(AContent);
          break;
        case "GridReferenceY":
          Row.GridReferenceY = Convert.ToInt32(AContent);
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      DestinationDataset.DestinationRow Row = (DestinationDataset.DestinationRow)ARow;
    }
	
  }

  public partial class DestinationXmlDataTableEncoder : XmlDataTableEncoder
  {
    public DestinationXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationDataset.DestinationDataTable Table = new DestinationDataset.DestinationDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationDataset.DestinationDataTable Table = (DestinationDataset.DestinationDataTable)ATable;
      return Table.NewDestinationRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      DestinationDataset.DestinationRow Row = (DestinationDataset.DestinationRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "Code":
          return Row.sCode;
        case "ShortDescription":
          return Row.sShortDescription;
        case "LongDescription":
          return Row.sLongDescription;
        case "DestinationTypeId":
          return Row.sDestinationTypeId;
        case "ClassificationId":
          return Row.sClassificationId;
        case "ArrivalZoneData":
          return Row.sArrivalZoneData;
        case "Address":
          return Row.sAddress;
        case "TelephoneNo":
          return Row.sTelephoneNo;
        case "Image1Filename":
          return Row.sImage1Filename;
        case "MasterAreaId":
          return Row.sMasterAreaId;
        case "ThemeId":
          return Row.sThemeId;
        case "VersionNo":
          return Row.sVersionNo;
        case "Recommendation":
          return Row.sRecommendation;
        case "Condition":
          return Row.sCondition;
        case "Location":
          return Row.sLocation;
        case "DestinationTypeCode":
          return Row.sDestinationTypeCode;
        case "ClassificationCode":
          return Row.sClassificationCode;
        case "Latitude":
          return Row.sLatitude;
        case "Longitude":
          return Row.sLongitude;
        case "CellNo":
          return Row.sCellNo;
        case "Image2Filename":
          return Row.sImage2Filename;
        case "Comment1":
          return Row.sComment1;
        case "Comment2":
          return Row.sComment2;
        case "Comment3":
          return Row.sComment3;
        case "Comment4":
          return Row.sComment4;
        case "Booking":
          return Row.sBooking;
        case "Language":
          return Row.sLanguage;
        case "Comment":
          return Row.sComment;
        case "CollectionId":
          return Row.sCollectionId;
        case "IncludeInNearestSearch":
          if (Row.IncludeInNearestSearch == true)
            return "1";
          else
            return "0";
        case "ArrivalZoneType":
          return Row.sArrivalZoneType;
        case "ArrivalZoneMinLatitude":
          return Row.sArrivalZoneMinLatitude;
        case "ArrivalZoneMaxLatitude":
          return Row.sArrivalZoneMaxLatitude;
        case "ArrivalZoneMinLongitude":
          return Row.sArrivalZoneMinLongitude;
        case "ArrivalZoneMaxLongitude":
          return Row.sArrivalZoneMaxLongitude;
        case "GridReferenceX":
          return Row.sGridReferenceX;
        case "GridReferenceY":
          return Row.sGridReferenceY;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      DestinationDataset.DestinationRow Row = (DestinationDataset.DestinationRow)ARow;
    }
	
  }
}