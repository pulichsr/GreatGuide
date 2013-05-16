using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class DestinationTypeConverter
  {
    public static DataObjects.DestinationType ToDataOject(DestinationTypeDataset.DestinationTypeRow row)
    {
      DataObjects.DestinationType dataObject = new DataObjects.DestinationType();
      dataObject.Code = row.sCode;
      dataObject.Comment1Label = row.sComment1Label;
      dataObject.Comment2Label = row.sComment2Label;
      dataObject.Comment3Label = row.sComment3Label;
      dataObject.Comment4Label = row.sComment4Label;
      dataObject.Description = row.sDescription;
      dataObject.IconResourceName = row.sIconResourceName;
      dataObject.Id = row.IsIdNull() ? new int?() : row.Id;
      dataObject.Name = row.sName;
    
      return dataObject;
    }
  }
}