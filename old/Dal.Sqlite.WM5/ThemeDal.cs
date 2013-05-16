using System;
using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class ThemeDal : ObjectDal<WrapperObjects.Theme, WrapperObjects.Themes>, IThemeRepository
  {
    public ThemeDal(IDbConnector connector) : base(connector)
    {
    }

    public void Insert(ThemeDataset.ThemeRow row)
    {
      WrapperObjects.Theme theme = new WrapperObjects.Theme();
      theme.Id = row.Id;
      theme.Description = row.sDescription;
      theme.Name = row.sName;
		
      base.Insert(theme);
    }
    public void Insert(ThemeDataset.ThemeDataTable table)
    {
      for (Int32 rowNo = 0; rowNo < table.Rows.Count; rowNo++)
        Insert(table[rowNo]);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }


    public ThemeDataset.ThemeDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);
      return GetHelper.GetTable<ThemeDataset, ThemeDataset.ThemeDataTable>(Connector, sql);
    }

  }
}