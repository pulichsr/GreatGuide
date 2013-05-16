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
  public partial class ControlDefinitionXmlDataTableDecoder : XmlDataTableDecoder
  {
    public ControlDefinitionXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ControlDefinitionXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      ControlDefinitionDataset.ControlDefinitionDataTable Table = new ControlDefinitionDataset.ControlDefinitionDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ControlDefinitionDataset.ControlDefinitionDataTable Table = (ControlDefinitionDataset.ControlDefinitionDataTable)ATable;
      return Table.NewControlDefinitionRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      ControlDefinitionDataset.ControlDefinitionRow Row = (ControlDefinitionDataset.ControlDefinitionRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "FormId":
          Row.FormId = Convert.ToInt32(AContent);
          break;
        case "Name":
          Row.sName = AContent;
          break;
        case "Text":
          Row.sText = AContent;
          break;
        case "Description":
          Row.sDescription = AContent;
          break;
        case "GraphicResourceName":
          Row.sGraphicResourceName = AContent;
          break;
        case "Colour":
          Row.sColour = AContent;
          break;
        case "Action":
          Row.sAction = AContent;
          break;
        case "ActionTarget":
          Row.sActionTarget = AContent;
          break;
        case "ActionData":
          Row.sActionData = AContent;
          break;
        case "MasterAreaId":
          Row.MasterAreaId = Convert.ToInt32(AContent);
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      ControlDefinitionDataset.ControlDefinitionRow Row = (ControlDefinitionDataset.ControlDefinitionRow)ARow;
    }
	
  }

  public partial class ControlDefinitionXmlDataTableEncoder : XmlDataTableEncoder
  {
    public ControlDefinitionXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ControlDefinitionXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      ControlDefinitionDataset.ControlDefinitionDataTable Table = new ControlDefinitionDataset.ControlDefinitionDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ControlDefinitionDataset.ControlDefinitionDataTable Table = (ControlDefinitionDataset.ControlDefinitionDataTable)ATable;
      return Table.NewControlDefinitionRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      ControlDefinitionDataset.ControlDefinitionRow Row = (ControlDefinitionDataset.ControlDefinitionRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "FormId":
          return Row.sFormId;
        case "Name":
          return Row.sName;
        case "Text":
          return Row.sText;
        case "Description":
          return Row.sDescription;
        case "GraphicResourceName":
          return Row.sGraphicResourceName;
        case "Colour":
          return Row.sColour;
        case "Action":
          return Row.sAction;
        case "ActionTarget":
          return Row.sActionTarget;
        case "ActionData":
          return Row.sActionData;
        case "MasterAreaId":
          return Row.sMasterAreaId;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      ControlDefinitionDataset.ControlDefinitionRow Row = (ControlDefinitionDataset.ControlDefinitionRow)ARow;
    }
	
  }
}