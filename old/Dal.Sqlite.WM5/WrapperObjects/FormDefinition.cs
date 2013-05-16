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
  [DbSelectViewName("VW_FORM_DEFINITION")]
  public class FormDefinition
  {
    public FormDefinition()
    {
    }
    public FormDefinition(DataObjects.FormDefinition formDefinition)
    {
      this.dataObject = formDefinition;
    }

    public DataObjects.FormDefinition DataObject
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
    public string FormTypeName
    {
      get { return dataObject.FormTypeName; }
      set { dataObject.FormTypeName = value; }
    }

		[DbColumnName("'TEXT'")]
    public string Text
    {
      get { return dataObject.Text; }
      set { dataObject.Text = value; }
    }

    [DbAutoColumnName]
    public string GraphicResourceName
    {
      get { return dataObject.GraphicResourceName; }
      set { dataObject.GraphicResourceName = value; }
    }

    [DbAutoColumnName]
    public Int32? MasterAreaId
    {
      get { return dataObject.MasterAreaId; }
      set { dataObject.MasterAreaId = value; }
    }

    [DbAutoColumnName]
    public string AdName
    {
      get { return dataObject.AdName; }
      set { dataObject.AdName = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.FormDefinition dataObject = new DataObjects.FormDefinition();

    #endregion
  }

  public class FormDefinitions : List<FormDefinition>
  {
    public DataObjects.FormDefinitions DataObjects
    {
      get
      {
        DataObjects.FormDefinitions objects = new DataObjects.FormDefinitions();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}