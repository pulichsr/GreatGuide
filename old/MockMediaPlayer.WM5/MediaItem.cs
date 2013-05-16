using System;
using System.Collections.Generic;

namespace Nucleo.GoodGuide.MockMediaPlayer
{
  [Serializable]
  public class MediaItem
  {
    public string Name
    {
      get { return name; }
      set { name = value; }
    }
    public short Duration
    {
      get { return duration; }
      set { duration = value; }
    }

    private string name;
    private Int16 duration;
  }

  [Serializable]
  public class MediaItems: List<MediaItem>
  {
    public MediaItem Find(string name)
    {
      foreach (MediaItem item in this)
        if (item.Name == name)
          return item;

      return null;
    }
  }
}
