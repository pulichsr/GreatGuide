using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class ControlDefinitionBll: ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public ControlDefinitionBll(IControlDefinitionRepository repository)
    {
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "ControlDefinition"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      ControlDefinitionDataset.ControlDefinitionDataTable data = (ControlDefinitionDataset.ControlDefinitionDataTable)newData.Tables["ControlDefinition"];
      if (data != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < data.Rows.Count; RowNo++)
        {
          data[RowNo].Id += offset;

          if (data[RowNo].IsFormIdNull() == false) data[RowNo].FormId += offset;
          if (data[RowNo].IsMasterAreaIdNull() == false) data[RowNo].MasterAreaId += offset;

          Insert(data[RowNo]);
        }

        data.Dispose();
      }
    }

    public void Insert(ControlDefinitionDataset.ControlDefinitionRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }

    private static void ValidateRow(ControlDefinitionDataset.ControlDefinitionRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("ControlDefinition.Id is null");

      if (Row.IsActionNull() == true)
        throw new DataException("ControlDefinition.Action is null");

      if (Row.IsFormIdNull() == true)
        throw new DataException("ControlDefinition.FormId is null");

      if (Row.IsNameNull() == true)
        throw new DataException("ControlDefinition.Name is null");

      if (Row.IsMasterAreaIdNull() == true)
        throw new DataException("ControlDefinition.MasterAreaId is null");

    }

    private readonly IControlDefinitionRepository repository = null;
  }

}
