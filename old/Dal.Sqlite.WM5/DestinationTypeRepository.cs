using System;
using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class DestinationTypeRepository: 
    ObjectDal<WrapperObjects.DestinationType, WrapperObjects.DestinationTypes>,
    IDestinationTypeRepository
  {
    public DestinationTypeRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IDestinationTypeRepository 
    public void Insert(DestinationTypeDataset.DestinationTypeRow row)
    {
      WrapperObjects.DestinationType wrapperObject = new WrapperObjects.DestinationType(DestinationTypeConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(DestinationTypeDataset.DestinationTypeDataTable table)
    {
      foreach (DestinationTypeDataset.DestinationTypeRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public DestinationTypeDataset.DestinationTypeDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<DestinationTypeDataset, DestinationTypeDataset.DestinationTypeDataTable>(Connector, sql.ToString());
    }
    public DestinationTypeDataset.DestinationTypeRow GetById(Int32 id)
    {
      string sql = string.Format("select * from {0} where ID = {1}", DbMetadata.SelectViewName, id);

      DestinationTypeDataset.DestinationTypeDataTable table = GetHelper.GetTable<DestinationTypeDataset, DestinationTypeDataset.DestinationTypeDataTable>(Connector, sql);

      if (table.Rows.Count == 0)
        return null;

      DestinationTypeDataset.DestinationTypeRow row = table[0];

      return row;
    }
    public DestinationTypeDataset.DestinationTypeRow GetByCode(string code)
    {
      IDbCommand command = Connector.CreateCommand();
      command.CommandText = string.Format("select * from {0} where CODE = {1}", DbMetadata.SelectViewName, Connector.FormatParameterName("CODE"));
      AddCommandParameter(command,"CODE",code);

      DestinationTypeDataset.DestinationTypeDataTable table = GetHelper.GetTable<DestinationTypeDataset, DestinationTypeDataset.DestinationTypeDataTable>(Connector, command);
      command.Dispose();

      if (table.Rows.Count == 0)
        return null;

      DestinationTypeDataset.DestinationTypeRow row = table[0];
      
      return row;
    }
    #endregion

    public void Insert(DataObjects.DestinationType objectToInsert)
    {
      WrapperObjects.DestinationType wrapperObject = new WrapperObjects.DestinationType(objectToInsert);

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(DataObjects.DestinationTypes objectsToInsert)
    {
      for (Int32 objectNo = 0;objectNo < objectsToInsert.Count;objectNo++)
        Insert(objectsToInsert[objectNo]);
    }
  }
}