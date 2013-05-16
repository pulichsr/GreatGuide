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
  [DbSelectViewName("VW_DESTINATION_TYPE")]
  public class DestinationType
  {
    public DestinationType()
    {
    }
    public DestinationType(DataObjects.DestinationType destinationType)
    {
      this.dataObject = destinationType;
    }

    public DataObjects.DestinationType DataObject
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
    public string Code
    {
      get { return dataObject.Code; }
      set { dataObject.Code = value; }
    }

    [DbAutoColumnName]
    public string Comment1Label
    {
      get { return dataObject.Comment1Label; }
      set { dataObject.Comment1Label = value; }
    }

    [DbAutoColumnName]
    public string Comment2Label
    {
      get { return dataObject.Comment2Label; }
      set { dataObject.Comment2Label = value; }
    }

    [DbAutoColumnName]
    public string Comment3Label
    {
      get { return dataObject.Comment3Label; }
      set { dataObject.Comment3Label = value; }
    }

    [DbAutoColumnName]
    public string Comment4Label
    {
      get { return dataObject.Comment4Label; }
      set { dataObject.Comment4Label = value; }
    }

    [DbAutoColumnName]
    public string IconResourceName
    {
      get { return dataObject.IconResourceName; }
      set { dataObject.IconResourceName = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.DestinationType dataObject = new DataObjects.DestinationType();

    #endregion
  }

  public class DestinationTypes : List<DestinationType>
  {
    public DataObjects.DestinationTypes DataObjects
    {
      get
      {
        DataObjects.DestinationTypes objects = new DataObjects.DestinationTypes();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}