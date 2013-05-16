using System;
using System.Threading;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Events.ControlEvents;

namespace Nucleo.GoodGuide.MockSerialGpsAdapter
{
  public class MockSerialGps
  {
    public const Int32 TimerInterval = 1000;

    #region Event broker
    [EventPublisher(EventTopics.GpsAdapter.RunState)]
    public event EventHandler<GoodGuideEventArgs> RunStateChanged;

    [EventPublisher(EventTopics.GpsAdapter.GpsRawData)]
    public event EventHandler<GoodGuideEventArgs> NewGpsRawData;

    [EventSubscriber(EventTopics.GpsAdapter.RunState)]
    public void RunStateHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is RunStateEvent)
      {
        RunStateEvent Event = (RunStateEvent)e.EventData;
        if (Event.IsRunning == true)
          Start();
        else 
          Stop();
      }
    }

    [EventSubscriber(EventTopics.GpsAdapter.RequestRunState)]
    public void RequestRunStateHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is RunStateEvent)
      {
        RunStateEvent Event = (RunStateEvent)e.EventData;
        Event.IsRunning = IsRunning;
      }
    }

    #endregion

    public bool IsRunning
    {
      get { return isRunning; }
      set { isRunning = value; }
    }

    public void Initialise()
    {
    }
    public void Finalise()
    {
    }

    public void Start()
    {
      string tripFilename = string.Format("{0}.rggt", Path.AssemblyNameWithPath(this));

      try
      {
        data = Nucleo.IO.File.ReadAllLines(tripFilename);
        if (data.Length == 0)
          data = null;
      }
      catch
      {
        data = null;
      }

      if (IsRunning == true)
        return;
      if (data == null)
        return;

      try
      {
        lock(timerLock)
        {
          if (timer != null)
            return;

          timer = new Timer(TimerElapsed, null, TimerInterval, TimerInterval);
          IsRunning = true;
          stopped = false;

          OnRunStateChanged(IsRunning);
        }
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error starting timer",exc);
      }
    }
    public void Stop()
    {
      if (IsRunning == false)
        return;

      try
      {
        lock (timerLock)
        {
          stopped = true;
          IsRunning = false;
          OnRunStateChanged(IsRunning);

          if (timer == null)
            return;

          timer.Dispose();
          timer = null;
        }
      }
      catch(Exception exc)
      {
        Logger.Write(this,"Error stopping timer", exc);
      }
    }

    private void TimerElapsed(object state)
    {
      lock(timerLock)
      {
        if (timer != null)
        {
          timer.Dispose();
          timer = null;
        }

        if (stopped == true)
          return;
      }

      #region Read line & send data
      if (lineNo >= data.Length)
        lineNo = 0;

      try
      {
        string line = data[lineNo];
        string[] fields = line.Split('|');
        if (fields.Length == 3)
        {
          eventSequence++;
          GpsRawEvent rawEvent = new GpsRawEvent(GpsPositionSources.Gps,eventSequence,fields[0],fields[1],fields[2]);

          OnGpsRawData(rawEvent);
          lineNo++;
        }
      }
      catch(Exception exc)
      {
        Logger.Write(this,"Error in TimerElapsed",exc);
      }
      #endregion

      lock(timerLock)
      {
        timer = new Timer(TimerElapsed, null, TimerInterval, TimerInterval);
      }
    }

    #region Event dispatchers
    private void OnRunStateChanged(Boolean state)
    {
      if (RunStateChanged == null)
        return;

      RunStateEvent eventData = new RunStateEvent(state);
      GoodGuideEventArgs e = new GoodGuideEventArgs(GetType().Name,eventData);

      try
      {
        RunStateChanged(this,e);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error raising RunStateChanged",exc);
      }
    }
    private void OnGpsRawData(GpsRawEvent gpsRawEvent)
    {
      if (NewGpsRawData == null)
        return;

      try
      {
        GoodGuideEventArgs Event = new GoodGuideEventArgs(GetType().Name,gpsRawEvent);
        NewGpsRawData(this, Event);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error raising NewGpsRawData", exc);
      }
    }
    #endregion
 
    public readonly LoggingHelper Logger = new LoggingHelper();

    private Boolean isRunning = false;
    private string[] data = null;
    private Int32 lineNo = 0;
    private Boolean stopped = false;
    private readonly object timerLock = new object();
    private Timer timer = null;
    private Int32 eventSequence = 0;
  }
}