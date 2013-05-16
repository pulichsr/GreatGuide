namespace Nucleo.GoodGuide.Types
{
  public static class ConfigurationParameters
  {
    public static class System
    {
      public const string LoggingState = "System.LoggingState";
      public const string ExceptionLoggingState = "System.ExceptionLoggingState";
      public const string Logger = "System.Logger";
      public const string EventWindow = "System.EventWindow";
      public const string NamedParameterRepository = "System.NamedParameterRepository";
      public const string AudioTestFilename = "System.AudioTestFilename";
      public const string StartupAudioFilename = "System.StartupAudioFilename";
      public const string AutoStartTripName = "System.AutoStartTripName";
    }


    public static class Gps
    {
      public const string FixQualityThreshold = "Gps.FixQualityThreshold";
      public const string RunState = "Gps.RunState";
      public const string TimeOffset = "Gps.TimeOffset";
      public const string PortName = "Gps.PortName";
      public const string BaudRate = "Gps.BaudRate";
      public const string SetSystemTimeOnValidFix = "Gps.SetSystemTimeOnValidFix";
      public const string IsLoggingActive = "Gps.IsLoggingActive";
    }

    public static class MediaManager
    {
      public const string ContentBasePath = "MediaManager.ContentBasePath";
      public const string ChannelGroupContentPath = "MediaManager.ChannelGroupContentPath";
      public const string ChannelContentPath = "MediaManager.ContentPath";
      public const string ChannelGroupId = "MediaManager.ChannelGroupId";
      public const string Volume = "MediaManager.Volume";
    }
    
    public static class TripRecorder
    {
      public const string PlaybackInterval = "TripRecorder.PlaybackInterval";
      public const string TripBasePath = "TripRecorder.TripBasePath";
    }

    public static class EventLogger
    {
      public const string RunState = "EventLogger.RunState";
    }

    public static class Navigator
    {
      public const string MapControl = "Navigator.MapControl";
      public const string Language = "Navigator.Language";
    }
  }
}
