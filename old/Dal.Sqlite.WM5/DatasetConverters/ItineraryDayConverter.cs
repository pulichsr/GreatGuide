using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class ItineraryDayConverter
  {
    public static DataObjects.ItineraryDay ToDataOject(ItineraryDayDataset.ItineraryDayRow row)
    {
      DataObjects.ItineraryDay dataObject = new DataObjects.ItineraryDay();
      dataObject.Comment = row.sComment;
      dataObject.Id = row.IsIdNull() ? new int?() : row.Id;
      dataObject.ItineraryDat = row.IsItineraryDatNull() ? new DateTime?() : row.ItineraryDat;
    
      return dataObject;
    }
  }
}