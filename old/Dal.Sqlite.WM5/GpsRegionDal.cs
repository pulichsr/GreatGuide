using System;
using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class GpsRegionDal : ObjectDal<WrapperObjects.GpsRegion, WrapperObjects.GpsRegions>, IGpsRegionRepository
  {
    public GpsRegionDal(IDbConnector connector) : base(connector)
    {
    }

    public void Insert(GpsRegionDataset.GpsRegionRow Row)
    {
      WrapperObjects.GpsRegion GpsRegion = new WrapperObjects.GpsRegion();
      GpsRegion.Id = Row.Id;
      GpsRegion.RegionType = Row.sRegionType;
      GpsRegion.RegionData = Row.sRegionData;
      GpsRegion.MaxLatitude = Row.IsMaxLatitudeNull() ? new decimal?() : (decimal)Row.MaxLatitude;
      GpsRegion.MaxLongitude = Row.IsMaxLongitudeNull() ? new decimal?() : (decimal)Row.MaxLongitude;
      GpsRegion.MinLatitude = Row.IsMinLatitudeNull() ? new decimal?() : (decimal)Row.MinLatitude;
      GpsRegion.MinLongitude = Row.IsMinLongitudeNull() ? new decimal?() : (decimal)Row.MinLongitude;
      GpsRegion.MasterAreaId = Row.IsMasterAreaIdNull() ? new int?() : Row.MasterAreaId;
      GpsRegion.ThemeId = Row.IsThemeIdNull() ? new int?() : Row.ThemeId;
      GpsRegion.ResetOnEntry = Row.IsResetOnEntryNull() ? false : Row.ResetOnEntry;

      base.Insert(GpsRegion);
    }
    public void Insert(GpsRegionDataset.GpsRegionDataTable table)
    {
      for (Int32 rowNo = 0; rowNo < table.Rows.Count; rowNo++)
        Insert(table[rowNo]);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}",DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public GpsRegionDataset.GpsRegionDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<GpsRegionDataset, GpsRegionDataset.GpsRegionDataTable>(Connector, sql);
    }
    public GpsRegionDataset.GpsRegionDataTable GetByMasterArea(Int32 masterAreaId)
    {
      string sql = string.Format("select * from {0} where MASTERAREAID = {1}", DbMetadata.SelectViewName,masterAreaId);

      return GetHelper.GetTable<GpsRegionDataset, GpsRegionDataset.GpsRegionDataTable>(Connector, sql);
    }

  }
}
