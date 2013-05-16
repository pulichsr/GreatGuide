using Nucleo.GoodGuide.Datasets;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.ContentManager
{
  public interface IChannelContentUpdater
  {
    void Update(ChannelContentDataset.ChannelContentRow channelContentItem, ChannelContentCollection fillerContent);
  }
}
