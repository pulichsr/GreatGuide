using System;
using System.Data;
using System.Text;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Dal.Sqlite.Factories;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class DestinationRepository: 
    ObjectDal<WrapperObjects.Destination, WrapperObjects.Destinations>, 
    IDestinationRepository
  {
    public DestinationRepository(IDbConnector connector)
      : base(connector)
    {
      fromParameterName = "FROM_SEARCH_KEY";
      toParameterName = "TO_SEARCH_KEY";
      whereClauseBuilder = new SearchKeyWhereClause("SEARCH_KEY", connector.FormatParameterName(fromParameterName), connector.FormatParameterName(toParameterName));
    }

    #region IDestinationRepository 
    public void Insert(DestinationDataset.DestinationRow row)
    {
      WrapperObjects.Destination wrapperObject = new WrapperObjects.Destination(DestinationConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(DestinationDataset.DestinationDataTable table)
    {
      foreach (DestinationDataset.DestinationRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }
    public void DeleteById(int id)
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0} where ID = {1}", this.DbMetadata.TableName,id));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public DestinationDataset.DestinationDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql);
    }
    public DestinationDataset.DestinationRow GetById(int id)
    {
      string sql = string.Format("select * from {0} where ID = {1} ",DbMetadata.SelectViewName, id);

      DestinationDataset.DestinationDataTable table = GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql);
      if (table.Rows.Count == 0)
        return null;

      DestinationDataset.DestinationRow row = table[0];

      return row;
    }
    public DestinationDataset.DestinationDataTable GetAll(int masterAreaId)
    {
      string sql = string.Format("select * from {0} where MASTERAREAID = {1}", DbMetadata.SelectViewName, masterAreaId);

      return GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql);
    }
    public DestinationDataset.DestinationDataTable GetByClassification(int masterAreaId,int classificationId)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select DST.* ");
      sql.Append(string.Format("  from {0} DST ",DbMetadata.SelectViewName));
      sql.Append(string.Format("  where DST.MASTERAREAID = {0} ", masterAreaId));
      sql.Append(string.Format("  and DST.CLASSIFICATIONID = {0} ", classificationId));

      return GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql.ToString());
    }
    public DestinationDataset.DestinationDataTable GetByClassificationCollection(int masterAreaId,int classificationId, int collectionId)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select DST.* ");
      sql.Append(string.Format("  from {0} DST ", DbMetadata.SelectViewName));
      sql.Append("  join DESTINATION_COLLECTION_MEMBER DCM on DCM.DESTINATION_ID = DST.ID");
      sql.Append(string.Format("  where DST.MASTERAREAID = {0} ", masterAreaId));
      sql.Append(string.Format("  and DST.CLASSIFICATIONID = {0} ", classificationId));
      sql.Append(string.Format("  and DCM.COLLECTION_ID = {0} ", collectionId));

      return GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql.ToString());
    }
    public DestinationDataset.DestinationDataTable GetByCollection(int masterAreaId,int collectionId)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select DST.* ");
      sql.Append(string.Format("  from {0} DST ", DbMetadata.SelectViewName));
      sql.Append("  join DESTINATION_COLLECTION_MEMBER DCM on DCM.DESTINATION_ID = DST.ID ");
      sql.Append(string.Format("  where DST.MASTERAREAID = {0} ", masterAreaId));
      sql.Append(string.Format("  and DCM.COLLECTION_ID = {0} ", collectionId));

      return GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql.ToString());
    }
    public DestinationDataset.DestinationDataTable GetByType(int masterAreaId,int typeId)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select DST.* ");
      sql.Append(string.Format("  from {0} DST ",DbMetadata.SelectViewName));
      sql.Append(string.Format("  where DST.MASTERAREAID = {0} ", masterAreaId));
      sql.Append(string.Format("  and DST.DESTINATIONTYPEID = {0} ", typeId));

      return GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql.ToString());
    }
    public DestinationDataset.DestinationDataTable GetByTypeCollection(int masterAreaId,int typeId, int collectionId)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select DST.* ");
      sql.Append(string.Format("  from {0} DST ", DbMetadata.SelectViewName));
      sql.Append("  join DESTINATION_COLLECTION_MEMBER DCM on DCM.DESTINATION_ID = DST.ID");
      sql.Append(string.Format("  where DST.MASTERAREAID = {0} ", masterAreaId));
      sql.Append(string.Format("  and DST.DESTINATIONTYPEID = {0} ", typeId));
      sql.Append(string.Format("  and DCM.COLLECTION_ID = {0} ", collectionId));

      return GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql.ToString());
    }

    public DestinationDataset.DestinationDataTable GetMySelection()
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select DST.* ");
      sql.Append(string.Format("  from {0} DST ", DbMetadata.SelectViewName));
      sql.Append("  join MY_SELECTION MSEL on MSEL.DESTINATION_ID = DST.ID ");

      return GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql.ToString());
    }
    public DestinationDataset.DestinationDataTable GetByItineraryDayId(int id)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select DST.* ");
      sql.Append(string.Format("  from {0} DST ", DbMetadata.SelectViewName));
      sql.Append("  join ITINERARY_DESTINATION IDST on IDST.DESTINATION_ID = DST.ID ");
      sql.Append(string.Format("  where IDST.ITINERARY_DAY_ID = {0} ", id));
      sql.Append("  order by IDST.DESTINATION_ORDER ");

      return GetHelper.GetTable<DestinationDataset, DestinationDataset.DestinationDataTable>(Connector, sql.ToString());
    }

    public DataObjects.Destinations GetMatching(string searchCriteria, Int16 maxRows)
    {
      if (maxRows < 1)
        throw new ArgumentException("Invalid maxRows");

      IDbCommand command = Connector.CreateCommand();

      StringBuilder sql = new StringBuilder();
      sql.Append("SELECT DST.*,DSTT.DESCRIPTION as DESTINATION_TYPE_DESCRIPTION ");
      sql.Append("  from DESTINATION DST ");
      sql.Append("  left outer join DESTINATION_TYPE DSTT on DSTT.ID = DST.DESTINATION_TYPE_ID ");

      if (string.IsNullOrEmpty(searchCriteria) == false)
      {
        string fromParameterValue;
        string toParameterValue;
        string whereClause = whereClauseBuilder.Build(searchCriteria, out fromParameterValue, out toParameterValue);

        sql.Append(whereClause);
        AddCommandParameter(command, fromParameterName, fromParameterValue);
        AddCommandParameter(command, toParameterName, toParameterValue);
      }

      sql.Append(string.Format(" LIMIT {0}", maxRows));
      command.CommandText = sql.ToString();

      WrapperObjects.Destinations data = Get(command);
      command.Dispose();

      DataObjects.Destinations destinations = new DataObjects.Destinations();
      DestinationFactory factory = new DestinationFactory();
      for (Int32 i = 0; i < data.Count; i++)
        destinations.Add(factory.Create(data[i]));

      return destinations;
    }

    #endregion

    private readonly SearchKeyWhereClause whereClauseBuilder;
    private readonly string fromParameterName;
    private readonly string toParameterName;

  }
}