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
  public partial class ChannelXmlDataTableDecoder : XmlDataTableDecoder
  {
    public ChannelXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ChannelXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
	  ChannelDataset.ChannelDataTable Table = new ChannelDataset.ChannelDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
	  return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ChannelDataset.ChannelDataTable Table = (ChannelDataset.ChannelDataTable)ATable;
      return Table.NewChannelRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      ChannelDataset.ChannelRow Row = (ChannelDataset.ChannelRow)ARow;

      switch (AColumnName)
      {
				case "Id":
					Row.Id = Convert.ToInt32(AContent);
					break;
				case "ChannelGroupId":
					Row.ChannelGroupId = Convert.ToInt32(AContent);
					break;
				case "ContentPath":
					Row.sContentPath = AContent;
					break;
				case "Language":
					Row.sLanguage = AContent;
					break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      ChannelDataset.ChannelRow Row = (ChannelDataset.ChannelRow)ARow;
    }
	
  }	
  
  
  public partial class ChannelXmlDataTableEncoder : XmlDataTableEncoder
  {
    public ChannelXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ChannelXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
	  ChannelDataset.ChannelDataTable Table = new ChannelDataset.ChannelDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
	  return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ChannelDataset.ChannelDataTable Table = (ChannelDataset.ChannelDataTable)ATable;
      return Table.NewChannelRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      ChannelDataset.ChannelRow Row = (ChannelDataset.ChannelRow)ARow;

      switch (AColumnName)
      {
				case "Id":
					return Row.sId;
				case "ChannelGroupId":
					return Row.sChannelGroupId;
				case "ContentPath":
					return Row.sContentPath;
				case "Language":
					return Row.sLanguage;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      ChannelDataset.ChannelRow Row = (ChannelDataset.ChannelRow)ARow;
    }
	
  }
}    
		  
