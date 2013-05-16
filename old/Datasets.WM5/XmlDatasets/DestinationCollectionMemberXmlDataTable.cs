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
  public partial class DestinationCollectionMemberXmlDataTableDecoder : XmlDataTableDecoder
  {
    public DestinationCollectionMemberXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationCollectionMemberXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable Table = new DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable Table = (DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable)ATable;
      return Table.NewDestinationCollectionMemberRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      DestinationCollectionMemberDataset.DestinationCollectionMemberRow Row = (DestinationCollectionMemberDataset.DestinationCollectionMemberRow)ARow;

      switch (AColumnName)
      {
        case "CollectionId":
          Row.CollectionId = Convert.ToInt32(AContent);
          break;
        case "DestinationId":
          Row.DestinationId = Convert.ToInt32(AContent);
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      DestinationCollectionMemberDataset.DestinationCollectionMemberRow Row = (DestinationCollectionMemberDataset.DestinationCollectionMemberRow)ARow;
    }
	
  }

  public partial class DestinationCollectionMemberXmlDataTableEncoder : XmlDataTableEncoder
  {
    public DestinationCollectionMemberXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public DestinationCollectionMemberXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable Table = new DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable Table = (DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable)ATable;
      return Table.NewDestinationCollectionMemberRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      DestinationCollectionMemberDataset.DestinationCollectionMemberRow Row = (DestinationCollectionMemberDataset.DestinationCollectionMemberRow)ARow;

      switch (AColumnName)
      {
        case "CollectionId":
          return Row.sCollectionId;
        case "DestinationId":
          return Row.sDestinationId;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      DestinationCollectionMemberDataset.DestinationCollectionMemberRow Row = (DestinationCollectionMemberDataset.DestinationCollectionMemberRow)ARow;
    }
	
  }
}