using System.Collections.Generic;

namespace Nucleo.GoodGuide.Types
{
  public class NavigatorAddress
  {
    public NavigatorAddress()
    {
    }
    public NavigatorAddress(string displayText,object data)
    {
      this.displayText = displayText;
      this.data = data;
    }

    public string DisplayText
    {
      get { return displayText; }
      set { displayText = value; }
    }
    public object Data
    {
      get { return data; }
      set { data = value; }
    }

    private string displayText;
    private object data;
  }

  public class NavigatorAddresses: List<NavigatorAddress>
  {}
}
