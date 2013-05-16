using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class ItineraryDestinationBll : ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public ItineraryDestinationBll(ILogger logger,IItineraryDestinationRepository repository)
    {
      Guard.ArgumentNotNull(repository, "repository");
      Guard.ArgumentNotNull(logger, "logger");

      this.repository = repository;
      this.logger = logger;
    }

    public string DatasetName
    {
      get { return "ItineraryDestination"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      logger.Write(this, ">> ISyncTarget.Insert");

      ItineraryDestinationDataset.ItineraryDestinationDataTable Data = (ItineraryDestinationDataset.ItineraryDestinationDataTable)newData.Tables["ItineraryDestination"];
      if (Data != null)
      {
        if (merge == false)
        {
          logger.Write(this, "   Not merging. DeleteAll");
          DeleteAll();
        }

        for (Int32 RowNo = 0; RowNo < Data.Rows.Count; RowNo++)
        {
          Data[RowNo].Id += offset;

          if (Data[RowNo].IsDestinationIdNull() == false) Data[RowNo].DestinationId += offset;
          if (Data[RowNo].IsItineraryDayIdNull() == false) Data[RowNo].ItineraryDayId += offset;

          logger.Write(this, 
            string.Format("  Inserting: Id:{0}, ItineraryDayId:{1}, DestinationId:{2}, DestinationOrder:{3}", 
            Data[RowNo].Id, 
            Data[RowNo].ItineraryDayId, 
            Data[RowNo].DestinationId, 
            Data[RowNo].DestinationOrder));

          Insert(Data[RowNo]);
        }

        Data.Dispose();
      }
    }

    public void Insert(ItineraryDestinationDataset.ItineraryDestinationRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }

    private void ValidateRow(ItineraryDestinationDataset.ItineraryDestinationRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("ItineraryDestination.Id is null");

      if (Row.IsItineraryDayIdNull() == true)
        throw new DataException("ItineraryDestination.ItineraryDayId is null");

      if (Row.IsDestinationIdNull() == true)
        throw new DataException("ItineraryDestination.DestinationId is null");

      if (Row.IsDestinationOrderNull() == true)
        Row.DestinationOrder = 0;
    }

    private readonly ILogger logger;
    private readonly IItineraryDestinationRepository repository = null;
  }

}
