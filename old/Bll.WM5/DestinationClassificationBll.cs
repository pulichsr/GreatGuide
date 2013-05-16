using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class DestinationClassificationBll : ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public DestinationClassificationBll(IDestinationClassificationRepository repository)
    {
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "DestinationClassification"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      DestinationClassificationDataset.DestinationClassificationDataTable Data = (DestinationClassificationDataset.DestinationClassificationDataTable)newData.Tables["DestinationClassification"];
      if (Data != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < Data.Rows.Count; RowNo++)
        {
          Data[RowNo].Id += offset;

          if (Data[RowNo].IsDestinationTypeIdNull() == false) Data[RowNo].DestinationTypeId += offset;

          Insert(Data[RowNo]);
        }

        Data.Dispose();
      }
    }

    public DestinationClassificationDataset.DestinationClassificationRow GetByCode(string code)
    {
      return repository.GetByCode(code);
    }
    public void Insert(DestinationClassificationDataset.DestinationClassificationRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }

    private void ValidateRow(DestinationClassificationDataset.DestinationClassificationRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("DestinationClassification.Id is null");

      if (Row.IsDestinationTypeIdNull() == true)
        throw new DataException("DestinationClassification.DestinationTypeId is null");

      if (Row.IsCodeNull() == true)
        throw new DataException("DestinationClassification.Code is null");
    }

    private readonly IDestinationClassificationRepository repository = null;
  }

}
