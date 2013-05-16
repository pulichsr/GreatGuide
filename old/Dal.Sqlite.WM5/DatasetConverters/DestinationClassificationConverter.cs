using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class DestinationClassificationConverter
  {
    public static DataObjects.DestinationClassification ToDataOject(DestinationClassificationDataset.DestinationClassificationRow row)
    {
      DataObjects.DestinationClassification dataObject = new DataObjects.DestinationClassification();
      dataObject.Code = row.sCode;
      dataObject.Description = row.sDescription;
      dataObject.DestinationTypeId = row.IsDestinationTypeIdNull() ? new int?() : row.DestinationTypeId;
      dataObject.Id = row.IsIdNull() ? new int?() : row.Id;
      dataObject.Name = row.sName;
    
      return dataObject;
    }
  }
}