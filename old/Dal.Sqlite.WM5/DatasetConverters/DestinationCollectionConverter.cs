using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class DestinationCollectionConverter
  {
    public static DataObjects.DestinationCollection ToDataOject(DestinationCollectionDataset.DestinationCollectionRow row)
    {
      DataObjects.DestinationCollection dataObject = new DataObjects.DestinationCollection();
      dataObject.Code = row.sCode;
      dataObject.Id = row.IsIdNull() ? new int?() : row.Id;
      dataObject.Name = row.sName;
    
      return dataObject;
    }
  }
}