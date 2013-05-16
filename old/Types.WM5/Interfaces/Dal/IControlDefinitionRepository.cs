using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IControlDefinitionRepository
  {
    void Insert(ControlDefinitionDataset.ControlDefinitionRow row);
    void Insert(ControlDefinitionDataset.ControlDefinitionDataTable table);
    void DeleteAll();

    ControlDefinitionDataset.ControlDefinitionDataTable Get();
    ControlDefinitionDataset.ControlDefinitionDataTable GetByMasterArea(Int32 masterAreaId);
    ControlDefinitionDataset.ControlDefinitionDataTable GetByFormId(Int32 formId);
  }
}