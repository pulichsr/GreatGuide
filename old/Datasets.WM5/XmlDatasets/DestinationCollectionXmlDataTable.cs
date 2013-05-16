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
  public partial class DestinationCollectionXmlDataTableDecoder : XmlDataTableDecoder
  {
    public DestinationCollectionXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationCollectionXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationCollectionDataset.DestinationCollectionDataTable Table = new DestinationCollectionDataset.DestinationCollectionDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationCollectionDataset.DestinationCollectionDataTable Table = (DestinationCollectionDataset.DestinationCollectionDataTable)ATable;
      return Table.NewDestinationCollectionRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      DestinationCollectionDataset.DestinationCollectionRow Row = (DestinationCollectionDataset.DestinationCollectionRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "Code":
          Row.sCode = AContent;
          break;
        case "Name":
          Row.sName = AContent;
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      DestinationCollectionDataset.DestinationCollectionRow Row = (DestinationCollectionDataset.DestinationCollectionRow)ARow;
    }
	
  }

  public partial class DestinationCollectionXmlDataTableEncoder : XmlDataTableEncoder
  {
    public DestinationCollectionXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationCollectionXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationCollectionDataset.DestinationCollectionDataTable Table = new DestinationCollectionDataset.DestinationCollectionDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationCollectionDataset.DestinationCollectionDataTable Table = (DestinationCollectionDataset.DestinationCollectionDataTable)ATable;
      return Table.NewDestinationCollectionRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      DestinationCollectionDataset.DestinationCollectionRow Row = (DestinationCollectionDataset.DestinationCollectionRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "Code":
          return Row.sCode;
        case "Name":
          return Row.sName;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      DestinationCollectionDataset.DestinationCollectionRow Row = (DestinationCollectionDataset.DestinationCollectionRow)ARow;
    }
	
  }
}