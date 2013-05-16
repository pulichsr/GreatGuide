using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IItineraryDestinationRepository
  {
    void Insert(ItineraryDestinationDataset.ItineraryDestinationRow ARow);
    void Insert(ItineraryDestinationDataset.ItineraryDestinationDataTable ATable);
    void DeleteAll();

    ItineraryDestinationDataset.ItineraryDestinationDataTable Get();
  }
}