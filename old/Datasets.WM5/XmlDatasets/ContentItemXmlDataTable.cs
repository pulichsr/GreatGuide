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
  public partial class ContentItemXmlDataTableDecoder : XmlDataTableDecoder
  {
    public ContentItemXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ContentItemXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
	  ContentItemDataset.ContentItemDataTable Table = new ContentItemDataset.ContentItemDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
	  return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ContentItemDataset.ContentItemDataTable Table = (ContentItemDataset.ContentItemDataTable)ATable;
      return Table.NewContentItemRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      ContentItemDataset.ContentItemRow Row = (ContentItemDataset.ContentItemRow)ARow;

      switch (AColumnName)
      {
				case "Id":
					Row.Id = Convert.ToInt32(AContent);
					break;
				case "ContentTypeCode":
					Row.sContentTypeCode = AContent;
					break;
				case "Filename":
					Row.sFilename = AContent;
					break;
				case "ChannelGroupId":
					Row.ChannelGroupId = Convert.ToInt32(AContent);
					break;
				case "ChannelContentId":
					Row.ChannelContentId = Convert.ToInt32(AContent);
					break;
				case "Description":
					Row.Description = AContent;
					break;
				case "IsFillerContent":
          if (AContent == "1")
            Row.IsFillerContent = true;
          else
            Row.IsFillerContent = false;
          break;
        case "DisplaySeq":
          Row.DisplaySeq = Convert.ToInt32(AContent);
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      ContentItemDataset.ContentItemRow Row = (ContentItemDataset.ContentItemRow)ARow;
    }
	
  }	
  
  
  public partial class ContentItemXmlDataTableEncoder : XmlDataTableEncoder
  {
    public ContentItemXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ContentItemXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
	  ContentItemDataset.ContentItemDataTable Table = new ContentItemDataset.ContentItemDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
	  return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ContentItemDataset.ContentItemDataTable Table = (ContentItemDataset.ContentItemDataTable)ATable;
      return Table.NewContentItemRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      ContentItemDataset.ContentItemRow Row = (ContentItemDataset.ContentItemRow)ARow;

      switch (AColumnName)
      {
				case "Id":
					return Row.sId;
				case "ContentTypeCode":
					return Row.sContentTypeCode;
				case "Filename":
					return Row.sFilename;
				case "ChannelGroupId":
					return Row.sChannelGroupId;
				case "ChannelContentId":
					return Row.sChannelContentId;
				case "Description":
					return Row.sDescription;
        case "IsFillerContent":
          if (Row.IsFillerContent == true)
            return "1";
          else
            return "0";
        case "DisplaySeq":
          return Row.sDisplaySeq;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      ContentItemDataset.ContentItemRow Row = (ContentItemDataset.ContentItemRow)ARow;
    }
	
  }
}    
		  
