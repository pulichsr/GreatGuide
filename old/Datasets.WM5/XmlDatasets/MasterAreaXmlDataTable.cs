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
  public partial class MasterAreaXmlDataTableDecoder : XmlDataTableDecoder
  {
    public MasterAreaXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public MasterAreaXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      MasterAreaDataset.MasterAreaDataTable Table = new MasterAreaDataset.MasterAreaDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      MasterAreaDataset.MasterAreaDataTable Table = (MasterAreaDataset.MasterAreaDataTable)ATable;
      return Table.NewMasterAreaRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      MasterAreaDataset.MasterAreaRow Row = (MasterAreaDataset.MasterAreaRow)ARow;

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
        case "MinLongitude":
          Row.sMinLongitude = AContent;
          break;
        case "MaxLongitude":
          Row.sMaxLongitude = AContent;
          break;
        case "MinLatitude":
          Row.sMinLatitude = AContent;
          break;
        case "MaxLatitude":
          Row.sMaxLatitude = AContent;
          break;
        case "ContentBasePath":
          Row.sContentBasePath = AContent;
          break;
        case "RegionData":
          Row.sRegionData = AContent;
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      MasterAreaDataset.MasterAreaRow Row = (MasterAreaDataset.MasterAreaRow)ARow;
    }
	
  }

  public partial class MasterAreaXmlDataTableEncoder : XmlDataTableEncoder
  {
    public MasterAreaXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public MasterAreaXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      MasterAreaDataset.MasterAreaDataTable Table = new MasterAreaDataset.MasterAreaDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      MasterAreaDataset.MasterAreaDataTable Table = (MasterAreaDataset.MasterAreaDataTable)ATable;
      return Table.NewMasterAreaRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      MasterAreaDataset.MasterAreaRow Row = (MasterAreaDataset.MasterAreaRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "Name":
          return Row.sName;
        case "Description":
          return Row.sDescription;
        case "MinLongitude":
          return Row.sMinLongitude;
        case "MaxLongitude":
          return Row.sMaxLongitude;
        case "MinLatitude":
          return Row.sMinLatitude;
        case "MaxLatitude":
          return Row.sMaxLatitude;
        case "ContentBasePath":
          return Row.sContentBasePath;
        case "RegionData":
          return Row.sRegionData;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      MasterAreaDataset.MasterAreaRow Row = (MasterAreaDataset.MasterAreaRow)ARow;
    }
	
  }
}