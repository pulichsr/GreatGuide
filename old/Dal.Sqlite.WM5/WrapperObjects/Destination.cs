using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_DESTINATION")]
  public class Destination
  {
    public Destination()
    {
    }
    public Destination(DataObjects.Destination destination)
    {
      this.dataObject = destination;
    }

    public DataObjects.Destination DataObject
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
    public string Code
    {
      get { return dataObject.Code; }
      set { dataObject.Code = value; }
    }

    [DbAutoColumnName]
    public string ShortDescription
    {
      get { return dataObject.ShortDescription; }
      set { dataObject.ShortDescription = value; }
    }

    [DbAutoColumnName]
    public string LongDescription
    {
      get { return dataObject.LongDescription; }
      set { dataObject.LongDescription = value; }
    }

    [DbAutoColumnName]
    public Int32? DestinationTypeId
    {
      get { return dataObject.DestinationTypeId; }
      set { dataObject.DestinationTypeId = value; }
    }

    [DbAutoColumnName]
    public Int32? ClassificationId
    {
      get { return dataObject.ClassificationId; }
      set { dataObject.ClassificationId = value; }
    }

    [DbAutoColumnName]
    public string ArrivalZoneData
    {
      get { return dataObject.ArrivalZoneData; }
      set { dataObject.ArrivalZoneData = value; }
    }

		[DbAutoColumnName]
    public string Address
    {
      get { return dataObject.Address; }
      set { dataObject.Address = value; }
    }

    [DbAutoColumnName]
    public string TelephoneNo
    {
      get { return dataObject.TelephoneNo; }
      set { dataObject.TelephoneNo = value; }
    }

    [DbAutoColumnName]
    public Int32? MasterAreaId
    {
      get { return dataObject.MasterAreaId; }
      set { dataObject.MasterAreaId = value; }
    }

    [DbAutoColumnName]
    public Int32? ThemeId
    {
      get { return dataObject.ThemeId; }
      set { dataObject.ThemeId = value; }
    }

    [DbAutoColumnName]
    public Int32? VersionNo
    {
      get { return dataObject.VersionNo; }
      set { dataObject.VersionNo = value; }
    }

		[DbAutoColumnName]
    public Int16? Recommendation
    {
      get { return dataObject.Recommendation; }
      set { dataObject.Recommendation = value; }
    }

		[DbAutoColumnName]
    public string Location
    {
      get { return dataObject.Location; }
      set { dataObject.Location = value; }
    }

		[DbAutoColumnName]
    public string Condition
    {
      get { return dataObject.Condition; }
      set { dataObject.Condition = value; }
    }

		[DbAutoColumnName]
    public decimal? Latitude
    {
      get { return dataObject.Latitude; }
      set { dataObject.Latitude = value; }
    }

		[DbAutoColumnName]
    public decimal? Longitude
    {
      get { return dataObject.Longitude; }
      set { dataObject.Longitude = value; }
    }

    [DbAutoColumnName]
    public string CellNo
    {
      get { return dataObject.CellNo; }
      set { dataObject.CellNo = value; }
    }

    [DbAutoColumnName]
    public string Image1Filename
    {
      get { return dataObject.Image1Filename; }
      set { dataObject.Image1Filename = value; }
    }

    [DbAutoColumnName]
    public string Image2Filename
    {
      get { return dataObject.Image2Filename; }
      set { dataObject.Image2Filename = value; }
    }

		[DbAutoColumnName]
    public string Comment1
    {
      get { return dataObject.Comment1; }
      set { dataObject.Comment1 = value; }
    }

		[DbAutoColumnName]
    public string Comment2
    {
      get { return dataObject.Comment2; }
      set { dataObject.Comment2 = value; }
    }

		[DbAutoColumnName]
    public string Comment3
    {
      get { return dataObject.Comment3; }
      set { dataObject.Comment3 = value; }
    }

		[DbAutoColumnName]
    public string Comment4
    {
      get { return dataObject.Comment4; }
      set { dataObject.Comment4 = value; }
    }

		[DbAutoColumnName]
    public string Booking
    {
      get { return dataObject.Booking; }
      set { dataObject.Booking = value; }
    }

		[DbAutoColumnName]
    public string Language
    {
      get { return dataObject.Language; }
      set { dataObject.Language = value; }
    }

		[DbAutoColumnName]
    public string Comment
    {
      get { return dataObject.Comment; }
      set { dataObject.Comment = value; }
    }

    [DbAutoColumnName]
    public Int32? CollectionId
    {
      get { return dataObject.CollectionId; }
      set { dataObject.CollectionId = value; }
    }

    [DbAutoColumnName]
    public string ArrivalZoneType
    {
      get { return dataObject.ArrivalZoneType; }
      set { dataObject.ArrivalZoneType = value; }
    }

    [DbAutoColumnName]
    public decimal? ArrivalZoneMinLatitude
    {
      get { return dataObject.ArrivalZoneMinLatitude; }
      set { dataObject.ArrivalZoneMinLatitude = value; }
    }

    [DbAutoColumnName]
    public decimal? ArrivalZoneMaxLatitude
    {
      get { return dataObject.ArrivalZoneMaxLatitude; }
      set { dataObject.ArrivalZoneMaxLatitude = value; }
    }

    [DbAutoColumnName]
    public decimal? ArrivalZoneMinLongitude
    {
      get { return dataObject.ArrivalZoneMinLongitude; }
      set { dataObject.ArrivalZoneMinLongitude = value; }
    }

    [DbAutoColumnName]
    public decimal? ArrivalZoneMaxLongitude
    {
      get { return dataObject.ArrivalZoneMaxLongitude; }
      set { dataObject.ArrivalZoneMaxLongitude = value; }
    }

    [DbAutoColumnName]
    public Int32? GridReferenceX
    {
      get { return dataObject.GridReferenceX; }
      set { dataObject.GridReferenceX = value; }
    }

    [DbAutoColumnName]
    public Int32? GridReferenceY
    {
      get { return dataObject.GridReferenceY; }
      set { dataObject.GridReferenceY = value; }
    }

    [DbAutoColumnName]
    public bool IncludeInNearestSearch
    {
      get { return dataObject.IncludeInNearestSearch; }
      set { dataObject.IncludeInNearestSearch = value; }
    }

    [DbAutoColumnName]
    public string SearchKey
    {
      get { return dataObject.SearchKey; }
      set { dataObject.SearchKey = value; }
    }

    [DbAutoColumnName]
    [DbReadonlyColumn]
    public string DestinationTypeDescription
    {
      get { return dataObject.DestinationTypeDescription; }
      set { dataObject.DestinationTypeDescription = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.Destination dataObject = new DataObjects.Destination();

    #endregion
  }

  public class Destinations : List<Destination>
  {
    public DataObjects.Destinations DataObjects
    {
      get
      {
        DataObjects.Destinations objects = new DataObjects.Destinations();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}