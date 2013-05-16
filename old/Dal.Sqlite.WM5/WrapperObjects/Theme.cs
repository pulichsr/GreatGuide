using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_THEME")]
  public class Theme
  {
    public Theme()
    {
    }
    public Theme(DataObjects.Theme theme)
    {
      this.dataObject = theme;
    }

    public DataObjects.Theme DataObject
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

    #endregion

    #region Fields

    private readonly DataObjects.Theme dataObject = new DataObjects.Theme();

    #endregion

  }

  public class Themes : List<Theme>
  {
    public DataObjects.Themes DataObjects
    {
      get
      {
        DataObjects.Themes objects = new DataObjects.Themes();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}