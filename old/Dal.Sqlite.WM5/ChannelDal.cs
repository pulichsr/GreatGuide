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
  public class ChannelDal : ObjectDal<WrapperObjects.Channel, WrapperObjects.Channels>, IChannelRepository
  {
    public ChannelDal(IDbConnector connector) : base(connector)
    {
    }

    public void Insert(ChannelDataset.ChannelRow row)
    {
      WrapperObjects.Channel channel = new WrapperObjects.Channel();
      channel.Id = row.Id;
      channel.ChannelGroupId = row.IsChannelGroupIdNull() ? new int?() : row.ChannelGroupId;
      channel.ContentPath = row.sContentPath;
      channel.Language = row.sLanguage;

      base.Insert(channel);
    }
    public void Insert(ChannelDataset.ChannelDataTable table)
    {
      for (Int32 rowNo = 0; rowNo < table.Rows.Count; rowNo++)
        Insert(table[rowNo]);
    }
    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public ChannelChannelGroupDataset.ChannelChannelGroupDataTable GetChannels()
    {
      StringBuilder Sql = new StringBuilder();
      Sql.Append("select");
      Sql.Append(" CG.ID as ChannelGroupId");
      Sql.Append(",CG.NAME as ChannelGroupName");
      Sql.Append(",C.ID as ChannelId");
      Sql.Append(",C.CONTENT_PATH as ChannelContentPath");
      Sql.Append(",C.LANGUAGE as ChannelLanguage");
      Sql.Append(" from CHANNEL C,CHANNEL_GROUP CG");
      Sql.Append(" where C.CHANNEL_GROUP_ID = CG.ID");
      Sql.Append(" order by CG.NAME,C.LANGUAGE");

      return GetHelper.GetTable<ChannelChannelGroupDataset, ChannelChannelGroupDataset.ChannelChannelGroupDataTable>(Connector, Sql.ToString());
    }
    public ChannelDataset.ChannelDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);
      return GetHelper.GetTable<ChannelDataset, ChannelDataset.ChannelDataTable>(Connector, sql);
    }


  }
}