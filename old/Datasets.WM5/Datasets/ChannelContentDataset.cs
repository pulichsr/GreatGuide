using System;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ChannelContentDataset
  {
    public partial class ChannelContentDataTable
    {
      public Boolean HasItem(ChannelContentRow row)
      {
        for (Int32 RowNo = 0; RowNo < Rows.Count; RowNo++)
          if (this[RowNo].Id == row.Id)
            return true;

        return false;
      }

      public void AddNoDuplicates(ChannelContentRow row)
      {
        Boolean HasNewItem = HasItem(row);
        if (HasNewItem == false)
          AddChannelContentRow(row);
      }

      public ChannelContentDataTable CloneWithData()
      {
        ChannelContentDataTable Clone = (ChannelContentDataTable)this.Clone();

        for (Int32 RowNo = 0; RowNo < Rows.Count; RowNo++)
          Clone.Rows.Add(Rows[RowNo].ItemArray);

        return Clone;
      }
    }

    public partial class ChannelContentRow
    {
    }
  }
}