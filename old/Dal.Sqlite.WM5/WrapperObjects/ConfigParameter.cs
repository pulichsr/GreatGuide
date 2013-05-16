using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [Serializable]
  [DbAutoTableName]
  public class ConfigParameter
  {
    public ConfigParameter()
    {
    }
    public ConfigParameter(DataObjects.ConfigParameter configParameter)
    {
      if (configParameter == null)
        this.dataObject = new DataObjects.ConfigParameter();
      else
        this.dataObject = configParameter;
    }

    public DataObjects.ConfigParameter DataObject
    {
      get { return dataObject; }
    }

    #region Properties

    [DbAutoColumnName]
    [DbPrimaryKey]
    public string ParamName
    {
      get { return dataObject.ParamName; }
      set { dataObject.ParamName = value; }
    }

    [DbAutoColumnName]
    public string ParamValue
    {
      get { return dataObject.ParamValue; }
      set { dataObject.ParamValue = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.ConfigParameter dataObject = new DataObjects.ConfigParameter();

    #endregion

  }

  [Serializable]
  public class ConfigParameters : List<ConfigParameter>
  {
    public DataObjects.ConfigParameters DataObjects
    {
      get
      {
        DataObjects.ConfigParameters objects = new DataObjects.ConfigParameters();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}