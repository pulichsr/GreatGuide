using System;
namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public enum TripRecorderStates
  {
    Stopped,
    Recording,
    Playing,
    Paused
  }

  public class TripRecorderControlEvent : ApplicationEvent
  {
    public const Int32 UndefinedPoint = -1;

    public TripRecorderControlEvent(string tripName, TripRecorderStates state) : 
      this(tripName,state,UndefinedPoint,UndefinedPoint,false)
    {
    }
    public TripRecorderControlEvent(string tripName,TripRecorderStates state,Int32 startPoint,Int32 stopPoint,Boolean loopPlayback)
    {
      this.tripName = tripName;
      this.state = state;
      this.startPoint = startPoint;
      this.stopPoint = stopPoint;
      this.loopPlayback = loopPlayback;
    }

    public string TripName
    {
      get { return tripName; }
    }
    public TripRecorderStates State
    {
      get { return state; }
    }

    public Int32 StartPoint
    {
      get { return startPoint; }
    }
    public Int32 StopPoint
    {
      get { return stopPoint; }
    }
    public Boolean CanAction
    {
      get { return canAction; }
      set { canAction = value; }
    }
    public string Message
    {
      get { return message; }
      set { message = value; }
    }
    public Boolean LoopPlayback
    {
      get { return loopPlayback; }
    }


    private readonly string tripName;
    private readonly TripRecorderStates state;
    private readonly Int32 startPoint;
    private readonly Int32 stopPoint;
    private readonly Boolean loopPlayback;
    private Boolean canAction = false;
    private string message = string.Empty;
  }
}