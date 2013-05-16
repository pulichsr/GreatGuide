using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_CHANNEL")]
  public class Channel
  {
    public Channel()
    {
    }
    public Channel(DataObjects.Channel channel)
    {
      this.dataObject = channel;
    }

    public DataObjects.Channel DataObject
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
    public Int32? ChannelGroupId
    {
      get { return dataObject.ChannelGroupId; }
      set { dataObject.ChannelGroupId = value; }
    }

    [DbAutoColumnName]
    public string ContentPath
    {
      get { return dataObject.ContentPath; }
      set { dataObject.ContentPath = value; }
    }

    [DbAutoColumnName]
    public string Language
    {
      get { return dataObject.Language; }
      set { dataObject.Language = value; }
    }

    #endregion

    #region Fields

    private readonly DataObjects.Channel dataObject = new DataObjects.Channel();

    #endregion
  }

  public class Channels : List<Channel>
  {
    public DataObjects.Channels DataObjects
    {
      get
      {
        DataObjects.Channels objects = new DataObjects.Channels();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}