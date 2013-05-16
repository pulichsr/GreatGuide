using System;
using System.Data;
using System.Text;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.Factories;
using Nucleo.GoodGuide.Dal.Sqlite.RecordReaders;
using Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class SearchStreetRepository :
    ISearchStreetRepository
  {
    public SearchStreetRepository(IDbConnector connector,ILogger logger)
    {
      Guard.ArgumentNotNull(connector, "connector");
      Guard.ArgumentNotNull(logger, "logger");

      this.connector = connector;
      this.logger = logger;

      dal = new ObjectDal<SearchStreet,SearchStreets>(connector,new SearchStreetRecordReader());

      fromParameterName = "FROM_SEARCH_KEY";
      toParameterName = "TO_SEARCH_KEY";
      whereClauseBuilder = new SearchKeyWhereClause("SS.SEARCH_KEY",connector.FormatParameterName(fromParameterName),connector.FormatParameterName(toParameterName));
    }

    public Streets GetMatching(string searchCriteria,Int16 maxRows)
    {
      if (maxRows < 1)
        throw new ArgumentException("Invalid maxRows");

      IDbCommand command = connector.CreateCommand();

      StringBuilder sql = new StringBuilder();
      sql.Append("SELECT ");
      sql.Append("  SS.ID,");
      sql.Append("  SS.REGION_ID,");
      sql.Append("  SS.NAME as NAME,");
      sql.Append("  SR.COLLATED_NAME as REGION_COLLATED_NAME,");
      sql.Append("  SS.LATITUDE,");
      sql.Append("  SS.LONGITUDE, ");
      sql.Append("  SS.STREET_NUMBERS ");
      sql.Append("  FROM SEARCH_STREET SS ");
      sql.Append("  join SEARCH_REGION SR on SR.ID = SS.REGION_ID ");

      if (string.IsNullOrEmpty(searchCriteria) == false)
      {
        string fromParameterValue;
        string toParameterValue;
        string whereClause = whereClauseBuilder.Build(searchCriteria,out fromParameterValue,out toParameterValue);

        sql.Append(whereClause);
        dal.AddCommandParameter(command,fromParameterName,fromParameterValue);
        dal.AddCommandParameter(command, toParameterName, toParameterValue);        
      }

      sql.Append(string.Format(" LIMIT {0}",maxRows));
      command.CommandText = sql.ToString();

      SearchStreets searchStreets = dal.Get(command);
      command.Dispose();

      Streets streets = new Streets();
      StreetFactory factory = new StreetFactory();
      for (Int32 i = 0; i < searchStreets.Count;i++)
        streets.Add(factory.Create(searchStreets[i]));

      return streets;
    }
    public Streets GetMatching(Region region,string searchCriteria,Int16 maxRows)
    {
      if (maxRows < 1)
        throw new ArgumentException("Invalid maxRows");

      IDbCommand command = connector.CreateCommand();

      StringBuilder sql = new StringBuilder();
      sql.Append("SELECT ");
      sql.Append("  SS.ID,");
      sql.Append("  SS.REGION_ID,");
      sql.Append("  SS.NAME as NAME,");
      sql.Append("  SR.COLLATED_NAME as REGION_COLLATED_NAME,");
      sql.Append("  SS.LATITUDE,");
      sql.Append("  SS.LONGITUDE, ");
      sql.Append("  SS.STREET_NUMBERS ");
      sql.Append("  FROM SEARCH_STREET SS ");
      sql.Append(string.Format("  join SEARCH_REGION_STREET SRS on SRS.STREET_ID = SS.ID and SRS.REGION_ID = {0} ",region.Id));
      sql.Append("  join SEARCH_REGION SR on SR.ID = SS.REGION_ID ");

      if (string.IsNullOrEmpty(searchCriteria) == false)
      {
        string fromParameterValue;
        string toParameterValue;
        string whereClause = whereClauseBuilder.Build(searchCriteria,out fromParameterValue,out toParameterValue);

        sql.Append(whereClause);
        dal.AddCommandParameter(command,fromParameterName,fromParameterValue);
        dal.AddCommandParameter(command, toParameterName, toParameterValue);        
      }

      sql.Append(string.Format(" LIMIT {0}",maxRows));
      logger.Write(this,string.Format("SQL:",sql.ToString()));

      command.CommandText = sql.ToString();

      SearchStreets searchStreets = dal.Get(command);
      command.Dispose();

      Streets streets = new Streets();
      StreetFactory factory = new StreetFactory();
      for (Int32 i = 0; i < searchStreets.Count;i++)
        streets.Add(factory.Create(searchStreets[i]));

      return streets;
    }

    private readonly IDbConnector connector;
    private readonly ILogger logger;
    private readonly ObjectDal<SearchStreet,SearchStreets> dal;
    private readonly SearchKeyWhereClause whereClauseBuilder;
    private readonly string fromParameterName;
    private readonly string toParameterName;
  }
}