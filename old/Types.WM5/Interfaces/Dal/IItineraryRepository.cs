using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IItineraryRepository
  {
    void Insert(ItineraryDataset.ItineraryRow ARow);
    void Insert(ItineraryDataset.ItineraryDataTable ATable);
    void DeleteAll();

    ItineraryDataset.ItineraryDataTable Get();
    ItineraryDataset.ItineraryRow GetRow();
  }
}