using System;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Datasets;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.ContentManager
{
  public class PresentedCountUpdater : IChannelContentUpdater
  {
    public PresentedCountUpdater(ChannelContentBll bll)
    {
      this.bll = bll;
    }

    public void Update(ChannelContentDataset.ChannelContentRow channelContentItem, ChannelContentCollection fillerContent)
    {
      if (channelContentItem.IsPresentedCountNull() == true) 
        return;

      lock (bll)
      {
        // Update the PresentedCOunt to indicate that the predecessor (this item) has played
        bll.SetPresentedCount(channelContentItem.Id,(Int16)(channelContentItem.PresentedCount + 1));
      }
      channelContentItem.PresentedCount++;
    }

    private readonly ChannelContentBll bll = null;
  }
}
