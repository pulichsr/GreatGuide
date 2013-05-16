using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_ITINERARY_DESTINATION")]
  public class ItineraryDestination
  {
    public ItineraryDestination()
    {
    }
    public ItineraryDestination(DataObjects.ItineraryDestination itineraryDestination)
    {
      this.dataObject = itineraryDestination;
    }

    public DataObjects.ItineraryDestination DataObject
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
    public Int32? ItineraryDayId
    {
      get { return dataObject.ItineraryDayId; }
      set { dataObject.ItineraryDayId = value; }
    }

    [DbAutoColumnName]
    public Int32? DestinationId
    {
      get { return dataObject.DestinationId; }
      set { dataObject.DestinationId = value; }
    }

    [DbAutoColumnName]
    public Int16? DestinationOrder
    {
      get { return dataObject.DestinationOrder; }
      set { dataObject.DestinationOrder = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.ItineraryDestination dataObject = new DataObjects.ItineraryDestination();

    #endregion    
  }

  public class ItineraryDestinations : List<ItineraryDestination>
  {
    public DataObjects.ItineraryDestinations DataObjects
    {
      get
      {
        DataObjects.ItineraryDestinations objects = new DataObjects.ItineraryDestinations();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}