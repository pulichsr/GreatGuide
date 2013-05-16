using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class ContentItemBll:
    ISyncTarget,
    ISyncSource
  {
    public ContentItemBll(IContentItemRepository contentItemRepository)
    {
      this.contentItemRepository = contentItemRepository;
    }

    public string DatasetName
    {
      get { return "ContentItem"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      ContentItemDataset.ContentItemDataTable data = (ContentItemDataset.ContentItemDataTable)newData.Tables["ContentItem"];
      if (data != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < data.Rows.Count; RowNo++)
        {
          data[RowNo].Id += offset;

          if (data[RowNo].IsChannelContentIdNull() == false) data[RowNo].ChannelContentId += offset;
          // ChannelGroupId is not offset because it needs to remain to interface to MLMR

          Insert(data[RowNo]);
        }

        data.Dispose();
      }
    }
    public DataTable Get()
    {
      return contentItemRepository.Get();
    }
    public ContentItemDataset.ContentItemRow GetById(Int32 id)
    {
      return contentItemRepository.GetById(id);
    }

    public ContentItemDataset.ContentItemDataTable GetByMasterArea(Int32 masterAreaId)
    {
      return contentItemRepository.GetByMasterArea(masterAreaId);
    }
    public DataTable GetAll()
    {
      return contentItemRepository.Get();
    }

    public void Insert(ContentItemDataset.ContentItemRow Row)
    {
      ValidateRow(Row);

      contentItemRepository.Insert(Row);
    }
    public void Update(ContentItemDataset.ContentItemRow row)
    {
      contentItemRepository.Update(row);
    }
    public void DeleteAll()
    {
      contentItemRepository.DeleteAll();
    }
    public DataTable GetFillerContent(Int32 masterAreaId)
    {
      return contentItemRepository.GetFillerContent(masterAreaId);
    }

    private void ValidateRow(ContentItemDataset.ContentItemRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("ContentItem.Id is null");

      if (Row.IsContentTypeCodeNull() == true)
        throw new DataException("ContentItem.ContentTypeCode is null");

      if (Row.sFilename == string.Empty)
        throw new DataException("ContentItem.Filename is undefined");

      if (Row.IsChannelContentIdNull() == true)
        throw new DataException("ContentItem.ChannelContentId is null");

      if (Row.IsChannelGroupIdNull() == true)
        throw new DataException("ContentItem.ChannelGroupId is null");
    }

    private readonly IContentItemRepository contentItemRepository = null;
  }

}
