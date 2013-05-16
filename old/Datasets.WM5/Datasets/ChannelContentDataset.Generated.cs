//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ChannelContentDataset
  {
    public partial class ChannelContentRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableChannelContent.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sGpsRegionId
      {
        get
        {
          if (IsNull(tableChannelContent.GpsRegionIdColumn) == true)
            return string.Empty;
          else
            return GpsRegionId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.GpsRegionIdColumn] = DBNull.Value;
          else
            GpsRegionId = Convert.ToInt32(value.Trim());
        }
      }
      public string sDestinationId
      {
        get
        {
          if (IsNull(tableChannelContent.DestinationIdColumn) == true)
            return string.Empty;
          else
            return DestinationId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.DestinationIdColumn] = DBNull.Value;
          else
            DestinationId = Convert.ToInt32(value.Trim());
        }
      }
      public string sPriority
      {
        get
        {
          if (IsNull(tableChannelContent.PriorityColumn) == true)
            return string.Empty;
          else
            return Priority.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.PriorityColumn] = DBNull.Value;
          else
            Priority = Convert.ToByte(value.Trim());
        }
      }
      public string sHeading
      {
        get
        {
          if (IsNull(tableChannelContent.HeadingColumn) == true)
            return string.Empty;
          else
            return Heading.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.HeadingColumn] = DBNull.Value;
          else
            Heading = Convert.ToInt16(value.Trim());
        }
      }
      public string sHeadingVariance
      {
        get
        {
          if (IsNull(tableChannelContent.HeadingVarianceColumn) == true)
            return string.Empty;
          else
            return HeadingVariance.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.HeadingVarianceColumn] = DBNull.Value;
          else
            HeadingVariance = Convert.ToInt16(value.Trim());
        }
      }
      public string sAutoPresent
      {
        get
        {
          if (IsNull(tableChannelContent.AutoPresentColumn) == true)
            return string.Empty;
          else
            return AutoPresent.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.AutoPresentColumn] = DBNull.Value;
          else
            AutoPresent = Convert.ToBoolean(value.Trim());
        }
      }
      public string sTriggerType
      {
        get
        {
          if (IsNull(tableChannelContent.TriggerTypeColumn) == true)
            return string.Empty;
          else
            return TriggerType;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.TriggerTypeColumn] = DBNull.Value;
          else
            TriggerType = value.Trim();
        }
      }
      public string sFillerDelay
      {
        get
        {
          if (IsNull(tableChannelContent.FillerDelayColumn) == true)
            return string.Empty;
          else
            return FillerDelay.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.FillerDelayColumn] = DBNull.Value;
          else
            FillerDelay = Convert.ToInt16(value.Trim());
        }
      }
      public string sFillerContinueAfterInterrupt
      {
        get
        {
          if (IsNull(tableChannelContent.FillerContinueAfterInterruptColumn) == true)
            return string.Empty;
          else
            return FillerContinueAfterInterrupt.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.FillerContinueAfterInterruptColumn] = DBNull.Value;
          else
            FillerContinueAfterInterrupt = Convert.ToBoolean(value.Trim());
        }
      }
      public string sPresentedCount
      {
        get
        {
          if (IsNull(tableChannelContent.PresentedCountColumn) == true)
            return string.Empty;
          else
            return PresentedCount.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.PresentedCountColumn] = DBNull.Value;
          else
            PresentedCount = Convert.ToInt16(value.Trim());
        }
      }
      public string sMaxPresentedCount
      {
        get
        {
          if (IsNull(tableChannelContent.MaxPresentedCountColumn) == true)
            return string.Empty;
          else
            return MaxPresentedCount.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.MaxPresentedCountColumn] = DBNull.Value;
          else
            MaxPresentedCount = Convert.ToInt16(value.Trim());
        }
      }
      public string sActivePeriodStartDtm
      {
        get
        {
          if (IsNull(tableChannelContent.ActivePeriodStartDtmColumn) == true)
            return string.Empty;
          else
            return ActivePeriodStartDtm.ToString("yyyy-MM-dd hh:mm:ss");
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.ActivePeriodStartDtmColumn] = DBNull.Value;
          else
            ActivePeriodStartDtm = Convert.ToDateTime(value.Trim());
        }
      }
      public string sActivePeriodEndDtm
      {
        get
        {
          if (IsNull(tableChannelContent.ActivePeriodEndDtmColumn) == true)
            return string.Empty;
          else
            return ActivePeriodEndDtm.ToString("yyyy-MM-dd hh:mm:ss");
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.ActivePeriodEndDtmColumn] = DBNull.Value;
          else
            ActivePeriodEndDtm = Convert.ToDateTime(value.Trim());
        }
      }
      public string sActivePeriodIsSeason
      {
        get
        {
          if (IsNull(tableChannelContent.ActivePeriodIsSeasonColumn) == true)
            return string.Empty;
          else
            return ActivePeriodIsSeason.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.ActivePeriodIsSeasonColumn] = DBNull.Value;
          else
            ActivePeriodIsSeason = Convert.ToBoolean(value.Trim());
        }
      }
      public string sActiveStartTime
      {
        get
        {
          if (IsNull(tableChannelContent.ActiveStartTimeColumn) == true)
            return string.Empty;
          else
            return ActiveStartTime.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.ActiveStartTimeColumn] = DBNull.Value;
          else
            ActiveStartTime = Convert.ToInt32(value.Trim());
        }
      }
      public string sActiveEndTime
      {
        get
        {
          if (IsNull(tableChannelContent.ActiveEndTimeColumn) == true)
            return string.Empty;
          else
            return ActiveEndTime.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.ActiveEndTimeColumn] = DBNull.Value;
          else
            ActiveEndTime = Convert.ToInt32(value.Trim());
        }
      }
      public string sActiveDays
      {
        get
        {
          if (IsNull(tableChannelContent.ActiveDaysColumn) == true)
            return string.Empty;
          else
            return ActiveDays.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.ActiveDaysColumn] = DBNull.Value;
          else
            ActiveDays = Convert.ToByte(value.Trim());
        }
      }
      public string sSuppressedByParent
      {
        get
        {
          if (IsNull(tableChannelContent.SuppressedByParentColumn) == true)
            return string.Empty;
          else
            return SuppressedByParent.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.SuppressedByParentColumn] = DBNull.Value;
          else
            SuppressedByParent = Convert.ToBoolean(value.Trim());
        }
      }
      public string sHasLinkedContent
      {
        get
        {
          if (IsNull(tableChannelContent.HasLinkedContentColumn) == true)
            return string.Empty;
          else
            return HasLinkedContent.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.HasLinkedContentColumn] = DBNull.Value;
          else
            HasLinkedContent = Convert.ToBoolean(value.Trim());
        }
      }
      public string sIsSequenced
      {
        get
        {
          if (IsNull(tableChannelContent.IsSequencedColumn) == true)
            return string.Empty;
          else
            return IsSequenced.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.IsSequencedColumn] = DBNull.Value;
          else
            IsSequenced = Convert.ToBoolean(value.Trim());
        }
      }
      public string sSequencePredecessorIsComplete
      {
        get
        {
          if (IsNull(tableChannelContent.SequencePredecessorIsCompleteColumn) == true)
            return string.Empty;
          else
            return SequencePredecessorIsComplete.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.SequencePredecessorIsCompleteColumn] = DBNull.Value;
          else
            SequencePredecessorIsComplete = Convert.ToBoolean(value.Trim());
        }
      }
      public string sSequenceSuccessor
      {
        get
        {
          if (IsNull(tableChannelContent.SequenceSuccessorColumn) == true)
            return string.Empty;
          else
            return SequenceSuccessor.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.SequenceSuccessorColumn] = DBNull.Value;
          else
            SequenceSuccessor = Convert.ToInt32(value.Trim());
        }
      }
      public string sIsFiller
      {
        get
        {
          if (IsNull(tableChannelContent.IsFillerColumn) == true)
            return string.Empty;
          else
            return IsFiller.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.IsFillerColumn] = DBNull.Value;
          else
            IsFiller = Convert.ToBoolean(value.Trim());
        }
      }
      public string sContentItemFilename
      {
        get
        {
          if (IsNull(tableChannelContent.ContentItemFilenameColumn) == true)
            return string.Empty;
          else
            return ContentItemFilename;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.ContentItemFilenameColumn] = DBNull.Value;
          else
            ContentItemFilename = value.Trim();
        }
      }
      public string sContentTypeCode
      {
        get
        {
          if (IsNull(tableChannelContent.ContentTypeCodeColumn) == true)
            return string.Empty;
          else
            return ContentTypeCode;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.ContentTypeCodeColumn] = DBNull.Value;
          else
            ContentTypeCode = value.Trim();
        }
      }
      public string sSequencePredecessor
      {
        get
        {
          if (IsNull(tableChannelContent.SequencePredecessorColumn) == true)
            return string.Empty;
          else
            return SequencePredecessor.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.SequencePredecessorColumn] = DBNull.Value;
          else
            SequencePredecessor = Convert.ToInt32(value.Trim());
        }
      }
      public string sSequenceIsComplete
      {
        get
        {
          if (IsNull(tableChannelContent.SequenceIsCompleteColumn) == true)
            return string.Empty;
          else
            return SequenceIsComplete.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.SequenceIsCompleteColumn] = DBNull.Value;
          else
            SequenceIsComplete = Convert.ToBoolean(value.Trim());
        }
      }
      public string sChannelGroupId
      {
        get
        {
          if (IsNull(tableChannelContent.ChannelGroupIdColumn) == true)
            return string.Empty;
          else
            return ChannelGroupId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.ChannelGroupIdColumn] = DBNull.Value;
          else
            ChannelGroupId = Convert.ToInt32(value.Trim());
        }
      }
      public string sSpeedBelowThreshold
      {
        get
        {
          if (IsNull(tableChannelContent.SpeedBelowThresholdColumn) == true)
            return string.Empty;
          else
            return SpeedBelowThreshold.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.SpeedBelowThresholdColumn] = DBNull.Value;
          else
            SpeedBelowThreshold = Convert.ToInt16(value.Trim());
        }
      }
      public string sWhileSpeedBelowThreshold
      {
        get
        {
          if (IsNull(tableChannelContent.WhileSpeedBelowThresholdColumn) == true)
            return string.Empty;
          else
            return WhileSpeedBelowThreshold.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.WhileSpeedBelowThresholdColumn] = DBNull.Value;
          else
            WhileSpeedBelowThreshold = Convert.ToBoolean(value.Trim());
        }
      }
      public string sSpeedAboveThreshold
      {
        get
        {
          if (IsNull(tableChannelContent.SpeedAboveThresholdColumn) == true)
            return string.Empty;
          else
            return SpeedAboveThreshold.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.SpeedAboveThresholdColumn] = DBNull.Value;
          else
            SpeedAboveThreshold = Convert.ToInt16(value.Trim());
        }
      }
      public string sWhileSpeedAboveThreshold
      {
        get
        {
          if (IsNull(tableChannelContent.WhileSpeedAboveThresholdColumn) == true)
            return string.Empty;
          else
            return WhileSpeedAboveThreshold.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.WhileSpeedAboveThresholdColumn] = DBNull.Value;
          else
            WhileSpeedAboveThreshold = Convert.ToBoolean(value.Trim());
        }
      }
      public string sSpeedThresholdCanRetrigger
      {
        get
        {
          if (IsNull(tableChannelContent.SpeedThresholdCanRetriggerColumn) == true)
            return string.Empty;
          else
            return SpeedThresholdCanRetrigger.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannelContent.SpeedThresholdCanRetriggerColumn] = DBNull.Value;
          else
            SpeedThresholdCanRetrigger = Convert.ToBoolean(value.Trim());
        }
      }
      #endregion
 
    }
  }
}