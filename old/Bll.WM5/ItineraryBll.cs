using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class ItineraryBll:
    ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public ItineraryBll(ILogger logger,IItineraryRepository repository)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(repository, "repository");

      this.logger = logger;
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "Itinerary"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      logger.Write(this, ">> ISyncTarget.Insert");

      ItineraryDataset.ItineraryDataTable Data = (ItineraryDataset.ItineraryDataTable)newData.Tables["Itinerary"];
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

          Insert(Data[RowNo]);
        }

        Data.Dispose();
      }
    }

    public ItineraryDataset.ItineraryRow GetFirst()
    {
      return repository.GetRow();
    }
    public void Insert(ItineraryDataset.ItineraryRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }

    private void ValidateRow(ItineraryDataset.ItineraryRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("Itinerary.Id is null");

      if (Row.IsArrivalDatNull() == true)
        Row.sArrivalDat = string.Empty;

      if (Row.IsDepartureDatNull() == true)
        Row.sDepartureDat = string.Empty;

      if (Row.IsGracePeriodNull() == true)
        Row.GracePeriod = 3;

      if (Row.IsGeofenceDataNull() == true)
        Row.GeofenceData = string.Empty;

      if (Row.IsCultureNull() == true)
        Row.Culture = LanguageHelper.CreateCulture(Language.English).Name;
    }

    private readonly ILogger logger = null;
    private readonly IItineraryRepository repository = null;
  }

}
