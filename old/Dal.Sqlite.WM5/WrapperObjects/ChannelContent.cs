using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_CHANNEL_CONTENT")]
  public class ChannelContent
  {
    public ChannelContent()
    {
    }
    public ChannelContent(DataObjects.ChannelContent channelContent)
    {
      this.dataObject = channelContent;
    }

    public DataObjects.ChannelContent DataObject
    {
      get { return dataObject; }
    }

    #region Properties

    [DbAutoColumnName]
    [DbPrimaryKey]
    public Int32? Id
    {
      get { return dataObject.Id; }
      set { dataObject.Id = value; }
    }

    [DbAutoColumnName]
    public Int32? GpsRegionId
    {
      get { return dataObject.GpsRegionId; }
      set { dataObject.GpsRegionId = value; }
    }

    [DbAutoColumnName]
    public Int32? DestinationId
    {
      get { return dataObject.DestinationId; }
      set { dataObject.DestinationId = value; }
    }

    [DbAutoColumnName]
    public Int16? Priority
    {
      get { return dataObject.Priority; }
      set { dataObject.Priority = value; }
    }

    [DbAutoColumnName]
    public Int16? Heading
    {
      get { return dataObject.Heading; }
      set { dataObject.Heading = value; }
    }

    [DbAutoColumnName]
    public Int16? HeadingVariance
    {
      get { return dataObject.HeadingVariance; }
      set { dataObject.HeadingVariance = value; }
    }

    [DbAutoColumnName]
    public bool AutoPresent
    {
      get { return dataObject.AutoPresent; }
      set { dataObject.AutoPresent = value; }
    }

    [DbAutoColumnName]
    public Int16? FillerDelay
    {
      get { return dataObject.FillerDelay; }
      set { dataObject.FillerDelay = value; }
    }

    [DbAutoColumnName]
    public bool FillerContinueAfterInterrupt
    {
      get { return dataObject.FillerContinueAfterInterrupt; }
      set { dataObject.FillerContinueAfterInterrupt = value; }
    }

    [DbAutoColumnName]
    public Int16? PresentedCount
    {
      get { return dataObject.PresentedCount; }
      set { dataObject.PresentedCount = value; }
    }

    [DbAutoColumnName]
    public Int16? MaxPresentedCount
    {
      get { return dataObject.MaxPresentedCount; }
      set { dataObject.MaxPresentedCount = value; }
    }

    [DbAutoColumnName]
    public DateTime? ActivePeriodStartDtm
    {
      get { return dataObject.ActivePeriodStartDtm; }
      set { dataObject.ActivePeriodStartDtm = value; }
    }

    [DbAutoColumnName]
    public DateTime? ActivePeriodEndDtm
    {
      get { return dataObject.ActivePeriodEndDtm; }
      set { dataObject.ActivePeriodEndDtm = value; }
    }

    [DbAutoColumnName]
    public bool ActivePeriodIsSeason
    {
      get { return dataObject.ActivePeriodIsSeason; }
      set { dataObject.ActivePeriodIsSeason = value; }
    }

    [DbAutoColumnName]
    public Int32? ActiveStartTime
    {
      get { return dataObject.ActiveStartTime; }
      set { dataObject.ActiveStartTime = value; }
    }

    [DbAutoColumnName]
    public Int32? ActiveEndTime
    {
      get { return dataObject.ActiveEndTime; }
      set { dataObject.ActiveEndTime = value; }
    }

    [DbAutoColumnName]
    public Int16? ActiveDays
    {
      get { return dataObject.ActiveDays; }
      set { dataObject.ActiveDays = value; }
    }

    [DbAutoColumnName]
    public bool SuppressedByParent
    {
      get { return dataObject.SuppressedByParent; }
      set { dataObject.SuppressedByParent = value; }
    }

    [DbAutoColumnName]
    public bool HasLinkedContent
    {
      get { return dataObject.HasLinkedContent; }
      set { dataObject.HasLinkedContent = value; }
    }

    [DbAutoColumnName]
    public bool IsSequenced
    {
      get { return dataObject.IsSequenced; }
      set { dataObject.IsSequenced = value; }
    }

    [DbColumnName("SEQUENCE_PRED_IS_COMPLETE")]
    public bool SequencePredecessorIsComplete
    {
      get { return dataObject.SequencePredecessorIsComplete; }
      set { dataObject.SequencePredecessorIsComplete = value; }
    }

    [DbAutoColumnName]
    public Int32? SequenceSuccessor
    {
      get { return dataObject.SequenceSuccessor; }
      set { dataObject.SequenceSuccessor = value; }
    }

    [DbAutoColumnName]
    public Int32? SequencePredecessor
    {
      get { return dataObject.SequencePredecessor; }
      set { dataObject.SequencePredecessor = value; }
    }

    [DbAutoColumnName]
    public bool SequenceIsComplete
    {
      get { return dataObject.SequenceIsComplete; }
      set { dataObject.SequenceIsComplete = value; }
    }

    [DbAutoColumnName]
    public string TriggerType
    {
      get { return dataObject.TriggerType; }
      set { dataObject.TriggerType = value; }
    }

    [DbAutoColumnName]
    public Int16? SpeedBelowThreshold
    {
      get { return dataObject.SpeedBelowThreshold; }
      set { dataObject.SpeedBelowThreshold = value; }
    }

    [DbAutoColumnName]
    public bool WhileSpeedBelowThreshold
    {
      get { return dataObject.WhileSpeedBelowThreshold; }
      set { dataObject.WhileSpeedBelowThreshold = value; }
    }

    [DbAutoColumnName]
    public Int16? SpeedAboveThreshold
    {
      get { return dataObject.SpeedAboveThreshold; }
      set { dataObject.SpeedAboveThreshold = value; }
    }

    [DbAutoColumnName]
    public bool WhileSpeedAboveThreshold
    {
      get { return dataObject.WhileSpeedAboveThreshold; }
      set { dataObject.WhileSpeedAboveThreshold = value; }
    }

    [DbAutoColumnName]
    public bool SpeedThresholdCanRetrigger
    {
      get { return dataObject.SpeedThresholdCanRetrigger; }
      set { dataObject.SpeedThresholdCanRetrigger = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.ChannelContent dataObject = new DataObjects.ChannelContent();
    
    #endregion
  }

  public class ChannelContents : List<ChannelContent>
  {
    public DataObjects.ChannelContents DataObjects
    {
      get
      {
        DataObjects.ChannelContents objects = new DataObjects.ChannelContents();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}