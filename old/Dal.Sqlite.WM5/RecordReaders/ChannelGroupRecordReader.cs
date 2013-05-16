using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ChannelGroupRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.ChannelGroup channelGroup = (WrapperObjects.ChannelGroup)targetObject;

      channelGroup.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      channelGroup.Name = RecordReaderHelper.ReadString(dataReader, "NAME"); 
    }
  }
}
