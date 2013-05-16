using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_CHANNEL_GROUP")]
  public class ChannelGroup
  {
    public ChannelGroup()
    {
    }
    public ChannelGroup(DataObjects.ChannelGroup channelGroup)
    {
      this.dataObject = channelGroup;
    }

    public DataObjects.ChannelGroup DataObject
    {
      get { return dataObject; }
    }

    #region Properties

    [DbAutoColumnName]
    [DbPrimaryKey]
    public Int32? Id
    {
      get { return dataObject.Id; }
      set { dataObject.Id = value; }
    }

    [DbAutoColumnName]
    public string Name
    {
      get { return dataObject.Name; }
      set { dataObject.Name = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.ChannelGroup dataObject = new DataObjects.ChannelGroup();
    
    #endregion

  }

  public class ChannelGroups : List<ChannelGroup>
  {
    public DataObjects.ChannelGroups DataObjects
    {
      get
      {
        DataObjects.ChannelGroups objects = new DataObjects.ChannelGroups();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}