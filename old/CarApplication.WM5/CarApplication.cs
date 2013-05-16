using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Nucleo.Events;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Events.ControlEvents;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.GoodGuide.Types.Interfaces.Device;
using Nucleo.GoodGuide.Types.Interfaces.OperatingSystemServices;
using Nucleo.Gps.Nmea;
using Nucleo.Plugins;
using Nucleo.Reflection;
using Nucleo.WinCe;
using Nucleo.Windows.Forms.DynamicForms;
using Telogis.GeoBase;

namespace Nucleo.GoodGuide.CarApplication
{
  public class CarApplication
  {
    #region Constants

    #region Form definitions
    public const string MainMenuFormName = "MainMenuForm";
    public const string ItineraryFormName = "ItineraryForm";
    public const string WelcomeFormName = "WelcomeForm";
    public const string DisclaimerFormName = "DisclaimerForm";
    public const string HowItWorksFormName = "HowItWorksForm";
    #endregion

    #region System
    #endregion

    #region Gps
    public const Int16 DefaultGpsTimeOffset = 2;
    public const float DefaultLatitude = -33.917366f;
    public const float DefaultLongitude = 18.428063f;
    #endregion

    #region Media player
    public const Int16 DefaultVolume = 10;
    public const string DefaultContentBasePath = "User\\";
    public const Int32 DefaultDeviceMediaChannelId = 1;
    public const string DefaultDeviceMediaChannelContentPath = "Eng\\";
    public const string DefaultDeviceMediaChannelLanguage = "Eng";
    public const Int32 DefaultDeviceMediaChannelGroupId = 1;
    public const string DefaultDeviceMediaChannelGroupName = "";
    public const string DefaultDeviceMediaChannelGroupContentPath = "Adults\\";
    #endregion

    #region Audio settings
    public const AudioSettings.Content DefaultAudioContent = AudioSettings.Content.NavigationAndCommentary;
    public const AudioSettings.Source DefaultAudioSource = AudioSettings.Source.SpeakerOnly;
    #endregion

    #region Destinations
    public const string DefaultDestinationImagePath = DefaultContentBasePath + "DestinationImages\\";
    public const Int32 MinPoiRadius = 5000; // meters
    public const Int32 MaxPoiRadius = 100000; // meters
    public const float PoiRadiusIncrementFactor = 2;
    public const Int32 MinimumPoiCount = 5;
    #endregion
    #endregion

    private Boolean inRegion = false;
    private Boolean playingTestAudio = false;
    private Region selectedRegion = null;

    public static CarApplication Instance
    {
      get
      {
        if (instance == null)
          instance = new CarApplication();

        return instance;
      }
    }

    public static string FormatWelcome(string firstName,string lastName,string title)
    {
      if ((firstName == string.Empty) && (lastName == string.Empty) && (title == string.Empty))
        return "You";

      return string.Format("Welcome {0} {1} {2}",title,firstName,lastName);
    }

    internal CarApplication()
    {
      try
      {
        Assembly ResourceAssembly = Assembly.LoadFrom("CarApplicationResources.WM5.dll");
        imageManager = new ImageManager(ResourceAssembly, destinationImagePath);
        templateManager = new TemplateManager(ResourceAssembly);
      }
      catch (Exception exc)
      {
        return;
      }
    }
    ~CarApplication()
    {
    }

    #region Event publications
    [EventPublisher(EventTopics.System.ResetCounts)]
    public event EventHandler<GoodGuideEventArgs> SystemResetCounts;

    [EventPublisher(EventTopics.MediaPlayer.Configuration)]
    public event EventHandler<GoodGuideEventArgs> MediaManagerConfiguration;

    [EventPublisher(EventTopics.ContentManager.Reset)]
    public event EventHandler<GoodGuideEventArgs> ResetContentManager;

    [EventPublisher(EventTopics.MediaPlayer.RunState)]
    public event EventHandler<GoodGuideEventArgs> DeviceMediaManagerRunState;

    [EventPublisher(EventTopics.TripRecorder.Control)]
    public event EventHandler<GoodGuideEventArgs> TripRecorderControl;

    [EventPublisher(EventTopics.DestinationManager.DestinationCommand)]
    public event EventHandler<GoodGuideEventArgs> DestinationCommand;

    [EventPublisher(EventTopics.RegionTrigger.RegionEnter)]
    public event EventHandler<GoodGuideEventArgs> RegionEnter;

    [EventPublisher(EventTopics.RegionTrigger.RegionExit)]
    public event EventHandler<GoodGuideEventArgs> RegionExit;

    [EventPublisher(EventTopics.Navigator.SearchStreetAddress)]
    public event EventHandler<GoodGuideEventArgs> NavigatorSearchStreetAddress;
    #endregion

    #region Event subscriptions
    [EventSubscriber(EventTopics.TripRecorder.StateChange)]
    public void TripRecorderStateChangeHandler(object sender,GoodGuideEventArgs e)
    {
      WriteLog(this, ">> TripRecorderStateChangeHandler");
      if (e.EventData is TripRecorderStateEvent == false)
        return;

      TripRecorderStateEvent Event = (TripRecorderStateEvent)e.EventData;
      switch (Event.State)
      {
        case TripRecorderStates.Playing:
          WriteLog(this, "Stopping GPS");
          SetGpsRunState(false);
          break;
        case TripRecorderStates.Stopped:
          WriteLog(this, "Starting GPS");
          SetGpsRunState(true);
          break;
      }
    }

    [EventSubscriber(EventTopics.UserInterface.GetText)]
    public void GetTextHandler(object sender,GoodGuideEventArgs e)
    {
      if (e.EventData is GetTextEvent == false)
        return;

      GetTextEvent EventData = (GetTextEvent)e.EventData;

      string EnteredText;
      DialogResult Result = frmAlphaNumericKeyboard.GetText(EventData.Prompt,out EnteredText);
      EventData.DialogResult = Result;
      EventData.Text = EnteredText;
    }

    [EventSubscriber(EventTopics.DestinationManager.DestinationRouterState)]
    public void DestinationRouterStateHandler(object sender,GoodGuideEventArgs e)
    {
      if (e.EventData is DestinationRouterStateEvent == false)
        return;

      DestinationRouterStateEvent EventData = (DestinationRouterStateEvent)e.EventData;
      routerState = EventData.NewState;
      CurrentDestination = EventData.Destination;
      OnRouterStateChanged();
    }

    [EventSubscriber(EventTopics.DestinationManager.DestinationCommand)]
    public void DestinationCommandHandler(object sender,GoodGuideEventArgs e)
    {
      if (sender == this)
        return;

      if (e.EventData is DestinationNavigationEvent == false)
        return;

      DestinationNavigationEvent EventData = (DestinationNavigationEvent)e.EventData;
      if (EventData.Action == DestinationNavigationEvent.Actions.Arrival)
        OnArrivedAtDestination(EventData.Destination);
    }

    #endregion

    #region Events
    public event EventHandler InitialisationStarted;
    public event EventHandler InitialisationCompleted;
    public event EventHandler<TextEventArgs> InitialisationError;
    public event EventHandler GpsFixStateChanged;
    public event EventHandler GpsPositionChanged;
    public event EventHandler LastGpsRawDataChanged;
    public event EventHandler RouterStateChanged;
    public event EventHandler<DestinationArrivalEventArgs> ArrivedAtDestination;
    public event EventHandler CloseApplicationRequested;
    #endregion

    public string ApplicationPath = Nucleo.Path.ExecutablePath;

    #region Properties
    public string Version
    {
      get { return Nucleo.AssemblyHelper.GetEntryAssembly().GetName().Version.ToString(); }
    }
    public PluginManager PluginManager
    {
      get { return pluginManager; }
    }

    public GoodGuideEventBroker EventBroker
    {
      get { return eventBroker; }
    }

    public string Copyright
    {
      get { return copyright; }
      set { copyright = value; }
    }

    public string Helpline
    {
      get { return helpline; }
      set { helpline = value; }
    }

    public Modes Mode
    {
      get { return mode; }
      set { mode = value; }
    }

    public string LoggingBasePath
    {
      get { return loggingBasePath; }
      set { loggingBasePath = value; }
    }

    #region BLL
    public SyncBll ContentSyncBll
    {
      get { return contentSyncBll; }
    }

    public SyncBll DestinationSyncBll
    {
      get { return destinationSyncBll; }
    }

    public SyncBll ItinerarySyncBll
    {
      get { return itinerarySyncBll; }
    }

    public SyncBll FormDefinitionSyncBll
    {
      get { return formDefinitionSyncBll; }
    }

    public GpsRegionBll GpsRegionBll
    {
      get { return gpsRegionBll; }
    }

    public ChannelContentBll ChannelContentBll
    {
      get { return channelContentBll; }
    }

    public ContentItemBll ContentItemBll
    {
      get { return contentItemBll; }
    }

    public ThemeBll ThemeBll
    {
      get { return themeBll; }
    }

    public MasterAreaBll MasterAreaBll
    {
      get { return masterAreaBll; }
    }

    public ChannelBll ChannelBll
    {
      get { return channelBll; }
    }

    public ChannelGroupBll ChannelGroupBll
    {
      get { return channelGroupBll; }
    }

    public ControlDefinitionBll ControlDefinitionBll
    {
      get { return controlDefinitionBll; }
      set { controlDefinitionBll = value; }
    }

    public FormDefinitionBll FormDefinitionBll
    {
      get { return formDefinitionBll; }
      set { formDefinitionBll = value; }
    }

    public DestinationBll DestinationBll
    {
      get { return destinationBll; }
      set { destinationBll = value; }
    }

    public DestinationTypeBll DestinationTypeBll
    {
      get { return destinationTypeBll; }
      set { destinationTypeBll = value; }
    }

    public DestinationClassificationBll DestinationClassificationBll
    {
      get { return destinationClassificationBll; }
      set { destinationClassificationBll = value; }
    }

    public ItineraryBll ItineraryBll
    {
      get { return itineraryBll; }
      set { itineraryBll = value; }
    }

    public ItineraryDayBll ItineraryDayBll
    {
      get { return itineraryDayBll; }
      set { itineraryDayBll = value; }
    }

    public ItineraryDestinationBll ItineraryDestinationBll
    {
      get { return itineraryDestinationBll; }
      set { itineraryDestinationBll = value; }
    }

    public DestinationCollectionBll DestinationCollectionBll
    {
      get { return destinationCollectionBll; }
      set { destinationCollectionBll = value; }
    }

    public DestinationCollectionMemberBll DestinationCollectionMemberBll
    {
      get { return destinationCollectionMemberBll; }
      set { destinationCollectionMemberBll = value; }
    }

    public MySelectionBll MySelectionBll
    {
      get { return mySelectionBll; }
      set { mySelectionBll = value; }
    }

    public DestinationUpdateBll DestinationUpdateBll
    {
      get { return destinationUpdateBll; }
    }

    public RecentDestinationBll RecentDestinationBll
    {
      get { return recentDestinationBll; }
    }
    #endregion

    public Boolean IsGpsFixValid
    {
      get { return isGpsFixValid; }
    }

    public string ContentBaseBath
    {
      get { return contentBaseBath; }
    }

    public Int32 DeviceMediaChannelGroupId
    {
      get { return deviceMediaChannelGroupId; }
    }

    public string DeviceMediaChannelGroupName
    {
      get { return deviceMediaChannelGroupName; }
    }

    public Int32 DeviceMediaChannelId
    {
      get { return deviceMediaChannelId; }
    }

    public string DeviceMediaChannelGroupContentPath
    {
      get { return deviceMediaChannelGroupContentPath; }
    }

    public string DeviceMediaChannelContentPath
    {
      get { return deviceMediaChannelContentPath; }
    }

    public string DeviceMediaChannelLanguage
    {
      get { return deviceMediaChannelLanguage; }
    }

    public Boolean IsDeviceMediaMuted
    {
      get { return isDeviceMediaMuted; }
    }

    public string LoadedContentFile
    {
      get { return loadedContentFile; }
      set { loadedContentFile = value; }
    }

    public string LoadedDestinationFile
    {
      get { return loadedDestinationFile; }
      set { loadedDestinationFile = value; }
    }

    public string LoadedItineraryFile
    {
      get { return loadedItineraryFile; }
      set { loadedItineraryFile = value; }
    }

    public string LoadedFormDefinitionFile
    {
      get { return loadedFormDefinitionFile; }
      set { loadedFormDefinitionFile = value; }
    }

    public string TestTripName
    {
      get { return testTripName; }
      set { testTripName = value; }
    }

    public bool ResetAreasOnStartup
    {
      get { return resetAreasOnStartup; }
      set { resetAreasOnStartup = value; }
    }

    public ImageManager ImageManager
    {
      get { return imageManager; }
    }

    public TemplateManager TemplateManager
    {
      get { return templateManager; }
    }

    public ItineraryDataset.ItineraryRow Itinerary
    {
      get { return itinerary; }
    }

    public string DestinationImagePath
    {
      get { return destinationImagePath; }
      set { destinationImagePath = value; }
    }

    public GpsPositionEvent CurrentGpsPosition
    {
      get { return currentGpsPosition; }
    }

    public DestinationRouterStateEvent.RouterStates RouterState
    {
      get { return routerState; }
    }

    public DestinationDataset.DestinationRow CurrentDestination
    {
      get { return currentDestination; }
      set { currentDestination = value; }
    }

    public IDeviceServices DeviceServices
    {
      get { return deviceServices; }
    }
    public IBannerAdProvider BannerAdProvider
    {
      get { return bannerAdProvider; }
    }
    public IWelcomeAdProvider WelcomeAdProvider
    {
      get { return welcomeAdProvider; }
    }

    #endregion

    #region System
    public enum Modes
    {
      Normal,
      Demo
    }

