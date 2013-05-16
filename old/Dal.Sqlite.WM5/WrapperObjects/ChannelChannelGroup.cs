using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  public class ChannelChannelGroup
  {
    public ChannelChannelGroup()
    {
    }
    public ChannelChannelGroup(DataObjects.ChannelChannelGroup channelChannelGroup)
    {
      this.dataObject = channelChannelGroup;
    }

    public DataObjects.ChannelChannelGroup DataObject
    {
      get { return dataObject; }
    }

    #region Properties

    [DbAutoColumnName]
    [DbPrimaryKey]
    public int? ChannelGroupId
    {
      get { return dataObject.ChannelGroupId; }
      set { dataObject.ChannelGroupId = value; }
    }

    [DbAutoColumnName]
    public string ChannelGroupName
    {
      get { return dataObject.ChannelGroupName; }
      set { dataObject.ChannelGroupName = value; }
    }

    [DbAutoColumnName]
    public int? ChannelId
    {
      get { return dataObject.ChannelId; }
      set { dataObject.ChannelId = value; }
    }

    [DbAutoColumnName]
    public string ChannelContentPath
    {
      get { return dataObject.ChannelContentPath; }
      set { dataObject.ChannelContentPath = value; }
    }

    [DbAutoColumnName]
    public string ChannelLanguage
    {
      get { return dataObject.ChannelLanguage; }
      set { dataObject.ChannelLanguage = value; }
    }

    [DbAutoColumnName]
    public string ChannelGroupContentPath
    {
      get { return dataObject.ChannelGroupContentPath; }
      set { dataObject.ChannelGroupContentPath = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.ChannelChannelGroup dataObject = new DataObjects.ChannelChannelGroup();

    #endregion
  }

  public class ChannelChannelGroups : List<ChannelChannelGroup>
  {
    public DataObjects.ChannelChannelGroups DataObjects
    {
      get
      {
        DataObjects.ChannelChannelGroups objects = new DataObjects.ChannelChannelGroups();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}