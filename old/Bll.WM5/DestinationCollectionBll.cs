using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class DestinationCollectionBll : ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public DestinationCollectionBll(IDestinationCollectionRepository repository)
    {
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "DestinationCollection"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      DestinationCollectionDataset.DestinationCollectionDataTable Data = (DestinationCollectionDataset.DestinationCollectionDataTable)newData.Tables["DestinationCollection"];
      if (Data != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < Data.Rows.Count; RowNo++)
        {
          Data[RowNo].Id += offset;
          
          Insert(Data[RowNo]);
        }

        Data.Dispose();
      }
    }

    public void Insert(DestinationCollectionDataset.DestinationCollectionRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }
    public DestinationCollectionDataset.DestinationCollectionRow GetByCode(string code)
    {
      return repository.GetByCode(code);
    }

    private void ValidateRow(DestinationCollectionDataset.DestinationCollectionRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("DestinationCollection.Id is null");

      if (Row.IsCodeNull() == true)
        throw new DataException("DestinationCollection.Code is null");

      if (Row.Code == string.Empty)
        throw new DataException("DestinationCollection.Code is undefined");
    }

    private readonly IDestinationCollectionRepository repository = null;
  }

}
