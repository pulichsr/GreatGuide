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
  [DbSelectViewName("VW_DESTINATION_COLLECTION")]
  public class DestinationCollection
  {
    public DestinationCollection()
    {
    }
    public DestinationCollection(DataObjects.DestinationCollection destinationCollection)
    {
      this.dataObject = destinationCollection;
    }

    public DataObjects.DestinationCollection DataObject
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
    public string Code
    {
      get { return dataObject.Code; }
      set { dataObject.Code = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.DestinationCollection dataObject = new DataObjects.DestinationCollection();

    #endregion
  }

  public class DestinationCollections : List<DestinationCollection>
  {
    public DataObjects.DestinationCollections DataObjects
    {
      get
      {
        DataObjects.DestinationCollections objects = new DataObjects.DestinationCollections();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}