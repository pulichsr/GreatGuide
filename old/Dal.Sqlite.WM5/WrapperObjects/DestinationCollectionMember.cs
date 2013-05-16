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
  [DbSelectViewName("VW_DESTINATION_COLLECTION_MEMBER")]
  public class DestinationCollectionMember
  {
    public DestinationCollectionMember()
    {
    }
    public DestinationCollectionMember(DataObjects.DestinationCollectionMember destinationCollectionMember)
    {
      this.dataObject = destinationCollectionMember;
    }

    public DataObjects.DestinationCollectionMember DataObject
    {
      get { return dataObject; }
    }

    #region Properties

    [DbAutoColumnName]
    public Int32? CollectionId
    {
      get { return dataObject.CollectionId; }
      set { dataObject.CollectionId = value; }
    }

    [DbAutoColumnName]
    public Int32? DestinationId
    {
      get { return dataObject.DestinationId; }
      set { dataObject.DestinationId = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.DestinationCollectionMember dataObject = new DataObjects.DestinationCollectionMember();

    #endregion
  }

  public class DestinationCollectionMembers : List<DestinationCollectionMember>
  {
    public DataObjects.DestinationCollectionMembers DataObjects
    {
      get
      {
        DataObjects.DestinationCollectionMembers objects = new DataObjects.DestinationCollectionMembers();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}