using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IDestinationCollectionMemberRepository
  {
    void Insert(DestinationCollectionMemberDataset.DestinationCollectionMemberRow row);
    void Insert(DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable table);
    void DeleteAll();

    DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable Get();
  }
}