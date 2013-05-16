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
  [DbSelectViewName("VW_CONTROL_DEFINITION")]
  public class ControlDefinition
  {
    public ControlDefinition()
    {
    }
    public ControlDefinition(DataObjects.ControlDefinition controlDefinition)
    {
      this.dataObject = controlDefinition;
    }

    public DataObjects.ControlDefinition DataObject
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
    public Int32? FormId
    {
      get { return dataObject.FormId; }
      set { dataObject.FormId = value; }
    }

		[DbAutoColumnName]
    public string Name
    {
      get { return dataObject.Name; }
      set { dataObject.Name = value; }
    }

		[DbAutoColumnName]
    public string Text
    {
      get { return dataObject.Text; }
      set { dataObject.Text = value; }
    }

		[DbAutoColumnName]
    public string Description
    {
      get { return dataObject.Description; }
      set { dataObject.Description = value; }
    }

    [DbAutoColumnName]
    public string GraphicResourceName
    {
      get { return dataObject.GraphicResourceName; }
      set { dataObject.GraphicResourceName = value; }
    }

		[DbAutoColumnName]
    public string Colour
    {
      get { return dataObject.Colour; }
      set { dataObject.Colour = value; }
    }

		[DbAutoColumnName]
    public string Action
    {
      get { return dataObject.Action; }
      set { dataObject.Action = value; }
    }

    [DbAutoColumnName]
    public string ActionTarget
    {
      get { return dataObject.ActionTarget; }
      set { dataObject.ActionTarget = value; }
    }

    [DbAutoColumnName]
    public Int32? MasterAreaId
    {
      get { return dataObject.MasterAreaId; }
      set { dataObject.MasterAreaId = value; }
    }

    [DbAutoColumnName]
    public string ActionData
    {
      get { return dataObject.ActionData; }
      set { dataObject.ActionData = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.ControlDefinition dataObject = new DataObjects.ControlDefinition();

    #endregion
  }

  public class ControlDefinitions : List<ControlDefinition>
  {
    public DataObjects.ControlDefinitions DataObjects
    {
      get
      {
        DataObjects.ControlDefinitions objects = new DataObjects.ControlDefinitions();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}