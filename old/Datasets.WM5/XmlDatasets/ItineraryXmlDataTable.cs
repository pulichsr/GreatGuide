//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToXmlDataTable template, $Rev: 1h $
//========================================================================
using System;
using System.Data;
using Nucleo.Xml;
using ItineraryDataset=Nucleo.GoodGuide.Datasets.Datasets.ItineraryDataset;

namespace Nucleo.GoodGuide.Datasets.XmlDatasets
{
  public partial class ItineraryXmlDataTableDecoder : XmlDataTableDecoder
  {
    public ItineraryXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ItineraryXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      Datasets.ItineraryDataset.ItineraryDataTable Table = new Datasets.ItineraryDataset.ItineraryDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      Datasets.ItineraryDataset.ItineraryDataTable Table = (Datasets.ItineraryDataset.ItineraryDataTable)ATable;
      return Table.NewItineraryRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      Datasets.ItineraryDataset.ItineraryRow Row = (Datasets.ItineraryDataset.ItineraryRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "FirstName":
          Row.sFirstName = AContent;
          break;
        case "LastName":
          Row.sLastName = AContent;
          break;
        case "Title":
          Row.sTitle = AContent;
          break;
        case "ArrivalDat":
          Row.sArrivalDat = AContent;
          break;
        case "DepartureDat":
          Row.sDepartureDat = AContent;
          break;
        case "GracePeriod":
          Row.GracePeriod = Convert.ToInt16(AContent);
          break;
        case "BookingReference":
          Row.sBookingReference = AContent;
          break;
        case "GeofenceData":
          Row.sGeofenceData = AContent;
          break;
        case "Branding1":
          Row.sBranding1 = AContent;
          break;
        case "Branding2":
          Row.sBranding2 = AContent;
          break;
        case "Branding3":
          Row.sBranding3 = AContent;
          break;
        case "Culture":
          Row.sCulture = AContent;
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      Datasets.ItineraryDataset.ItineraryRow Row = (Datasets.ItineraryDataset.ItineraryRow)ARow;
    }
	
  }

  public partial class ItineraryXmlDataTableEncoder : XmlDataTableEncoder
  {
    public ItineraryXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ItineraryXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      Datasets.ItineraryDataset.ItineraryDataTable Table = new Datasets.ItineraryDataset.ItineraryDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      Datasets.ItineraryDataset.ItineraryDataTable Table = (Datasets.ItineraryDataset.ItineraryDataTable)ATable;
      return Table.NewItineraryRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      Datasets.ItineraryDataset.ItineraryRow Row = (Datasets.ItineraryDataset.ItineraryRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "FirstName":
          return Row.sFirstName;
        case "LastName":
          return Row.sLastName;
        case "Title":
          return Row.sTitle;
        case "ArrivalDat":
          return Row.sArrivalDat;
        case "DepartureDat":
          return Row.sDepartureDat;
        case "GracePeriod":
          return Row.sGracePeriod;
        case "BookingReference":
          return Row.sBookingReference;
        case "GeofenceData":
          return Row.sGeofenceData;
        case "Branding1":
          return Row.sBranding1;
        case "Branding2":
          return Row.sBranding2;
        case "Branding3":
          return Row.sBranding3;
        case "Culture":
          return Row.sCulture;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      Datasets.ItineraryDataset.ItineraryRow Row = (ItineraryDataset.ItineraryRow)ARow;
    }
	
  }
}