using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ContentItemRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.ContentItem contentItem = (WrapperObjects.ContentItem)targetObject;

      contentItem.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      contentItem.ContentTypeCode = RecordReaderHelper.ReadString(dataReader, "CONTENT_TYPE_CODE"); 
      contentItem.Filename = RecordReaderHelper.ReadString(dataReader, "FILENAME"); 
      contentItem.ChannelContentId = RecordReaderHelper.ReadInt32(dataReader,"CHANNEL_CONTENT_ID");
      contentItem.ChannelGroupId = RecordReaderHelper.ReadInt32(dataReader,"CHANNEL_GROUP_ID");
      contentItem.Description = RecordReaderHelper.ReadString(dataReader, "DESCRIPTION");
      contentItem.DisplaySeq = RecordReaderHelper.ReadInt32(dataReader,"DISPLAY_SEQ");
    }
  }
}
