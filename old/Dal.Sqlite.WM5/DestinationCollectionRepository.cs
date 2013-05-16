using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class DestinationCollectionRepository : ObjectDal<WrapperObjects.DestinationCollection, WrapperObjects.DestinationCollections>, IDestinationCollectionRepository
  {
    public DestinationCollectionRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IDestinationCollectionRepository
    public void Insert(DestinationCollectionDataset.DestinationCollectionRow row)
    {
      WrapperObjects.DestinationCollection wrapperObject = new WrapperObjects.DestinationCollection(DestinationCollectionConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(DestinationCollectionDataset.DestinationCollectionDataTable table)
    {
      foreach (DestinationCollectionDataset.DestinationCollectionRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public DestinationCollectionDataset.DestinationCollectionDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<DestinationCollectionDataset, DestinationCollectionDataset.DestinationCollectionDataTable>(Connector, sql);

    }
    public DestinationCollectionDataset.DestinationCollectionRow GetByCode(string code)
    {
      IDbCommand command = Connector.CreateCommand();
      command.CommandText = string.Format("select * from {0} where CODE = {1}", DbMetadata.SelectViewName, Connector.FormatParameterName("CODE"));
      AddCommandParameter(command,"CODE",code);

      DestinationCollectionDataset.DestinationCollectionDataTable table = GetHelper.GetTable<DestinationCollectionDataset, DestinationCollectionDataset.DestinationCollectionDataTable>(Connector, command);
      command.Dispose();

      if (table.Rows.Count == 0)
        return null;

      DestinationCollectionDataset.DestinationCollectionRow row = table[0];

      return row;
    }

    #endregion
  }
}