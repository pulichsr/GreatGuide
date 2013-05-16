using System;
using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Types.DataObjects;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class ConfigParameterDbDal : ObjectDal<WrapperObjects.ConfigParameter, WrapperObjects.ConfigParameters>, INamedParameterRepository
  {
    public ConfigParameterDbDal(IDbConnector connector) : base(connector)
    {
      repositoryHelper = new NamedRepositoryHelper(SetByName, GetByName);
    }

    public void SetInt32(string paramName, Int32 paramValue)
    {
      repositoryHelper.SetInt32(paramName, paramValue);
    }
    public void SetInt16(string paramName, Int16 paramValue)
    {
      repositoryHelper.SetInt16(paramName, paramValue);
    }
    public void SetBoolean(string paramName, Boolean paramValue)
    {
      repositoryHelper.SetBoolean(paramName, paramValue);
    }
    public void SetString(string paramName, string paramValue)
    {
      repositoryHelper.SetString(paramName, paramValue);
    }
    public void SetSingle(string paramName, Single paramValue)
    {
      repositoryHelper.SetSingle(paramName, paramValue);
    }
    public void SetDateTime(string paramName, DateTime paramValue)
    {
      repositoryHelper.SetDateTime(paramName, paramValue);
    }

    public Int32? GetInt32(string paramName)
    {
      return repositoryHelper.GetInt32(paramName);
    }
    public Int16? GetInt16(string paramName)
    {
      return repositoryHelper.GetInt16(paramName);
    }
    public Boolean? GetBoolean(string paramName)
    {
      return repositoryHelper.GetBoolean(paramName);
    }
    public string GetString(string paramName)
    {
      return repositoryHelper.GetString(paramName);
    }
    public Single? GetSingle(string paramName)
    {
      return repositoryHelper.GetSingle(paramName);
    }
    public DateTime? GetDateTime(string paramName)
    {
      return repositoryHelper.GetDateTime(paramName);
    }

    private string GetByName(string paramName)
    {
      ConfigParameter parameter = Get(paramName);

      return parameter == null ? null : parameter.ParamValue;
    }
    private void SetByName(string paramName, string paramValue)
    {
      DataObjects.ConfigParameter parameter = Get(paramName);
      if (parameter == null)
      {
        WrapperObjects.ConfigParameter wrapperObject = new WrapperObjects.ConfigParameter();
        wrapperObject.ParamName = paramName;
        wrapperObject.ParamValue = paramValue;

        Insert(wrapperObject);
      }
      else
      {
        WrapperObjects.ConfigParameter wrapperObject = new WrapperObjects.ConfigParameter(parameter);
        parameter.ParamValue = paramValue;
        Update(wrapperObject);
      }
    }

    private new DataObjects.ConfigParameter Get(string paramName)
    {
      IDbCommand Command = Connector.CreateCommand(string.Format("select PARAM_NAME,PARAM_VALUE from CONFIG_PARAMETER where PARAM_NAME = {0}", Connector.FormatParameterName("ParamName")));
      AddCommandParameter(Command, "ParamName", typeof(string), paramName);

      WrapperObjects.ConfigParameters list = Get(Command, 1);

      return list.Count == 0 ? null : list[0].DataObject;
    }

    private readonly NamedRepositoryHelper repositoryHelper = null;

  }
}