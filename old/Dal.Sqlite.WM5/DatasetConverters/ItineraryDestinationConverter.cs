using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class ItineraryDestinationConverter
  {
    public static DataObjects.ItineraryDestination ToDataOject(ItineraryDestinationDataset.ItineraryDestinationRow row)
    {
      DataObjects.ItineraryDestination dataObject = new DataObjects.ItineraryDestination();
      dataObject.DestinationId = row.IsDestinationIdNull() ? new int?() : row.DestinationId;
      dataObject.DestinationOrder = row.IsDestinationOrderNull() ? new Int16?() : row.DestinationOrder;
      dataObject.Id = row.IsIdNull() ? new int?() : row.Id;
      dataObject.ItineraryDayId = row.IsItineraryDayIdNull() ? new Int32?() : row.ItineraryDayId;
    
      return dataObject;
    }
  }
}