using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class DestinationTypeBll : ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public DestinationTypeBll(IDestinationTypeRepository repository)
    {
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "DestinationType"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      DestinationTypeDataset.DestinationTypeDataTable Data = (DestinationTypeDataset.DestinationTypeDataTable)newData.Tables["DestinationType"];
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

    public void Insert(DestinationTypeDataset.DestinationTypeRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }

    public DestinationTypeDataset.DestinationTypeRow GetById(Int32 id)
    {
      return repository.GetById(id);
    }
    public DestinationTypeDataset.DestinationTypeRow GetByCode(string code)
    {
      return repository.GetByCode(code);
    }
    public DestinationTypeDataset.DestinationTypeDataTable Get()
    {
      return repository.Get();
    }

    private void ValidateRow(DestinationTypeDataset.DestinationTypeRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("DestinationType.Id is null");

      if (Row.IsCodeNull() == true)
        throw new DataException("DestinationType.Code is null");
    }

    private readonly IDestinationTypeRepository repository = null;
  }

}
