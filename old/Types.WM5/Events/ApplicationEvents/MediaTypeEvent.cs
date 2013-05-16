
namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class MediaTypeEvent: ApplicationEvent
  {
    public MediaTypeEvent(MediaTypes mediaType)
    {
      this.mediaType = mediaType;
    }

    public MediaTypes MediaType
    {
      get { return mediaType; }
    }

    private readonly MediaTypes mediaType;
  }
}
