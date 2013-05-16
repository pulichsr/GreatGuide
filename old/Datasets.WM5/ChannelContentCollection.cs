using System;
using System.Collections.Generic;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Datasets
{
  public class ChannelContentCollection : List<ChannelContentDataset.ChannelContentRow>
  {
    public ChannelContentDataset.ChannelContentRow GetById(Int32 id)
    {
      for (Int32 RowNo = 0; RowNo < this.Count; RowNo++)
        if (this[RowNo].Id == id)
          return this[RowNo];

      return null;
    }
    public Boolean HasItem(ChannelContentDataset.ChannelContentRow row)
    {
      return GetById(row.Id) != null;
    }

    public void AddNoDuplicates(ChannelContentDataset.ChannelContentRow row)
    {
      Boolean HasNewItem = HasItem(row);
      if (HasNewItem == false)
        Add(row);
    }

    public ChannelContentCollection Copy()
    {
      ChannelContentCollection Clone = new ChannelContentCollection();

      for (Int32 RowNo = 0; RowNo < this.Count; RowNo++)
        Clone.Add(this[RowNo]);

      return Clone;
    }

    public void RemoveAll()
    {
      while (Count > 0)
        RemoveAt(0);
    }
  }
}