using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IDestinationCollectionRepository
  {
    void Insert(DestinationCollectionDataset.DestinationCollectionRow row);
    void Insert(DestinationCollectionDataset.DestinationCollectionDataTable table);
    void DeleteAll();

    DestinationCollectionDataset.DestinationCollectionDataTable Get();
    DestinationCollectionDataset.DestinationCollectionRow GetByCode(string code);
  }
}