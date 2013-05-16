using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.GoodGuide.Types;
using GetDataResponse=Nucleo.GoodGuide.Datasets.XmlDatasets.GetDataResponse;

namespace Nucleo.GoodGuide.Bll
{
  public class MasterAreaBll
    : ISyncTarget,
    ISyncSource
  {
    public const Int32 UndefinedId = -1;

    public MasterAreaBll(IMasterAreaRepository dal)
    {
      this.repository = dal;
    }

    public string DatasetName
    {
      get { return "MasterArea"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      MasterAreaDataset.MasterAreaDataTable MasterAreaData = (MasterAreaDataset.MasterAreaDataTable)newData.Tables["MasterArea"];
      if (MasterAreaData != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < MasterAreaData.Rows.Count; RowNo++)
        {
          MasterAreaData[RowNo].Id += offset;
          Insert(MasterAreaData[RowNo]);
        }

        MasterAreaData.Dispose();
      }
    }
    public DataTable Get()
    {
      return repository.Get();
    }

    public Regions GetRegions()
    {
      Regions Regions = new Regions();

      MasterAreaDataset.MasterAreaDataTable Data = repository.Get();
      if (Data == null)
        return Regions;

      Region NewRegion;
      MasterAreaDataset.MasterAreaRow Row;
      for (Int32 RegionNo = 0; RegionNo < Data.Rows.Count; RegionNo++)
      {
        Row = Data[RegionNo];
        NewRegion = new PolygonRegion(Row.Id, (float)Row.MinLatitude, (float)Row.MaxLatitude, (float)Row.MinLongitude, (float)Row.MaxLongitude, Row.sRegionData, false);

        Regions.Add(NewRegion);
      }

      Data.Dispose();

      return Regions;
    }

    public MasterAreaDataset.MasterAreaRow GetById(Int32 id)
    {
      return repository.GetById(id);
    }
    public void Insert(MasterAreaDataset.MasterAreaRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }
    public MasterAreaDataset.MasterAreaDataTable GetAll()
    {
      return repository.Get();
    }

    private void ValidateRow(MasterAreaDataset.MasterAreaRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("MasterArea.Id is null");

      if (Row.IsMinLatitudeNull() == true)
        Row.MinLatitude = 0;

      if (Row.IsMaxLatitudeNull() == true)
        Row.MaxLatitude = 0;

      if (Row.IsMinLongitudeNull() == true)
        Row.MinLongitude = 0;

      if (Row.IsMaxLongitudeNull() == true)
        Row.MaxLongitude = 0;

      if (Row.IsRegionDataNull() == true)
        throw new DataException("MasterArea.RegionData is null");
    }

    private readonly IMasterAreaRepository repository = null;
  }

}
