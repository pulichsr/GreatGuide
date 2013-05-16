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
  public partial class DestinationClassificationXmlDataTableDecoder : XmlDataTableDecoder
  {
    public DestinationClassificationXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationClassificationXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationClassificationDataset.DestinationClassificationDataTable Table = new DestinationClassificationDataset.DestinationClassificationDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationClassificationDataset.DestinationClassificationDataTable Table = (DestinationClassificationDataset.DestinationClassificationDataTable)ATable;
      return Table.NewDestinationClassificationRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      DestinationClassificationDataset.DestinationClassificationRow Row = (DestinationClassificationDataset.DestinationClassificationRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "DestinationTypeId":
          Row.DestinationTypeId = Convert.ToInt32(AContent);
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
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      DestinationClassificationDataset.DestinationClassificationRow Row = (DestinationClassificationDataset.DestinationClassificationRow)ARow;
    }
	
  }

  public partial class DestinationClassificationXmlDataTableEncoder : XmlDataTableEncoder
  {
    public DestinationClassificationXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationClassificationXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationClassificationDataset.DestinationClassificationDataTable Table = new DestinationClassificationDataset.DestinationClassificationDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationClassificationDataset.DestinationClassificationDataTable Table = (DestinationClassificationDataset.DestinationClassificationDataTable)ATable;
      return Table.NewDestinationClassificationRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      DestinationClassificationDataset.DestinationClassificationRow Row = (DestinationClassificationDataset.DestinationClassificationRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "DestinationTypeId":
          return Row.sDestinationTypeId;
        case "Name":
          return Row.sName;
        case "Description":
          return Row.sDescription;
        case "Code":
          return Row.sCode;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      DestinationClassificationDataset.DestinationClassificationRow Row = (DestinationClassificationDataset.DestinationClassificationRow)ARow;
    }
	
  }
}