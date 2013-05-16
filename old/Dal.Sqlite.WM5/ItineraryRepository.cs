using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class ItineraryRepository: 
    ObjectDal<WrapperObjects.Itinerary, WrapperObjects.Itineraries>, 
    IItineraryRepository
  {
    public ItineraryRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IItineraryRepository Members
    public void Insert(ItineraryDataset.ItineraryRow row)
    {
      WrapperObjects.Itinerary wrapperObject = new WrapperObjects.Itinerary(ItineraryConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(ItineraryDataset.ItineraryDataTable table)
    {
      foreach (ItineraryDataset.ItineraryRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public ItineraryDataset.ItineraryDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<ItineraryDataset, ItineraryDataset.ItineraryDataTable>(Connector, sql);
    }
    public ItineraryDataset.ItineraryRow GetRow()
    {
      if (itinerary != null)
        return itinerary;

      ItineraryDataset.ItineraryDataTable table = Get();

      if (table.Rows.Count == 0)
        return null;

      itinerary = table[0];

      return itinerary;
    }
    #endregion


    private ItineraryDataset.ItineraryRow itinerary = null;
  }
}