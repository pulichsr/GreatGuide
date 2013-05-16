using System;
using System.Data;
using System.Data.SQLite;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class MasterAreaDal : ObjectDal<WrapperObjects.MasterArea, WrapperObjects.MasterAreas>, IMasterAreaRepository
  {
    public MasterAreaDal(IDbConnector connector) : base(connector)
    {
    }

    public void Insert(MasterAreaDataset.MasterAreaRow Row)
    {
      WrapperObjects.MasterArea masterArea = new WrapperObjects.MasterArea();
      masterArea.Id = Row.Id;
      masterArea.ContentBasePath = Row.sContentBasePath;
      masterArea.Description = Row.sDescription;
      masterArea.MaxLatitude = Row.IsMaxLatitudeNull() ? new decimal?() : (decimal)Row.MaxLatitude;
      masterArea.MaxLongitude = Row.IsMaxLongitudeNull() ? new decimal?() : (decimal)Row.MaxLongitude;
      masterArea.MinLatitude = Row.IsMinLatitudeNull() ? new decimal?() : (decimal)Row.MinLatitude;
      masterArea.MinLongitude = Row.IsMinLongitudeNull() ? new decimal?() : (decimal)Row.MinLongitude;
      masterArea.Name = Row.sName;
      masterArea.RegionData = Row.sRegionData;

      base.Insert(masterArea);
    }
    public void Insert(MasterAreaDataset.MasterAreaDataTable table)
    {
      for (Int32 rowNo = 0;rowNo < table.Rows.Count;rowNo++)
        Insert(table[rowNo]);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}",this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public MasterAreaDataset.MasterAreaDataTable Get()
    {
      IDbCommand command = Connector.CreateCommand();
      command.CommandText = string.Format("select * from {0} order by NAME", DbMetadata.SelectViewName);

      SQLiteDataAdapter adapter = (SQLiteDataAdapter)Connector.CreateDataAdapter(command);
      MasterAreaDataset dataset = new MasterAreaDataset();
      adapter.Fill(dataset,dataset.MasterArea.TableName);
      command.Dispose();

      if (dataset.Tables.Count == 0)
        return null;

      return dataset.MasterArea;
    }
    public MasterAreaDataset.MasterAreaRow GetById(int id)
    {
      IDbCommand command = Connector.CreateCommand();
      command.CommandText = string.Format("select * from {0} where ID = {1}", DbMetadata.SelectViewName, id);

      SQLiteDataAdapter adapter = (SQLiteDataAdapter)Connector.CreateDataAdapter(command);
      MasterAreaDataset dataset = new MasterAreaDataset();
      adapter.Fill(dataset, dataset.MasterArea.TableName);
      command.Dispose();

      if (dataset.MasterArea.Rows.Count == 0)
        return null;

      return dataset.MasterArea[0];
    }

  }
}
