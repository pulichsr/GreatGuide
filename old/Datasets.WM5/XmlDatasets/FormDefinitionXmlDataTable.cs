//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToXmlDataTable template, $Rev: 1h $
//========================================================================
using System;
using System.Data;
using Nucleo.Xml;
using FormDefinitionDataset=Nucleo.GoodGuide.Datasets.Datasets.FormDefinitionDataset;

namespace Nucleo.GoodGuide.Datasets.XmlDatasets
{
  public partial class FormDefinitionXmlDataTableDecoder : XmlDataTableDecoder
  {
    public FormDefinitionXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public FormDefinitionXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      Datasets.FormDefinitionDataset.FormDefinitionDataTable Table = new Datasets.FormDefinitionDataset.FormDefinitionDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      Datasets.FormDefinitionDataset.FormDefinitionDataTable Table = (Datasets.FormDefinitionDataset.FormDefinitionDataTable)ATable;
      return Table.NewFormDefinitionRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      Datasets.FormDefinitionDataset.FormDefinitionRow Row = (Datasets.FormDefinitionDataset.FormDefinitionRow)ARow;

      switch (AColumnName)
      {
        case "Text":
          Row.sText = AContent;
          break;
        case "Name":
          Row.sName = AContent;
          break;
        case "FormTypeName":
          Row.sFormTypeName = AContent;
          break;
        case "GraphicResourceName":
          Row.sGraphicResourceName = AContent;
          break;
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "MasterAreaId":
          Row.MasterAreaId = Convert.ToInt32(AContent);
          break;
        case "AdName":
          Row.sAdName = AContent;
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      Datasets.FormDefinitionDataset.FormDefinitionRow Row = (Datasets.FormDefinitionDataset.FormDefinitionRow)ARow;
    }
	
  }

  public partial class FormDefinitionXmlDataTableEncoder : XmlDataTableEncoder
  {
    public FormDefinitionXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public FormDefinitionXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      Datasets.FormDefinitionDataset.FormDefinitionDataTable Table = new Datasets.FormDefinitionDataset.FormDefinitionDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      Datasets.FormDefinitionDataset.FormDefinitionDataTable Table = (Datasets.FormDefinitionDataset.FormDefinitionDataTable)ATable;
      return Table.NewFormDefinitionRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      Datasets.FormDefinitionDataset.FormDefinitionRow Row = (Datasets.FormDefinitionDataset.FormDefinitionRow)ARow;

      switch (AColumnName)
      {
        case "Text":
          return Row.sText;
        case "Name":
          return Row.sName;
        case "FormTypeName":
          return Row.sFormTypeName;
        case "GraphicResourceName":
          return Row.sGraphicResourceName;
        case "Id":
          return Row.sId;
        case "MasterAreaId":
          return Row.sMasterAreaId;
        case "AdName":
          return Row.sAdName;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      Datasets.FormDefinitionDataset.FormDefinitionRow Row = (FormDefinitionDataset.FormDefinitionRow)ARow;
    }
	
  }
}