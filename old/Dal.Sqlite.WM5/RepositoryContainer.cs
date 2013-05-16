using System;
using System.Collections.Generic;
using Nucleo;
using Nucleo.Data;
using Nucleo.Data.Sqlite;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class RepositoryContainer: IRepository, IRepositoryLocator
  {
    #region IRepository Members

    public void Initialise(string databaseFilename,ILogger logger)
    {
      Guard.ArgumentNotNull(logger, "logger");

      this.logger = logger;

      databaseFilename = Nucleo.Path.FileInExecutablePath(databaseFilename);
      IDbConnectionString connectionString = new SqliteDefaultConnectionString(databaseFilename, null);

      logger.Write(this,string.Format("ConnectionString: {0}",connectionString.ToString()));

      try
      {
        logger.Write(this, "Creating connector");
        connector = new SqliteDbConnector(connectionString.ToString());
      }
      catch (Exception exc)
      {
        logger.Write(this, "Error creating connector",exc);
      }
      if (connector == null)
      {
        throw new InvalidOperationException("Connector not created");
      }

      logger.Write(this, "Opening connector");
      try
      {
        connector.Open();
      }
      catch (Exception exc)
      {
        logger.Write(this,"Error opening connector",exc);
      }

      logger.Write(this, "Creating repositories");
      CreateRepositories();
    }
    public void Finalise()
    {
      connector.Close();
    }

    #endregion

    public T LocateRepository<T>()
    {
      object repository;
      Boolean got = repositories.TryGetValue(typeof(T),out repository);
      if (got == false)
        return default(T);

      return (T)repository;
    }

    private void CreateRepositories()
    {
      repositories[typeof(IChannelContentRepository)] = new ChannelContentDal(connector);
      repositories[typeof(IChannelRepository)] = new ChannelDal(connector);
      repositories[typeof(IChannelGroupRepository)] = new ChannelGroupDal(connector);
      repositories[typeof(INamedParameterRepository)] = new ConfigParameterDbDal(connector);
      repositories[typeof(IContentItemRepository)] = new ContentItemDal(connector);
      repositories[typeof(IControlDefinitionRepository)] = new ControlDefinitionRepository(connector);
      repositories[typeof(IDestinationClassificationRepository)] = new DestinationClassificationRepository(connector);
      repositories[typeof(IDestinationCollectionMemberRepository)] = new DestinationCollectionMemberRepository(connector);
      repositories[typeof(IDestinationCollectionRepository)] = new DestinationCollectionRepository(connector);
      repositories[typeof(IDestinationRepository)] = new DestinationRepository(connector);
      repositories[typeof(IDestinationTypeRepository)] = new DestinationTypeRepository(connector);
      repositories[typeof(IFormDefinitionRepository)] = new FormDefinitionRepository(connector);
      repositories[typeof(IGpsRegionRepository)] = new GpsRegionDal(connector);
      repositories[typeof(IItineraryDayRepository)] = new ItineraryDayRepository(connector,logger);
      repositories[typeof(IItineraryDestinationRepository)] = new ItineraryDestinationRepository(connector);
      repositories[typeof(IItineraryRepository)] = new ItineraryRepository(connector);
      repositories[typeof(IMasterAreaRepository)] = new MasterAreaDal(connector);
      repositories[typeof(IMySelectionRepository)] = new MySelectionRepository(connector);
      repositories[typeof(IRecentDestinationRepository)] = new RecentDestinationRepository(connector);
      repositories[typeof(IThemeRepository)] = new ThemeDal(connector);
      repositories[typeof(ISearchStreetRepository)] = new SearchStreetRepository(connector,logger);
      repositories[typeof(ISearchRegionRepository)] = new SearchRegionRepository(connector,logger);
    }

    private ILogger logger;
    private IDbConnector connector;
    private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();
  }
}