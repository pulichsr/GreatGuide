using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ChannelContentRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.ChannelContent channelContent = (WrapperObjects.ChannelContent)targetObject;

      channelContent.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      channelContent.GpsRegionId = RecordReaderHelper.ReadInt32(dataReader,"GPS_REGION_ID");
      channelContent.DestinationId = RecordReaderHelper.ReadInt32(dataReader, "DESTINATION_ID"); 
      channelContent.Priority = RecordReaderHelper.ReadInt16(dataReader, "PRIORITY"); 
		  channelContent.Heading = RecordReaderHelper.ReadInt16(dataReader, "HEADING");
      channelContent.HeadingVariance = RecordReaderHelper.ReadInt16(dataReader, "HEADING_VARIANCE"); 
      channelContent.AutoPresent = RecordReaderHelper.ReadBoolean(dataReader, "AUTO_PRESENT");
      channelContent.FillerDelay = RecordReaderHelper.ReadInt16(dataReader, "FILLER_DELAY"); 
		  channelContent.FillerContinueAfterInterrupt = RecordReaderHelper.ReadBoolean(dataReader, "FILLER_CONTINUE_AFTER_INTERRUPT");
      channelContent.PresentedCount = RecordReaderHelper.ReadInt16(dataReader, "PRESENTED_COUNT");
      channelContent.MaxPresentedCount = RecordReaderHelper.ReadInt16(dataReader, "MAX_PRESENTED_COUNT"); 
		  channelContent.ActivePeriodStartDtm = RecordReaderHelper.ReadDateTime(dataReader, "ACTIVE_PERIOD_START_DTM");
      channelContent.ActivePeriodEndDtm = RecordReaderHelper.ReadDateTime(dataReader, "ACTIVE_PERIOD_END_DTM");
      channelContent.ActivePeriodIsSeason = RecordReaderHelper.ReadBoolean(dataReader, "ACTIVE_PERIOD_IS_SEASON");
      channelContent.ActiveStartTime = RecordReaderHelper.ReadInt32(dataReader, "ACTIVE_PERIOD_START_TIME");
      channelContent.ActiveEndTime = RecordReaderHelper.ReadInt32(dataReader, "ACTIVE_PERIOD_END_TIME");
      channelContent.ActiveDays = RecordReaderHelper.ReadByte(dataReader, "ACTIVE_DAYS");
      channelContent.SuppressedByParent = RecordReaderHelper.ReadBoolean(dataReader, "SUPPRESSED_BY_PARENT"); 
		  channelContent.HasLinkedContent = RecordReaderHelper.ReadBoolean(dataReader, "HAS_LINKED_CONTENT"); 
		  channelContent.IsSequenced = RecordReaderHelper.ReadBoolean(dataReader, "IS_SEQUENCED");
      channelContent.SequencePredecessorIsComplete = RecordReaderHelper.ReadBoolean(dataReader, "SEQUENCE_PRED_IS_COMPLETE");
      channelContent.SequenceSuccessor = RecordReaderHelper.ReadInt32(dataReader, "SEQUENCE_SUCCESSOR");
      channelContent.SequencePredecessor = RecordReaderHelper.ReadInt32(dataReader, "SEQUENCE_PREDECESSOR");
      channelContent.SequenceIsComplete = RecordReaderHelper.ReadBoolean(dataReader, "SEQUENCE_IS_COMPLETE"); 
		  channelContent.TriggerType = RecordReaderHelper.ReadString(dataReader, "TRIGGER_TYPE"); 
      channelContent.SpeedBelowThreshold = RecordReaderHelper.ReadInt16(dataReader, "SPEED_BELOW_THRESHOLD");
      channelContent.WhileSpeedBelowThreshold = RecordReaderHelper.ReadBoolean(dataReader, "WHILE_SPEED_BELOW_THRESHOLD");
      channelContent.SpeedAboveThreshold = RecordReaderHelper.ReadInt16(dataReader, "SPEED_ABOVE_THRESHOLD");
      channelContent.WhileSpeedAboveThreshold = RecordReaderHelper.ReadBoolean(dataReader, "WHILE_SPEED_ABOVE_THRESHOLD");
      channelContent.SpeedThresholdCanRetrigger = RecordReaderHelper.ReadBoolean(dataReader,"SPEED_THRESHOLD_CAN_RETRIGGER");
    }
  }
}
