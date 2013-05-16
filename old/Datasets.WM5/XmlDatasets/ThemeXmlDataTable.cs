//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToXmlDataTable template, $Rev: 1h $
//========================================================================
using System;
using System.Data;
using Nucleo.Xml;
using ThemeDataset=Nucleo.GoodGuide.Datasets.Datasets.ThemeDataset;

namespace Nucleo.GoodGuide.Datasets.XmlDatasets
{
  public partial class ThemeXmlDataTableDecoder : XmlDataTableDecoder
  {
    public ThemeXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ThemeXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
	  Datasets.ThemeDataset.ThemeDataTable Table = new Datasets.ThemeDataset.ThemeDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
	  return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      Datasets.ThemeDataset.ThemeDataTable Table = (Datasets.ThemeDataset.ThemeDataTable)ATable;
      return Table.NewThemeRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      Datasets.ThemeDataset.ThemeRow Row = (Datasets.ThemeDataset.ThemeRow)ARow;

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
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      Datasets.ThemeDataset.ThemeRow Row = (Datasets.ThemeDataset.ThemeRow)ARow;
    }
	
  }	
  
  
  public partial class ThemeXmlDataTableEncoder : XmlDataTableEncoder
  {
    public ThemeXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ThemeXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
	  Datasets.ThemeDataset.ThemeDataTable Table = new Datasets.ThemeDataset.ThemeDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
	  return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      Datasets.ThemeDataset.ThemeDataTable Table = (Datasets.ThemeDataset.ThemeDataTable)ATable;
      return Table.NewThemeRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      Datasets.ThemeDataset.ThemeRow Row = (Datasets.ThemeDataset.ThemeRow)ARow;

      switch (AColumnName)
      {
				case "Id":
					return Row.sId;
				case "Name":
					return Row.sName;
				case "Description":
					return Row.sDescription;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      Datasets.ThemeDataset.ThemeRow Row = (ThemeDataset.ThemeRow)ARow;
    }
	
  }
}    
		  
