using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using GetDataResponse=Nucleo.GoodGuide.Datasets.XmlDatasets.GetDataResponse;

namespace Nucleo.GoodGuide.Bll
{
  public class ChannelGroupBll:
    ISyncTarget,
    ISyncSource
  {
    public const Int32 UndefinedId = -1;

    public ChannelGroupBll(IChannelGroupRepository dal)
    {
      this.repository = dal;
    }

    public string DatasetName
    {
      get { return "ChannelGroup"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      // Dont insert ChannelGroup records if merging
      if (merge == true)
        return;

      ChannelGroupDataset.ChannelGroupDataTable ChannelGroupData = (ChannelGroupDataset.ChannelGroupDataTable)newData.Tables["ChannelGroup"];
      if (ChannelGroupData != null)
      {
        DeleteAll();

        for (Int32 RowNo = 0; RowNo < ChannelGroupData.Rows.Count; RowNo++)
        {
          Insert(ChannelGroupData[RowNo]);
        }

        ChannelGroupData.Dispose();
      }
    }
    public DataTable Get()
    {
      return repository.Get();
    }

    public DataTable GetAll()
    {
      return repository.Get();
    }
    public void Insert(ChannelGroupDataset.ChannelGroupRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }

    private void ValidateRow(ChannelGroupDataset.ChannelGroupRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("ChannelGroup.Id is null");

      if (Row.IsNameNull() == true)
        Row.Name = string.Empty;
    }

    private readonly IChannelGroupRepository repository = null;
  }

}
