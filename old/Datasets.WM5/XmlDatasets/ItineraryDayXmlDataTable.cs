//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToXmlDataTable template, $Rev: 1h $
//========================================================================
using System;
using System.Data;
using Nucleo.Xml;
using ItineraryDayDataset=Nucleo.GoodGuide.Datasets.Datasets.ItineraryDayDataset;

namespace Nucleo.GoodGuide.Datasets.XmlDatasets
{
  public partial class ItineraryDayXmlDataTableDecoder : XmlDataTableDecoder
  {
    public ItineraryDayXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ItineraryDayXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      Datasets.ItineraryDayDataset.ItineraryDayDataTable Table = new Datasets.ItineraryDayDataset.ItineraryDayDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      Datasets.ItineraryDayDataset.ItineraryDayDataTable Table = (Datasets.ItineraryDayDataset.ItineraryDayDataTable)ATable;
      return Table.NewItineraryDayRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      Datasets.ItineraryDayDataset.ItineraryDayRow Row = (Datasets.ItineraryDayDataset.ItineraryDayRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "ItineraryDat":
          Row.sItineraryDat = AContent;
          break;
        case "Comment":
          Row.sComment = AContent;
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      Datasets.ItineraryDayDataset.ItineraryDayRow Row = (Datasets.ItineraryDayDataset.ItineraryDayRow)ARow;
    }
	
  }

  public partial class ItineraryDayXmlDataTableEncoder : XmlDataTableEncoder
  {
    public ItineraryDayXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ItineraryDayXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      Datasets.ItineraryDayDataset.ItineraryDayDataTable Table = new Datasets.ItineraryDayDataset.ItineraryDayDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      Datasets.ItineraryDayDataset.ItineraryDayDataTable Table = (Datasets.ItineraryDayDataset.ItineraryDayDataTable)ATable;
      return Table.NewItineraryDayRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      Datasets.ItineraryDayDataset.ItineraryDayRow Row = (Datasets.ItineraryDayDataset.ItineraryDayRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "ItineraryDat":
          return Row.sItineraryDat;
        case "Comment":
          return Row.sComment;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      Datasets.ItineraryDayDataset.ItineraryDayRow Row = (ItineraryDayDataset.ItineraryDayRow)ARow;
    }
	
  }
}