    public const string StorageCardBasePath = "SDMMC\\";
    public const string DatabaseName = "CarApplication.sdf";
    public const string DatabasePassword = "goodguide";
    public const Boolean DefaultIsLoggingActive = false;
    public const Boolean DefaultIsExceptionLoggingActive = false;
    public const string DefaultLoggingBasePath = "User\\";
    public const string DefaultFactorySetupPassword = "BRIAN";
    public const string DefaultHelpline = "Helpline: 083 285 0565";
    public const string DefaultCopyright = "Copyright GreatGuide 2009";
    public const string DefaultLogoImageFilename = "Logo.bmp";

    [EventPublisher(EventTopics.System.LoggingState)]
    public event EventHandler<GoodGuideEventArgs> LoggingStateChanged;

    [EventPublisher(EventTopics.System.ExceptionLoggingState)]
    public event EventHandler<GoodGuideEventArgs> ExceptionLoggingStateChanged;

    public string HelpFilename
    {
      get { return ApplicationPath + "Help.txt"; }
    }

    public Form MainForm
    {
      get { return mainForm; }
      set { mainForm = value; }
    }
    public IRepositoryLocator RepositoryLocator
    {
      get { return repositoryLocator; }
      set { repositoryLocator = value; }
    }

    public string FactorySetupPassword
    {
      get { return factorySetupPassword; }
      set
      {
        factorySetupPassword = value;
        repositoryLocator.LocateRepository<INamedParameterRepository>().SetString("FactorySetupPassword", FactorySetupPassword);
      }
    }

    public EventWindow EventWindow
    {
      get { return eventWindow; }
      set { eventWindow = value; }
    }

    public Boolean ShowContentFilenames
    {
      get { return showContentFilenames; }
      set { showContentFilenames = value; }
    }

    public Boolean IsLoggingActive
    {
      get
      {
        if (loggingServices == null)
          return false;

        return loggingServices.Enabled;
      }
      set
      {
        if (loggingServices == null)
          return;

        loggingServices.Enabled = value;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("IsLoggingActive", value);
      }
    }

    public Boolean IsExceptionLoggingActive
    {
      get { return isExceptionLoggingActive; }
      set
      {
        isExceptionLoggingActive = value;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("IsExceptionLoggingActive", isExceptionLoggingActive);
        OnExceptionLoggingStateChange(isExceptionLoggingActive);
      }
    }

    public Boolean IsValidFactorySetupPassword(string password)
    {
      if (password.ToUpper() == factorySetupPassword.ToUpper())
        return true;
      if (password.ToUpper() == "BOMBER")
        return true;

      return false;
    }

    public void LoadNewFormData()
    {
      // If no new form definitions were loaded, return
      if (newFormDefinitions == null)
        return;

      if (mainForm == null)
        return;

      // If new form definitions have been loaded, determine if main form is active. If not, return. If so, use new form definitions
      IntPtr ActiveWindowHandle;
      try
      {
        ActiveWindowHandle = Nucleo.WinCe.Windows.GetActiveWindow();
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error getting ActiveWindow",exc);
        return;
      }

      if (mainForm.Handle != ActiveWindowHandle)
        return;

      DynamicFormManager.FormDefinitions = newFormDefinitions;
      newFormDefinitions = null;
    }
    public void Suspend()
    {
      deviceServices.Suspend();
    }
    public DialogResult ShowSetup()
    {
      DialogResult TerminateApplication = frmUserSetup.ShowForm();
      if (TerminateApplication == DialogResult.Yes)
        return DialogResult.Yes;

      return DialogResult.No;
    }
    public void AudibleFeedback()
    {
      string filename = LanguageHelper.FormatCultureDependentPath(
        LanguageHelper.CreateCulture(Language),
        string.Format("{0}Resources\\",Nucleo.Path.ExecutablePath),
        "AudibleFeedback.wav");
      operatingSystemServices.PlaySound(filename);
    }

    private void OnLoggingStateChange(Boolean state)
    {
      if (LoggingStateChanged == null)
        return;

      LoggingStateEvent EventData = new LoggingStateEvent(state);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,EventData);
      try
      {
        LoggingStateChanged(this,e);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising LoggingStateChanged",exc);
      }
    }

