using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class ThemeBll:
    ISyncTarget,
    ISyncSource
  {
    public ThemeBll(IThemeRepository repository)
    {
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "Theme"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      ThemeDataset.ThemeDataTable ThemeData = (ThemeDataset.ThemeDataTable)newData.Tables["Theme"];
      if (ThemeData != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < ThemeData.Rows.Count; RowNo++)
        {
          ThemeData[RowNo].Id += offset;

          Insert(ThemeData[RowNo]);
        }

        ThemeData.Dispose();
      }
    }
    public DataTable Get()
    {
      return repository.Get();
    }

    public void Insert(ThemeDataset.ThemeRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }

    private void ValidateRow(ThemeDataset.ThemeRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("Theme.Id is null");

      if (Row.IsNameNull() == true)
        throw new DataException("Theme.Name is null");
    }

    private readonly IThemeRepository repository = null;
  }

}
