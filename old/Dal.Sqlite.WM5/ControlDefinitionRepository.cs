using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class ControlDefinitionRepository: 
    ObjectDal<WrapperObjects.ControlDefinition, WrapperObjects.ControlDefinitions>, 
    IControlDefinitionRepository
  {
    public ControlDefinitionRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IControlDefinitionRepository Members
    public void Insert(ControlDefinitionDataset.ControlDefinitionRow row)
    {
      WrapperObjects.ControlDefinition wrapperObject = new WrapperObjects.ControlDefinition(ControlDefinitionConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(ControlDefinitionDataset.ControlDefinitionDataTable table)
    {
      foreach (ControlDefinitionDataset.ControlDefinitionRow row in table.Rows)
        Insert(row);
    }


    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public ControlDefinitionDataset.ControlDefinitionDataTable Get()
    {
      string sql = string.Format("select * from {0}", DbMetadata.SelectViewName);

      return GetHelper.GetTable<ControlDefinitionDataset, ControlDefinitionDataset.ControlDefinitionDataTable>(Connector, sql);
    }
    public ControlDefinitionDataset.ControlDefinitionDataTable GetByFormId(int formId)
    {
      string sql = string.Format("select * from {0} where FORMID = {1}", DbMetadata.SelectViewName, formId);

      ControlDefinitionDataset.ControlDefinitionDataTable table = GetHelper.GetTable<ControlDefinitionDataset, ControlDefinitionDataset.ControlDefinitionDataTable>(Connector, sql);

      return table;
    }
    public ControlDefinitionDataset.ControlDefinitionDataTable GetByMasterArea(int masterAreaId)
    {
      string sql = string.Format("select * from {0} where MASTERAREAID = {1}", DbMetadata.SelectViewName, masterAreaId);

      ControlDefinitionDataset.ControlDefinitionDataTable table = GetHelper.GetTable<ControlDefinitionDataset, ControlDefinitionDataset.ControlDefinitionDataTable>(Connector, sql);

      return table;
    }


    #endregion
  }
}