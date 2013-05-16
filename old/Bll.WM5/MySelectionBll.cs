using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class MySelectionBll
  {
    public const Int32 UndefinedId = -1;

    public MySelectionBll(IMySelectionRepository repository)
    {
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "MySelection"; }
    }
    public void SyncData(GetDataResponse newData)
    {
      DeleteAll();
    }
    public void Insert(MySelectionDataset.MySelectionRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }
    public Boolean IsInMySelection(Int32 destinationId)
    {
      return repository.IsInMySelection(destinationId);
    }
    public void Add(Int32 destinationId)
    {
      MySelectionDataset ds = new MySelectionDataset();
      MySelectionDataset.MySelectionRow Row = ds.MySelection.NewMySelectionRow();
      Row.DestinationId = destinationId;

      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void Remove(Int32 destinationId)
    {
      repository.Delete(destinationId);
    }


    private static void ValidateRow(MySelectionDataset.MySelectionRow Row)
    {
      if (Row.IsDestinationIdNull() == true)
        throw new DataException("MySelection.DestinationId is null");
    }

    private readonly IMySelectionRepository repository = null;
  }

}
