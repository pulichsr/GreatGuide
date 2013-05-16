using System;

namespace Nucleo.GoodGuide.Types
{
  public class ChannelMedia
  {
    public ChannelMedia(byte channel,string mediaFilename)
    {
      Channel = channel;
      MediaFilename = mediaFilename;
    }

    public Byte Channel = 0;
    public string MediaFilename = string.Empty;
  }
}
