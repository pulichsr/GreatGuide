using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.GoodGuide.Types;
using GetDataResponse=Nucleo.GoodGuide.Datasets.XmlDatasets.GetDataResponse;

namespace Nucleo.GoodGuide.Bll
{
  public class GpsRegionBll:
    ISyncTarget,
    ISyncSource
  {
    public GpsRegionBll(IGpsRegionRepository dal)
    {
      this.repository = dal;
    }

    public string DatasetName
    {
      get { return "GpsRegion"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      GpsRegionDataset.GpsRegionDataTable data = (GpsRegionDataset.GpsRegionDataTable)newData.Tables["GpsRegion"];
      if (data != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < data.Rows.Count; RowNo++)
        {
          data[RowNo].Id += offset;

          if (data[RowNo].IsMasterAreaIdNull() == false) data[RowNo].MasterAreaId += offset;
          if (data[RowNo].IsThemeIdNull() == false) data[RowNo].ThemeId += offset;

          Insert(data[RowNo]);
        }

        data.Dispose();
      }
    }
    public DataTable Get()
    {
      return repository.Get();
    }

    public Regions GetRegionsByMasterArea(Int32 masterAreaId)
    {
      GpsRegionDataset.GpsRegionDataTable Data = repository.GetByMasterArea(masterAreaId);

      Regions Regions = CreateRegions(Data);
      Data.Dispose();

      return Regions;
    }
    public void Insert(GpsRegionDataset.GpsRegionRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }
    

    private static void ValidateRow(GpsRegionDataset.GpsRegionRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("GpsRegion.Id is null");

      if (Row.IsRegionTypeNull() == true)
        throw new DataException("GpsRegion.RegionType is null");

      if (Row.IsRegionDataNull() == true)
        throw new DataException("GpsRegion.RegionData is null");

      if (Row.IsMinLatitudeNull() == true)
        Row.MinLatitude = 0;

      if (Row.IsMaxLatitudeNull() == true)
        Row.MaxLatitude = 0;

      if (Row.IsMinLongitudeNull() == true)
        Row.MinLongitude = 0;

      if (Row.IsMaxLongitudeNull() == true)
        Row.MaxLongitude = 0;

      if (Row.IsResetOnEntryNull() == true)
        Row.ResetOnEntry = false;

      if (Row.IsMasterAreaIdNull() == true)
        throw new DataException("GpsRegion.MasterAreaId is null");
    }
    private static Regions CreateRegions(GpsRegionDataset.GpsRegionDataTable data)
    {
      Regions Regions = new Regions();

      if (data == null)
        return Regions;

      Region NewRegion = null;
      GpsRegionDataset.GpsRegionRow Row;
      for (Int32 RegionNo = 0; RegionNo < data.Rows.Count; RegionNo++)
      {
        Row = data[RegionNo];
        if (Row.sRegionType == CircularRegion.RegionType)
          NewRegion = new CircularRegion(Row.Id, Row.MinLatitude, Row.MaxLatitude, Row.MinLongitude, Row.MaxLongitude, Row.sRegionData, Row.ResetOnEntry);
        if (Row.sRegionType == PolygonRegion.RegionType)
          NewRegion = new PolygonRegion(Row.Id, Row.MinLatitude, Row.MaxLatitude, Row.MinLongitude, Row.MaxLongitude, Row.sRegionData, Row.ResetOnEntry);

        if (NewRegion == null)
          throw new InvalidOperationException("Invalid RegionType");
        else
          Regions.Add(NewRegion);
      }

      return Regions;     
    }

    private readonly IGpsRegionRepository repository = null;
  }

}
