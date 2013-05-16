using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_MASTER_AREA")]
  public class MasterArea
  {
    public MasterArea()
    {
    }
    public MasterArea(DataObjects.MasterArea masterArea)
    {
      this.dataObject = masterArea;
    }

    public DataObjects.MasterArea DataObject
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
    public string Name
    {
      get { return dataObject.Name; }
      set { dataObject.Name = value; }
    }

    [DbAutoColumnName]
    public string Description
    {
      get { return dataObject.Description; }
      set { dataObject.Description = value; }
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
    public string ContentBasePath
    {
      get { return dataObject.ContentBasePath; }
      set { dataObject.ContentBasePath = value; }
    }

    [DbAutoColumnName]
    public string RegionData
    {
      get { return dataObject.RegionData; }
      set { dataObject.RegionData = value; }
    }

    [DbAutoColumnName]
    public string RegionType
    {
      get { return dataObject.RegionType; }
      set { dataObject.RegionType = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.MasterArea dataObject = new DataObjects.MasterArea();

    #endregion
  }

  public class MasterAreas : List<MasterArea>
  {
    public DataObjects.MasterAreas DataObjects
    {
      get
      {
        DataObjects.MasterAreas objects = new DataObjects.MasterAreas();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}