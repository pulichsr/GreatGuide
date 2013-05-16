using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IDestinationRepository
  {
    void Insert(DestinationDataset.DestinationRow row);
    void Insert(DestinationDataset.DestinationDataTable table);
    void DeleteAll();
    void DeleteById(Int32 id);

    DestinationDataset.DestinationDataTable Get();
    DestinationDataset.DestinationRow GetById(Int32 id);
    DestinationDataset.DestinationDataTable GetAll(Int32 masterAreaId);
    DestinationDataset.DestinationDataTable GetByType(Int32 masterAreaId,Int32 typeId);
    DestinationDataset.DestinationDataTable GetByClassification(Int32 masterAreaId,Int32 classificationId);
    DestinationDataset.DestinationDataTable GetByCollection(Int32 masterAreaId,Int32 collectionId);
    DestinationDataset.DestinationDataTable GetByClassificationCollection(Int32 masterAreaId,Int32 classificationId,Int32 collectionId);
    DestinationDataset.DestinationDataTable GetByTypeCollection(Int32 masterAreaId,Int32 typeId,Int32 collectionId);
    DestinationDataset.DestinationDataTable GetByItineraryDayId(Int32 id);
    DestinationDataset.DestinationDataTable GetMySelection();

    DataObjects.Destinations GetMatching(string searchCriteria, Int16 maxRows);
  }
}