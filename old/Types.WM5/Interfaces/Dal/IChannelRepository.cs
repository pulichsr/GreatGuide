using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IChannelRepository
  {
    void Insert(ChannelDataset.ChannelRow row);
    void Insert(ChannelDataset.ChannelDataTable table);

    void DeleteAll();
    ChannelDataset.ChannelDataTable Get();

    ChannelChannelGroupDataset.ChannelChannelGroupDataTable GetChannels();
  }
}