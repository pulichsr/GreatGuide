using System;
using System.Collections.Generic;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.ContentManager
{
  public class ChannelGroupManager
  {
    public const Int16 DefaultLastPlayedContentItemCount = 10;
    public ChannelGroupManager(int id)
    {
      Id = id;
    }

    public ChannelContentDataset.ChannelContentRow currentlyPlayingContent = null;
    public DateTime fillerContentBaseTime = DateTime.Now;

    public Boolean IsCurrentlyPlaying()
    {
      return currentlyPlayingContent != null;
    }
    public void Play(ChannelContentDataset.ChannelContentRow contentToPlay, DateTime currentTime)
    {
      currentlyPlayingContent = contentToPlay;
      fillerContentBaseTime = currentTime;

      lastPlayedContent = contentToPlay;
    }
    public void Stop(DateTime currentTime)
    {
      currentlyPlayingContent = null;
      fillerContentBaseTime = currentTime;
    }
    public void ResetFillerBaseTime(DateTime currentTime)
    {
      fillerContentBaseTime = currentTime;
    }

    public Int32 Id = -1;
    public ChannelContentDataset.ChannelContentRow lastPlayedContent = null;
  }

  public class ChannelGroupManagers : List<ChannelGroupManager>
  {
    public Boolean IsAnyPlaying
    {
      get
      {
        for (Int32 ChannelGroupNo = 0; ChannelGroupNo < Count; ChannelGroupNo++)
          if (this[ChannelGroupNo].IsCurrentlyPlaying() == true)
            return true;

        return false;
      }
    }
    public ChannelGroupManager GetById(Int32 channelGroupId)
    {
      for (Int32 ChannelGroupNo = 0; ChannelGroupNo < Count;ChannelGroupNo++ )
        if (this[ChannelGroupNo].Id == channelGroupId)
          return this[ChannelGroupNo];

      return null;
    }
  }
}
