
namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class VideoSizeEvent: ApplicationEvent
  {
    public VideoSizeEvent(VideoSize videoSize)
    {
      this.videoSize = videoSize;
    }

    public VideoSize VideoSize
    {
      get { return videoSize; }
    }

    private readonly VideoSize videoSize;
  }
}
