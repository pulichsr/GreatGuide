using System;
using System.Collections.Generic;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class ChannelChannelGroup
  {
    #region Fields
    // Fields are public for performance on CF2
    public Int32? ChannelGroupId;
    public string ChannelGroupName;
    public Int32? ChannelId;
    public string ChannelContentPath;
    public string ChannelLanguage;
    public string ChannelGroupContentPath;
    #endregion
  }

  public class ChannelChannelGroups : List<ChannelChannelGroup>
  { 
  }
}