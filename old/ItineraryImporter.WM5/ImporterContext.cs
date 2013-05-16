using System;
using System.Collections.Generic;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Reflection;

namespace Nucleo.GoodGuide.ItineraryImporter
{
  public class ImporterContext
  {
    public ImporterContext()
    {
      #region Create logger
      logger = LoggerFactory.Create();
      #endregion

      #region Create DAL locator
      List<IRepository> dals = Loader.LoadFromPath<IRepository>(Path.ExecutablePath, "dal");
      if (dals.Count == 0)
        throw new ApplicationException("IRepository not found");

      IRepository repository = dals[0];
      if (repository is IRepositoryLocator == false)
        throw new ApplicationException("IRepositoryLocator not found");

      repositoryLocator = repository as IRepositoryLocator;
      if (repositoryLocator == null)
        throw new ApplicationException("IRepositoryLocator not found");

      repository.Initialise(Logger);
      #endregion

      #region Create BLL
      gpsRegionBll = new GpsRegionBll(repositoryLocator.LocateRepository<IGpsRegionRepository>());
      channelContentBll = new ChannelContentBll(repositoryLocator.LocateRepository<IChannelContentRepository>());
      contentItemBll = new ContentItemBll(repositoryLocator.LocateRepository<IContentItemRepository>());
      themeBll = new ThemeBll(repositoryLocator.LocateRepository<IThemeRepository>());
      masterAreaBll = new MasterAreaBll(repositoryLocator.LocateRepository<IMasterAreaRepository>());
      channelBll = new ChannelBll(repositoryLocator.LocateRepository<IChannelRepository>());
      channelGroupBll = new ChannelGroupBll(repositoryLocator.LocateRepository<IChannelGroupRepository>());
      controlDefinitionBll = new ControlDefinitionBll(repositoryLocator.LocateRepository<IControlDefinitionRepository>());
      formDefinitionBll = new FormDefinitionBll(repositoryLocator.LocateRepository<IFormDefinitionRepository>(), repositoryLocator.LocateRepository<IControlDefinitionRepository>());
      destinationTypeBll = new DestinationTypeBll(repositoryLocator.LocateRepository<IDestinationTypeRepository>());
      destinationClassificationBll = new DestinationClassificationBll(repositoryLocator.LocateRepository<IDestinationClassificationRepository>());
      itineraryBll = new ItineraryBll(repositoryLocator.LocateRepository<IItineraryRepository>());
      itineraryDayBll = new ItineraryDayBll(repositoryLocator.LocateRepository<IItineraryDayRepository>(), Logger);
      itineraryDestinationBll = new ItineraryDestinationBll(repositoryLocator.LocateRepository<IItineraryDestinationRepository>());
      destinationBll = new DestinationBll(
        repositoryLocator.LocateRepository<IDestinationRepository>(),
        repositoryLocator.LocateRepository<IDestinationTypeRepository>(),
        repositoryLocator.LocateRepository<IDestinationClassificationRepository>(),
        repositoryLocator.LocateRepository<IDestinationCollectionRepository>(), itineraryDayBll);
      destinationCollectionBll = new DestinationCollectionBll(repositoryLocator.LocateRepository<IDestinationCollectionRepository>());
      destinationCollectionMemberBll = new DestinationCollectionMemberBll(repositoryLocator.LocateRepository<IDestinationCollectionMemberRepository>());
      destinationUpdateBll = new DestinationUpdateBll(repositoryLocator.LocateRepository<IDestinationRepository>(), destinationBll);
      #endregion

      #region SyncBll
      contentSyncBll = new SyncBll();
      ContentSyncBll.SyncTargets.Add(channelBll);
      ContentSyncBll.SyncTargets.Add(channelContentBll);
      ContentSyncBll.SyncTargets.Add(channelGroupBll);
      ContentSyncBll.SyncTargets.Add(contentItemBll);
      ContentSyncBll.SyncTargets.Add(gpsRegionBll);
      ContentSyncBll.SyncTargets.Add(masterAreaBll);
      ContentSyncBll.SyncTargets.Add(themeBll);

      destinationSyncBll = new SyncBll();
      DestinationSyncBll.SyncTargets.Add(destinationBll);
      DestinationSyncBll.SyncTargets.Add(destinationClassificationBll);
      DestinationSyncBll.SyncTargets.Add(destinationCollectionBll);
      DestinationSyncBll.SyncTargets.Add(destinationCollectionMemberBll);
      DestinationSyncBll.SyncTargets.Add(destinationTypeBll);

      itinerarySyncBll = new SyncBll();
      ItinerarySyncBll.SyncTargets.Add(ItineraryBll);
      ItinerarySyncBll.SyncTargets.Add(itineraryDayBll);
      ItinerarySyncBll.SyncTargets.Add(itineraryDestinationBll);
      ItinerarySyncBll.SyncTargets.Add(destinationUpdateBll);

      formDefinitionSyncBll = new SyncBll();
      FormDefinitionSyncBll.SyncTargets.Add(controlDefinitionBll);
      FormDefinitionSyncBll.SyncTargets.Add(formDefinitionBll);
      #endregion
    }

    #region Properties
    public ILogger Logger
    {
      get { return logger; }
    }

    public ItineraryBll ItineraryBll
    {
      get { return itineraryBll; }
    }
    
    public SyncBll ContentSyncBll
    {
      get { return contentSyncBll; }
    }
    public SyncBll DestinationSyncBll
    {
      get { return destinationSyncBll; }
    }
    public SyncBll FormDefinitionSyncBll
    {
      get { return formDefinitionSyncBll; }
    }
    public SyncBll ItinerarySyncBll
    {
      get { return itinerarySyncBll; }
    }
    #endregion

    #region Fields
    private readonly ILogger logger;
    private readonly IRepositoryLocator repositoryLocator;
    private readonly GpsRegionBll gpsRegionBll;
    private readonly ChannelContentBll channelContentBll;
    private readonly ContentItemBll contentItemBll;
    private readonly ThemeBll themeBll;
    private readonly MasterAreaBll masterAreaBll;
    private readonly ChannelBll channelBll;
    private readonly ChannelGroupBll channelGroupBll;
    private readonly ControlDefinitionBll controlDefinitionBll;
    private readonly FormDefinitionBll formDefinitionBll;
    private readonly DestinationBll destinationBll;
    private readonly DestinationTypeBll destinationTypeBll;
    private readonly DestinationClassificationBll destinationClassificationBll;
    private readonly ItineraryBll itineraryBll;
    private readonly ItineraryDayBll itineraryDayBll;
    private readonly ItineraryDestinationBll itineraryDestinationBll;
    private readonly DestinationUpdateBll destinationUpdateBll;
    private readonly DestinationCollectionBll destinationCollectionBll;
    private readonly DestinationCollectionMemberBll destinationCollectionMemberBll;

    private readonly SyncBll contentSyncBll;
    private readonly SyncBll destinationSyncBll;
    private readonly SyncBll formDefinitionSyncBll;
    private readonly SyncBll itinerarySyncBll;
    #endregion
  }
}
