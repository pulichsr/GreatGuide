using System;
using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class FormDefinitionRepository: 
    ObjectDal<WrapperObjects.FormDefinition, WrapperObjects.FormDefinitions>, 
    IFormDefinitionRepository
  {
    public FormDefinitionRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IFormDefinitionRepository Members
    public void Insert(FormDefinitionDataset.FormDefinitionRow row)
    {
      WrapperObjects.FormDefinition wrapperObject = new WrapperObjects.FormDefinition(FormDefinitionConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(FormDefinitionDataset.FormDefinitionDataTable table)
    {
      foreach (FormDefinitionDataset.FormDefinitionRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public FormDefinitionDataset.FormDefinitionDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);
      return GetHelper.GetTable<FormDefinitionDataset, FormDefinitionDataset.FormDefinitionDataTable>(Connector, sql);
    }
    public FormDefinitionDataset.FormDefinitionDataTable GetByMasterArea(Int32 masterAreaId)
    {
      string sql = string.Format("select * from {0} where MASTERAREAID = {1}",DbMetadata.SelectViewName,masterAreaId);
      
      return GetHelper.GetTable<FormDefinitionDataset, FormDefinitionDataset.FormDefinitionDataTable>(Connector, sql);
    }
    public FormDefinitionDataset.FormDefinitionRow GetByName(Int32 masterAreaId, string name)
    {
      string sql = string.Format("select * from {0} where MASTERAREAID = {1} and NAME = {2}",DbMetadata.SelectViewName,masterAreaId,Connector.FormatParameterName("NAME"));
      IDbCommand cmd = Connector.CreateCommand(sql);
      AddCommandParameter(cmd,"NAME",name);
      
      FormDefinitionDataset.FormDefinitionDataTable table = GetHelper.GetTable<FormDefinitionDataset, FormDefinitionDataset.FormDefinitionDataTable>(Connector,cmd);
      cmd.Dispose();

      if (table.Rows.Count == 0)
        return null;

      return table[0];
    }



    #endregion
  }
}