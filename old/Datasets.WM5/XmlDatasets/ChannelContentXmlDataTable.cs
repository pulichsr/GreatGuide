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

  public partial class ChannelContentXmlDataTableDecoder : XmlDataTableDecoder
  {
    public ChannelContentXmlDataTableDecoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ChannelContentXmlDataTableDecoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      ChannelContentDataset.ChannelContentDataTable Table = new ChannelContentDataset.ChannelContentDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ChannelContentDataset.ChannelContentDataTable Table = (ChannelContentDataset.ChannelContentDataTable)ATable;
      return Table.NewChannelContentRow();
    }
  
    protected override void SetColumnValue(DataRow ARow, string AColumnName, string AContent)
    {
      ChannelContentDataset.ChannelContentRow Row = (ChannelContentDataset.ChannelContentRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          Row.Id = Convert.ToInt32(AContent);
          break;
        case "GpsRegionId":
          Row.GpsRegionId = Convert.ToInt32(AContent);
          break;
        case "DestinationId":
          Row.DestinationId = Convert.ToInt32(AContent);
          break;
        case "Priority":
          Row.sPriority = AContent;
          break;
        case "Heading":
          Row.Heading = Convert.ToInt16(AContent);
          break;
        case "HeadingVariance":
          Row.HeadingVariance = Convert.ToInt16(AContent);
          break;
        case "AutoPresent":
          if (AContent == "1")
            Row.AutoPresent = true;
          else
            Row.AutoPresent = false;
          break;
        case "TriggerType":
          Row.sTriggerType = AContent;
          break;
        case "FillerDelay":
          Row.FillerDelay = Convert.ToInt16(AContent);
          break;
        case "FillerContinueAfterInterrupt":
          if (AContent == "1")
            Row.FillerContinueAfterInterrupt = true;
          else
            Row.FillerContinueAfterInterrupt = false;
          break;
        case "PresentedCount":
          Row.PresentedCount = Convert.ToInt16(AContent);
          break;
        case "MaxPresentedCount":
          Row.MaxPresentedCount = Convert.ToInt16(AContent);
          break;
        case "ActivePeriodStartDtm":
          Row.sActivePeriodStartDtm = AContent;
          break;
        case "ActivePeriodEndDtm":
          Row.sActivePeriodEndDtm = AContent;
          break;
        case "ActivePeriodIsSeason":
          if (AContent == "1")
            Row.ActivePeriodIsSeason = true;
          else
            Row.ActivePeriodIsSeason = false;
          break;
        case "ActiveStartTime":
          Row.ActiveStartTime = Convert.ToInt32(AContent);
          break;
        case "ActiveEndTime":
          Row.ActiveEndTime = Convert.ToInt32(AContent);
          break;
        case "ActiveDays":
          Row.sActiveDays = AContent;
          break;
        case "SuppressedByParent":
          if (AContent == "1")
            Row.SuppressedByParent = true;
          else
            Row.SuppressedByParent = false;
          break;
        case "HasLinkedContent":
          if (AContent == "1")
            Row.HasLinkedContent = true;
          else
            Row.HasLinkedContent = false;
          break;
        case "IsSequenced":
          if (AContent == "1")
            Row.IsSequenced = true;
          else
            Row.IsSequenced = false;
          break;
        case "SequencePredecessorIsComplete":
          if (AContent == "1")
            Row.SequencePredecessorIsComplete = true;
          else
            Row.SequencePredecessorIsComplete = false;
          break;
        case "SequenceSuccessor":
          Row.SequenceSuccessor = Convert.ToInt32(AContent);
          break;
        case "ContentItemFilename":
          Row.sContentItemFilename = AContent;
          break;
        case "ContentTypeCode":
          Row.sContentTypeCode = AContent;
          break;
        case "SequencePredecessor":
          Row.SequencePredecessor = Convert.ToInt32(AContent);
          break;
        case "SequenceIsComplete":
          if (AContent == "1")
            Row.SequenceIsComplete = true;
          else
            Row.SequenceIsComplete = false;
          break;
        case "ChannelGroupId":
          Row.ChannelGroupId = Convert.ToInt32(AContent);
          break;
        case "SpeedBelowThreshold":
          Row.SpeedBelowThreshold = Convert.ToInt16(AContent);
          break;
        case "WhileSpeedBelowThreshold":
          if (AContent == "1")
            Row.WhileSpeedBelowThreshold = true;
          else
            Row.WhileSpeedBelowThreshold = false;
          break;
        case "SpeedAboveThreshold":
          Row.SpeedAboveThreshold = Convert.ToInt16(AContent);
          break;
        case "WhileSpeedAboveThreshold":
          if (AContent == "1")
            Row.WhileSpeedAboveThreshold = true;
          else
            Row.WhileSpeedAboveThreshold = false;
          break;
        case "SpeedThresholdCanRetrigger":
          if (AContent == "1")
            Row.SpeedThresholdCanRetrigger = true;
          else
            Row.SpeedThresholdCanRetrigger = false;
          break;
 
        default:
          break;
      }
    }
	
    protected override void DecodedRow(DataRow ARow)
    {
      ChannelContentDataset.ChannelContentRow Row = (ChannelContentDataset.ChannelContentRow)ARow;
    }
	
  }

  public partial class ChannelContentXmlDataTableEncoder : XmlDataTableEncoder
  {
    public ChannelContentXmlDataTableEncoder(string ATagName, string ARecordTagName) :
      base(ATagName,ARecordTagName)
    {
    }
    public ChannelContentXmlDataTableEncoder(string ATableName,string ATagName, string ARecordTagName) :
      base(ATableName,ATagName,ARecordTagName)
    {
    }

    protected override DataTable GetTable()
    {
      ChannelContentDataset.ChannelContentDataTable Table = new ChannelContentDataset.ChannelContentDataTable();
      if (TableName != string.Empty)
        Table.TableName = TableName;
	  
      return Table;
    }
    protected override DataRow GetRow(DataTable ATable)
    {
      ChannelContentDataset.ChannelContentDataTable Table = (ChannelContentDataset.ChannelContentDataTable)ATable;
      return Table.NewChannelContentRow();
    }
  
    protected override string GetColumnValue(DataRow ARow, string AColumnName)
    {
      ChannelContentDataset.ChannelContentRow Row = (ChannelContentDataset.ChannelContentRow)ARow;

      switch (AColumnName)
      {
        case "Id":
          return Row.sId;
        case "GpsRegionId":
          return Row.sGpsRegionId;
        case "DestinationId":
          return Row.sDestinationId;
        case "Priority":
          return Row.sPriority;
        case "Heading":
          return Row.sHeading;
        case "HeadingVariance":
          return Row.sHeadingVariance;
        case "AutoPresent":
          if (Row.AutoPresent == true)
            return "1";
          else
            return "0";
        case "TriggerType":
          return Row.sTriggerType;
        case "FillerDelay":
          return Row.sFillerDelay;
        case "FillerContinueAfterInterrupt":
          if (Row.FillerContinueAfterInterrupt == true)
            return "1";
          else
            return "0";
        case "PresentedCount":
          return Row.sPresentedCount;
        case "MaxPresentedCount":
          return Row.sMaxPresentedCount;
        case "ActivePeriodStartDtm":
          return Row.sActivePeriodStartDtm;
        case "ActivePeriodEndDtm":
          return Row.sActivePeriodEndDtm;
        case "ActivePeriodIsSeason":
          if (Row.ActivePeriodIsSeason == true)
            return "1";
          else
            return "0";
        case "ActiveStartTime":
          return Row.sActiveStartTime;
        case "ActiveEndTime":
          return Row.sActiveEndTime;
        case "ActiveDays":
          return Row.sActiveDays;
        case "SuppressedByParent":
          if (Row.SuppressedByParent == true)
            return "1";
          else
            return "0";
        case "HasLinkedContent":
          if (Row.HasLinkedContent == true)
            return "1";
          else
            return "0";
        case "IsSequenced":
          if (Row.IsSequenced == true)
            return "1";
          else
            return "0";
        case "SequencePredecessorIsComplete":
          if (Row.SequencePredecessorIsComplete == true)
            return "1";
          else
            return "0";
        case "SequenceSuccessor":
          return Row.sSequenceSuccessor;
        case "ContentItemFilename":
          return Row.sContentItemFilename;
        case "ContentTypeCode":
          return Row.sContentTypeCode;
        case "SequencePredecessor":
          return Row.sSequencePredecessor;
        case "SequenceIsComplete":
          if (Row.SequenceIsComplete == true)
            return "1";
          else
            return "0";
        case "ChannelGroupId":
          return Row.sChannelGroupId;
        case "SpeedBelowThreshold":
          return Row.sSpeedBelowThreshold;
        case "WhileSpeedBelowThreshold":
          if (Row.WhileSpeedBelowThreshold == true)
            return "1";
          else
            return "0";
        case "SpeedAboveThreshold":
          return Row.sSpeedAboveThreshold;
        case "WhileSpeedAboveThreshold":
          if (Row.WhileSpeedAboveThreshold == true)
            return "1";
          else
            return "0";
        case "SpeedThresholdCanRetrigger":
          if (Row.SpeedThresholdCanRetrigger == true)
            return "1";
          else
            return "0";
 
        default:
          return string.Empty;
      }
    }	
	
    protected override void EncodedRow(DataRow ARow)
    {
      ChannelContentDataset.ChannelContentRow Row = (ChannelContentDataset.ChannelContentRow)ARow;
    }
	
  }
}