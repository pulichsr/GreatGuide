using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IFormDefinitionRepository
  {
    void Insert(FormDefinitionDataset.FormDefinitionRow row);
    void Insert(FormDefinitionDataset.FormDefinitionDataTable table);
    void DeleteAll();

    FormDefinitionDataset.FormDefinitionDataTable Get();
    FormDefinitionDataset.FormDefinitionDataTable GetByMasterArea(Int32 masterAreaId);
    FormDefinitionDataset.FormDefinitionRow GetByName(Int32 masterAreaId,string name);
  }
}