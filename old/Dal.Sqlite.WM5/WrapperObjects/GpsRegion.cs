using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_GPS_REGION")]
  public class GpsRegion
  {
    public GpsRegion()
    {
    }
    public GpsRegion(DataObjects.GpsRegion gpsRegion)
    {
      this.dataObject = gpsRegion;
    }

    public DataObjects.GpsRegion DataObject
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
    public string RegionData
    {
      get { return dataObject.RegionData; }
      set { dataObject.RegionData = value; }
    }

    [DbAutoColumnName]
    public decimal? MinLatitude
    {
      get { return dataObject.MinLatitude; }
      set { dataObject.MinLatitude = value; }
    }

    [DbAutoColumnName]
    public decimal? MaxLatitude
    {
      get { return dataObject.MaxLatitude; }
      set { dataObject.MaxLatitude = value; }
    }

    [DbAutoColumnName]
    public decimal? MinLongitude
    {
      get { return dataObject.MinLongitude; }
      set { dataObject.MinLongitude = value; }
    }

    [DbAutoColumnName]
    public decimal? MaxLongitude
    {
      get { return dataObject.MaxLongitude; }
      set { dataObject.MaxLongitude = value; }
    }

    [DbAutoColumnName]
    public string RegionType
    {
      get { return dataObject.RegionType; }
      set { dataObject.RegionType = value; }
    }

    [DbAutoColumnName]
    public bool ResetOnEntry
    {
      get { return dataObject.ResetOnEntry; }
      set { dataObject.ResetOnEntry = value; }
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
    public string Name
    {
      get { return dataObject.Name; }
      set { dataObject.Name = value; }
    }
    #endregion

    #region Fields

    private readonly DataObjects.GpsRegion dataObject = new DataObjects.GpsRegion();

    #endregion
  }

  public class GpsRegions : List<GpsRegion>
  { 
    public DataObjects.GpsRegions DataObjects
    {
      get
      {
        DataObjects.GpsRegions objects = new DataObjects.GpsRegions();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}