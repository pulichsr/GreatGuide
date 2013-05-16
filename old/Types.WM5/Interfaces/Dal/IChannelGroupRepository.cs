using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IChannelGroupRepository
  {
    void Insert(ChannelGroupDataset.ChannelGroupRow row);
    void Insert(ChannelGroupDataset.ChannelGroupDataTable table);
    void DeleteAll();
    ChannelGroupDataset.ChannelGroupDataTable Get();
  }
}