using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IDestinationTypeRepository
  {
    void Insert(DestinationTypeDataset.DestinationTypeRow ARow);
    void Insert(DestinationTypeDataset.DestinationTypeDataTable ATable);
    void DeleteAll();
    
    DestinationTypeDataset.DestinationTypeDataTable Get();
    DestinationTypeDataset.DestinationTypeRow GetById(Int32 id);
    DestinationTypeDataset.DestinationTypeRow GetByCode(string code);
  }
}