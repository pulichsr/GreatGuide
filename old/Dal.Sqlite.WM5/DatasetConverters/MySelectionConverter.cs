using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class MySelectionConverter
  {
    public static DataObjects.MySelection ToDataOject(MySelectionDataset.MySelectionRow row)
    {
      DataObjects.MySelection dataObject = new DataObjects.MySelection();
      dataObject.DestinationId = row.IsDestinationIdNull() ? new int?() : row.DestinationId;
    
      return dataObject;
    }
  }
}