using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class DestinationCollectionMemberConverter
  {
    public static DataObjects.DestinationCollectionMember ToDataOject(DestinationCollectionMemberDataset.DestinationCollectionMemberRow row)
    {
      DataObjects.DestinationCollectionMember dataObject = new DataObjects.DestinationCollectionMember();
      dataObject.CollectionId = row.IsCollectionIdNull() ? new int?() : row.CollectionId;
      dataObject.DestinationId = row.IsDestinationIdNull() ? new int?() : row.DestinationId;
    
      return dataObject;
    }
  }
}