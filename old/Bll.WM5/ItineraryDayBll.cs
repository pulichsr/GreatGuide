using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class ItineraryDayBll : ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public ItineraryDayBll(ILogger logger,IItineraryDayRepository repository)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(repository, "repository");

      this.logger = logger;
      this.repository = repository;
    }

    public string DatasetName
    {
      get { return "ItineraryDay"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      firstItineraryDay = null;
      lastItineraryDay = null;

      ItineraryDayDataset.ItineraryDayDataTable Data = (ItineraryDayDataset.ItineraryDayDataTable)newData.Tables["ItineraryDay"];
      if (Data != null)
      {
        if (merge == false)
        {
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

    public ItineraryDayDataset.ItineraryDayRow GetByDate(DateTime date)
    {
      return repository.GetByDate(date);
    }
    public ItineraryDayDataset.ItineraryDayRow GetByDate(DateTime date,out Int16 dayNumber,out Int16 numberOfDays)
    {
      dayNumber = 0;
      numberOfDays = 0;

      if (firstItineraryDay.HasValue == false)
        firstItineraryDay = repository.GetFirstDay();

      if (firstItineraryDay.HasValue == false)
        return null;

      if (lastItineraryDay.HasValue == false)
        lastItineraryDay = repository.GetLastDay();

      if (lastItineraryDay.HasValue == false)
        return null;

      if ((date < firstItineraryDay) || (date > lastItineraryDay))
        return null;

      dayNumber = (Int16)((date.Date - firstItineraryDay.Value.Date).TotalDays + 1);
      numberOfDays = (Int16)((lastItineraryDay.Value.Date - firstItineraryDay.Value.Date).TotalDays + 1);

      return repository.GetByDate(date);
    }
    public ItineraryDayDataset.ItineraryDayRow GetByDayNumber(Int16 dayNumber, out DateTime date, out Int16 numberOfDays)
    {
      date = DateTime.MinValue;
      numberOfDays = 0;

      if (dayNumber < 1)
      {
        return null;
      }

      if (firstItineraryDay.HasValue == false)
      {
        firstItineraryDay = repository.GetFirstDay();
      }

      if (firstItineraryDay == null)
      {
        return null;
      }

      if (lastItineraryDay.HasValue == false)
      {
        lastItineraryDay = repository.GetLastDay();
      }

      if (lastItineraryDay == null)
      {
        return null;
      }

      date = firstItineraryDay.Value.AddDays(dayNumber - 1);
      numberOfDays = (Int16)((lastItineraryDay.Value.Date - firstItineraryDay.Value.Date).TotalDays + 1);

      if (dayNumber > numberOfDays)
        return null;

      ItineraryDayDataset.ItineraryDayRow itineraryDayRow = null;

      try
      {
        itineraryDayRow = repository.GetByDate(date);
      }
      catch (Exception exc)
      {
        logger.Write(this, "Error in repository.GetByDate", exc);
      }

      return itineraryDayRow;
    }

    public void Insert(ItineraryDayDataset.ItineraryDayRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }

    private void ValidateRow(ItineraryDayDataset.ItineraryDayRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("ItineraryDay.Id is null");

      if (Row.IsItineraryDatNull() == true)
        throw new DataException("ItineraryDay.ItineraryDat is null");
    }

    private readonly ILogger logger;
    private readonly IItineraryDayRepository repository = null;
    private DateTime? firstItineraryDay;
    private DateTime? lastItineraryDay;
  }

}
