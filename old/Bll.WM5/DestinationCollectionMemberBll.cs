using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class DestinationCollectionMemberBll : ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public DestinationCollectionMemberBll(IDestinationCollectionMemberRepository repository)
    {
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "DestinationCollectionMember"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable Data = (DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable)newData.Tables["DestinationCollectionMember"];
      if (Data != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < Data.Rows.Count; RowNo++)
        {
          if(Data[RowNo].IsCollectionIdNull() == false) Data[RowNo].CollectionId += offset;
          if (Data[RowNo].IsDestinationIdNull() == false) Data[RowNo].DestinationId += offset;

          Insert(Data[RowNo]);
        }

        Data.Dispose();
      }
    }

    public void Insert(DestinationCollectionMemberDataset.DestinationCollectionMemberRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }

    private void ValidateRow(DestinationCollectionMemberDataset.DestinationCollectionMemberRow Row)
    {
      if (Row.IsCollectionIdNull() == true)
        throw new DataException("DestinationCollectionMember.CollectionId is null");

      if (Row.IsDestinationIdNull() == true)
        throw new DataException("DestinationCollectionMember.DestinationId is null");
    }

    private readonly IDestinationCollectionMemberRepository repository = null;
  }

}
