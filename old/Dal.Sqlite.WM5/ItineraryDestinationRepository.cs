using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class ItineraryDestinationRepository: 
    ObjectDal<WrapperObjects.ItineraryDestination, WrapperObjects.ItineraryDestinations>, 
    IItineraryDestinationRepository
  {
    public ItineraryDestinationRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IItineraryDestinationRepository
    public void Insert(ItineraryDestinationDataset.ItineraryDestinationRow row)
    {
      WrapperObjects.ItineraryDestination wrapperObject = new WrapperObjects.ItineraryDestination(ItineraryDestinationConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(ItineraryDestinationDataset.ItineraryDestinationDataTable table)
    {
      foreach (ItineraryDestinationDataset.ItineraryDestinationRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public ItineraryDestinationDataset.ItineraryDestinationDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<ItineraryDestinationDataset, ItineraryDestinationDataset.ItineraryDestinationDataTable>(Connector, sql);
    }
    #endregion
  }
}