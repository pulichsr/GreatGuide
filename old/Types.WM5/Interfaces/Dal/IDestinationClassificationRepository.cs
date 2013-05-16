using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IDestinationClassificationRepository
  {
    void Insert(DestinationClassificationDataset.DestinationClassificationRow row);
    void Insert(DestinationClassificationDataset.DestinationClassificationDataTable table);
    void DeleteAll();

    DestinationClassificationDataset.DestinationClassificationDataTable Get();
    DestinationClassificationDataset.DestinationClassificationRow GetByCode(string code);
  }
}