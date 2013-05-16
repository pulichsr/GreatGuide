
using Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class WaypointData
  {
    #region Fields
    // Fields are public for performance on CF2

    public MasterAreas MasterAreas = new MasterAreas();
    public Themes Themes = new Themes();
    public Channels Channels = new Channels();
    public ChannelGroups ChannelGroups = new ChannelGroups();
    public ChannelContents ChannelContents = new ChannelContents();
    public ContentItems ContentItems = new ContentItems();
    public GpsRegions GpsRegions = new GpsRegions();
    #endregion
  }
}
