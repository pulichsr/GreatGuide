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
  public class MySelectionRepository: 
    ObjectDal<WrapperObjects.MySelection, WrapperObjects.MySelections>, 
    IMySelectionRepository
  {
    public MySelectionRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IMySelectionRepository
    public void Insert(MySelectionDataset.MySelectionRow row)
    {
      WrapperObjects.MySelection wrapperObject = new WrapperObjects.MySelection(MySelectionConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(MySelectionDataset.MySelectionDataTable table)
    {
      foreach (MySelectionDataset.MySelectionRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }
    public void Delete(Int32 destinationId)
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0} where DESTINATION_ID = {1}", this.DbMetadata.TableName, destinationId));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public Boolean IsInMySelection(int destinationId)
    {
      IDbCommand command = Connector.CreateCommand();
      command.CommandText = string.Format("select * from {0} where DESTINATIONID = {1}", DbMetadata.SelectViewName,destinationId);

      DestinationTypeDataset.DestinationTypeDataTable table = GetHelper.GetTable<DestinationTypeDataset, DestinationTypeDataset.DestinationTypeDataTable>(Connector, command);
      command.Dispose();

      return table.Rows.Count != 0;
    }

    public MySelectionDataset.MySelectionDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<MySelectionDataset, MySelectionDataset.MySelectionDataTable>(Connector, sql);
    }

    #endregion
  }
}