using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

///
/// Auto genetared with ObjectGeneratorApp
///
namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_ITINERARY")]
  public class Itinerary
  {
    public Itinerary()
    {
    }
    public Itinerary(DataObjects.Itinerary itinerary)
    {
      this.dataObject = itinerary;
    }

    public DataObjects.Itinerary DataObject
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
    public string FirstName
    {
      get { return dataObject.FirstName; }
      set { dataObject.FirstName = value; }
    }

    [DbAutoColumnName]
    public string LastName
    {
      get { return dataObject.LastName; }
      set { dataObject.LastName = value; }
    }

		[DbAutoColumnName]
    public string Title
    {
      get { return dataObject.Title; }
      set { dataObject.Title = value; }
    }

    [DbAutoColumnName]
    public Int16? GracePeriod
    {
      get { return dataObject.GracePeriod; }
      set { dataObject.GracePeriod = value; }
    }

    [DbAutoColumnName]
    public string BookingReference
    {
      get { return dataObject.BookingReference; }
      set { dataObject.BookingReference = value; }
    }

    [DbAutoColumnName]
    public string GeoFenceData
    {
      get { return dataObject.GeoFenceData; }
      set { dataObject.GeoFenceData = value; }
    }

    [DbAutoColumnName]
    public DateTime? ArrivalDat
    {
      get { return dataObject.ArrivalDat; }
      set { dataObject.ArrivalDat = value; }
    }

    [DbAutoColumnName]
    public DateTime? DepartureDat
    {
      get { return dataObject.DepartureDat; }
      set { dataObject.DepartureDat = value; }
    }

		[DbAutoColumnName]
    public string Branding1
    {
      get { return dataObject.Branding1; }
      set { dataObject.Branding1 = value; }
    }

		[DbAutoColumnName]
    public string Branding2
    {
      get { return dataObject.Branding2; }
      set { dataObject.Branding2 = value; }
    }

		[DbAutoColumnName]
    public string Branding3
    {
      get { return dataObject.Branding3; }
      set { dataObject.Branding3 = value; }
    }

		[DbAutoColumnName]
    public string Culture
    {
      get { return dataObject.Culture; }
      set { dataObject.Culture = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.Itinerary dataObject = new DataObjects.Itinerary();

    #endregion
  }

  public class Itineraries : List<Itinerary>
  {
    public DataObjects.Itineraries DataObjects
    {
      get
      {
        DataObjects.Itineraries objects = new DataObjects.Itineraries();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}