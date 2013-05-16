using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IMasterAreaRepository
  {
    void Insert(MasterAreaDataset.MasterAreaRow row);
    void Insert(MasterAreaDataset.MasterAreaDataTable table);
    void DeleteAll();

    MasterAreaDataset.MasterAreaDataTable Get();
    MasterAreaDataset.MasterAreaRow GetById(Int32 id);
  }
}