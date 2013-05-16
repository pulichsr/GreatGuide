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
  public class SearchRegionRepository:
    ISearchRegionRepository
  {
    public SearchRegionRepository(IDbConnector connector, ILogger logger)
    {
      Guard.ArgumentNotNull(connector, "connector");
      Guard.ArgumentNotNull(logger, "logger");

      this.connector = connector;
      this.logger = logger;

      dal = new ObjectDal<SearchRegion, SearchRegions>(connector, new SearchRegionRecordReader());
      fromParameterName = "FROM_SEARCH_KEY";
      toParameterName = "TO_SEARCH_KEY";
      whereClauseBuilder = new SearchKeyWhereClause("SR.SEARCH_KEY", connector.FormatParameterName(fromParameterName), connector.FormatParameterName(toParameterName));
    }

    public Region GetById(Int32 id)
    {
      IDbCommand command = connector.CreateCommand();

      StringBuilder sql = new StringBuilder();
      sql.Append("select * from SEARCH_REGION SR ");
      sql.Append(string.Format("  where ID = {0}",connector.FormatParameterName("ID")));
      dal.AddCommandParameter(command, "ID", id);        

      command.CommandText = sql.ToString();

      SearchRegions searchRegions = dal.Get(command);
      command.Dispose();

      if (searchRegions.Count == 0)
        return null;

      RegionFactory factory = new RegionFactory();
      return factory.Create(searchRegions[0]);
    }
    public Regions GetMatching(string searchCriteria, Int16 maxRows)
    {
      if (maxRows < 1)
        throw new ArgumentException("Invalid maxRows");

      IDbCommand command = connector.CreateCommand();

      StringBuilder sql = new StringBuilder();
      sql.Append("SELECT ");
      sql.Append("  SR.ID,");
      sql.Append("  SR.PARENT_ID, ");
      sql.Append("  SR.SEARCH_KEY, ");
      sql.Append("  SR.NAME, ");
      sql.Append("  SR.COLLATED_NAME, ");
      sql.Append("  SR.LEVEL, ");
      sql.Append("  SR.LATITUDE,");
      sql.Append("  SR.LONGITUDE ");
      sql.Append("  from SEARCH_REGION SR ");

      if (string.IsNullOrEmpty(searchCriteria) == false)
      {
        string fromParameterValue;
        string toParameterValue;
        string whereClause = whereClauseBuilder.Build(searchCriteria, out fromParameterValue, out toParameterValue);

        sql.Append(whereClause);
        dal.AddCommandParameter(command, fromParameterName, fromParameterValue);
        dal.AddCommandParameter(command, toParameterName, toParameterValue);
      }

      sql.Append(string.Format(" LIMIT {0}", maxRows));
      command.CommandText = sql.ToString();

      SearchRegions searchRegions = dal.Get(command);
      command.Dispose();

      Regions regions = new Regions();
      RegionFactory factory = new RegionFactory();
      for (Int32 i = 0; i < searchRegions.Count; i++)
        regions.Add(factory.Create(searchRegions[i]));

      return regions;
    }


    private readonly IDbConnector connector;
    private readonly ILogger logger;
    private readonly ObjectDal<SearchRegion, SearchRegions> dal;
    private readonly SearchKeyWhereClause whereClauseBuilder;
    private readonly string fromParameterName;
    private readonly string toParameterName;
  }
}