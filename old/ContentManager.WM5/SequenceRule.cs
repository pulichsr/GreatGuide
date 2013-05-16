using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class SequenceRule : IContentManagerRule
  {
    public Boolean IsValid(
      ChannelGroupManager channelGroupContent,
      ChannelContentDataset.ChannelContentRow channelContentItem,
      GpsPositionEvent lastGpsPosition,
      GpsPositionEvent lastMediaStartedGpsPosition,
      DateTime currentDtm,
      out string message)
    {
      message = string.Empty;

      // Check if this content item is part of a sequence
      if (channelContentItem.IsSequenced == false)
        return true;

      // Check if this item has played in the sequence
      if (channelContentItem.SequenceIsComplete == true)
      {
        message = "SequenceIsComplete == true";
        return false;
      }

      // Check if this is the first item in the sequence
      if (channelContentItem.IsSequencePredecessorNull() == true)
        return true;

      // Check if predecessor has played. If the field is null or the predecessor has not played then this item cannot play
      if (channelContentItem.SequencePredecessorIsComplete == false)
      {
        message = "SequencePredecessorIsComplete == false";
        return false;
      }

      return true;
    }
  }
}
