using System;
using Nucleo.GoodGuide.Datasets;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Datasets
{
  public static class ChannelContentHelper
  {
    public static ChannelContentCollection DataTableToList(ChannelContentDataset.ChannelContentDataTable table)
    {
      ChannelContentCollection List = new ChannelContentCollection();
      for (Int32 RowNo = 0; RowNo < table.Rows.Count; RowNo++)
        List.Add(table[RowNo]);

      table.Dispose();

      return List;
    }
  }
}