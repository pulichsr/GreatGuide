
using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IGpsRegionRepository
  {
    void Insert(GpsRegionDataset.GpsRegionRow row);
    void Insert(GpsRegionDataset.GpsRegionDataTable table);
    void DeleteAll();

    GpsRegionDataset.GpsRegionDataTable Get();
    GpsRegionDataset.GpsRegionDataTable GetByMasterArea(Int32 masterAreaId);
  }
}