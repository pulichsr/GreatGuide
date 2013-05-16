namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class ShowNavigatorDialogEvent : ApplicationEvent
  {
    public enum Dialogs
    {
      Address,
      PointsOfInterest,
      Configuration,
    }

    public ShowNavigatorDialogEvent()
    {      
    }
    public ShowNavigatorDialogEvent(Dialogs dialog)
    {
      Dialog = dialog;
    }

    public Dialogs Dialog;
  }
}
