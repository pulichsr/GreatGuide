using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ChannelRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.Channel channel = (WrapperObjects.Channel)targetObject;

      channel.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      channel.ChannelGroupId = RecordReaderHelper.ReadInt32(dataReader,"CHANNEL_GROUP_ID");
      channel.ContentPath = RecordReaderHelper.ReadString(dataReader, "CONTENT_PATH");
      channel.Language = RecordReaderHelper.ReadString(dataReader, "LANGUAGE"); 
    }
  }
}
