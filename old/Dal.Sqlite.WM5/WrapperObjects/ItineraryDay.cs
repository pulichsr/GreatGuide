using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_ITINERARY_DAY")]
  public class ItineraryDay
  {
    public ItineraryDay()
    {
    }
    public ItineraryDay(DataObjects.ItineraryDay itineraryDay)
    {
      this.dataObject = itineraryDay;
    }

    public DataObjects.ItineraryDay DataObject
    {
      get { return dataObject; }
    }

    #region Properties

    [DbAutoColumnName]
    [DbPrimaryKey]
    public Int32? Id
    {
      get { return dataObject.Id; }
      set { dataObject.Id = value; }
    }

    [DbAutoColumnName]
    public DateTime? ItineraryDat
    {
      get { return dataObject.ItineraryDat; }
      set { dataObject.ItineraryDat = value; }
    }

		[DbAutoColumnName]
    public string Comment
    {
      get { return dataObject.Comment; }
      set { dataObject.Comment = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.ItineraryDay dataObject = new DataObjects.ItineraryDay();

    #endregion
  }

  public class ItineraryDays : List<ItineraryDay>
  { 
    public DataObjects.ItineraryDays DataObjects
    {
      get
      {
        DataObjects.ItineraryDays objects = new DataObjects.ItineraryDays();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}