using System;
using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class ChannelGroupDal : ObjectDal<WrapperObjects.ChannelGroup, WrapperObjects.ChannelGroups>, IChannelGroupRepository
  {
    public ChannelGroupDal(IDbConnector connector) : base(connector)
    {
    }

    public void Insert(ChannelGroupDataset.ChannelGroupRow row)
    {
      WrapperObjects.ChannelGroup group = new WrapperObjects.ChannelGroup();
      group.Id = row.Id;
      group.Name = row.sName;

      base.Insert(group);
    }
    public void Insert(ChannelGroupDataset.ChannelGroupDataTable table)
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

    public ChannelGroupDataset.ChannelGroupDataTable Get()
    {
      string sql = string.Format("select * from {0} order by NAME", DbMetadata.SelectViewName);

      return GetHelper.GetTable<ChannelGroupDataset, ChannelGroupDataset.ChannelGroupDataTable>(Connector, sql);
    }

  }
}