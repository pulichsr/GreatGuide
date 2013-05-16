using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IChannelContentRepository
  {
    void Insert(ChannelContentDataset.ChannelContentRow row);
    void Insert(ChannelContentDataset.ChannelContentDataTable table);
    void DeleteAll();

    void Load(Int32 masterAreaId);

    ChannelContentDataset.ChannelContentDataTable Get();
    ChannelContentDataset.ChannelContentDataTable Get(string triggerType);
    ChannelContentDataset.ChannelContentDataTable GetByRegion(Int32 regionId, string[] triggerTypes);
    ChannelContentDataset.ChannelContentDataTable GetByRegionChannelGroup(Int32 regionId, Int32 channelGroupId, string[] triggerTypes);

    void SetSequencePredecessorIsComplete(Int32 id, Boolean isComplete);
    void SetSequenceIsComplete(Int32 id, Boolean isComplete);
    void SetPresentedCount(Int32 id, Int16 count);
    void ResetCounts();
  }
}