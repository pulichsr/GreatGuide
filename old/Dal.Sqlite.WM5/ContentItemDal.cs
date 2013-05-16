using System;
using System.Data;
using System.Text;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class ContentItemDal : ObjectDal<WrapperObjects.ContentItem, WrapperObjects.ContentItems>, IContentItemRepository
  {
    public static string SoundContentItemTypeCode = "S";

    public ContentItemDal(IDbConnector connector) : base(connector)
    {
    }

    public void Insert(ContentItemDataset.ContentItemRow row)
    {
      WrapperObjects.ContentItem contentItem = new WrapperObjects.ContentItem();
      contentItem.Id = row.Id;
      contentItem.ChannelContentId = row.IsChannelContentIdNull() ? new int?() : row.ChannelContentId;
      contentItem.ChannelGroupId = row.IsChannelGroupIdNull() ? new int?() : row.ChannelGroupId;
      contentItem.ContentTypeCode = row.sContentTypeCode;
      contentItem.Filename = row.sFilename;

      if (string.IsNullOrEmpty(row.Description) == true)
        contentItem.Description = System.IO.Path.GetFileNameWithoutExtension(row.Filename);
      else
        contentItem.Description = row.Description;

      contentItem.IsFillerContent = row.IsIsFillerContentNull() ? false : row.IsFillerContent;
      contentItem.DisplaySeq = row.IsDisplaySeqNull() ? new int?() : row.DisplaySeq;

      base.Insert(contentItem);
    }
    public void Insert(ContentItemDataset.ContentItemDataTable table)
    {
      for (Int32 rowNo = 0; rowNo < table.Rows.Count; rowNo++)
        Insert(table[rowNo]);
    }
    public void Update(ContentItemDataset.ContentItemRow row)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("update CONTENT_ITEM set ");
      sql.Append(string.Format("  FILENAME =  {0},", Connector.FormatParameterName("FILENAME")));
      sql.Append(string.Format("  DESCRIPTION =  {0},", Connector.FormatParameterName("DESCRIPTION")));
      sql.Append(string.Format("  IS_FILLER_CONTENT =  {0},", Connector.FormatParameterName("IS_FILLER_CONTENT")));
      sql.Append(string.Format("  DISPLAY_SEQ =  {0}", Connector.FormatParameterName("DISPLAY_SEQ")));
      sql.Append(string.Format("  where ID =  {0}", Connector.FormatParameterName("ID")));

      IDbCommand command = Connector.CreateCommand(sql.ToString());
      IDataParameter pFilename = Connector.CreateParameter("FILENAME", typeof(string));
      IDataParameter pDescription = Connector.CreateParameter("DESCRIPTION", typeof(string));
      IDataParameter pIsFillerContent = Connector.CreateParameter("IS_FILLER_CONTENT", typeof(Int16));
      IDataParameter pDisplaySeq = Connector.CreateParameter("DISPLAY_SEQ", typeof(Int32));
      IDataParameter pId = Connector.CreateParameter("ID", typeof(Int32));

      command.Parameters.Add(pFilename);
      command.Parameters.Add(pDescription);
      command.Parameters.Add(pIsFillerContent);
      command.Parameters.Add(pDisplaySeq);
      command.Parameters.Add(pId);

      pFilename.Value = row.sFilename;
      pDescription.Value = row.sDescription;
      pIsFillerContent.Value = row.IsIsFillerContentNull() == true ? 0 : row.IsFillerContent == true ? 1 : 0;
      pDisplaySeq.Value = row.IsDisplaySeqNull() == true ? new Int32() : row.DisplaySeq;
      pId.Value = row.Id;

      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }
    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public ContentItemDataset.ContentItemDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);
      return Get(sql);
    }
    public ContentItemDataset.ContentItemDataTable GetByMasterArea(Int32 masterAreaId)
    {
      StringBuilder sql = new StringBuilder();
      sql.Append("select  ");
      sql.Append("  CI.* ");
      sql.Append("  from VW_CONTENT_ITEM CI ");
      sql.Append("  left outer join CHANNEL_CONTENT CC on CC.ID = CI.CHANNELCONTENTID ");
      sql.Append("  left outer join GPS_REGION GPR on GPR.ID = CC.GPS_REGION_ID ");
      sql.Append(string.Format("  where GPR.MASTER_AREA_ID = {0}",masterAreaId));
      sql.Append("  and DISPLAYSEQ is not null");
      sql.Append("  order by DISPLAYSEQ,FILENAME");

      return Get(sql.ToString());
    }

    public ContentItemDataset.ContentItemRow GetById(Int32 id)
    {
      string Sql = string.Format("select * from {0} where ID = {1}", DbMetadata.SelectViewName, id);
      ContentItemDataset.ContentItemDataTable table = Get(Sql);
      if (table.Rows.Count == 0)
        return null;

      ContentItemDataset.ContentItemRow row = table[0];

      return row;
    }

    public DataTable GetFillerContent(Int32 masterAreaId)
    {
      #region Build SQL
      StringBuilder sql = new StringBuilder();
      sql.Append("select * from  ");
      sql.Append("(");
      sql.Append("select  ");
      sql.Append("  GPR.NAME as GPS_REGION_NAME,  ");
      sql.Append("  CI.ID as CONTENT_ITEM_ID,  ");
      sql.Append("  CI.FILENAME,  ");
      sql.Append("  CI.DESCRIPTION,  ");
      sql.Append("  cast(CI.IS_FILLER_CONTENT as smallint) as IS_FILLER_CONTENT,  ");
      sql.Append("  CI.DISPLAY_SEQ  ");
      sql.Append("  from CONTENT_ITEM CI  ");
      sql.Append("  join CHANNEL_CONTENT CC on CC.ID = CI.CHANNEL_CONTENT_ID  ");
      sql.Append("  join GPS_REGION GPR  on GPR.ID = CC.GPS_REGION_ID  ");
      sql.Append(string.Format("  where GPR.MASTER_AREA_ID = {0}  ", masterAreaId));
      sql.Append("  and DISPLAY_SEQ is not null ");
      sql.Append("  order by CI.IS_FILLER_CONTENT,DISPLAY_SEQ  ");
      sql.Append(")  ");

      sql.Append("union all ");

      sql.Append("select * from  ");
      sql.Append("(  ");
      sql.Append("select  ");
      sql.Append("  GPR.NAME as GPS_REGION_NAME,  ");
      sql.Append("  CI.ID as CONTENT_ITEM_ID,  ");
      sql.Append("  CI.FILENAME,  ");
      sql.Append("  CI.DESCRIPTION,  ");
      sql.Append("  cast(CI.IS_FILLER_CONTENT as smallint) as IS_FILLER_CONTENT,  ");
      sql.Append("  CI.DISPLAY_SEQ  ");
      sql.Append("  from CONTENT_ITEM CI  ");
      sql.Append("  join CHANNEL_CONTENT CC on CC.ID = CI.CHANNEL_CONTENT_ID  ");
      sql.Append("  join GPS_REGION GPR  on GPR.ID = CC.GPS_REGION_ID  ");
      sql.Append(string.Format("  where GPR.MASTER_AREA_ID = {0}  ", masterAreaId));
      sql.Append("  and DISPLAY_SEQ is null ");
      sql.Append("  order by GPR.NAME ");
      sql.Append(")");
      #endregion

      #region Get data
      IDbCommand command = Connector.CreateCommand(sql.ToString());
      IDataAdapter adapter = Connector.CreateDataAdapter(command);
      DataSet dataset = new DataSet();
      adapter.Fill(dataset);
      command.Dispose();

      if (dataset.Tables.Count == 0)
        return null;
      else
        return dataset.Tables[0];
      #endregion
    }

    private new ContentItemDataset.ContentItemDataTable Get(string sql)
    {
      return GetHelper.GetTable<ContentItemDataset, ContentItemDataset.ContentItemDataTable>(Connector, sql);
    }

  }
}