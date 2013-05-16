using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class ChannelContent
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (Id == null)
        result.AddValidationError("Id is undefined");

      if (GpsRegionId == null)
        result.AddValidationError("GpsRegionId is undefined");

      if (Priority == null)
        result.AddValidationError("Priority is undefined");

      if (string.IsNullOrEmpty(TriggerType) == true)
        result.AddValidationError("TriggerType is undefined");

      if ((MaxPresentedCount != null) && (PresentedCount == null))
        PresentedCount = 0;

      // If this is the first item in the sequence, ensure that the predecessor is seen as complete
      if ((IsSequenced == true) && (SequencePredecessor == null))
        SequencePredecessorIsComplete = true;

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2
    public Int32? Id;
    public Int32? GpsRegionId;
    public Int32? DestinationId;
    public Int16? Priority;
		public Int16? Heading;
		public Int16? HeadingVariance;
    public bool AutoPresent;
		public Int16? FillerDelay;
		public bool FillerContinueAfterInterrupt;
    public Int16? PresentedCount;
		public Int16? MaxPresentedCount;
		public DateTime? ActivePeriodStartDtm;
		public DateTime? ActivePeriodEndDtm;
    public bool ActivePeriodIsSeason;
		public Int32? ActiveStartTime;
		public Int32? ActiveEndTime;
		public Int16? ActiveDays;
    public bool SuppressedByParent;
		public bool HasLinkedContent;
		public bool IsSequenced;
		public bool SequencePredecessorIsComplete;
    public Int32? SequenceSuccessor;
		public Int32? SequencePredecessor;
		public bool SequenceIsComplete;
		public string TriggerType;
    public Int16? SpeedBelowThreshold;
    public bool WhileSpeedBelowThreshold;
    public Int16? SpeedAboveThreshold;
    public bool WhileSpeedAboveThreshold;
    public bool SpeedThresholdCanRetrigger;

    public float? LastSpeed;
    public string ContentItemFilename;
    public Int32? ChannelGroupId;
    #endregion
  }

  public class ChannelContents: List<ChannelContent>
  {
    public ChannelContent GetById(Int32 id)
    {
      for (Int32 rowNo = 0; rowNo < this.Count; rowNo++)
        if (this[rowNo].Id == id)
          return this[rowNo];

      return null;
    }
    public new Boolean Contains(ChannelContent channelContent)
    {
      if (channelContent.Id == null)
        throw new ArgumentException("ChannelContent.Id is undefined");

      return GetById(channelContent.Id.Value) != null;
    }

    public void AddNoDuplicates(ChannelContent channelContent)
    {
      Boolean hasItem = Contains(channelContent);
      if (hasItem == false)
        Add(channelContent);
    }
    public void RemoveAll()
    {
      while (Count > 0)
        RemoveAt(0);
    }
  }
}
		
		
    
 