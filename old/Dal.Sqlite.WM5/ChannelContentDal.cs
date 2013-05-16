using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Datasets;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class ChannelContentDal : ObjectDal<WrapperObjects.ChannelContent, WrapperObjects.ChannelContents>, IChannelContentRepository
  {
    public ChannelContentDal(IDbConnector connector)
      : base(connector)
    {
    }

    public void Insert(ChannelContentDataset.ChannelContentRow row)
    {
      WrapperObjects.ChannelContent channelContent = new WrapperObjects.ChannelContent();

      channelContent.Id = row.Id;
      channelContent.GpsRegionId = row.IsGpsRegionIdNull() ? new int?() : row.GpsRegionId;
      channelContent.DestinationId = row.IsDestinationIdNull() ? new int?() : row.DestinationId;
      channelContent.Priority = row.IsPriorityNull() ? new short?() : row.Priority;
      channelContent.Heading = row.IsHeadingNull() ? new short?() : row.Heading;
      channelContent.HeadingVariance = row.IsHeadingVarianceNull() ? new short?() : row.HeadingVariance;
      channelContent.AutoPresent = row.IsAutoPresentNull() ? false : row.AutoPresent;
      channelContent.TriggerType = row.sTriggerType;
      channelContent.FillerDelay = row.IsFillerDelayNull() ? new short?() : row.FillerDelay;
      channelContent.FillerContinueAfterInterrupt = row.IsFillerContinueAfterInterruptNull() ? false : row.FillerContinueAfterInterrupt;
      channelContent.PresentedCount = row.IsPresentedCountNull() ? new short?() : row.PresentedCount;
      channelContent.MaxPresentedCount = row.IsMaxPresentedCountNull() ? new short?() : row.MaxPresentedCount;
      channelContent.ActivePeriodStartDtm = row.IsActivePeriodStartDtmNull() ? new DateTime?() : row.ActivePeriodStartDtm;
      channelContent.ActivePeriodEndDtm = row.IsActivePeriodEndDtmNull() ? new DateTime?() : row.ActivePeriodEndDtm;
      channelContent.ActivePeriodIsSeason = row.IsActivePeriodIsSeasonNull() ? false : row.ActivePeriodIsSeason;
      channelContent.ActiveStartTime = row.IsActiveStartTimeNull() ? new int?() : row.ActiveStartTime;
      channelContent.ActiveEndTime = row.IsActiveEndTimeNull() ? new int?() : row.ActiveEndTime;
      channelContent.ActiveDays = row.IsActiveDaysNull() ? new short?() : row.ActiveDays;
      channelContent.SuppressedByParent = row.IsSuppressedByParentNull() ? false : row.SuppressedByParent;
      channelContent.HasLinkedContent = row.IsHasLinkedContentNull() ? false : row.HasLinkedContent;
      channelContent.IsSequenced = row.IsIsSequencedNull() ? false : row.IsSequenced;
      channelContent.SequencePredecessorIsComplete = row.IsSequencePredecessorIsCompleteNull() ? false : row.SequencePredecessorIsComplete;
      channelContent.SequenceSuccessor = row.IsSequenceSuccessorNull() ? new int?() : row.SequenceSuccessor;
      channelContent.SequencePredecessor = row.IsSequencePredecessorNull() ? new int?() : row.SequencePredecessor;
      channelContent.SequenceIsComplete = row.IsSequenceIsCompleteNull() ? false : row.SequenceIsComplete;
      channelContent.SpeedBelowThreshold = row.IsSpeedBelowThresholdNull() ? new short?() : row.SpeedBelowThreshold;
      channelContent.WhileSpeedBelowThreshold = row.IsWhileSpeedBelowThresholdNull() ? false : row.WhileSpeedBelowThreshold;
      channelContent.SpeedAboveThreshold = row.IsSpeedAboveThresholdNull() ? new short?() : row.SpeedAboveThreshold;
      channelContent.WhileSpeedAboveThreshold = row.WhileSpeedAboveThreshold ? false : row.WhileSpeedAboveThreshold;
      channelContent.SpeedThresholdCanRetrigger = row.IsSpeedThresholdCanRetriggerNull() ? false : row.SpeedThresholdCanRetrigger;

      base.Insert(channelContent);
    }
    public void Insert(ChannelContentDataset.ChannelContentDataTable table)
    {
      for (Int32 rowNo = 0; rowNo < table.Rows.Count; rowNo++)
        Insert(table[rowNo]);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public ChannelContentDataset.ChannelContentDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);
      return GetHelper.GetTable<ChannelContentDataset,ChannelContentDataset.ChannelContentDataTable>(Connector,sql);
    }
    public new ChannelContentDataset.ChannelContentDataTable Get(string triggerType)
    {
      ChannelContentDataset.ChannelContentDataTable table = new ChannelContentDataset.ChannelContentDataTable();

      ChannelContentDataset.ChannelContentRow[] items = new ChannelContentDataset.ChannelContentRow[channelContentById.Count];
      channelContentById.Values.CopyTo(items, 0);
      for (Int32 rowNo = 0; rowNo < items.Length; rowNo++)
      {
        ChannelContentDataset.ChannelContentRow row = items[rowNo];

        if (row.IsTriggerTypeNull() == true)
          continue;

        string rowTriggerType = row.TriggerType.ToUpper();
        if (triggerType == rowTriggerType)
          table.ImportRow(row);
      }

      return table;
    }
    public ChannelContentDataset.ChannelContentDataTable GetByRegion(Int32 regionId, string[] triggerTypes)
    {
      ChannelContentDataset.ChannelContentDataTable table = new ChannelContentDataset.ChannelContentDataTable();
      if (channelContentByGpsRegionId.ContainsKey(regionId) == false)
        return table;

      List<ChannelContentDataset.ChannelContentRow> items = channelContentByGpsRegionId[regionId];
      for (Int32 rowNo = 0; rowNo < items.Count; rowNo++)
      {
        ChannelContentDataset.ChannelContentRow row = items[rowNo];

        if (row.IsTriggerTypeNull() == true)
          continue;

        string rowTriggerType = row.TriggerType.ToUpper();
        for (Int32 triggerTypeNo = 0; triggerTypeNo < triggerTypes.Length; triggerTypeNo++)
        {
          string triggerType = triggerTypes[triggerTypeNo].ToUpper();
          if (triggerType == rowTriggerType)
            table.ImportRow(row);
        }
      }

      return table;
    }
    public ChannelContentDataset.ChannelContentDataTable GetByRegionChannelGroup(Int32 regionId, Int32 channelGroupId, string[] triggerTypes)
    {
      ChannelContentDataset.ChannelContentDataTable table = new ChannelContentDataset.ChannelContentDataTable();
      if (channelContentByGpsRegionId.ContainsKey(regionId) == false)
        return table;

      List<ChannelContentDataset.ChannelContentRow> items = channelContentByGpsRegionId[regionId];
      for (Int32 rowNo = 0; rowNo < items.Count; rowNo++)
      {
        ChannelContentDataset.ChannelContentRow row = items[rowNo];

        if (row.IsTriggerTypeNull() == true)
          continue;
        if (row.IsChannelGroupIdNull() == true)
          continue;

        string rowTriggerType = row.TriggerType.ToUpper();
        Int32 rowChannelGroupId = row.ChannelGroupId;
        for (Int32 triggerTypeNo = 0; triggerTypeNo < triggerTypes.Length; triggerTypeNo++)
        {
          string triggerType = triggerTypes[triggerTypeNo].ToUpper();
          if ((triggerType == rowTriggerType) && (channelGroupId == rowChannelGroupId))
            table.ImportRow(row);
        }
      }

      return table;
    }

    public void Load(Int32 masterAreaId)
    {
      ChannelContentDataset.ChannelContentDataTable content = GetByMasterArea(masterAreaId);
      ChannelContentCollection channelContentCollection = ChannelContentHelper.DataTableToList(content);
      content.Dispose();

      channelContentById.Clear();
      channelContentByGpsRegionId.Clear();

      foreach (ChannelContentDataset.ChannelContentRow row in channelContentCollection)
      {
        if (row.IsIdNull() == false)
          channelContentById[row.Id] = row;

        if (row.IsGpsRegionIdNull() == false)
        {
          if (channelContentByGpsRegionId.ContainsKey(row.GpsRegionId) == false)
            channelContentByGpsRegionId[row.GpsRegionId] = new List<ChannelContentDataset.ChannelContentRow>();

          channelContentByGpsRegionId[row.GpsRegionId].Add(row);
        }
      }

      content.Dispose();
    }

    public void SetSequencePredecessorIsComplete(Int32 id, Boolean isComplete)
    {
      #region Update cache
      if (channelContentById.ContainsKey(id) == false)
        return;

      channelContentById[id].SequencePredecessorIsComplete = isComplete;
      #endregion

      #region Update database
      if (updateDatabase == true)
      {
        string sql = string.Format("update CHANNEL_CONTENT set SEQUENCE_PRED_IS_COMPLETE = {0} where ID = {1}", Connector.FormatParameterName("SequencePredecessorIsComplete"), id);
        IDbCommand command = Connector.CreateCommand(sql);
        AddCommandParameter(command, "SequencePredecessorIsComplete", typeof(Boolean), isComplete);

        Connector.ExecuteNonQuery(command);
        command.Dispose();       
      }
      #endregion
    }
    public void SetSequenceIsComplete(Int32 id, Boolean isComplete)
    {
      #region Update cache
      if (channelContentById.ContainsKey(id) == false)
        return;

      channelContentById[id].SequenceIsComplete = isComplete;
      #endregion

      #region Update database
      if (updateDatabase == true)
      {
        string sql = string.Format("update CHANNEL_CONTENT set SEQUENCE_IS_COMPLETE = {0} where ID = {1}", Connector.FormatParameterName("SequenceIsComplete"), id);
        IDbCommand command = Connector.CreateCommand(sql);
        AddCommandParameter(command, "SequenceIsComplete", typeof(Boolean), isComplete);

        Connector.ExecuteNonQuery(command);
        command.Dispose();       
      }
      #endregion
    }
    public void SetPresentedCount(Int32 id, Int16 count)
    {
      #region Update cache
      if (channelContentById.ContainsKey(id) == true)
      {
        channelContentById[id].PresentedCount = count;
      }  
      #endregion

      #region Update database
      if (updateDatabase == true)
      {
        string sql = string.Format("update CHANNEL_CONTENT set PRESENTED_COUNT = {0} where ID = {1}", Connector.FormatParameterName("PresentedCount"), id);
        IDbCommand command = Connector.CreateCommand(sql);
        AddCommandParameter(command, "PresentedCount", typeof(Int16), count);

        Connector.ExecuteNonQuery(command);
        command.Dispose();        
      }
      #endregion
    }
    public void ResetCounts()
    {
      #region Update cache
      Dictionary<int,ChannelContentDataset.ChannelContentRow>.Enumerator enumerator = channelContentById.GetEnumerator();
      while (enumerator.MoveNext() == true)
      {
        enumerator.Current.Value.PresentedCount = 0;
        enumerator.Current.Value.SequenceIsComplete = false;
        enumerator.Current.Value.SequencePredecessorIsComplete = false;
      }
      #endregion

      #region Update database
      if (updateDatabase == true)
      {
        string sql = "update CHANNEL_CONTENT set PRESENTED_COUNT = 0,SEQUENCE_IS_COMPLETE = 0, SEQUENCE_PRED_IS_COMPLETE = 0";
        IDbCommand command = Connector.CreateCommand(sql);

        Connector.ExecuteNonQuery(command);
        command.Dispose();        
      }
      #endregion
    }

    private ChannelContentDataset.ChannelContentDataTable GetByMasterArea(Int32 masterAreaId)
    {
      StringBuilder sql = new StringBuilder();

      sql.Append("select  ");
      sql.Append("  CC.*, ");
      sql.Append("  CI.CONTENTTYPECODE as CONTENTTYPECODE, CI.FILENAME as CONTENTITEMFILENAME,CI.CHANNELGROUPID as CHANNELGROUPID ");
      sql.Append("  from VW_CHANNEL_CONTENT CC ");
      sql.Append("  join VW_CONTENT_ITEM CI on CI.CHANNELCONTENTID = CC.ID ");
      sql.Append("  join VW_GPS_REGION GR on GR.ID = CC.GPSREGIONID ");
      sql.Append(string.Format("  where GR.MASTERAREAID = {0} ", masterAreaId));

      return GetHelper.GetTable<ChannelContentDataset, ChannelContentDataset.ChannelContentDataTable>(Connector, sql.ToString());
    }

    private readonly Boolean updateDatabase = true;
    private readonly Dictionary<Int32, List<ChannelContentDataset.ChannelContentRow>> channelContentByGpsRegionId = new Dictionary<int, List<ChannelContentDataset.ChannelContentRow>>();
    private readonly Dictionary<Int32, ChannelContentDataset.ChannelContentRow> channelContentById = new Dictionary<int, ChannelContentDataset.ChannelContentRow>();
  }
}