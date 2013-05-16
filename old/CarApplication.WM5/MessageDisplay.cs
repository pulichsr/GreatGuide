using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.CarApplication
{
  public class MessageDisplay:
    IMessageDisplay
  {
    #region IMessageDisplay
    public void Display(string message)
    {
      frmMessage.ShowText(message);
    }
    #endregion
  }
}
