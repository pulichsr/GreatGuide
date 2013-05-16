namespace Nucleo.GoodGuide.Types.Events
{
  public static class EventTopics
  {
    public class System
    {
      public const string ResetCounts = "topic://System.ResetCounts";
      public const string LoggingState = "topic://System.LoggingState";
      public const string ExceptionLoggingState = "topic://System.ExceptionLoggingState";
      public const string StopAllContent = "topic://System.StopAllContent";
    }

    public class UserInterface
    {
      public const string GetText = "topic://UserInterface.GetText";
    }

    public class GpsAdapter
    {
      public const string GpsPosition = "topic://GpsAdapter.GpsPosition";
      public const string GpsRawData = "topic://GpsAdapter.GpsRawData";
      public const string Configuration = "topic://GpsAdapter.Configuration";
      public const string RunState = "topic://GpsAdapter.RunState";
      public const string RequestRunState = "topic://GpsAdapter.RequestRunState";
      public const string FixState = "topic://GpsAdapter.FixState";
      public const string RequestCurrentFixState = "topic://GpsAdapter.RequestCurrentFixState";
      public const string ShowDiagnoticsForm = "topic://GpsAdapter.ShowDiagnosticsForm";
    }

    public class MasterAreaTrigger
    {
      public const string RunState = "topic://MasterAreaTrigger.RunState";
      public const string Configuration = "topic://MasterAreaTrigger.Configuration";
      public const string MasterAreaEnter = "topic://MasterAreaTrigger.MasterAreaEnter";
      public const string MasterAreaExit = "topic://MasterAreaTrigger.MasterAreaExit";
    }

    public class RegionTrigger
    {
      public const string Configuration = "topic://RegionTrigger.Configuration";
      public const string RegionEnter = "topic://RegionTrigger.RegionEnter";
      public const string RegionExit = "topic://RegionTrigger.RegionExit";
      public const string ActiveRegions = "topic://RegionTrigger.ActiveRegions";
    }

    public class TripRecorder
    {
      public const string Configuration = "topic://TripRecorder.Configuration";
      public const string Control = "topic://TripRecorder.Control";
      public const string StateChange = "topic://TripRecorder.StateChange";
      public const string RequestCurrentState = "topic://TripRecorder.RequestCurrentState";
      public const string ShowForm = "topic://TripRecorder.ShowForm";
    }

    public class ContentManager
    {
      public const string MediaControl = "topic://ContentManager.MediaControl";
      public const string Reset = "topic://ContentManager.Reset";
      public const string RunState = "topic://ContentManager.RunState";
      public const string ContentControl = "topic://ContentManager.ContentControl";
      public const string RequestCurrentFillerContent = "topic://ContentManager.RequestCurrentFillerContent";
    }

    public class MediaPlayer
    {
      public const string RunState = "topic://MediaPlayer.RunState";
      public const string MediaStateChange = "topic://MediaPlayer.MediaStateChange";
      public const string Configuration = "topic://MediaPlayer.Configuration";
      public const string ConnectedStateChange = "topic://MediaPlayers.ConnectedStateChange";
      public const string MediaTypeControl = "topic://MediaPlayers.MediaTypeControl";
      public const string VideoSizeControl = "topic://MediaPlayers.VideoSizeControl";
    }

    public class EboxMediaManager
    {
      public const string Configuration = "topic://EboxMediaManager.Configuration";
      public const string Poll = "topic://EboxMediaManager.Poll";
      public const string RunState = "topic://EboxMediaManager.RunState";
      public const string ActiveState = "topic://EboxMediaManager.ActiveState";
    }

    public class EventLogger
    {
      public const string Configuration = "topic://EventLogger.Configuration";
      public const string RunState = "topic://EventLogger.RunState";
      public const string RequestRunState = "topic://EventLogger.RequestRunState";
      public const string ShowControlForm = "topic://GpsAdapter.ShowControlForm";
    }

    public class Navigator
    {
      public const string NavigatorCommand = "topic://Navigator.NavigatorCommand";
      public const string ShowDialog = "topic://Navigator.ShowDialog";
      public const string RouterState = "topic://Navigator.RouterState";
      public const string GetPoiCategoryList = "topic://Navigator.GetPoiCategoryList";
      public const string GetPoiSubcategoryList = "topic://Navigator.GetPoiSubcategoryList";
      public const string GetPoiDestinationList = "topic://Navigator.GetPoiDestinationList";
      public const string SetLanguage = "topic://Navigator.SetLanguage";
      public const string SetUnits = "topic://Navigator.SetUnits";
      public const string GetAddressOfPosition = "topic://Navigator.GetAddressOfPosition";
      public const string SearchStreetAddress = "topic://Navigator.SearchStreetAddress";
      public const string MapControl = "topic://Navigator.MapControl";
      public const string BeforeNotification = "topic://Navigator.BeforeNotification";
      public const string AfterNotification = "topic://Navigator.AfterNotification";
      public const string IsNotificationsActive = "topic://Navigator.IsNotificationsActive";
      public const string Directions = "topic://Navigator.Directions";
    }

    public class DestinationManager
    {
      public const string DestinationCommand = "topic://DestinationManager.DestinationCommand";
      public const string DestinationRouterState = "topic://DestinationManager.DestinationRouterState";
    }
  }
}