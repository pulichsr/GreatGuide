namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class NavigatorAddressSearchEvent : ApplicationEvent
  {
    public NavigatorAddressSearchEvent(string searchText)
    {
      this.searchText = searchText;
    }

    public string SearchText
    {
      get { return searchText; }
      set { searchText = value; }
    }
    public NavigatorAddresses Addresses
    {
      get { return addresses; }
      set { addresses = value; }
    }

    private string searchText;
    private NavigatorAddresses addresses = new NavigatorAddresses();
  }
}
