using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IContentItemRepository
  {
    void Insert(ContentItemDataset.ContentItemRow row);
    void Insert(ContentItemDataset.ContentItemDataTable table);
    void Update(ContentItemDataset.ContentItemRow row);
    void DeleteAll();

    ContentItemDataset.ContentItemDataTable Get();
    ContentItemDataset.ContentItemRow GetById(Int32 id);
    ContentItemDataset.ContentItemDataTable GetByMasterArea(Int32 masterAreaId);

    DataTable GetFillerContent(Int32 masterAreaId);
  }
}