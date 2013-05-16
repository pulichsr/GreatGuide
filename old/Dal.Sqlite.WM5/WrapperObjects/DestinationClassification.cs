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
  [DbSelectViewName("VW_DESTINATION_CLASSIFICATION")]
  public class DestinationClassification
  {
    public DestinationClassification()
    {
    }
    public DestinationClassification(DataObjects.DestinationClassification destinationClassification)
    {
      this.dataObject = destinationClassification;
    }

    public DataObjects.DestinationClassification DataObject
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
    public Int32? DestinationTypeId
    {
      get { return dataObject.DestinationTypeId; }
      set { dataObject.DestinationTypeId = value; }
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
    public string Code
    {
      get { return dataObject.Code; }
      set { dataObject.Code = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.DestinationClassification dataObject = new DataObjects.DestinationClassification();

    #endregion
  }

  public class DestinationClassifications : List<DestinationClassification>
  {
    public DataObjects.DestinationClassifications DataObjects
    {
      get
      {
        DataObjects.DestinationClassifications objects = new DataObjects.DestinationClassifications();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}