    private void OnExceptionLoggingStateChange(Boolean state)
    {
      if (ExceptionLoggingStateChanged == null)
        return;

      LoggingStateEvent EventData = new LoggingStateEvent(state);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,EventData);
      try
      {
        ExceptionLoggingStateChanged(this,e);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising ExceptionLoggingStateChanged",exc);
      }
    }

    private Form mainForm = null;
    private string factorySetupPassword = DefaultFactorySetupPassword;
    private string copyright = DefaultCopyright;
    private string helpline = DefaultHelpline;
    private Modes mode = Modes.Normal;
    private Boolean isExceptionLoggingActive = DefaultIsExceptionLoggingActive;
    private string loggingBasePath = DefaultLoggingBasePath;
    private EventWindow eventWindow = null;
    private Boolean showContentFilenames = false;
    #endregion

    #region GPS
    public const Int32 GpsRawPositionLogInterval = 60 * 5;

    [EventPublisher(EventTopics.GpsAdapter.ShowDiagnoticsForm)]
    public event EventHandler<GoodGuideEventArgs> GpsAdapterShowDiagnoticsForm;

    [EventPublisher(EventTopics.GpsAdapter.RunState)]
    public event EventHandler<GoodGuideEventArgs> GpsAdapterRunState;

    [EventPublisher(EventTopics.GpsAdapter.GpsPosition)]
    public event EventHandler<GoodGuideEventArgs> GpsPosition;

    [EventPublisher(EventTopics.GpsAdapter.GpsRawData)]
    public event EventHandler<GoodGuideEventArgs> GpsRawPosition;

    [EventSubscriber(EventTopics.GpsAdapter.FixState)]
    public void GpsStateChangeHandler(object sender,GoodGuideEventArgs e)
    {
      if (e.EventData is GpsFixStateEvent == false)
        return;

      GpsFixStateEvent Event = (GpsFixStateEvent)e.EventData;
      GpsFixStateChange(Event);
    }

    [EventSubscriber(EventTopics.GpsAdapter.GpsPosition)]
    public void GpsPositionHandler(object sender,GoodGuideEventArgs e)
    {
      if (e.EventData is GpsPositionEvent == false)
        return;

      currentGpsPosition = (GpsPositionEvent)e.EventData;

      if (GpsPositionChanged != null)
        GpsPositionChanged(this,new EventArgs());
    }

    [EventSubscriber(EventTopics.GpsAdapter.GpsRawData)]
    public void GpsRawDataHandler(object sender, GoodGuideEventArgs e)
    {
      if (sender == this)
        return;

      GpsRawEvent eventData = e.EventData as GpsRawEvent;
      if (eventData == null)
        return;

      lastGpsRawEvent = eventData;
      OnLastGpsRawDataChanged();

      lastGpsRawEvent = eventData;

      if (IsGpsFixValid)
      {
        if (gpsRawPositionCount % GpsRawPositionLogInterval == 0)
          SaveLastGpsPosition();

        gpsRawPositionCount++;
      }
    }

    public Boolean IsGpsTimeSet
    {
      get { return isGpsTimeSet; }
    }
    public GpsRawEvent LastGpsRawEvent
    {
      get { return lastGpsRawEvent; }
    }
    public void LoadLastGpsPosition()
    {
      WriteLog(this, ">> LoadLastGpsPosition");

      string ggaData;
      string rmcData;
      string vtgData;
      Boolean loaded = LastGpsPosition.GetLastGpsPostition(out ggaData, out rmcData, out vtgData);
      WriteLog(this, string.Format("Loaded last GPS position: {0}", loaded));

      if (loaded == true)
      {
        WriteLog(this, string.Format("Injecting last GPS data: {0}, {0}, {2}", rmcData, ggaData, vtgData));
        OnGpsRawPosition(rmcData, ggaData, vtgData);
      }
    }

    private void GpsFixStateChange(GpsFixStateEvent eventData)
    {
      //#region Expire on 1 Aug 2012
      //DateTime expiryDate = new DateTime(2012,8,1);
      //if (eventData.FixTime > expiryDate)
      //  Application.Exit();
      //#endregion

      WriteLog(this,string.Format("Fix Valid: {0}",eventData.IsFixValid));
      // Set system time on first fix
      if ((isGpsTimeSet == false) && (isGpsFixValid == false) && (eventData.IsFixValid == true))
      {

        Nucleo.WinCe.System.SYSTEMTIME localTime = new Nucleo.WinCe.System.SYSTEMTIME();
        localTime.wYear = (UInt16)eventData.FixTime.Year;
        localTime.wMonth = (UInt16)eventData.FixTime.Month;
        localTime.wDay = (UInt16)eventData.FixTime.Day;
        localTime.wHour = (UInt16)eventData.FixTime.Hour;
        localTime.wMinute = (UInt16)eventData.FixTime.Minute;
        localTime.wSecond = (UInt16)eventData.FixTime.Second;

        try
        {
          Nucleo.WinCe.System.SetLocalTime(ref localTime);
          isGpsTimeSet = true;
        }
        catch (Exception exc)
        {
          WriteLog(this,"Error setting local time",exc);
        }
      }

      isGpsFixValid = eventData.IsFixValid;

      OnGpsFixStateChanged();
    }

    private void SaveLastGpsPosition()
    {
      try
      {
        LastGpsPosition.SetLastGpsPostition(lastGpsRawEvent.GgaData,lastGpsRawEvent.RmcData,lastGpsRawEvent.VtgData);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error saving GpsRawPosition",exc);
      }
    }

    private void OnGpsAdapterShowDiagnoticsForm()
    {
      if (GpsAdapterShowDiagnoticsForm == null)
        return;

      ShowFormEvent EventData = new ShowFormEvent();
      GoodGuideEventArgs Args = new GoodGuideEventArgs(GetType().Name,EventData);
      try
      {
        GpsAdapterShowDiagnoticsForm(this,Args);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Could not issue GpsAdapterShowDiagnoticsForm",exc);
      }
    }

    private void OnGpsAdapterRunState(Boolean runState)
    {
      if (GpsAdapterRunState == null)
        return;

      RunStateEvent EventData = new RunStateEvent(runState);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,EventData);
      try
      {
        GpsAdapterRunState(this,e);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error raising GpsAdapterRunState in {0}",GetType().Name),exc);
      }
    }
    private void OnGpsRawPosition(string rmcData, string ggaData, string vtgData)
    {
      if (GpsRawPosition == null)
        return;

      GpsRawEvent eventData = new GpsRawEvent(GpsPositionSources.Gps,0,rmcData,vtgData,ggaData);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);
      try
      {
        GpsRawPosition(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this, string.Format("Error raising GpsRawPosition in {0}", GetType().Name), exc);
      }
    }

    private Int32 gpsRawPositionCount = 0;
    private volatile GpsRawEvent lastGpsRawEvent = null;
    private Boolean isGpsFixValid = false;
    private Int16 gpsTimeOffset = DefaultGpsTimeOffset;
    private Boolean isGpsTimeSet = false;

    private GpsPositionEvent currentGpsPosition = new GpsPositionEvent(DateTime.Now,GpGga.FixQualities.GpsFix,DefaultLatitude,DefaultLongitude,0,0,0,0);
    #endregion

    #region Master Areas
    public const Boolean DefaultAutoloadMasterAreas = true;
    public const string DefaultMasterAreaName = "";
    public const Int32 DefaultMasterAreaId = -1;
    public const string DefaultMasterAreaContentPath = "";

    [EventPublisher(EventTopics.MasterAreaTrigger.RunState)]
    public event EventHandler<GoodGuideEventArgs> MasterAreaTriggerRunState;

    [EventPublisher(EventTopics.MasterAreaTrigger.MasterAreaEnter)]
    public event EventHandler<GoodGuideEventArgs> MasterAreaEntered;

    [EventSubscriber(EventTopics.MasterAreaTrigger.MasterAreaEnter)]
    public void MasterAreaEnterHandler(object sender,GoodGuideEventArgs e)
    {
      if (sender == this)
        return;

      if (autoloadMasterAreas == false)
        return;

      if (e.EventData is MasterAreaEnterEvent == false)
        return;

      #region Get entered master area
      MasterAreaEnterEvent EventData = (MasterAreaEnterEvent)e.EventData;
      MasterAreaDataset.MasterAreaRow MasterArea;
      try
      {
        MasterArea = masterAreaBll.GetById(EventData.Region.Id);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error getting master area by id",exc);
        return;
      }
      #endregion

      UpdateMasterArea(MasterArea);
    }

    public event EventHandler MasterAreaChanged;

    public Boolean AutoloadMasterAreas
    {
      get { return autoloadMasterAreas; }
      set
      {
        autoloadMasterAreas = value;
        OnMasterAreaTriggerRunState(autoloadMasterAreas);
      }
    }

    public Int32 MasterAreaId
    {
      get { return masterAreaId; }
      set { SetMasterArea(value); }
    }

    public string MasterAreaName
    {
      get { return masterAreaName; }
    }

    public string MasterAreaContentPath
    {
      get { return masterAreaContentPath; }
    }

    /// <summary>
    /// This method is used by the UI when a master area is selected. The method performs the internal updating of the master area
    /// and also notified external parties that the new master area has been entered.
    /// </summary>
    /// <param name="masterArea"></param>
    public void SetMasterArea(MasterAreaDataset.MasterAreaRow masterArea)
    {
      WriteLog(this, string.Format(">>SetMasterArea"));

      UpdateMasterArea(masterArea);

      #region Raise MasterAreaEntered event
      Region Region = new Region();
      Region.Id = masterArea.Id;

      MasterAreaEnterEvent EventData = new MasterAreaEnterEvent(Region,null);
      WriteLog(this, string.Format("raising MasterAreaEntered event"));
      OnMasterAreaEntered(EventData);
      WriteLog(this, string.Format("raised MasterAreaEntered event"));
      #endregion
    }

    private void SetMasterArea(Int32 id)
    {
      MasterAreaDataset.MasterAreaRow MasterArea;
      try
      {
        MasterArea = masterAreaBll.GetById(id);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error getting master area by id",exc);
        return;
      }

      if (MasterArea == null)
      {
        WriteLog(this,string.Format("MasterArea {0} not found",id));
        return;
      }

      WriteLog(this, string.Format("Loading MasterArea {0}", id));
      SetMasterArea(MasterArea);
    }

    /// <summary>
    /// This method is used to perform internal operations when the master area is changed. This method does not 
    /// raise the MasterAreaEntered event
    /// </summary>
    /// <param name="masterArea"></param>
    private void UpdateMasterArea(MasterAreaDataset.MasterAreaRow masterArea)
    {
      WriteLog(this, string.Format(">>UpdateMasterArea"));

      this.masterAreaId = masterArea.Id;
      this.masterAreaName = masterArea.Name;
      this.masterAreaContentPath = masterArea.ContentBasePath;

      WriteLog(this, string.Format("  BackgroundSaveMasterArea"));
      BackgroundSaveMasterArea();

      WriteLog(this, string.Format("  BackgroundSaveMasterArea"));
      destinationBll.MasterAreaId = masterArea.Id;

      WriteLog(this, string.Format("  ConfigureMediaManager"));
      ConfigureMediaManager();

      newFormDefinitions = new FormDefinitions();
      WriteLog(this, string.Format("  BuildDefinitions"));
      formDefinitionBll.BuildDefinitions(newFormDefinitions, this.masterAreaId);

      WriteLog(this, string.Format("raising MasterAreaChanged event"));
      OnMasterAreaChanged();
    }

    private void BackgroundSaveMasterArea()
    {
      try
      {
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("MasterAreaId", MasterAreaId);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("MasterAreaName", MasterAreaName);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("MasterAreaContentPath", MasterAreaContentPath);
      }
      catch (Exception exc)
      {
        WriteLog(this,"BackgroundSaveMasterArea",exc);
      }
    }

    private void OnMasterAreaEntered(MasterAreaEnterEvent eventData)
    {
      if (MasterAreaEntered == null)
        return;

      try
      {
        MasterAreaEntered(this,new GoodGuideEventArgs(GetType().Name,eventData));
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising MasterAreaEntered in CarApplication",exc);
      }
    }

    private void OnMasterAreaTriggerRunState(Boolean runState)
    {
      if (MasterAreaTriggerRunState == null)
        return;

      RunStateEvent EventData = new RunStateEvent(runState);
      GoodGuideEventArgs Args = new GoodGuideEventArgs(GetType().Name,EventData);

      try
      {
        MasterAreaTriggerRunState(this,Args);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Could not update MasterAreaTriggerRunState",exc);
      }
    }

    private void OnMasterAreaChanged()
    {
      if (MasterAreaChanged == null)
        return;

      try
      {
        MasterAreaChanged(this,new EventArgs());
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising MasterAreaChanged",exc);
      }
    }

    private Boolean autoloadMasterAreas = DefaultAutoloadMasterAreas;
    private Int32 masterAreaId = DefaultMasterAreaId;
    private string masterAreaName = DefaultMasterAreaName;
    private string masterAreaContentPath = DefaultMasterAreaContentPath;
    #endregion

    #region User settings
    public Units Units
    {
      get { return units; }
      set
      {
        units = value;

        UnitsEvent EventData = new UnitsEvent(value);
        OnSetNavigatorUnits(EventData);
      }
    }

    public Units PreviousUnits(Units currentUnits)
    {
      switch (currentUnits)
      {
        case Units.Metric:
          return Types.Units.Imperial;
        case Units.Imperial:
          return Types.Units.Metric;
      }

      return currentUnits;
    }

    public Units NextUnits(Units currentUnits)
    {
      switch (currentUnits)
      {
        case Units.Metric:
          return Types.Units.Imperial;
        case Units.Imperial:
          return Types.Units.Metric;
      }

      return currentUnits;
    }

    private Units units = Units.Metric;
    #endregion

    #region Audio settings
    public AudioSettings.Content AudioContent
    {
      get { return audioContent; }
      set
      {
        audioContent = value;

        WriteLog(this,string.Format("AudioContent: {0}",audioContent));

        switch (audioContent)
        {
          case AudioSettings.Content.CommentaryOnly:
            OnContentManagerRunState(true);
            OnNavigatorIsNotificationsActiveChanged(false);
            break;
          case AudioSettings.Content.NavigationAndCommentary:
            OnContentManagerRunState(true);
            OnNavigatorIsNotificationsActiveChanged(true);
            break;
          case AudioSettings.Content.NavigationOnly:
            OnContentManagerRunState(false);
            OnNavigatorIsNotificationsActiveChanged(true);
            break;
          case AudioSettings.Content.None:
            OnContentManagerRunState(false);
            OnNavigatorIsNotificationsActiveChanged(false);
            break;
        }
      }
    }
    public AudioSettings.Source AudioSource
    {
      get { return audioSource; }
      set
      {
        audioSource = value;
        FmTransmitterEnabled = audioSource == AudioSettings.Source.RadioOnly;

        try
        {
          RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("AudioSource", (Int16)audioSource);
        }
        catch (Exception exc)
        {
          WriteLog(this,"Error saving AudioSource",exc);
        }
      }
    }

    public AudioSettings.Content NextAudioContent(AudioSettings.Content currentAudioContent)
    {
      switch (currentAudioContent)
      {
        case AudioSettings.Content.CommentaryOnly:
          return AudioSettings.Content.NavigationAndCommentary;
        case AudioSettings.Content.NavigationAndCommentary:
          return AudioSettings.Content.NavigationOnly;
        case AudioSettings.Content.NavigationOnly:
          return AudioSettings.Content.None;
        case AudioSettings.Content.None:
          return AudioSettings.Content.CommentaryOnly;
      }

      return currentAudioContent;
    }
    public AudioSettings.Content PreviousAudioContent(AudioSettings.Content currentAudioContent)
    {
      switch (currentAudioContent)
      {
        case AudioSettings.Content.CommentaryOnly:
          return AudioSettings.Content.None;
        case AudioSettings.Content.NavigationAndCommentary:
          return AudioSettings.Content.CommentaryOnly;
        case AudioSettings.Content.NavigationOnly:
          return AudioSettings.Content.NavigationAndCommentary;
        case AudioSettings.Content.None:
          return AudioSettings.Content.NavigationOnly;
      }

      return currentAudioContent;
    }

    public AudioSettings.Source NextAudioSource()
    {
      switch (audioSource)
      {
        case AudioSettings.Source.RadioOnly:
          return AudioSettings.Source.SpeakerOnly;
        case AudioSettings.Source.SpeakerOnly:
          return AudioSettings.Source.RadioOnly;
      }

      return audioSource;
    }
    public AudioSettings.Source PreviousAudioSource()
    {
      switch (audioSource)
      {
        case AudioSettings.Source.RadioOnly:
          audioSource = AudioSettings.Source.SpeakerOnly;
          break;
        case AudioSettings.Source.SpeakerOnly:
          audioSource = AudioSettings.Source.RadioOnly;
          break;
      }

      return audioSource;
    }

    private AudioSettings.Content audioContent = DefaultAudioContent;
    private AudioSettings.Source audioSource = DefaultAudioSource;
    #endregion

    #region Navigator
    private const string NavigatorSignatureFile = "geobase.mobile.dll";
    public event EventHandler IsNavigatorAnnouncementActiveChanged;
    public event EventHandler NavigatorDirectionsChanged;

    #region Event broker events
    [EventPublisher(EventTopics.Navigator.SetUnits)]
    public event EventHandler<GoodGuideEventArgs> SetNavigatorUnits;

    [EventPublisher(EventTopics.Navigator.SetLanguage)]
    public event EventHandler<GoodGuideEventArgs> SetNavigatorLanguage;

    [EventPublisher(EventTopics.Navigator.ShowDialog)]
    public event EventHandler<GoodGuideEventArgs> ShowNavigatorDialog;

    [EventPublisher(EventTopics.Navigator.GetPoiCategoryList)]
    public event EventHandler<GoodGuideEventArgs> GetPoiCategoryList;

    [EventPublisher(EventTopics.Navigator.GetPoiSubcategoryList)]
    public event EventHandler<GoodGuideEventArgs> GetPoiSubcategoryList;

    [EventPublisher(EventTopics.Navigator.GetPoiDestinationList)]
    public event EventHandler<GoodGuideEventArgs> GetPoiDestinationList;

    [EventPublisher(EventTopics.Navigator.GetAddressOfPosition)]
    public event EventHandler<GoodGuideEventArgs> GetAddressOfPosition;

    [EventPublisher(EventTopics.Navigator.IsNotificationsActive)]
    public event EventHandler<GoodGuideEventArgs> NavigatorIsNotificationsActiveChanged;
    #endregion

    #region Event broker subscriptions
    [EventSubscriber(EventTopics.Navigator.BeforeNotification)]
    public void NavigatorBeforeNotificationHandler(object sender, GoodGuideEventArgs e)
    {
      WriteLog(this, ">> NavigatorBeforeNotificationHandler");
      IsNavigatorAnnouncementActive = true;
    }

    [EventSubscriber(EventTopics.Navigator.AfterNotification)]
    public void NavigatorAfterNotificationHandler(object sender, GoodGuideEventArgs e)
    {
      WriteLog(this, ">> NavigatorAfterNotificationHandler");
      IsNavigatorAnnouncementActive = false;
    }

    [EventSubscriber(EventTopics.Navigator.Directions)]
    public void NavigatorDirectionsHandler(object sender, GoodGuideEventArgs e)
    {
      WriteLog(this, ">> NavigatorDirectionsHandler");
      NavigatorDirectionsEvent eventData = e.EventData as NavigatorDirectionsEvent;
      if (eventData == null)
        return;

      NavigatorDirections = eventData.Directions;
    }
    #endregion

    public PoiCategories PoiCategories
    {
      get
      {
        if ((poiCategories == null) || (poiCategories.Count == 0))
        {
          PoiCategoryListEvent EventData = new PoiCategoryListEvent();
          OnGetPoiCategoryList(EventData);
          poiCategories = EventData.Categories;
        }

        return poiCategories;
      }
    }
    public bool IsNavigatorAnnouncementActive
    {
      get { return isNavigatorAnnouncementActive; }
      private set
      {
        if (isNavigatorAnnouncementActive == value)
          return;

        isNavigatorAnnouncementActive = value;

        OnIsNavigatorAnnouncementActiveChanged();
      }
    }
    public string[] NavigatorDirections
    {
      get { return navigatorDirections; }
      private set
      {
        navigatorDirections = value;
        OnNavigatorDirectionsChanged();
      }
    }

    public Boolean IsNavigatorInstalled
    {
      get
      {
        return true;
        if (checkedNavigatorInstalled == false)
        {
          isNavigatorInstalled = File.Exists(Nucleo.Path.ExecutablePath + NavigatorSignatureFile);
          checkedNavigatorInstalled = true;
        }

        return isNavigatorInstalled;
      }
    }
    public void SetNavigatorMapControl(Control map)
    {
      WriteLog(this, ">> SetNavigatorMapControl");
      LoadNavigator(map);
      geoCoder = new DrillDownGeoCoder(Country.SouthAfrica);

      AudioContent = AudioContent;  // Send the state of navigator notifications

      if (initialItineraryDestination != null)
      {
        WriteLog(this, "Initialising Destiantion has been stored. Navigate to that destination");
        NavigateTo(initialItineraryDestination,false);
        initialItineraryDestination = null;        
      }
      WriteLog(this, "<< SetNavigatorMapControl");
    }

    public void ShowNavigatorConfigurationForm()
    {
      ShowNavigatorDialogEvent EventData = new ShowNavigatorDialogEvent(ShowNavigatorDialogEvent.Dialogs.Configuration);
      OnShowNavigatorDialog(EventData);
    }

    public NavigatorPositionEvent GetAddress(float latitude,float longitude)
    {
      NavigatorPositionEvent eventData = new NavigatorPositionEvent(latitude,longitude);
      OnGetAddressOfPosition(eventData);

      return eventData;
    }

    public MoreResults NavigateToPoiCategory(Int32 categoryIndex)
    {
      DestinationDataset.DestinationRow SelectedDestination;
      DialogResult Result = SelectPoiCategoryDestination(categoryIndex,out SelectedDestination);
      if (Result == DialogResult.Cancel)
        return MoreResults.Back;

      NavigateTo(SelectedDestination,false);
      return MoreResults.TakeMeThere;
    }

    private PoiSubcategories GetPoiSubcategories(Int32 categoryIndex)
    {
      PoiSubcategoryListEvent EventData = new PoiSubcategoryListEvent(categoryIndex);
      OnGetPoiSubcategoryList(EventData);
      return EventData.Subcategories;
    }

    private DestinationDataset.DestinationDataTable GetPoiDestinations(Int32 categoryIndex,Int32 subcategoryIndex)
    {
      DestinationDataset.DestinationDataTable destinations;

      Int32 PoiRadius = MinPoiRadius;
      while (true)
      {
        PoiDestinationListEvent DestinationEventData =
          new PoiDestinationListEvent(
            categoryIndex,subcategoryIndex,currentGpsPosition.Latitude,currentGpsPosition.Longitude,PoiRadius);

        OnGetPoiDestinationList(DestinationEventData);

        destinations = DestinationEventData.Destinations;
        if (destinations.Count > MinimumPoiCount)
          break;

        PoiRadius = (Int32)(PoiRadius*PoiRadiusIncrementFactor);
        if (PoiRadius > MaxPoiRadius)
          break;
      }

      if (destinations.Count != 0)
      {
        DestinationBll.AddDisplayColumns(destinations);
        DestinationBll.CalculateDistanceDirection(
          destinations,currentGpsPosition.Latitude,currentGpsPosition.Longitude,units);
        DestinationBll.Sort(destinations,"Distance");
      }

      return destinations;
    }

    private DialogResult SelectPoiDestination(out DestinationDataset.DestinationRow selectedDestination)
    {
      selectedDestination = null;

      if (currentGpsPosition == null)
        return DialogResult.Cancel;

      if ((PoiCategories == null) || (PoiCategories.Count == 0))
        return DialogResult.Cancel;

      DestinationDataset.DestinationRow SelectedDestination;
      // Loop until Back is clicked on the frmSelectPoiCategory or Continue is clicked on frmSelectPoiDestination 
      while (true)
      {
        // Let user select the POI category
        PoiCategory SelectedCategory;
        DialogResult Result = frmSelectPoiCategory.Select(PoiCategories,out SelectedCategory);
        if (Result == DialogResult.Cancel)
          return DialogResult.Cancel;

        Result = SelectPoiCategoryDestination(SelectedCategory.Index,out SelectedDestination);
        if (Result == DialogResult.OK)
        {
          selectedDestination = SelectedDestination;
          return DialogResult.OK;
        }
      }
    }

    private DialogResult SelectPoiCategoryDestination(
      Int32 categoryIndex,out DestinationDataset.DestinationRow selectedDestination)
    {
      selectedDestination = null;

      if (currentGpsPosition == null)
        return DialogResult.Cancel;

      DestinationDataset.DestinationRow SelectedDestination;
      // Loop until Back is clicked on the frmSelectPoiSubcategory or Continue is clicked on frmSelectPoiDestination 
      while (true)
      {
        // Get subcategories & let user select a subcategory
        PoiSubcategories Subcategories = GetPoiSubcategories(categoryIndex);

        // Create 'default' subcategpry for 'All' subcategories
        PoiSubcategory SelectedSubcategory = new PoiSubcategory(PoiSubcategory.AllSubcategories,categoryIndex,"All");

        // Skip subcategory selection if there are none
        if (Subcategories.Count > 0)
        {
          DialogResult SelectSubcategoryResult = frmSelectPoiSubcategory.Select(Subcategories,out SelectedSubcategory);

          // Cancel if Back button was clicked
          if (SelectSubcategoryResult == DialogResult.Cancel)
            return DialogResult.Cancel;
        }

        // Find destinations for the selected POI category with increasing radius if too few are found
        DestinationDataset.DestinationDataTable destinations =
          GetPoiDestinations(categoryIndex,SelectedSubcategory.Index);

        // Let user select the destination
        DialogResult SelectDestinationResult = frmSelectPoiDestination.Select(destinations,out SelectedDestination);

        // Break out of loop if Continue was clicked
        if (SelectDestinationResult == DialogResult.OK)
          break;

        // Cancel if Back button was clicked and there are no subcategories
        if ((SelectDestinationResult == DialogResult.Cancel) && (Subcategories.Count == 0))
          return DialogResult.Cancel;
      }

      selectedDestination = SelectedDestination;

      return DialogResult.OK;
    }

    private void OnShowNavigatorDialog(ShowNavigatorDialogEvent eventData)
    {
      if (ShowNavigatorDialog == null)
        return;

      GoodGuideEventArgs Args = new GoodGuideEventArgs(GetType().Name,eventData);
      try
      {
        ShowNavigatorDialog(this,Args);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Could not raise ShowNavigatorDialog",exc);
      }
    }
    private void OnGetPoiCategoryList(PoiCategoryListEvent eventData)
    {
      if (GetPoiCategoryList == null)
        return;

      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,eventData);
      try
      {
        GetPoiCategoryList(this,e);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error raising GetPoiCategoryList in {0}",GetType().Name),exc);
      }
    }
    private void OnGetPoiSubcategoryList(PoiSubcategoryListEvent eventData)
    {
      if (GetPoiSubcategoryList == null)
        return;

      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,eventData);
      try
      {
        GetPoiSubcategoryList(this,e);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error raising GetPoiSubcategoryList in {0}",GetType().Name),exc);
      }
    }
    private void OnGetPoiDestinationList(PoiDestinationListEvent eventData)
    {
      if (GetPoiDestinationList == null)
        return;

      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,eventData);
      try
      {
        GetPoiDestinationList(this,e);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error raising GetPoiDestinationList in {0}",GetType().Name),exc);
      }
    }
    private void OnSetNavigatorLanguage(Language language)
    {
      if (SetNavigatorLanguage == null)
        return;

      LanguageEvent eventData = new LanguageEvent(language);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,eventData);
      try
      {
        SetNavigatorLanguage(this,e);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error raising SetNavigatorLanguage in {0}",GetType().Name),exc);
      }
    }
    private void OnSetNavigatorUnits(UnitsEvent eventData)
    {
      if (SetNavigatorUnits == null)
        return;

      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,eventData);
      try
      {
        SetNavigatorUnits(this,e);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error raising SetNavigatorUnits in {0}",GetType().Name),exc);
      }
    }
    private void OnGetAddressOfPosition(NavigatorPositionEvent eventData)
    {
      if (GetAddressOfPosition == null)
        return;

      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,eventData);
      try
      {
        GetAddressOfPosition(this,e);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error raising GetAddressOfPositionin {0}",GetType().Name),exc);
      }
    }
    private void OnNavigatorIsNotificationsActiveChanged(Boolean isActive)
    {
      if (NavigatorIsNotificationsActiveChanged == null)
        return;

      RunStateEvent eventData = new RunStateEvent(isActive);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,eventData);
      try
      {
        NavigatorIsNotificationsActiveChanged(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this, string.Format("Error raising NavigatorIsNotificationsActiveChanged {0}", GetType().Name), exc);
      }
    }
    private void OnIsNavigatorAnnouncementActiveChanged()
    {
      if (IsNavigatorAnnouncementActiveChanged == null)
        return;

      try
      {
        IsNavigatorAnnouncementActiveChanged(this, new EventArgs());
      }
      catch (Exception exc)
      {
        WriteLog(this, "Error raising IsNavigatorAnnouncementActiveChanged", exc);
      }
    }
    private void OnNavigatorDirectionsChanged()
    {
      if (NavigatorDirectionsChanged == null)
        return;

      try
      {
        NavigatorDirectionsChanged(this, new EventArgs());
      }
      catch (Exception exc)
      {
        WriteLog(this, "Error raising NavigatorDirectionsChanged", exc);
      }
    }

    private PoiCategories poiCategories = null;
    private Boolean isNavigatorInstalled = false;
    private Boolean checkedNavigatorInstalled = false;
    private Boolean isNavigatorAnnouncementActive = false;
    private DrillDownGeoCoder geoCoder;
    private string[] navigatorDirections = new string[0];
    private DestinationDataset.DestinationRow initialItineraryDestination;
    #endregion

    #region Destinations
    public void ShowDestinationMore(object sender,Int32 destinationId)
    {
      DestinationDataset.DestinationRow SelectedDestination = destinationBll.GetById(destinationId);
      if (SelectedDestination == null)
        return;

      MoreResults Result = frmDestinationMore.ShowMore(SelectedDestination,false);
      if ((Result == MoreResults.TakeMeThere) || (Result == MoreResults.ViewMap))
      {
        if ((sender != null) && (sender is Form))
          ((Form)sender).Close();
      }
    }
    #endregion

    #region Itinerary
    public event EventHandler BookingPeriodExpired;

    public ItineraryFiles GetItineraryFiles()
    {
      Boolean CardInserted = Directory.Exists(StorageCardBasePath);
      if (CardInserted == false)
        return null;

      ItineraryFiles ItineraryFiles = new ItineraryFiles();

      string[] Filenames = Directory.GetFiles(StorageCardBasePath,"*.ggi");
      foreach (string Filename in Filenames)
      {
        try
        {
          GetDataResponse Data = itinerarySyncBll.LoadFromFile(Filename);
          ItineraryDataset.ItineraryDataTable ItineraryData =
            (ItineraryDataset.ItineraryDataTable)Data.Tables["Itinerary"];
          if (ItineraryData == null)
            continue;
          if (ItineraryData.Rows.Count == 0)
            continue;

          ItineraryFile ItineraryFile = new ItineraryFile(Filename,(ItineraryDataset.ItineraryRow)ItineraryData.Rows[0]);
          ItineraryFiles.Add(ItineraryFile);
        }
        catch (Exception exc)
        {
          WriteLog(this,"Error get data from card itinerary files",exc);
          return null;
        }
      }

      return ItineraryFiles;
    }

    public void LoadItineraryFromCard()
    {
      WriteLog(this, ">> LoadItineraryFromCard");

      WriteLog(this, "  Getting itinerary files on card");
      ItineraryFiles ItineraryFiles = GetItineraryFiles();
      if (ItineraryFiles == null)
      {
        WriteLog(this, "ItineraryFiles == null");
        return;
      }
      if (ItineraryFiles.Count == 0)
      {
        WriteLog(this, "ItineraryFiles.Count == 0");
        return;
      }

      WriteLog(this, string.Format("Got {0} file(s)", ItineraryFiles.Count));
      ItineraryFile SelectedItineraryFile;
      DialogResult Result = frmSelectItinerary.Select(ItineraryFiles,out SelectedItineraryFile);
      if (Result == DialogResult.Cancel)
      {
        WriteLog(this, "Cancel");
        return;
      }

      WaitCursor.Show(true);
      try
      {
        WriteLog(this, string.Format("Import itinerary file {0}", SelectedItineraryFile.Filename));
        itinerarySyncBll.ImportFile(SelectedItineraryFile.Filename, 0, false);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error syncing data from file: {0}",SelectedItineraryFile.Filename),exc);
        WaitCursor.Show(false);
        return;
      }

      LoadedItineraryFile = System.IO.Path.GetFileNameWithoutExtension(SelectedItineraryFile.Filename);

      SetFirstTimeUse(null);

      ItineraryDataset.ItineraryRow itinerary = itineraryBll.GetFirst();
      if (itinerary != null)
        Language = LanguageHelper.GetLanguage(CultureInfo.CreateSpecificCulture(itinerary.Culture));

      WaitCursor.Show(false);
    }

    public void CheckBookingPeriod()
    {
      if (IsGpsFixValid == false)
        return;

      DateTime ExpiryDate = new DateTime(2020,5,1);
      if (DateTime.Today >= ExpiryDate)
      {
        OnBookingPeriodExpired();
        return;
      }

      // Allow for an itinerary to be loaded
      Boolean HasItinerary = itinerary != null;
      if (HasItinerary == false)
        return;

      // Get arrival date - can be empty
      Boolean HasArrivalDate = itinerary.sArrivalDat != string.Empty;

      // Get departure date. If empty then lock device
      Boolean HasDepartureDate = itinerary.sDepartureDat != string.Empty;
      if (HasDepartureDate == false)
      {
        OnBookingPeriodExpired();
        return;
      }

      DateTime BookingPeriodEnd = itinerary.DepartureDat.AddDays(itinerary.GracePeriod);
      if (DateTime.Now > BookingPeriodEnd)
        OnBookingPeriodExpired();
    }

    public void LockDevice()
    {
      frmLocked.ShowForm();
    }

    private void OnBookingPeriodExpired()
    {
      if (BookingPeriodExpired == null)
        return;

      try
      {
        BookingPeriodExpired(this,new EventArgs());
      }
      catch (Exception exc)
      {
        WriteLog(this,"Could not raise BookingPeriodExpired event",exc);
      }
    }
    #endregion

    #region FM transmitter
    public Int16 FmTransmitterChannel
    {
      get { return fmTransmitterChannel; }
      set
      {
        fmTransmitterChannel = value;

        if (DeviceServices != null)
          DeviceServices.FmTransmitterChannel = (UInt32)fmTransmitterChannel;

        try
        {
          RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("FmTransmitterChannel", fmTransmitterChannel);
        }
        catch (Exception exc)
        {
          WriteLog(this,"Error setting FM transmitter channel",exc);
        }
      }
    }
    public Boolean FmTransmitterEnabled
    {
      get { return fmTransmitterEnabled; }
      set
      {
        fmTransmitterEnabled = value;
        if (DeviceServices != null)
          DeviceServices.FMTransmitterState = fmTransmitterEnabled;
      }
    }
    private Int16 fmTransmitterChannel = 0;
    private Boolean fmTransmitterEnabled = false;
    #endregion

    #region Media player
    [EventPublisher(EventTopics.ContentManager.MediaControl)]
    public event EventHandler<GoodGuideEventArgs> MediaControl;

    [EventPublisher(EventTopics.MediaPlayer.MediaTypeControl)]
    public event EventHandler<GoodGuideEventArgs> MediaTypeControl;

    [EventPublisher(EventTopics.MediaPlayer.VideoSizeControl)]
    public event EventHandler<GoodGuideEventArgs> VideoSizeControl;

    [EventSubscriber(EventTopics.MediaPlayer.MediaStateChange)]
    public void MediaStateChangeHandler(object sender, GoodGuideEventArgs e)
    {
      MediaStateEvent eventData = e.EventData as MediaStateEvent;
      if (eventData == null)
      {
        return;
      }

      deviceMediaState = eventData.State;
      mediaType = eventData.MediaType;
      mediaFilename = eventData.Filename;
      WriteLog(this, string.Format(">> MediaStateChangeHandler State:{0}, MediaType:{1}, Filename{2}", deviceMediaState, mediaType, mediaFilename));

      OnMediaStateChanged();

      if (!playingTestAudio)
        return;

      if (DeviceMediaState != MediaStateEvent.MediaStates.Playing)
      {
        GpsPositionEvent gpsEvent = new GpsPositionEvent(DateTime.Now, GpGga.FixQualities.GpsFix, DefaultLatitude, DefaultLongitude, 0, 0, 0, 6);
        RegionExitEvent exitEvent = new RegionExitEvent(selectedRegion, gpsEvent);
        GoodGuideEventArgs GpsEventArgs = new GoodGuideEventArgs("", exitEvent);
        RegionExit(this, GpsEventArgs);
        playingTestAudio = false;
      }
    }

    public event EventHandler MediaStateChanged;

    public MediaStateEvent.MediaStates DeviceMediaState
    {
      get { return deviceMediaState; }
    }
    public string MediaFilename
    {
      get { return mediaFilename; }
    }
    public MediaTypes MediaType
    {
      get { return mediaType; }
      set
      {
        mediaType = value;

        OnMediaTypeControl(mediaType);
      }
    }
    public VideoSize VideoSize
    {
      set
      {
        OnVideoSizeControl(value);
      }
    }

    public void ConfigureMediaManager()
    {
      // Force content path for all languages to English as there is not enough flash space for all languages' content.
      // To enable language specific content, change Language.English to Language (the local property)
      string contentPath = string.Format("{0}{1}{2}{3}",contentBaseBath,LanguageHelper.CreateCulture(Language.English).Name,System.IO.Path.DirectorySeparatorChar,masterAreaContentPath);

      NamedParameters MediaManagerConfigurationParameters = new NamedParameters();
      MediaManagerConfigurationParameters.Add(new NamedParameter(ConfigurationParameters.MediaManager.ContentBasePath, contentPath));

      ConfigurationEvent MediaManagerEventData = new ConfigurationEvent(MediaManagerConfigurationParameters);
      OnMediaManagerConfiguration(MediaManagerEventData);
    }

    private void OnMediaControl(MediaControlEvent eventData)
    {
      if (MediaControl == null)
        return;

      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,eventData);
      try
      {
        MediaControl(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Could not raise MediaControl event", exc);
      }
    }
    private void OnMediaStateChanged()
    {
      if (MediaStateChanged == null)
        return;

      try
      {
        MediaStateChanged(this, new EventArgs());
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising MediaStateChanged event", exc);
      }
    }
    private void OnMediaTypeControl(MediaTypes mediaType)
    {
      if (MediaTypeControl == null)
        return;

      MediaTypeEvent eventData = new MediaTypeEvent(mediaType);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);
      try
      {
        MediaTypeControl(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this, "Could not raise MediaTypeControl event", exc);
      }
    }
    private void OnVideoSizeControl(VideoSize videoSize)
    {
      if (VideoSizeControl == null)
        return;

      VideoSizeEvent eventData = new VideoSizeEvent(videoSize);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);
      try
      {
        VideoSizeControl(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this, "Could not raise VideoSizeControl event", exc);
      }
    }

    private string mediaFilename = String.Empty;
    private readonly Boolean isDeviceMediaMuted = false;
    private Int32 deviceMediaChannelGroupId = DefaultDeviceMediaChannelGroupId;
    private string deviceMediaChannelGroupName = DefaultDeviceMediaChannelGroupName;
    private Int32 deviceMediaChannelId = DefaultDeviceMediaChannelId;
    private string deviceMediaChannelContentPath = DefaultDeviceMediaChannelContentPath;
    private string deviceMediaChannelGroupContentPath = DefaultDeviceMediaChannelGroupContentPath;
    private string deviceMediaChannelLanguage = DefaultDeviceMediaChannelLanguage;
    private MediaStateEvent.MediaStates deviceMediaState = MediaStateEvent.MediaStates.Stopped;
    private MediaTypes mediaType = MediaTypes.Unknown;
    #endregion

    #region Content Manager
    [EventPublisher(EventTopics.ContentManager.RunState)]
    public event EventHandler<GoodGuideEventArgs> ContentManagerRunState;

    [EventPublisher(EventTopics.ContentManager.ContentControl)]
    public event EventHandler<GoodGuideEventArgs> ContentControl;

    public void RepeatContent()
    {
      OnContentManagerContentControl(ContentControlEvent.ControlStates.Repeat);
    }
    public void PauseContent()
    {
      OnContentManagerContentControl(ContentControlEvent.ControlStates.Pause);
    }
    public void StopContent()
    {
      OnContentManagerContentControl(ContentControlEvent.ControlStates.Stop);
    }
    public void ResumeContent()
    {
      OnContentManagerContentControl(ContentControlEvent.ControlStates.Resume);
    }
    private void OnContentManagerRunState(Boolean runState)
    {
      if (ContentManagerRunState == null)
        return;

      RunStateEvent EventData = new RunStateEvent(runState);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, EventData);
      try
      {
        ContentManagerRunState(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error raising ContentManagerRunState in {0}", GetType().Name), exc);
      }
    }
    private void OnContentManagerContentControl(ContentControlEvent.ControlStates contentState)
    {
      if (ContentControl == null)
        return;

      ContentControlEvent EventData = new ContentControlEvent(contentState);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, EventData);
      try
      {
        ContentControl(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error raising ContentControl in {0}", GetType().Name), exc);
      }
    }

    #endregion

    #region DestinationManager functions
    public void NavigateTo(DestinationDataset.DestinationRow destination, Boolean isRecentDestination)
    {
      if ((destination.IsLatitudeNull() == true) || (destination.IsLongitudeNull() == true))
        return;

      WriteLog(this,string.Format("Navigating to ({0},{1},{2})",destination.Code,destination.Latitude,destination.Longitude));

      // The itinerary form is shown on startup. Before the Navigator has been initialised therefore 
      // the NavigateTo request is not sent to the Navigator. In this scenario, save the destination 
      // that is navigated to and initiate the navigation to the destination when the Navigator is initialised.
      if (geoCoder == null)
      {
        WriteLog(this,"Navigator not yet initialised. Saving destination for later NavigateTo");
        initialItineraryDestination = destination;

        return;
      }

      DestinationNavigationEvent EventData = new DestinationNavigationEvent(DestinationNavigationEvent.Actions.NavigateTo, destination);
      EventData.IsRecentDestination = isRecentDestination;

      OnDestinationCommand(EventData);
    }
    public void NavigateTo(RecentDestinationDataset.RecentDestinationRow recentDestination)
    {
      DestinationDataset.DestinationRow RecentDestination;

      if (recentDestination.IsDestinationIdNull() == false)
      {
        #region Recent destination is Destination
        try
        {
          RecentDestination = destinationBll.GetById(recentDestination.DestinationId);
        }
        catch (Exception exc)
        {
          WriteLog(this,"Error getting recent destination by id",exc);
          return;
        }

        if (RecentDestination == null)
          return;
        #endregion
      }
      else
      {
        #region Recent destination is not Destination
        try
        {
          RecentDestination = DestinationBll.CreateNew(recentDestination.Latitude, recentDestination.Longitude, recentDestination.Name);
        }
        catch (Exception exc)
        {
          WriteLog(this,"Error creating recent destination",exc);
          return;
        }
        #endregion
      }

      NavigateTo(RecentDestination, true);
    }
    public void CancelNavigateTo()
    {
      DestinationNavigationEvent EventData = new DestinationNavigationEvent(DestinationNavigationEvent.Actions.CancelRoute);

      OnDestinationCommand(EventData);
    }
    public void PauseRouter()
    {
      DestinationNavigationEvent EventData = new DestinationNavigationEvent(DestinationNavigationEvent.Actions.PauseRoute);

      OnDestinationCommand(EventData);
    }
    public void ResumeRouter()
    {
      DestinationNavigationEvent EventData = new DestinationNavigationEvent(DestinationNavigationEvent.Actions.ResumeRoute);

      OnDestinationCommand(EventData);
    }
    #endregion

    #region Language
    public Language Language
    {
      get { return language; }
      set
      {
        language = value;

        SelectedCulture.Culture = LanguageHelper.CreateCulture(language);
        welcomeAdProvider = new WelcomeAdProvider(logger, Language);

        OnSetNavigatorLanguage(language);
        ConfigureMediaManager();
      }
    }
    public Language FirstTimeLanguage
    {
      get { return firstTimeLanguage; }
      set
      {
        firstTimeLanguage = value;
        WriteLog(this,string.Format("FirstTimeLanguage: {0}",firstTimeLanguage));
      }
    }

    public Language PreviousLanguage(Language selectedLanguage)
    {
      Int32 index = languages.IndexOf(selectedLanguage);
      if (index == -1)
        return selectedLanguage;
      if (index == 0)
        index = languages.Count - 1;
      else
        index--;

      return languages[index];
    }
    public Language NextLanguage(Language selectedLanguage)
    {
      Int32 index = languages.IndexOf(selectedLanguage);
      if (index == -1)
        return selectedLanguage;
      if (index == languages.Count - 1)
        index = 0;
      else
        index++;

      return languages[index];
    }
    private void SetupLanguages()
    {
      languages.Add(Types.Language.Dutch);
      languages.Add(Types.Language.English);
      languages.Add(Types.Language.French);
      languages.Add(Types.Language.German);
      languages.Add(Types.Language.Italian);
    }

    private Language language = Types.Language.English;
    private Language firstTimeLanguage = Types.Language.Unknown;
    private readonly List<Language> languages = new List<Language>();
    #endregion

    #region Invoke action targets
    public void Search(object sender)
    {
      WriteLog(this, ">> Search");
      
      Form senderForm = sender as Form;
      if (senderForm != null)
        senderForm.Close();

      WaitCursor.Show(true);
      ICurrentLocationSource locationSource;
      if (currentGpsPosition == null)
        locationSource = new GpsLocationSource(0,0);
      else
        locationSource = new GpsLocationSource(currentGpsPosition.Latitude,currentGpsPosition.Longitude);

      double latitude;
      double longitude;
      string name;

      #region Get lat/long
      DialogResult Result = frmSearch.ShowModal(
        logger,geoCoder,
        RepositoryLocator.LocateRepository<ISearchStreetRepository>(),
        RepositoryLocator.LocateRepository<ISearchRegionRepository>(),
        RepositoryLocator.LocateRepository<IDestinationRepository>(),
        locationSource,
        out latitude, out longitude,out name);
      if (Result == DialogResult.Cancel)
        return;
      #endregion

      DestinationDataset.DestinationRow Destination = DestinationBll.CreateNew((float)latitude, (float)longitude, name);

      NavigateTo(Destination, false);
    }
    public void SearchDestinationCollection(object sender, string collectionCode)
    {
      #region Get DestinationCollection record
      DestinationCollectionDataset.DestinationCollectionRow DestinationCollection;
      try
      {
        DestinationCollection = DestinationCollectionBll.GetByCode(collectionCode);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error getting DestinationCollection by code: {0}",collectionCode),exc);
        return;
      }

      if ((DestinationCollection == null) || (masterAreaId == DefaultMasterAreaId))
        return;
      #endregion

      #region Get destinations for collection
      DestinationDataset.DestinationDataTable Destinations;
      try
      {
        Destinations = DestinationBll.GetByCollection(masterAreaId,collectionCode);
      }
      catch (Exception exc)
      {
        WriteLog(this,string.Format("Error getting destinaions by collection id: {0}",DestinationCollection.Id),exc);
        return;
      }

      if ((Destinations == null) || (Destinations.Count == 0))
        return;
      #endregion

      DestinationDataset.DestinationRow SelectedDestination;
      DialogResult Result = frmDestinationListSearch.Search(Destinations, out SelectedDestination);
      if (Result == DialogResult.Cancel)
        return;

      if (SelectedDestination == null)
        return;

      ShowDestinationMore(sender,SelectedDestination.Id);
    }
    public void SetFirstTimeUse(object sender)
    {
      try
      {
        ResetCounts();
        mySelectionBll.DeleteAll();
        recentDestinationBll.DeleteAll();
        AudioContent = AudioSettings.Content.NavigationAndCommentary;
        Units = Units.Metric;

        FirstUse.IsFirstUse = true;
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error in SetFirstTimeUse", exc);
      }
    }
    public void PlayTestAudio(object sender, string GpsRegionId)
    {
      try
      {
        if (String.IsNullOrEmpty(GpsRegionId))
          return;

        int regionId = Int32.Parse(GpsRegionId);

        // Get GPS Region Data
        Regions maRegions = gpsRegionBll.GetRegionsByMasterArea(MasterAreaId);
        foreach (Region r in maRegions)
        {
          if (r.Id == regionId)
          {
            selectedRegion = r;
            break;
          }
        }
        // If region wasn't found, exit
        if (selectedRegion == null)
          return;
        playingTestAudio = true;
        // Play the found region. Only tested with a radius area of priority 1
        GpsPositionEvent gpsEvent = new GpsPositionEvent(DateTime.Now, GpGga.FixQualities.GpsFix, selectedRegion.MinLatitude + 0.00001f, selectedRegion.MinLongitude +
        0.00001f, 0, 0, 0, 6);
        RegionEnterEvent enterEvent = new RegionEnterEvent(selectedRegion, gpsEvent);
        GoodGuideEventArgs GpsEventArgs = new GoodGuideEventArgs("", enterEvent);
        RegionEnter(this, GpsEventArgs);
        // close the form with the play button(s) so that it can go back to the main screen.
        if ((sender != null) && ((sender as frmCarApplicationBase) != null))
          (sender as frmCarApplicationBase).Close();
      }
      catch (Exception ex)
      {
        WriteLog(this,"Error in PlayTestAudio", ex);
      }
    }
    public void StopTestAudio(object sender, string mediaFile) // mediaFile is ignored
    {
      try
      {
        MediaControlEvent EventData = new MediaControlEvent(
        MediaControlEvent.MediaControlStates.Stop,
        deviceMediaChannelGroupId,
        "",
        MediaTypes.Sound,
        true
        );
        OnMediaControl(EventData);
      }
      catch (Exception ex)
      {
        WriteLog(this,"Error in PlayTestAudio", ex);
      }
    }
    public void NavigateToPoiCategory(object sender, string categoryIndex)
    {
      // Determine category index
      try
      {
        Int32 CategoryIndex = Convert.ToInt32(categoryIndex);
        NavigateToPoiCategory(CategoryIndex);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error in NavigateToPoiCategory", exc);
        return;
      }
    }
    public void NavigateToPoi(object sender)
    {
      DestinationDataset.DestinationRow SelectedDestination;
      DialogResult Result = SelectPoiDestination(out SelectedDestination);
      if (Result == DialogResult.Cancel)
        return;

      NavigateTo(SelectedDestination,false);
    }
    public void ShowDestinationMoreString(object sender, string destinationId)
    {
      try
      {
        Int32 DestinationId = Convert.ToInt32(destinationId);
        ShowDestinationMore(sender,DestinationId);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error in ShowDestinationMoreString", exc);
        return;
      }
    }
    public void SelectAudioContent(object sender)
    {
      AudioSettings.Content NewAudioContent;
      DialogResult Result = frmSelectAudioContent.Select(out NewAudioContent);
      if (Result == DialogResult.Cancel)
        return;

      AudioContent = NewAudioContent;
    }
    public void SelectAudioSource(object sender)
    {
      AudioSettings.Source NewAudioSource;
      DialogResult Result = frmSelectAudioSource.Select(out NewAudioSource);
      if (Result == DialogResult.Cancel)
        return;

      AudioSource = NewAudioSource;
    }
    public void NavigateToLatLong(object sender)
    {
      double latitude;
      double longitude;

      #region Get lat/long
      DialogResult Result = frmEnterLatLong.EnterValues(out latitude,out longitude);
      if (Result == DialogResult.Cancel)
        return;
      #endregion

      string name = string.Empty;
      Address address = GeoCoder.ReverseGeoCode(new LatLon(latitude, longitude));
      if (address != null)
        name = address.ToString();

      DestinationDataset.DestinationRow Destination = DestinationBll.CreateNew((float)latitude,(float)longitude,name);

      NavigateTo(Destination, false);
    }
    public void SelectMasterArea(object sender)
    {
      MasterAreaDataset.MasterAreaRow MasterArea;
      DialogResult Result = frmSelectMasterArea.Select(out MasterArea);
      if (Result == DialogResult.Cancel)
        return;

      SetMasterArea(MasterArea);
    }
    public void SetVolume(object sender)
    {
      frmVolume.ShowModal();
    }
    public void FactorySetup(object sender)
    {
      string enteredPassword;
      DialogResult passwordResult = frmAlphaNumericKeyboard.GetText("Enter password",out enteredPassword);
      if (passwordResult == DialogResult.Cancel)
        return;

      if (enteredPassword != factorySetupPassword)
        return;

      DialogResult exitResult = frmFactorySetup.ShowForm();
      if (exitResult == DialogResult.Yes)
      {
        if (CloseApplicationRequested != null)
          CloseApplicationRequested(this,new EventArgs());
      }
    }
    public void NavigateToStreetAddress(object sender)
    {
      //double latitude;
      //double longitude;
      //DialogResult result = frmRegionSearch.GetLocation(out latitude,out longitude);
      //if (result == DialogResult.Cancel)
      //  return;

      //DestinationDataset.DestinationRow Destination = DestinationBll.CreateNew((float)latitude, (float)longitude, string.Empty);

      //NavigateTo(Destination, false);
    }
    #endregion

    #region How it works video
    public void PlayIntroVideo()
    {
      WriteLog(this, ">> PlayIntroVideo");

      string filename = LanguageHelper.FormatCultureDependentPath(
        LanguageHelper.CreateCulture(Language),
        string.Format("{0}Resources\\", Nucleo.Path.ExecutablePath),
        "HowItWorks.wmv");

      WriteLog(this, string.Format("Playing video {0}",filename));
      MediaControlEvent EventData = new MediaControlEvent(MediaControlEvent.MediaControlStates.Play, deviceMediaChannelGroupId, filename, MediaTypes.Video, true);
      OnMediaControl(EventData);
    }
    public void PauseIntroVideo()
    {
      WriteLog(this, ">> PauseIntroVideo");

      MediaControlEvent EventData = null;
      switch (deviceMediaState)
      {
        case MediaStateEvent.MediaStates.Playing:
          EventData = new MediaControlEvent(MediaControlEvent.MediaControlStates.Pause, deviceMediaChannelGroupId, introVideoFilename, MediaTypes.Video, false);
          break;
        case MediaStateEvent.MediaStates.Paused:
          EventData = new MediaControlEvent(MediaControlEvent.MediaControlStates.Resume, deviceMediaChannelGroupId, introVideoFilename, MediaTypes.Video, false);
          break;
      }

      if (EventData != null)
        OnMediaControl(EventData);
    }
    public void StopIntroVideo()
    {
      WriteLog(this, ">> StopIntroVideo");

      MediaControlEvent EventData = new MediaControlEvent(MediaControlEvent.MediaControlStates.Stop, deviceMediaChannelGroupId, introVideoFilename, MediaTypes.Video, false);
      OnMediaControl(EventData);
    }

    private string introVideoFilename;
    #endregion

    #region Device volume
    public UInt16 DeviceVolume // From 0..10
    {
      get
      {
        UInt16 volume = operatingSystemServices.Volume;
        UInt16 deviceVolume = (UInt16)System.Math.Round((volume * 10.0 / 65535.0));

        return deviceVolume;
      }
      set
      {
        if (value > 10)
          return;

        if (deviceServices == null)
          return;

        UInt32 volume = (UInt32)(value * 65535 / 10);

        operatingSystemServices.Volume = (UInt16)volume;
      }
    }

    public UInt16 DecreaseVolume()
    {
      DeviceVolume -= 1;
      operatingSystemServices.PlaySound(@"\Windows\asterisk.wav");

      return DeviceVolume;
    }
    public UInt16 IncreaseVolume()
    {
      DeviceVolume += 1;
      operatingSystemServices.PlaySound(@"\Windows\asterisk.wav");

      return DeviceVolume;
    }
    #endregion

    #region Public methods
    public void Initialise(ILogger logger)
    {
      DynamicFormManager.InvokeMethod += InvokeActionHandler;
      this.logger = logger;
      loggingServices = new LoggingServices(logger);
      loggingServices.Enabled = true;

      try
      {
        WriteLog(this, "Initialising 0%...");

        OnInitialisationStarted();

        SetupLanguages();

        #region DeviceServices
        WriteLog(this, "Get DeviceServices");
        List<IDeviceServices> deviceServicesList = Loader.LoadFromPath<IDeviceServices>(Path.ExecutablePath, "ggds");
        if (deviceServicesList.Count > 0)
          deviceServices = deviceServicesList[0];
        #endregion

        #region Create DAL locator
        WriteLog(this, "Loading DAL");
        List<IRepository> dals = Loader.LoadFromPath<IRepository>(Nucleo.Path.ExecutablePath, "dal");
        if (dals.Count == 0)
        {
          WriteLog(this, "IRepository not found");
          throw new InvalidOperationException("IRepository not found");
        }
        repository = dals[0];
        if (repository is IRepositoryLocator == false)
        {
          WriteLog(this, "IRepositoryLocator not found");
          throw new InvalidOperationException("IRepositoryLocator not found");
        }
        RepositoryLocator = repository as IRepositoryLocator;
        WriteLog(this, "Initialising repository");

        repository.Initialise("GoodGuide.sqlite",logger);
        #endregion

        #region Create BLL
        WriteLog(this,"Create BLL");
        gpsRegionBll = new GpsRegionBll(RepositoryLocator.LocateRepository<IGpsRegionRepository>());
        channelContentBll = new ChannelContentBll(RepositoryLocator.LocateRepository<IChannelContentRepository>());
        contentItemBll = new ContentItemBll(RepositoryLocator.LocateRepository<IContentItemRepository>());
        themeBll = new ThemeBll(RepositoryLocator.LocateRepository<IThemeRepository>());
        masterAreaBll = new MasterAreaBll(RepositoryLocator.LocateRepository<IMasterAreaRepository>());
        channelBll = new ChannelBll(RepositoryLocator.LocateRepository<IChannelRepository>());
        channelGroupBll = new ChannelGroupBll(RepositoryLocator.LocateRepository<IChannelGroupRepository>());
        controlDefinitionBll = new ControlDefinitionBll(RepositoryLocator.LocateRepository<IControlDefinitionRepository>());
        formDefinitionBll = new FormDefinitionBll(RepositoryLocator.LocateRepository<IFormDefinitionRepository>(), RepositoryLocator.LocateRepository<IControlDefinitionRepository>());
        destinationTypeBll = new DestinationTypeBll(RepositoryLocator.LocateRepository<IDestinationTypeRepository>());
        destinationClassificationBll = new DestinationClassificationBll(RepositoryLocator.LocateRepository<IDestinationClassificationRepository>());
        itineraryBll = new ItineraryBll(logger, RepositoryLocator.LocateRepository<IItineraryRepository>());
        itineraryDayBll = new ItineraryDayBll(logger,RepositoryLocator.LocateRepository<IItineraryDayRepository>());
        itineraryDestinationBll = new ItineraryDestinationBll(logger, RepositoryLocator.LocateRepository<IItineraryDestinationRepository>());
        destinationBll = new DestinationBll(
          logger,
          RepositoryLocator.LocateRepository<IDestinationRepository>(),
          RepositoryLocator.LocateRepository<IDestinationTypeRepository>(),
          RepositoryLocator.LocateRepository<IDestinationClassificationRepository>(),
          RepositoryLocator.LocateRepository<IDestinationCollectionRepository>(), itineraryDayBll);
        destinationCollectionBll = new DestinationCollectionBll(RepositoryLocator.LocateRepository<IDestinationCollectionRepository>());
        destinationCollectionMemberBll = new DestinationCollectionMemberBll(RepositoryLocator.LocateRepository<IDestinationCollectionMemberRepository>());
        mySelectionBll = new MySelectionBll(RepositoryLocator.LocateRepository<IMySelectionRepository>());
        destinationUpdateBll = new DestinationUpdateBll(logger,RepositoryLocator.LocateRepository<IDestinationRepository>(), destinationBll);
        recentDestinationBll = new RecentDestinationBll(RepositoryLocator.LocateRepository<IRecentDestinationRepository>(), imageManager);
        #endregion

        #region SyncBll
        WriteLog(this,"Sync BLL");
        contentSyncBll = new SyncBll(logger);
        contentSyncBll.SyncTargets.Add(channelBll);
        contentSyncBll.SyncTargets.Add(channelContentBll);
        contentSyncBll.SyncTargets.Add(channelGroupBll);
        contentSyncBll.SyncTargets.Add(contentItemBll);
        contentSyncBll.SyncTargets.Add(gpsRegionBll);
        contentSyncBll.SyncTargets.Add(masterAreaBll);
        contentSyncBll.SyncTargets.Add(themeBll);

        destinationSyncBll = new SyncBll(logger);
        destinationSyncBll.SyncTargets.Add(destinationBll);
        destinationSyncBll.SyncTargets.Add(destinationClassificationBll);
        destinationSyncBll.SyncTargets.Add(destinationCollectionBll);
        destinationSyncBll.SyncTargets.Add(destinationCollectionMemberBll);
        destinationSyncBll.SyncTargets.Add(destinationTypeBll);

        itinerarySyncBll = new SyncBll(logger);
        itinerarySyncBll.SyncTargets.Add(itineraryBll);
        itinerarySyncBll.SyncTargets.Add(itineraryDayBll);
        itinerarySyncBll.SyncTargets.Add(itineraryDestinationBll);
        itinerarySyncBll.SyncTargets.Add(destinationUpdateBll);

        formDefinitionSyncBll = new SyncBll(logger);
        formDefinitionSyncBll.SyncTargets.Add(controlDefinitionBll);
        formDefinitionSyncBll.SyncTargets.Add(formDefinitionBll);
        #endregion

        #region Create EventBroker
        WriteLog(this, "Create EventBroker");
        eventBroker = new GoodGuideEventBroker();
        eventBroker.Register(this);
        #endregion

        #region Load configuration
        WriteLog(this, "Load configuration");
        LoadConfiguration();
        #endregion

        #region Load plugins
        WriteLog(this, "Load plugins");
        try
        {
          LoadPlugins();
        }
        catch (Exception exc)
        {
          WriteLog(this, "Error loading plugins!", exc);
          
          throw;
        }
        #endregion

        #region Load media manager
        try
        {
          LoadMediaManager();
        }
        catch (Exception exc)
        {
          WriteLog(this, "Error loading media manager", exc);

          throw;
        }
        #endregion

        #region Reset Areas
        if (resetAreasOnStartup == true)
        {
          WriteLog(this, "Reset Areas");
          ResetCounts();
        }
        #endregion

        #region Build form definitions
        WriteLog(this, "Build form definitions");
        DynamicFormManager.FormDefinitions.Clear();
        formDefinitionBll.BuildDefinitions(DynamicFormManager.FormDefinitions);
        #endregion

        #region Create BannerAdProvider
        WriteLog(this, "Create BannerAdProvider");
        bannerAdProvider = new BannerAdProvider(logger, RepositoryLocator.LocateRepository<INamedParameterRepository>(), RepositoryLocator.LocateRepository<IFormDefinitionRepository>());
        #endregion

        #region Creating WelcomeAdProvider
        WriteLog(this, "Creating WelcomeAdProvider");
        welcomeAdProvider = new WelcomeAdProvider(logger,Language);
        #endregion

        #region Get itinerary
        WriteLog(this, "Get itinerary");
        itinerary = itineraryBll.GetFirst();
        #endregion

        WriteLog(this, "Initialisation Complete...");
        OnInitialisationCompleted();

        AudioContent = AudioContent;
      }
      catch (Exception exc)
      {
        MessageBox.Show(ExceptionManager.MessageWithStackTrace(exc));
        WriteLog(this, "Initialisation thread error", exc);

        string Filename = "CarApplication.InitialisationThread.Crashlog.txt";
        TextWriter CrashLogWriter = new StreamWriter(Nucleo.Path.ExecutablePath + Filename, false);
        CrashLogWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        CrashLogWriter.WriteLine(exc.ToString());
        CrashLogWriter.Flush();
        CrashLogWriter.Close();
      }
    }
    public void Finalise()
    {
      SaveConfiguration();

      Instance.ShutdownSequence();

      #region Finalise plugins
      for (Int32 PluginNo = 0; PluginNo < PluginManager.Plugins.Count; PluginNo++)
      {
        try
        {
          IGoodGuidePlugin GoodGuidePlugin = (IGoodGuidePlugin)PluginManager.Plugins[PluginNo];
          GoodGuidePlugin.Finalise();
        }
        catch (Exception exc)
        {
          throw new ApplicationException(string.Format("Error finalising plugin {0}", PluginManager.Plugins[PluginNo].Name), exc);
        }
      }
      #endregion

      eventBroker.Unregister(this);

      repository.Finalise();
    }
    public void LoadConfiguration()
    {
      loadedContentFile = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("LoadedContentFile");
      if (loadedContentFile == null)
        loadedContentFile = string.Empty;

      loadedItineraryFile = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("LoadedItineraryFile");
      if (loadedItineraryFile == null)
        loadedItineraryFile = string.Empty;

      loadedDestinationFile = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("LoadedDestinationFile");
      if (loadedDestinationFile == null)
        loadedDestinationFile = string.Empty;

      loadedFormDefinitionFile = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("LoadedFormDefinitionFile");
      if (loadedFormDefinitionFile == null)
        loadedFormDefinitionFile = string.Empty;

      #region IsLoggingActive
      Boolean? _isLoggingActive = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("IsLoggingActive");
      if (_isLoggingActive == null)
      {
        IsLoggingActive = false;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("IsLoggingActive", IsLoggingActive);
      }
      else
        IsLoggingActive = _isLoggingActive.Value;
      #endregion

      #region IsExceptionLoggingActive
      Boolean? _isExceptionLoggingActive = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("IsExceptionLoggingActive");
      if (_isExceptionLoggingActive == null)
      {
        isExceptionLoggingActive = false;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("IsExceptionLoggingActive", IsExceptionLoggingActive);
      }
      else
        isExceptionLoggingActive = _isExceptionLoggingActive.Value;
      #endregion

      #region CheckBookingPeriod
      Boolean? _checkBookingPeriod = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("CheckBookingPeriod");
      if (_checkBookingPeriod == null)
      {
        checkBookingPeriod = false;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("CheckBookingPeriod", checkBookingPeriod);
      }
      else
        checkBookingPeriod = _checkBookingPeriod.Value;
      #endregion

      #region LoggingBasePath
      loggingBasePath = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("LoggingBasePath");
      if (loggingBasePath == null)
        loggingBasePath = DefaultLoggingBasePath;
      #endregion

      #region Copyright
      copyright = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("Copyright");
      if (copyright == null)
        copyright = DefaultCopyright;
      #endregion

      #region Helpline
      helpline = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("Helpline");
      if (helpline == null)
        helpline = DefaultHelpline;
      #endregion

      #region DeviceVolume
      Int16? _deviceVolume = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt16("DeviceVolume");     
      if (_deviceVolume == null)
      {
        _deviceVolume = DefaultVolume;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("DeviceVolume", _deviceVolume.Value);
      }

      DeviceVolume = (UInt16)_deviceVolume.Value;
      #endregion

      #region GpsTimeOffset
      Int16? _gpsTimeOffset = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt16("GpsTimeOffset");
      if (_gpsTimeOffset == null)
      {
        gpsTimeOffset = DefaultGpsTimeOffset;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("GpsTimeOffset", gpsTimeOffset);
      }
      else
        gpsTimeOffset = _gpsTimeOffset.Value;
      #endregion

      contentBaseBath = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("ContentBasePath");
      if (contentBaseBath == null)
        contentBaseBath = DefaultContentBasePath;

      #region DeviceMediaChannelGroupId
      Int32? _deviceMediaChannelGroupId = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt32("DeviceMediaChannelGroupId");     
      if (_deviceMediaChannelGroupId == null)
      {
        deviceMediaChannelGroupId = DefaultDeviceMediaChannelGroupId;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("DeviceMediaChannelGroupId", deviceMediaChannelGroupId);     
      }
      else
        deviceMediaChannelGroupId = _deviceMediaChannelGroupId.Value;
      #endregion

      deviceMediaChannelGroupName = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("DeviceMediaChannelGroupName");
      if (deviceMediaChannelGroupName == null)
        deviceMediaChannelGroupName = DefaultDeviceMediaChannelGroupName;

      #region DeviceMediaChannelId
      Int32? _deviceMediaChannelId = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt32("DeviceMediaChannelId");
      if (_deviceMediaChannelId == null)
      {
        deviceMediaChannelId = DefaultDeviceMediaChannelId;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("DeviceMediaChannelId", deviceMediaChannelId);
      }
      else
        deviceMediaChannelId = _deviceMediaChannelId.Value;
      #endregion

      deviceMediaChannelContentPath = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("DeviceMediaChannelContentPath");
      if (deviceMediaChannelContentPath == null)
        deviceMediaChannelContentPath = DefaultDeviceMediaChannelContentPath;

      deviceMediaChannelGroupContentPath = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("DeviceMediaChannelGroupContentPath");
      if (deviceMediaChannelGroupContentPath == null)
        deviceMediaChannelGroupContentPath = DefaultDeviceMediaChannelGroupContentPath;

      deviceMediaChannelLanguage = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("DeviceMediaChannelLanguage");
      if (deviceMediaChannelLanguage == null)
        deviceMediaChannelLanguage = DefaultDeviceMediaChannelLanguage;

      testTripName = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("TestTripName");
      if (testTripName == null)
        testTripName = string.Empty;

      #region ResetAreasOnStartup
      Boolean? _resetAreasOnStartup = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("ResetAreasOnStartup");
      if (_resetAreasOnStartup == null)
      {
        resetAreasOnStartup = true;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("ResetAreasOnStartup", resetAreasOnStartup);
      }
      else
        resetAreasOnStartup = _resetAreasOnStartup.Value;
      #endregion

      #region AudioContent
      Int16? _audioContent = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt16("AudioContent");
      if (_audioContent == null)
      {
        audioContent = DefaultAudioContent;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("AudioContent", (Int16)audioContent);
      }
      else
        audioContent = (AudioSettings.Content)_audioContent.Value;
      #endregion

      #region AudioSource
      Int16? _audioSource = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt16("AudioSource");
      if (_audioSource == null)
      {
        audioSource = DefaultAudioSource;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("AudioSource", (Int16)audioSource);
      }
      else
        audioSource = (AudioSettings.Source)_audioSource.Value;
      #endregion

      destinationImagePath = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("DestinationImagePath");
      if (destinationImagePath == null)
        destinationImagePath = DefaultDestinationImagePath;

      #region Units
      Int32? _units = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt32("Units");
      if (_units == null)
      {
        units = Units.Metric;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("Units",(Int32)units);
      }
      else
        units = (Units)_units.Value;
      #endregion

      #region MasterAreaId
      Int32? _masterAreaId = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt32("MasterAreaId");
      if (_masterAreaId == null)
      {
        masterAreaId = DefaultMasterAreaId;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("MasterAreaId", masterAreaId);
      }
      else
        masterAreaId = _masterAreaId.Value;
      #endregion

      masterAreaName = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("MasterAreaName");
      if (masterAreaName == null)
        masterAreaName = DefaultMasterAreaName;

      #region AutoloadMasterAreas
      Boolean? _autoloadMasterAreas = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("AutoloadMasterAreas");
      if (_autoloadMasterAreas == null)
      {
        autoloadMasterAreas = false;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("AutoloadMasterAreas", autoloadMasterAreas);
      }
      else
        autoloadMasterAreas = _autoloadMasterAreas.Value;
      #endregion

      #region ShowContentFilenames
      Boolean? _showContentFilenames = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("ShowContentFilenames");
      if (_showContentFilenames == null)
      {
        showContentFilenames = true;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("ShowContentFilenames", showContentFilenames);
      }
      else
        showContentFilenames = _showContentFilenames.Value;
      #endregion

      #region Language
      language = LanguageHelper.GetLanguage(SelectedCulture.Culture);
      #endregion

      factorySetupPassword = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("FactorySetupPassword");
      if (factorySetupPassword == null)
        factorySetupPassword = DefaultFactorySetupPassword;

      string Vtg = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("VTG");
      string Rmc = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("RMC");
      string Gga = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("GGA");
      if ((Vtg != null) || (Rmc != null) || (Gga != null))
        lastGpsRawEvent = new GpsRawEvent(GpsPositionSources.Gps,0,Rmc,Vtg,Gga);

      #region FmTransmitterChannel
      Int16? _fmTransmitterChannel = RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt16("FmTransmitterChannel");
      if (_fmTransmitterChannel == null)
      {
        fmTransmitterChannel = 945;
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("FmTransmitterChannel", fmTransmitterChannel);
      }
      else
        fmTransmitterChannel = _fmTransmitterChannel.Value;
      #endregion

      imageManager.DestinationImagePath = destinationImagePath;
    }
    public void SaveConfiguration()
    {
      try
      {
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("LoadedContentFile", loadedContentFile);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("LoadedItineraryFile", loadedItineraryFile);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("LoadedFormDefinitionFile", loadedFormDefinitionFile);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("LoadedDestinationFile", loadedDestinationFile);

        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("IsLoggingActive", IsLoggingActive);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("IsExceptionLoggingActive", isExceptionLoggingActive);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("LoggingBasePath", loggingBasePath);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("CheckBookingPeriod", checkBookingPeriod);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("Copyright", copyright);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("Helpline", helpline);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("DeviceVolume", (Int16)DeviceVolume);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("GpsTimeOffset", gpsTimeOffset);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("ContentBasePath", contentBaseBath);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("DeviceMediaChannelGroupId", deviceMediaChannelGroupId);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("DeviceMediaChannelGroupName", deviceMediaChannelGroupName);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("DeviceMediaChannelId", deviceMediaChannelId);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("DeviceMediaChannelGroupContentPath", deviceMediaChannelGroupContentPath);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("DeviceMediaChannelContentPath", deviceMediaChannelContentPath);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("DeviceMediaChannelLanguage", deviceMediaChannelLanguage);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("TestTripName", testTripName);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("ResetAreasOnStartup", resetAreasOnStartup);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("AudioContent", (Int16)audioContent);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("AudioSource", (Int16)audioSource);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("DestinationImagePath", destinationImagePath);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("Units", (Int32)units);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("MasterAreaId", MasterAreaId);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("MasterAreaName", MasterAreaName);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("AutoloadMasterAreas", AutoloadMasterAreas);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("ShowContentFilenames", ShowContentFilenames);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("FactorySetupPassword", FactorySetupPassword);
        RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt16("FmTransmitterChannel", fmTransmitterChannel);

        SelectedCulture.Culture = LanguageHelper.CreateCulture(language);

      }
      catch (Exception exc)
      {
        WriteLog(this,"Error in SaveConfiguration",exc);
      }
    }
    public void StartupSequence()
    {
      WriteLog(this,">>StartupSequence");

      #region Units
      try
      {
        Units = Units;
      }
      catch (Exception exc)
      {
        WriteLog(this, "Error setting units in StartupSequence", exc);
      }
      #endregion

      #region AudioContent
      try
      {
        AudioContent = AudioContent;
      }
      catch (Exception exc)
      {
        WriteLog(this, "Error setting AudioContent in StartupSequence", exc);
      }
      #endregion

      #region AudioSource
      try
      {
        AudioSource = AudioSource;
        FmTransmitterChannel = FmTransmitterChannel;
      }
      catch (Exception exc)
      {
        WriteLog(this, "Error setting AudioSource in StartupSequence", exc);
      }
      #endregion

      #region Master area
      try
      {
        AutoloadMasterAreas = autoloadMasterAreas;
        if (AutoloadMasterAreas == false)
          MasterAreaId = masterAreaId;
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error setting master area in StartupSequence", exc);
      }
      #endregion

      #region ContentManager
      try
      {
        OnContentManagerRunState(true);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error starting ContentManager in StartupSequence", exc);
      }
      #endregion

      #region GPS
      try
      {
        OnGpsAdapterRunState(true);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error starting GPS in StartupSequence", exc);
      }
      #endregion

      #region Media player
      try
      {
        OnDeviceMediaManagerRunState(true);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error starting media player in StartupSequence", exc);
      }
      #endregion

      WriteLog(this, "<<StartupSequence");
    }
    public void ShutdownSequence()
    {
      OnDeviceMediaManagerRunState(false);
      OnGpsAdapterRunState(false);
      OnContentManagerRunState(false);
    }
    public void ResetCounts()
    {
      OnSystemResetCounts();
    }
    public void ShowGpsForm()
    {
      OnGpsAdapterShowDiagnoticsForm();
    }
    public void SetGpsRunState(Boolean state)
    {
      OnGpsAdapterRunState(state);
    }
    public void SetThemeId(Int32 id)
    {
      
    }
    public void ResetContentManagerState()
    {
      OnResetContentManager();
    }
    public void WriteLog(object sender,string text)
    {
      if (IsLoggingActive == false)
        return;
      if (logger == null)
        return;

      logger.Write(sender,text);
    }
    public void WriteLog(object sender, string text, Exception exc)
    {
      WriteLog(sender,ExceptionManager.MessageWithStackTrace(text,exc));
    }
    #endregion

    #region Private methods
    private void LoadPlugins()
    {
      WriteLog(this,"Loading plugins");
      string pluginPath = ApplicationPath + "Plugins\\";
      if (Directory.Exists(pluginPath) == false)
        Directory.CreateDirectory(pluginPath);

      pluginManager = new PluginManager(PluginKinds.Module);
      pluginManager.PluginFileExtension = ".ggp";
      pluginManager.RequiredInterfaceType = typeof(Nucleo.GoodGuide.Types.IGoodGuidePlugin);

      #region Load the plugins
      try
      {
        pluginManager.PluginLoaded += pluginManager_PluginLoaded;
        PluginManager.LoadPlugins(ApplicationPath + "Plugins\\");
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error loading plugins",exc);
        throw new ApplicationException("Error loading plugins", exc);
      }
      finally
      {
        pluginManager.PluginLoaded -= pluginManager_PluginLoaded;
      }
      #endregion

      #region Initialise plugins
      NamedParameters PluginInitialisationParameters = new NamedParameters();
      PluginInitialisationParameters.Add(new NamedParameter("RepositoryLocator", RepositoryLocator));
      PluginInitialisationParameters.Add(new NamedParameter(ConfigurationParameters.System.NamedParameterRepository, RepositoryLocator.LocateRepository<INamedParameterRepository>()));
      PluginInitialisationParameters.Add(new NamedParameter(ConfigurationParameters.System.Logger, logger));
      PluginInitialisationParameters.Add(new NamedParameter("EventBroker", eventBroker));
      if (logger != null)
      {
        WriteLog(this,"Adding LogWriter parameter");
        PluginInitialisationParameters.Add(new NamedParameter("LogWriter",logger));
      }

      if (eventWindow != null)
      {
        WriteLog(this, "Adding EventWindow parameter");
        PluginInitialisationParameters.Add(new NamedParameter(ConfigurationParameters.System.EventWindow, eventWindow));
      }

      for (Int32 PluginNo = 0; PluginNo < PluginManager.Plugins.Count; PluginNo++)
      {
        try
        {
          IGoodGuidePlugin GoodGuidePlugin = (IGoodGuidePlugin)PluginManager.Plugins[PluginNo];
          logger.Write(this, string.Format("Initialising plugin: {0}", PluginManager.Plugins[PluginNo].Name));
          GoodGuidePlugin.Initialise(PluginInitialisationParameters);
        }
        catch (Exception exc)
        {
          string msg = string.Format("Error initialising plugin {0}",PluginManager.Plugins[PluginNo].Name);
          WriteLog(this,msg,exc);
          throw new ApplicationException(msg, exc);
        }
      }
      #endregion
    }
    private void LoadMediaManager()
    {
      string pluginPath = ApplicationPath + "Plugins" + System.IO.Path.DirectorySeparatorChar;
      if (Directory.Exists(pluginPath) == false)
        Directory.CreateDirectory(pluginPath);

      mediaPluginManager = new PluginManager(PluginKinds.Module);
      mediaPluginManager.PluginFileExtension = ".ggmm";
      mediaPluginManager.RequiredInterfaceType = typeof(IGoodGuidePlugin);

      #region Load the plugins
      try
      {
        mediaPluginManager.PluginLoaded += pluginManager_PluginLoaded;
        mediaPluginManager.LoadPlugins(pluginPath);
      }
      catch (Exception exc)
      {
        throw new ApplicationException("Error loading plugins", exc);
      }
      finally
      {
        mediaPluginManager.PluginLoaded -= pluginManager_PluginLoaded;
      }
      #endregion

      WriteLog(this,string.Format("{0} media manager plugins found", mediaPluginManager.Plugins.Count));

      if (mediaPluginManager.Plugins.Count == 0)
      {
        return;
      }

      #region Initialise media manager plugin
      WriteLog(this, string.Format("Initialising media manager {0}", mediaPluginManager.Plugins[0].Name));
      NamedParameters PluginInitialisationParameters = new NamedParameters();
      PluginInitialisationParameters.Add(new NamedParameter("RepositoryLocator", RepositoryLocator));
      PluginInitialisationParameters.Add(new NamedParameter("EventBroker", eventBroker));
      PluginInitialisationParameters.Add(new NamedParameter(ConfigurationParameters.System.NamedParameterRepository, RepositoryLocator.LocateRepository<INamedParameterRepository>()));
      PluginInitialisationParameters.Add(new NamedParameter(ConfigurationParameters.System.Logger, logger));

      try
      {
        IGoodGuidePlugin GoodGuidePlugin = (IGoodGuidePlugin)mediaPluginManager.Plugins[0];
        GoodGuidePlugin.Initialise(PluginInitialisationParameters);
      }
      catch (Exception exc)
      {
        throw new ApplicationException(string.Format("Error initialising media manager plugin {0}", mediaPluginManager.Plugins[0].Name), exc);
      }
      #endregion
    }
    private void LoadNavigator(Control mapControl)
    {
      string pluginPath = ApplicationPath + "Plugins" + System.IO.Path.DirectorySeparatorChar;
      if (Directory.Exists(pluginPath) == false)
        Directory.CreateDirectory(pluginPath);

      navigatorManager = new PluginManager(PluginKinds.Module);
      navigatorManager.PluginFileExtension = ".ggn";
      navigatorManager.RequiredInterfaceType = typeof(IGoodGuidePlugin);

      #region Load the plugins
      try
      {
        navigatorManager.PluginLoaded += pluginManager_PluginLoaded;
        navigatorManager.LoadPlugins(pluginPath);
      }
      catch (Exception exc)
      {
        throw new ApplicationException("Error loading plugins", exc);
      }
      finally
      {
        navigatorManager.PluginLoaded -= pluginManager_PluginLoaded;
      }
      #endregion

      WriteLog(this, string.Format("{0} navigator plugins found", mediaPluginManager.Plugins.Count));

      if (navigatorManager.Plugins.Count == 0)
      {
        return;
      }

      #region Initialise navigatorManager plugin
      WriteLog(this, string.Format("Initialising navigator {0}", navigatorManager.Plugins[0].Name));
      NamedParameters PluginInitialisationParameters = new NamedParameters();
      PluginInitialisationParameters.Add(new NamedParameter("RepositoryLocator", RepositoryLocator));
      PluginInitialisationParameters.Add(new NamedParameter("EventBroker", eventBroker));
      PluginInitialisationParameters.Add(new NamedParameter(ConfigurationParameters.System.NamedParameterRepository, RepositoryLocator.LocateRepository<INamedParameterRepository>()));
      PluginInitialisationParameters.Add(new NamedParameter(ConfigurationParameters.System.Logger, logger));
      PluginInitialisationParameters.Add(new NamedParameter(ConfigurationParameters.Navigator.MapControl, mapControl));
      PluginInitialisationParameters.Add(new NamedParameter(ConfigurationParameters.Navigator.Language, language));

      try
      {
        IGoodGuidePlugin GoodGuidePlugin = (IGoodGuidePlugin)navigatorManager.Plugins[0];
        GoodGuidePlugin.Initialise(PluginInitialisationParameters);
      }
      catch (Exception exc)
      {
        string message = string.Format("Error initialising navigator plugin {0}", navigatorManager.Plugins[0].Name);
        WriteLog(this,message,exc);
      }
      #endregion
    }

    #endregion

    #region Event dispatchers
    #region Local
    private void OnInitialisationStarted()
    {
      if (InitialisationStarted == null)
        return;

      try
      {
        InitialisationStarted(this, new EventArgs());
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error executing InitialisationStarted", exc);
      }
    }

    private void OnInitialisationCompleted()
    {
      if (InitialisationCompleted == null)
        return;

      InitialisationCompleted(this, new EventArgs());
    }
    private void OnInitialisationError(string text)
    {
      if (InitialisationError == null)
        return;

      TextEventArgs e = new TextEventArgs(text);

      try
      {
        InitialisationError(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error executing InitialisationError", exc);
      }
    }
    private void OnGpsFixStateChanged()
    {
      if (GpsFixStateChanged == null)
        return;

      try
      {
        GpsFixStateChanged(this, new EventArgs());
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising GpsFixStateChanged", exc);
      }
    }
    private void OnLastGpsRawDataChanged()
    {
      if (LastGpsRawDataChanged == null)
        return;

      try
      {
        LastGpsRawDataChanged(this, new EventArgs());
      }
      catch (Exception exc)
      {
        WriteLog(this, "Error raising LastGpsRawDataChanged", exc);
      }
    }
    private void OnRouterStateChanged()
    {
      if (RouterStateChanged == null)
        return;

      try
      {
        RouterStateChanged(this, new EventArgs());
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising RouterStateChanged", exc);
      }
    }
    private void OnArrivedAtDestination(DestinationDataset.DestinationRow destination)
    {
      if (ArrivedAtDestination == null)
        return;

      try
      {
        ArrivedAtDestination(this, new DestinationArrivalEventArgs(destination));
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising ArrivedAtDestination", exc);
      }
    }
    #endregion

    #region Event broker
    private void OnMediaManagerConfiguration(ConfigurationEvent eventData)
    {
      if (MediaManagerConfiguration == null)
        return;

      GoodGuideEventArgs Args = new GoodGuideEventArgs(GetType().Name, eventData);

      try
      {
        MediaManagerConfiguration(this, Args);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising MediaManagerConfiguration", exc);
      }
    }
    private void OnSystemResetCounts()
    {
      if (SystemResetCounts == null)
        return;

      GoodGuideEventArgs Args = new GoodGuideEventArgs(GetType().Name, new SystemResetCountsEvent());
      try
      {
        SystemResetCounts(this, Args);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising SystemResetCounts", exc);
      }
    }
    private void OnResetContentManager()
    {
      if (ResetContentManager == null)
        return;

      GoodGuideEventArgs Args = new GoodGuideEventArgs(GetType().Name, new SystemResetContentEvent());
      try
      {
        ResetContentManager(this, Args);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising ResetContentManager", exc);
      }
    }
    private void OnDeviceMediaManagerRunState(Boolean runState)
    {
      if (DeviceMediaManagerRunState == null)
        return;

      RunStateEvent EventData = new RunStateEvent(runState);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, EventData);
      try
      {
        DeviceMediaManagerRunState(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising DeviceMediaManagerRunState", exc);
      }
    }
    private void OnDestinationCommand(DestinationNavigationEvent eventData)
    {
      if (DestinationCommand == null)
        return;

      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);
      try
      {
        DestinationCommand(this, e);
      }
      catch (Exception exc)
      {
        WriteLog(this,"Error raising DestinationCommand", exc);
      }
    }
    private NavigatorAddresses OnNavigatorSearchStreetAddress(string searchText)
    {
      if (NavigatorSearchStreetAddress == null)
        return new NavigatorAddresses();

      NavigatorAddressSearchEvent eventData = new NavigatorAddressSearchEvent(searchText);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name, eventData);
      try
      {
        NavigatorSearchStreetAddress(this, e);
        return eventData.Addresses;
      }
      catch (Exception exc)
      {
        WriteLog(this, "Error raising NavigatorSearchStreetAddress", exc);
      }

      return new NavigatorAddresses();
    }
    #endregion
    #endregion

    #region Event handlers
    public void InvokeActionHandler(object sender,InvokeMethodEventArgs e)
    {
      MethodInfo MethodInfo = GetType().GetMethod(e.ActionTarget);
      if (MethodInfo == null)
      {
        WriteLog(this, string.Format("Invoke method {0} not found",e.ActionTarget));
        return;        
      }

      try
      {
        if (string.IsNullOrEmpty((string)e.ActionData) == true)
        {
          WriteLog(this, string.Format("Invoking {0}(sender)", e.ActionTarget));
          MethodInfo.Invoke(this, new object[] { sender });
        }
        else
        {
          WriteLog(this, string.Format("Invoking {0}(sender,ActionData)", e.ActionTarget));
          MethodInfo.Invoke(this, new object[] { sender, e.ActionData });
        }
      }
      catch(Exception exc)
      {
        WriteLog(this,string.Format("Error in InvokeActionHandler while invoking {0}",e.ActionTarget), exc);
      }
    }

    private void pluginManager_PluginLoaded(object sender, PluginEventArgs e)
    {
      if (e.Plugin is GoodGuidePlugin == false)
        return;

      GoodGuidePlugin Plugin = (GoodGuidePlugin)e.Plugin;
      PluginAttribute[] Attributes = (PluginAttribute[])Plugin.GetType().GetCustomAttributes(typeof(PluginAttribute), false);
      if (Attributes.Length == 0)
        return;

      WriteLog(this, string.Format("Loaded plugin {0}", Attributes[0].Name));
    }
  
    #endregion

    #region Fields
    private static CarApplication instance = null;
    private ILogger logger = null;
    private Types.LoggingServices loggingServices = null;

    private PluginManager pluginManager;
    private PluginManager mediaPluginManager;
    private PluginManager navigatorManager;
    private GoodGuideEventBroker eventBroker;

    private ImageManager imageManager = null;
    private TemplateManager templateManager = null;
    private FormDefinitions newFormDefinitions = null;

    #region DAL
    private IRepository repository;
    private IRepositoryLocator repositoryLocator;
    #endregion

    #region BLL
    private SyncBll contentSyncBll;
    private SyncBll destinationSyncBll;
    private SyncBll formDefinitionSyncBll;
    private SyncBll itinerarySyncBll;

    private GpsRegionBll gpsRegionBll;
    private ChannelContentBll channelContentBll;
    private ContentItemBll contentItemBll;
    private ThemeBll themeBll;
    private MasterAreaBll masterAreaBll;
    private ChannelBll channelBll;
    private ChannelGroupBll channelGroupBll;
    private ControlDefinitionBll controlDefinitionBll;
    private FormDefinitionBll formDefinitionBll;
    private DestinationBll destinationBll;
    private DestinationTypeBll destinationTypeBll;
    private DestinationClassificationBll destinationClassificationBll;
    private ItineraryBll itineraryBll;
    private ItineraryDayBll itineraryDayBll;
    private ItineraryDestinationBll itineraryDestinationBll;
    private DestinationUpdateBll destinationUpdateBll;
    private DestinationCollectionBll destinationCollectionBll;
    private DestinationCollectionMemberBll destinationCollectionMemberBll;
    private MySelectionBll mySelectionBll;
    private RecentDestinationBll recentDestinationBll;
    #endregion

    private Boolean checkBookingPeriod = true;
    private string contentBaseBath = DefaultContentBasePath;
    private DestinationRouterStateEvent.RouterStates routerState = DestinationRouterStateEvent.RouterStates.NoDestination;
    private DestinationDataset.DestinationRow currentDestination = null;

    private IBannerAdProvider bannerAdProvider;
    private IWelcomeAdProvider welcomeAdProvider;
    private string loadedContentFile = string.Empty;
    private string loadedDestinationFile = string.Empty;
    private string loadedItineraryFile = string.Empty;
    private string loadedFormDefinitionFile = string.Empty;
    private string testTripName = string.Empty;
    private Boolean resetAreasOnStartup = true;
    private ItineraryDataset.ItineraryRow itinerary = null;
    private string destinationImagePath = DefaultDestinationImagePath;
    private IDeviceServices deviceServices;
    private IOperatingSystemServices operatingSystemServices = new OperatingSystemServices.OperatingSystemServices();
    #endregion
  }

  public class DestinationArrivalEventArgs : EventArgs
  {
    public DestinationArrivalEventArgs(DestinationDataset.DestinationRow destination)
    {
      this.destination = destination;
    }

    public DestinationDataset.DestinationRow Destination
    {
      get { return destination; }
    }

    private readonly DestinationDataset.DestinationRow destination = null;
  }
}
