using System;
using System.IO;
using System.Threading;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.GpsRawDataRecorder
{
  public class GpsRawDataRecorder
  {
    public const Int16 DefaultPlaybackInterval = 1000;

    public GpsRawDataRecorder(INamedParameterRepository parameterRepository)
    {
      this.parameterRepository = parameterRepository;
    }

    #region Eventbroker
    [EventPublisher(EventTopics.TripRecorder.StateChange)]
    public event EventHandler<GoodGuideEventArgs> StateChanged;

    [EventPublisher(EventTopics.GpsAdapter.GpsRawData)]
    public event EventHandler<GoodGuideEventArgs> GpsRawData;

    [EventSubscriber(EventTopics.TripRecorder.Control)]
    public void ControlCommandHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is TripRecorderControlEvent == false)
        return;

      TripRecorderControlEvent EventData = (TripRecorderControlEvent)e.EventData;

      switch(EventData.State)
      {
        case TripRecorderStates.Paused:
          Pause(EventData);
          break;
        case TripRecorderStates.Playing:
          startPoint = EventData.StartPoint;
          stopPoint = EventData.StopPoint;
          loopPlayback = EventData.LoopPlayback;
          Play(EventData);
          break;
        case TripRecorderStates.Recording:
          Record(EventData);
          break;
        case TripRecorderStates.Stopped:
          Stop(EventData);
          break;
      }
    }

    [EventSubscriber(EventTopics.TripRecorder.RequestCurrentState)]
    public void RequestCurrentStateHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is TripRecorderStateEvent == false)
        return;

      ((TripRecorderStateEvent)e.EventData).OldState = State;
      ((TripRecorderStateEvent)e.EventData).State = State;
      ((TripRecorderStateEvent)e.EventData).Message = CurrentTripName;
    }

    [EventSubscriber(EventTopics.GpsAdapter.GpsRawData)]
    public void GpsRawDataHandler(object sender, GoodGuideEventArgs e)
    {
      if (State != TripRecorderStates.Recording)
        return;

      if (e.EventData is GpsRawEvent == false)
        return;

      if (fileWriter == null)
        return;

      GpsRawEvent EventData = (GpsRawEvent)e.EventData;

      fileWriter.WriteLine(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",EventData.RmcData,EventData.VtgData,EventData.GgaData,EventData.UserField1,EventData.UserField2,EventData.UserField3,EventData.UserField4,EventData.UserField5));
      recordedFixCount++;
      
      if (recordedFixCount % RecordingCommitCycle == 0)
      {
        fileWriter.Flush();
        recordedFixCount = 0;
      }
    }
    #endregion

    public TripRecorderStates State
    {
      get
      {
        lock (stateLock)
        {
          return state;
        }
      }
    }
    public string CurrentTripName
    {
      get
      {
        lock (currentTripLock)
        {
          return currentTripName;
        }
      }
      set
      {
        lock (currentTripLock)
        {
          currentTripName = value == null ? null : value.Trim();
        }
      }
    }

    public void Initialise()
    {
      LoadConfiguration();
    }
    public void Finalise()
    {
    }

    public void Play(TripRecorderControlEvent eventData)
    {
      Logger.Write(this,string.Format("Requesting to play trip: {0}",eventData.TripName));
      string tripName = eventData.TripName.Trim();

      #region Invalid trip name
      if (string.IsNullOrEmpty(tripName) == true)
      {
        Logger.Write(this, "Invalid or missing trip name");
        eventData.CanAction = false;
        eventData.Message = "Invalid trip name";
        return;
      }
      #endregion

      string tripFilename = GetTripFilename(tripName);
      Logger.Write(this, string.Format("Trip filename: {0}", tripFilename));

      #region Trip file not found
      if (System.IO.File.Exists(tripFilename) == false)
      {
        Logger.Write(this, string.Format("Trip filename {0} not found", tripFilename));
        eventData.CanAction = false;
        eventData.Message = "Trip file not found";
        return;
      }
      #endregion

      eventData.CanAction = true;
      eventData.Message = string.Empty;

      switch (State)
      {
        case TripRecorderStates.Stopped:
          try
          {
            CurrentTripName = tripName;

            CreatePlayThread();
            SetState(TripRecorderStates.Playing, CurrentTripName);
          }
          catch
          {
            return;
          }
          break;
        case TripRecorderStates.Paused:
          SetState(TripRecorderStates.Playing, CurrentTripName);
          break;
      }
    }
    public void Pause(TripRecorderControlEvent eventData)
    {
      Pause();

      eventData.CanAction = true;
      eventData.Message = string.Empty;
    }
    public void Pause()
    {
      switch (State)
      {
        case TripRecorderStates.Playing:
          SetState(TripRecorderStates.Paused, CurrentTripName);
          break;
      }
    }
    public void Stop(TripRecorderControlEvent eventData)
    {
      Stop();

      eventData.CanAction = true;
      eventData.Message = string.Empty;
    }
    public void Stop()
    {
      switch (State)
      {
        case TripRecorderStates.Playing:
          try
          {
            stoppedEvent.Reset();
            stopRequestedEvent.Set();

            Boolean stopped = stoppedEvent.WaitOne(5000,false);
            if (stopped == false)
            {
              backgroundThread.Abort();
            }

            SetState(TripRecorderStates.Stopped, CurrentTripName);

            tripFileReader.Close();

            CurrentTripName = null;
          }
          catch
          {
            return;
          }
          break;
        case TripRecorderStates.Recording:
          try
          {
            SetState(TripRecorderStates.Stopped, CurrentTripName);

            fileWriter.Flush();
            fileWriter.Close();
            fileWriter = null;

            CurrentTripName = null;
          }
          catch
          {
            return;
          }
          break;
      }
    }
    public void Record(TripRecorderControlEvent eventData)
    {
      string tripName = eventData.TripName.Trim();

      #region Invalid trip name
      if (string.IsNullOrEmpty(tripName) == true)
      {
        eventData.CanAction = false;
        eventData.Message = "Invalid trip name";
        return;
      }
      #endregion

      string tripFilename = GetTripFilename(tripName);

      #region Trip file already exists
      if (System.IO.File.Exists(tripFilename) == true)
      {
        eventData.CanAction = false;
        eventData.Message = "Trip file already exists. Please use another name";
        return;
      }
      #endregion

      eventData.CanAction = true;
      eventData.Message = string.Empty;

      switch (State)
      {
        case TripRecorderStates.Stopped:
          try
          {
            StartRecording(tripName);
          }
          catch
          {
            return;
          }
          break;
      }
    }

    protected void SetState(TripRecorderStates newState,string message)
    {
      TripRecorderStates OldState = state;

      lock (stateLock)
      {
        state = newState;
      }

      OnStateChanged(OldState,State,message);
    }

    private void LoadConfiguration()
    {
      try
      {
        playbackInterval = parameterRepository.GetInt16(ConfigurationParameters.TripRecorder.PlaybackInterval) ?? DefaultPlaybackInterval;
        tripBasePath = parameterRepository.GetString(ConfigurationParameters.TripRecorder.TripBasePath);
        if (string.IsNullOrEmpty(tripBasePath) == true)
          tripBasePath = string.Format("{0}Trips{1}", Path.ExecutablePath, System.IO.Path.DirectorySeparatorChar);
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error loading configuration", exc);

        playbackInterval = DefaultPlaybackInterval;
        string.Format("{0}Trips{1}", Path.ExecutablePath, System.IO.Path.DirectorySeparatorChar);
      }
    }

    private void CreatePlayThread()
    {
      stopRequestedEvent.Reset();
      ThreadStart ThreadStart = PlaybackThread;
      backgroundThread = new Thread(ThreadStart);
      backgroundThread.Name = "GpsRawDataRecorder.PlaybackThread";
      backgroundThread.Start();
    }
    private void PlaybackThread()
    {
      Logger.Write(this, "Starting trip playback thread");
      try
      {
        #region Initialisation
        string tripFilename = GetTripFilename(CurrentTripName);
        Logger.Write(this, string.Format("Opening trip file: {0}", tripFilename));
        tripFileReader = new StreamReader(tripFilename);
        #endregion

        #region Read up to start point
        string line;
        Int32 PointNo = 0;
        if (startPoint != TripRecorderControlEvent.UndefinedPoint)
        {
          for (PointNo = 0;PointNo < startPoint;PointNo++)
          {
            line = tripFileReader.ReadLine();

            #region If reading reaches end of file before start point, cleanup & exit
            if (line == null)
            {
              tripFileReader.Close();

              SetState(TripRecorderStates.Stopped,CurrentTripName);
              CurrentTripName = null;

              return;
            }
            #endregion
          }
        }
        #endregion

        Boolean stopRequested;
        while (true)
        {
          #region Main loop

          #region Check if stop signalled
          Logger.Write(this, "Checking if stop is signalled");
          stopRequested = stopRequestedEvent.WaitOne(playbackInterval,false);
          if (stopRequested == true)
          {
            Logger.Write(this, "Stop was signalled");
            break;
          }

          #endregion

          if (State != TripRecorderStates.Playing)
          {
            Logger.Write(this, "State != Playing");
            continue;
          }

          #region Read line & dispatch raw GPS event

          #region Read line
          Logger.Write(this, "Reading line");
          line = tripFileReader.ReadLine();
          if (line == null)
          {
            Logger.Write(this, "End of file");
            break;
          }

          if (line.Trim() == string.Empty)
            continue;
          #endregion

          string gga;
          string rmc;
          string vtg;
          string userField1 = string.Empty;
          string userField2 = string.Empty;
          string userField3 = string.Empty;
          string userField4 = string.Empty;
          string userField5 = string.Empty;

          #region Parse line
          try
          {
            string[] Fields = line.Split('|');
            if ((Fields.Length != 3) && (Fields.Length != 8))
              continue;

            rmc = Fields[0];
            vtg = Fields[1];
            gga = Fields[2];

            if (Fields.Length == 8)
            {
              userField1 = Fields[3];
              userField2 = Fields[4];
              userField3 = Fields[5];
              userField4 = Fields[6];
              userField5 = Fields[7];
            }
          }
          catch (Exception exc)
          {
            Logger.Write(this, string.Format("Error parsing line {0}", line), exc);
            continue;
          }
          #endregion

          eventSequence++;
          GpsRawEvent Event = new GpsRawEvent(GpsPositionSources.TripRecorder,eventSequence,rmc,vtg,gga,userField1,userField2,userField3,userField4,userField5);

          OnGpsRawData(Event);

          PointNo++;

          if ((stopPoint != TripRecorderControlEvent.UndefinedPoint) && (PointNo >= stopPoint))
          {
            Logger.Write(this, "Stop point reached");
            break;
          }
          #endregion

          #endregion
        }

        #region Close file reader
        tripFileReader.Close();
        #endregion

        if ((loopPlayback == true) && (stopRequested == false))
        {
          CreatePlayThread();
          return;
        }
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error in GpsRawDataRecorder.PlaybackThread", exc);
      }

      stoppedEvent.Set();
      SetState(TripRecorderStates.Stopped, CurrentTripName);
      CurrentTripName = null;
    }

    private void StartRecording(string tripName)
    {
      string tripFilename = GetTripFilename(tripName);
      fileWriter = new StreamWriter(tripFilename,false);

      CurrentTripName = tripName;

      SetState(TripRecorderStates.Recording, CurrentTripName);
    }

    private string GetTripFilename(string tripName)
    {
      return string.Format("{0}{1}.rggt", tripBasePath, tripName);
    }

    #region Event dispatchers
    private void OnStateChanged(TripRecorderStates oldState, TripRecorderStates newState, string message)
    {
      if (StateChanged == null)
        return;

      TripRecorderStateEvent Event = new TripRecorderStateEvent(oldState, newState, message);
      GoodGuideEventArgs EventArgs = new GoodGuideEventArgs("TripRecorder", Event);

      try
      {
        StateChanged(this, EventArgs);
      }
      catch
      {
      }
    }
    private void OnGpsRawData(GpsRawEvent gpsRawData)
    {
      if (GpsRawData == null)
        return;

      GoodGuideEventArgs EventArgs = new GoodGuideEventArgs("GpsRawDataRecorder", gpsRawData);

      try
      {
        GpsRawData(this, EventArgs);
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error raising GpsRawData", exc);
      }
    }
    #endregion

    #region Fields
    public readonly  LoggingHelper Logger = new LoggingHelper();
    private readonly INamedParameterRepository parameterRepository = null;
    private Thread backgroundThread = null;
    private TextReader tripFileReader = null;
    private TextWriter fileWriter = null;
    private Int16 playbackInterval = DefaultPlaybackInterval;
    private Int32 eventSequence = 0;
    private Int16 recordedFixCount = 0;
    private string tripBasePath = string.Format("{0}Trips{1}", Path.ExecutablePath,System.IO.Path.DirectorySeparatorChar);
    private Int32 startPoint = TripRecorderControlEvent.UndefinedPoint;
    private Int32 stopPoint = TripRecorderControlEvent.UndefinedPoint;
    private Boolean loopPlayback = false;
    private ManualResetEvent stopRequestedEvent = new ManualResetEvent(false);
    private ManualResetEvent stoppedEvent = new ManualResetEvent(false);

    private string currentTripName = string.Empty;
    public object currentTripLock = new object();

    private TripRecorderStates state = TripRecorderStates.Stopped;
    public object stateLock = new object();

    private const Int16 RecordingCommitCycle = 100;
    #endregion
  }
}
