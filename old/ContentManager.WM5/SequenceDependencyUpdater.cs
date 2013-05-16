using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Datasets;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.ContentManager
{
  public class SequenceDependencyUpdater : IChannelContentUpdater
  {
    public SequenceDependencyUpdater(ChannelContentBll bll)
    {
      this.bll = bll;
    }

    public void Update(ChannelContentDataset.ChannelContentRow channelContentItem, ChannelContentCollection fillerContent)
    {
      // Check if channel content item is sequenced
      if (channelContentItem.IsSequenced == false)
        return;

      lock (bll)
      {
        // Update the item to indicate that this item has played
        bll.SetSequenceIsComplete(channelContentItem.Id, true);
        channelContentItem.SequenceIsComplete = true;

        if (channelContentItem.IsSequenceSuccessorNull() == true)
          return;

        // Update the successor to indicate that the predecessor (this item) has played
        bll.SetSequencePredecessorIsComplete(channelContentItem.SequenceSuccessor,true);
      }

      if (fillerContent == null)
        return;

      ChannelContentDataset.ChannelContentRow Successor = fillerContent.GetById(channelContentItem.SequenceSuccessor);
      if (Successor == null)
        return;

      Successor.SequencePredecessorIsComplete = true;
    }

    private readonly ChannelContentBll bll = null;
  }
}
