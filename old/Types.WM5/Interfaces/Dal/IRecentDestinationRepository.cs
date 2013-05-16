using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IRecentDestinationRepository
  {
    void Insert(RecentDestinationDataset.RecentDestinationRow row);
    void Insert(RecentDestinationDataset.RecentDestinationDataTable table);
    void DeleteAll();

    RecentDestinationDataset.RecentDestinationDataTable Get();
    Boolean IsDestinationIdRecent(Int32 destinationId);
    Boolean IsDestinationNameRecent(string destinationName);
  }
}