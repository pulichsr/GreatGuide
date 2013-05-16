using System.Data;
using System.Text;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class RecentDestinationRepository:
    ObjectDal<WrapperObjects.RecentDestination, WrapperObjects.RecentDestinations>,
    IRecentDestinationRepository
  {
    public RecentDestinationRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IRecentDestinationRepository
    public void Insert(RecentDestinationDataset.RecentDestinationRow row)
    {
      WrapperObjects.RecentDestination wrapperObject = new WrapperObjects.RecentDestination(RecentDestinationConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(RecentDestinationDataset.RecentDestinationDataTable table)
    {
      foreach (RecentDestinationDataset.RecentDestinationRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public RecentDestinationDataset.RecentDestinationDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<RecentDestinationDataset, RecentDestinationDataset.RecentDestinationDataTable>(Connector, sql);
    }

    public bool IsDestinationIdRecent(int destinationId)
    {
      IDbCommand command = Connector.CreateCommand();
      command.CommandText = string.Format("select * from {0} where DESTINATIONID = {1}", DbMetadata.SelectViewName, destinationId);

      DestinationTypeDataset.DestinationTypeDataTable table = GetHelper.GetTable<DestinationTypeDataset, DestinationTypeDataset.DestinationTypeDataTable>(Connector, command);
      command.Dispose();

      return table.Rows.Count != 0;
    }

    public bool IsDestinationNameRecent(string destinationName)
    {
      StringBuilder Sql = new StringBuilder();
      Sql.Append(string.Format("select * from {0}",DbMetadata.SelectViewName));
      Sql.Append(string.Format(" where upper(NAME) = {0}", Connector.FormatParameterName("NAME")));

      IDbCommand command = Connector.CreateCommand(Sql.ToString());
      AddCommandParameter(command,"NAME",destinationName);

      DestinationTypeDataset.DestinationTypeDataTable table = GetHelper.GetTable<DestinationTypeDataset, DestinationTypeDataset.DestinationTypeDataTable>(Connector, command);
      command.Dispose();

      return table.Rows.Count != 0;
    }
    #endregion
  }
}