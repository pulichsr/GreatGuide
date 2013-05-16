using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_MY_SELECTION")]
  public class MySelection
  {
    public MySelection()
    {
    }
    public MySelection(DataObjects.MySelection mySelection)
    {
      this.dataObject = mySelection;
    }

    public DataObjects.MySelection DataObject
    {
      get { return dataObject; }
    }

    #region Properties

    [DbAutoColumnName]
    [DbPrimaryKey]
    public Int32? DestinationId
    {
      get { return dataObject.DestinationId; }
      set { dataObject.DestinationId = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.MySelection dataObject = new DataObjects.MySelection();

    #endregion    
  }

  public class MySelections : List<MySelection>
  {
    public Boolean ContainsId(Int32 destinationId)
    {
      return FindById(destinationId) != null;
    }

    public MySelection FindById(Int32 destinationId)
    {
      for (Int32 destinationNo = 0; destinationNo < this.Count; destinationNo++)
        if (this[destinationNo].DestinationId == destinationId)
          return this[destinationNo];

      return null;
    }

    public DataObjects.MySelections DataObjects
    {
      get
      {
        DataObjects.MySelections objects = new DataObjects.MySelections();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}