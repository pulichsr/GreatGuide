using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_RECENT_DESTINATION")]
  public class RecentDestination
  {
    public RecentDestination()
    {
    }
    public RecentDestination(DataObjects.RecentDestination recentDestination)
    {
      this.dataObject = recentDestination;
    }

    public DataObjects.RecentDestination DataObject
    {
      get { return dataObject; }
    }

    #region Properties

    [DbAutoColumnName]
    public DateTime? VisitedDat
    {
      get { return dataObject.VisitedDat; }
      set { dataObject.VisitedDat = value; }
    }

    [DbAutoColumnName]
    public Int32? DestinationId
    {
      get { return dataObject.DestinationId; }
      set { dataObject.DestinationId = value; }
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
    public string Name
    {
      get { return dataObject.Name; }
      set { dataObject.Name = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.RecentDestination dataObject = new DataObjects.RecentDestination();

    #endregion    
  }

  public class RecentDestinations : List<RecentDestination>
  { 
    public DataObjects.RecentDestinations DataObjects
    {
      get
      {
        DataObjects.RecentDestinations objects = new DataObjects.RecentDestinations();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}