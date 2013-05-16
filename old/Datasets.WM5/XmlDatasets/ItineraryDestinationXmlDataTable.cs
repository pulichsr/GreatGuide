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
  public partial class ItineraryDestinationXmlDataTableDecoder : XmlDataTableDecoder
  {
    public ItineraryDestinationXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ItineraryDestinationXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      ItineraryDestinationDataset.ItineraryDestinationDataTable Table = new ItineraryDestinationDataset.ItineraryDestinationDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ItineraryDestinationDataset.ItineraryDestinationDataTable Table = (ItineraryDestinationDataset.ItineraryDestinationDataTable)ATable;
      return Table.NewItineraryDestinationRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      ItineraryDestinationDataset.ItineraryDestinationRow Row = (ItineraryDestinationDataset.ItineraryDestinationRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "ItineraryDayId":
          Row.ItineraryDayId = Convert.ToInt32(AContent);
          break;
        case "DestinationId":
          Row.DestinationId = Convert.ToInt32(AContent);
          break;
        case "DestinationOrder":
          Row.DestinationOrder = Convert.ToInt16(AContent);
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      ItineraryDestinationDataset.ItineraryDestinationRow Row = (ItineraryDestinationDataset.ItineraryDestinationRow)ARow;
    }
	
  }

  public partial class ItineraryDestinationXmlDataTableEncoder : XmlDataTableEncoder
  {
    public ItineraryDestinationXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ItineraryDestinationXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      ItineraryDestinationDataset.ItineraryDestinationDataTable Table = new ItineraryDestinationDataset.ItineraryDestinationDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ItineraryDestinationDataset.ItineraryDestinationDataTable Table = (ItineraryDestinationDataset.ItineraryDestinationDataTable)ATable;
      return Table.NewItineraryDestinationRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      ItineraryDestinationDataset.ItineraryDestinationRow Row = (ItineraryDestinationDataset.ItineraryDestinationRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "ItineraryDayId":
          return Row.sItineraryDayId;
        case "DestinationId":
          return Row.sDestinationId;
        case "DestinationOrder":
          return Row.sDestinationOrder;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      ItineraryDestinationDataset.ItineraryDestinationRow Row = (ItineraryDestinationDataset.ItineraryDestinationRow)ARow;
    }
	
  }
}