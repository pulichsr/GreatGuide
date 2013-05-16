using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IItineraryDayRepository
  {
    void Insert(ItineraryDayDataset.ItineraryDayRow ARow);
    void Insert(ItineraryDayDataset.ItineraryDayDataTable ATable);
    void DeleteAll();

    ItineraryDayDataset.ItineraryDayDataTable Get();
    ItineraryDayDataset.ItineraryDayRow GetById(Int32 id);
    ItineraryDayDataset.ItineraryDayRow GetByDate(DateTime date);
    DateTime? GetFirstDay();
    DateTime? GetLastDay();

  }
}