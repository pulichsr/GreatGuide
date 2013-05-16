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
  public partial class DestinationTypeXmlDataTableDecoder : XmlDataTableDecoder
  {
    public DestinationTypeXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationTypeXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationTypeDataset.DestinationTypeDataTable Table = new DestinationTypeDataset.DestinationTypeDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationTypeDataset.DestinationTypeDataTable Table = (DestinationTypeDataset.DestinationTypeDataTable)ATable;
      return Table.NewDestinationTypeRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      DestinationTypeDataset.DestinationTypeRow Row = (DestinationTypeDataset.DestinationTypeRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "Name":
          Row.sName = AContent;
          break;
        case "Description":
          Row.sDescription = AContent;
          break;
        case "Code":
          Row.sCode = AContent;
          break;
        case "Comment1Label":
          Row.sComment1Label = AContent;
          break;
        case "Comment2Label":
          Row.sComment2Label = AContent;
          break;
        case "Comment3Label":
          Row.sComment3Label = AContent;
          break;
        case "Comment4Label":
          Row.sComment4Label = AContent;
          break;
        case "IconResourceName":
          Row.sIconResourceName = AContent;
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      DestinationTypeDataset.DestinationTypeRow Row = (DestinationTypeDataset.DestinationTypeRow)ARow;
    }
	
  }

  public partial class DestinationTypeXmlDataTableEncoder : XmlDataTableEncoder
  {
    public DestinationTypeXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationTypeXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationTypeDataset.DestinationTypeDataTable Table = new DestinationTypeDataset.DestinationTypeDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationTypeDataset.DestinationTypeDataTable Table = (DestinationTypeDataset.DestinationTypeDataTable)ATable;
      return Table.NewDestinationTypeRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      DestinationTypeDataset.DestinationTypeRow Row = (DestinationTypeDataset.DestinationTypeRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "Name":
          return Row.sName;
        case "Description":
          return Row.sDescription;
        case "Code":
          return Row.sCode;
        case "Comment1Label":
          return Row.sComment1Label;
        case "Comment2Label":
          return Row.sComment2Label;
        case "Comment3Label":
          return Row.sComment3Label;
        case "Comment4Label":
          return Row.sComment4Label;
        case "IconResourceName":
          return Row.sIconResourceName;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      DestinationTypeDataset.DestinationTypeRow Row = (DestinationTypeDataset.DestinationTypeRow)ARow;
    }
	
  }
}