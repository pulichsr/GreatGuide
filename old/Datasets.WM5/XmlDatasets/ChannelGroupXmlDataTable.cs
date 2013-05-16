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
  public partial class ChannelGroupXmlDataTableDecoder : XmlDataTableDecoder
  {
    public ChannelGroupXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ChannelGroupXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      ChannelGroupDataset.ChannelGroupDataTable Table = new ChannelGroupDataset.ChannelGroupDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
	  return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ChannelGroupDataset.ChannelGroupDataTable Table = (ChannelGroupDataset.ChannelGroupDataTable)ATable;
      return Table.NewChannelGroupRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      ChannelGroupDataset.ChannelGroupRow Row = (ChannelGroupDataset.ChannelGroupRow)ARow;

      switch (AColumnName)
      {
				case "Id":
					Row.Id = Convert.ToInt32(AContent);
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
      ChannelGroupDataset.ChannelGroupRow Row = (ChannelGroupDataset.ChannelGroupRow)ARow;
    }
	
  }	
  
  
  public partial class ChannelGroupXmlDataTableEncoder : XmlDataTableEncoder
  {
    public ChannelGroupXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ChannelGroupXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
	  ChannelGroupDataset.ChannelGroupDataTable Table = new ChannelGroupDataset.ChannelGroupDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
	  return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ChannelGroupDataset.ChannelGroupDataTable Table = (ChannelGroupDataset.ChannelGroupDataTable)ATable;
      return Table.NewChannelGroupRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      ChannelGroupDataset.ChannelGroupRow Row = (ChannelGroupDataset.ChannelGroupRow)ARow;

      switch (AColumnName)
      {
				case "Id":
					return Row.sId;
				case "Name":
					return Row.sName;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      ChannelGroupDataset.ChannelGroupRow Row = (ChannelGroupDataset.ChannelGroupRow)ARow;
    }
	
  }
}    
		  
