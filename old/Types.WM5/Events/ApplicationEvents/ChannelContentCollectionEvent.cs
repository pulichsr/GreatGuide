using Nucleo.GoodGuide.Datasets;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class ChannelContentCollectionEvent : ApplicationEvent
  {
    public ChannelContentCollectionEvent() 
    {}
    public ChannelContentCollectionEvent(ChannelContentCollection collection) 
    {
      this.collection = collection;
    }

    public ChannelContentCollection Collection
    {
      get { return collection; }
      set { collection = value; }
    }

    private ChannelContentCollection collection;

  }
}
