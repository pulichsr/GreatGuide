using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class ChannelBll:
    ISyncTarget,
    ISyncSource
  {
    public const Int32 UndefinedId = -1;

    public ChannelBll(IChannelRepository repository)
    {
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "Channel"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      // Dont insert Channel records if merging
      if (merge == true)
        return;

      ChannelDataset.ChannelDataTable data = (ChannelDataset.ChannelDataTable)newData.Tables["Channel"];
      if (data != null)
      {
        DeleteAll();

        for (Int32 RowNo = 0; RowNo < data.Rows.Count; RowNo++)
        {
          Insert(data[RowNo]);
        }

        data.Dispose();
      }
    }
    public DataTable Get()
    {
      return repository.Get();
    }

    public void Insert(ChannelDataset.ChannelRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }
    public ChannelChannelGroupDataset.ChannelChannelGroupDataTable GetChannels()
    {
      return repository.GetChannels();
    }

    private void ValidateRow(ChannelDataset.ChannelRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("Channel.Id is null");

      if (Row.IsChannelGroupIdNull() == true)
        throw new DataException("Channel.ChannelGroupId is null");

      if (Row.IsContentPathNull() == true)
        Row.ContentPath = string.Empty;
    }

    private readonly IChannelRepository repository = null;
  }

}
