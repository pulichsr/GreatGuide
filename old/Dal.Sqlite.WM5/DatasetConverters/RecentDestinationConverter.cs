using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class RecentDestinationConverter
  {
    public static DataObjects.RecentDestination ToDataOject(RecentDestinationDataset.RecentDestinationRow row)
    {
      DataObjects.RecentDestination dataObject = new DataObjects.RecentDestination();
      dataObject.DestinationId = row.IsDestinationIdNull() ? new int?() : row.DestinationId;
      dataObject.Latitude = row.IsLatitudeNull() ? new decimal?() : (decimal)row.Latitude;
      dataObject.Longitude = row.IsLongitudeNull() ? new decimal?() : (decimal)row.Longitude;
      dataObject.Name = row.sName;
      dataObject.VisitedDat = row.IsVisitedDatNull() ? new DateTime?() : row.VisitedDat;
    
      return dataObject;
    }
  }
}