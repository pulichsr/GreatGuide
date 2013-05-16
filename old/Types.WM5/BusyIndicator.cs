using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.Types
{
  public class BusyIndicator:
    IBusyIndicator
  {
    #region IBusyIndicator
    public void Show()
    {
      Nucleo.WinCe.WaitCursor.Show(true);
    }
    public void Hide()
    {
      Nucleo.WinCe.WaitCursor.Show(false);
    }

    #endregion
  }
}