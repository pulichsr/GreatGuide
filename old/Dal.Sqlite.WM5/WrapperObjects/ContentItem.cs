using System;
using System.Collections.Generic;
using Nucleo.Data.Attributes;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects
{
  [DbAutoTableName]
  [DbSelectViewName("VW_CONTENT_ITEM")]
  public class ContentItem
  {
    public ContentItem()
    {
    }
    public ContentItem(DataObjects.ContentItem contentItem)
    {
      this.dataObject = contentItem;
    }

    public DataObjects.ContentItem DataObject
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
    public string ContentTypeCode
    {
      get { return dataObject.ContentTypeCode; }
      set { dataObject.ContentTypeCode = value; }
    }

    [DbAutoColumnName]
    public string Filename
    {
      get { return dataObject.Filename; }
      set { dataObject.Filename = value; }
    }

    [DbAutoColumnName]
    public Int32? ChannelContentId
    {
      get { return dataObject.ChannelContentId; }
      set { dataObject.ChannelContentId = value; }
    }

    [DbAutoColumnName]
    public Int32? ChannelGroupId
    {
      get { return dataObject.ChannelGroupId; }
      set { dataObject.ChannelGroupId = value; }
    }

    [DbAutoColumnName]
    public string Description
    {
      get { return dataObject.Description; }
      set { dataObject.Description = value; }
    }

    [DbAutoColumnName]
    public bool IsFillerContent
    {
      get { return dataObject.IsFillerContent; }
      set { dataObject.IsFillerContent = value; }
    }

    [DbAutoColumnName]
    public Int32? DisplaySeq
    {
      get { return dataObject.DisplaySeq; }
      set { dataObject.DisplaySeq = value; }
    }
    #endregion

    #region Fields

    private readonly DataObjects.ContentItem dataObject = new DataObjects.ContentItem();

    #endregion
  }

  public class ContentItems : List<ContentItem>
  {
    public DataObjects.ContentItems DataObjects
    {
      get
      {
        DataObjects.ContentItems objects = new DataObjects.ContentItems();
        for (Int32 i = 0; i < Count; i++)
          objects.Add(this[i].DataObject);

        return objects;
      }
    }
  }
}