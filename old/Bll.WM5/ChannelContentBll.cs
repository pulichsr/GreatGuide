using System;
using System.Data;
using Nucleo.GoodGuide.Datasets;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class ChannelContentBll:
    ISyncTarget,
    ISyncSource
  {
    public ChannelContentBll(IChannelContentRepository channelContentRepository)
    {
      this.channelContentRepository = channelContentRepository;
    }

    public string DatasetName
    {
      get { return "ChannelContent"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      ChannelContentDataset.ChannelContentDataTable data = (ChannelContentDataset.ChannelContentDataTable)newData.Tables["ChannelContent"];
      if (data != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < data.Rows.Count; RowNo++)
        {
          data[RowNo].Id += offset;

          if (data[RowNo].IsChannelGroupIdNull() == false) data[RowNo].ChannelGroupId += offset;
          if (data[RowNo].IsDestinationIdNull() == false) data[RowNo].DestinationId += offset;
          if (data[RowNo].IsGpsRegionIdNull() == false) data[RowNo].GpsRegionId += offset;
          if (data[RowNo].IsSequencePredecessorNull() == false) data[RowNo].SequencePredecessor += offset;
          if (data[RowNo].IsSequenceSuccessorNull() == false) data[RowNo].SequenceSuccessor += offset;

          Insert(data[RowNo]);
        }

        data.Dispose();
      }
    }
    public DataTable Get()
    {
      return channelContentRepository.Get();
    }


    public ChannelContentCollection GetByRegion(Int32 regionId, string[] triggerTypes)
    {
      ChannelContentDataset.ChannelContentDataTable ChannelContent = channelContentRepository.GetByRegion(regionId, triggerTypes);
      AddRuleColumns(ChannelContent);

      ChannelContentCollection ChannelContentCollection = ChannelContentHelper.DataTableToList(ChannelContent);
      ChannelContent.Dispose();

      return ChannelContentCollection;
    }
    public ChannelContentCollection GetByRegionChannelGroup(Int32 regionId, Int32 channelId, string[] triggerTypes)
    {
      ChannelContentDataset.ChannelContentDataTable ChannelContent = channelContentRepository.GetByRegionChannelGroup(regionId, channelId, triggerTypes);
      AddRuleColumns(ChannelContent);

      ChannelContentCollection ChannelContentCollection = ChannelContentHelper.DataTableToList(ChannelContent);
      ChannelContent.Dispose();

      return ChannelContentCollection;
    }
    public void DeleteAll()
    {
      channelContentRepository.DeleteAll();
    }
   
    public void Insert(ChannelContentDataset.ChannelContentRow Row)
    {
      ValidateRow(Row);

      channelContentRepository.Insert(Row);
    }

    public void SetSequencePredecessorIsComplete(Int32 id, Boolean isComplete)
    {
      channelContentRepository.SetSequencePredecessorIsComplete(id, isComplete);
    }
    public void SetSequenceIsComplete(Int32 id, Boolean isComplete)
    {
      channelContentRepository.SetSequenceIsComplete(id, isComplete);
    }
    public void SetPresentedCount(Int32 id, Int16 count)
    {
      channelContentRepository.SetPresentedCount(id, count);
    }
    public void ResetCounts()
    {
      channelContentRepository.ResetCounts();
    }

    public void AddRuleColumns(ChannelContentDataset.ChannelContentDataTable channelContent)
    {
      if (channelContent == null)
        return;

      channelContent.Columns.Add("LastSpeed", typeof(float));
    }

    private void ValidateRow(ChannelContentDataset.ChannelContentRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("ChannelContent.Id is null");

      if (Row.IsGpsRegionIdNull() == true)
        throw new DataException("ChannelContent.GpsRegionId is null");

      if (Row.IsPriorityNull() == true)
        throw new DataException("ChannelContent.Priority is null");

      if (Row.IsAutoPresentNull() == true)
        Row.AutoPresent = true;

      if (Row.IsTriggerTypeNull() == true)
        throw new DataException("ChannelContent.TriggerType is null");

      if (Row.IsIsSequencedNull() == true)
        Row.IsSequenced = false;

      if ((Row.IsMaxPresentedCountNull() == false) && (Row.IsPresentedCountNull() == true))
        Row.PresentedCount = 0;

      if (Row.IsSequenceIsCompleteNull() == true)
        Row.SequenceIsComplete = false;

      if (Row.IsSequencePredecessorIsCompleteNull() == true)
        Row.SequencePredecessorIsComplete = false;

      // If this is the first item in the sequence, ensure that the predecessor is seen as complete
      if ((Row.IsSequenced == true) && (Row.IsSequencePredecessorNull() == true))
        Row.SequencePredecessorIsComplete = true;

      if (Row.IsWhileSpeedBelowThresholdNull() == true)
        Row.WhileSpeedBelowThreshold = false;

      if (Row.IsWhileSpeedAboveThresholdNull() == true)
        Row.WhileSpeedAboveThreshold = false;

      if (Row.IsSpeedThresholdCanRetriggerNull() == true)
        Row.SpeedThresholdCanRetrigger = false;

      if (Row.IsIsFillerNull() == true)
        Row.IsFiller = false;
    }

    private readonly IChannelContentRepository channelContentRepository = null;
  }

}
