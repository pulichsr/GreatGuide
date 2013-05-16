using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IMySelectionRepository
  {
    void Insert(MySelectionDataset.MySelectionRow ARow);
    void Insert(MySelectionDataset.MySelectionDataTable ATable);
    void DeleteAll();

    MySelectionDataset.MySelectionDataTable Get();
    Boolean IsInMySelection(Int32 destinationId);
    void Delete(Int32 destinationId);
  }
}