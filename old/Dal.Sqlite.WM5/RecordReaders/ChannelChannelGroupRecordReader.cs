using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ChannelChannelGroupRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.ChannelChannelGroup channelChannelGroup = (WrapperObjects.ChannelChannelGroup)targetObject;

      channelChannelGroup.ChannelGroupId = RecordReaderHelper.ReadInt32(dataReader,"CHANNEL_GROUP_ID");
      channelChannelGroup.ChannelGroupName = RecordReaderHelper.ReadString(dataReader,"CHANNEL_GROUP_NAME");
      channelChannelGroup.ChannelId = RecordReaderHelper.ReadInt32(dataReader,"CHANNEL_ID");
      channelChannelGroup.ChannelContentPath = RecordReaderHelper.ReadString(dataReader,"CHANNEL_CONTENT_PATH");
      channelChannelGroup.ChannelLanguage = RecordReaderHelper.ReadString(dataReader,"CHANNEL_LANGUAGE");
      channelChannelGroup.ChannelGroupContentPath = RecordReaderHelper.ReadString(dataReader,"CHANNEL_GROUP_CONTENT_PATH");
    }
  }
}
