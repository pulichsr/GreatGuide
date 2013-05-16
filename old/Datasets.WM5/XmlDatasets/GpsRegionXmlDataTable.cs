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
  public partial class GpsRegionXmlDataTableDecoder : XmlDataTableDecoder
  {
    public GpsRegionXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public GpsRegionXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      GpsRegionDataset.GpsRegionDataTable Table = new GpsRegionDataset.GpsRegionDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      GpsRegionDataset.GpsRegionDataTable Table = (GpsRegionDataset.GpsRegionDataTable)ATable;
      return Table.NewGpsRegionRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      GpsRegionDataset.GpsRegionRow Row = (GpsRegionDataset.GpsRegionRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "RegionType":
          Row.sRegionType = AContent;
          break;
        case "RegionData":
          Row.sRegionData = AContent;
          break;
        case "MinLatitude":
          Row.sMinLatitude = AContent;
          break;
        case "MaxLatitude":
          Row.sMaxLatitude = AContent;
          break;
        case "MinLongitude":
          Row.sMinLongitude = AContent;
          break;
        case "MaxLongitude":
          Row.sMaxLongitude = AContent;
          break;
        case "ResetOnEntry":
          if (AContent == "1")
            Row.ResetOnEntry = true;
          else
            Row.ResetOnEntry = false;
          break;
        case "MasterAreaId":
          Row.MasterAreaId = Convert.ToInt32(AContent);
          break;
        case "ThemeId":
          Row.ThemeId = Convert.ToInt32(AContent);
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
      GpsRegionDataset.GpsRegionRow Row = (GpsRegionDataset.GpsRegionRow)ARow;
    }
	
  }

  public partial class GpsRegionXmlDataTableEncoder : XmlDataTableEncoder
  {
    public GpsRegionXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public GpsRegionXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      GpsRegionDataset.GpsRegionDataTable Table = new GpsRegionDataset.GpsRegionDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      GpsRegionDataset.GpsRegionDataTable Table = (GpsRegionDataset.GpsRegionDataTable)ATable;
      return Table.NewGpsRegionRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      GpsRegionDataset.GpsRegionRow Row = (GpsRegionDataset.GpsRegionRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "RegionType":
          return Row.sRegionType;
        case "RegionData":
          return Row.sRegionData;
        case "MinLatitude":
          return Row.sMinLatitude;
        case "MaxLatitude":
          return Row.sMaxLatitude;
        case "MinLongitude":
          return Row.sMinLongitude;
        case "MaxLongitude":
          return Row.sMaxLongitude;
        case "ResetOnEntry":
          if (Row.ResetOnEntry == true)
            return "1";
          else
            return "0";
        case "MasterAreaId":
          return Row.sMasterAreaId;
        case "ThemeId":
          return Row.sThemeId;
        case "Name":
          return Row.sName;
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      GpsRegionDataset.GpsRegionRow Row = (GpsRegionDataset.GpsRegionRow)ARow;
    }
	
  }
}