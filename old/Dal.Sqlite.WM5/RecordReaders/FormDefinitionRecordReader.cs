using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class FormDefinitionRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.FormDefinition formDefinition = (WrapperObjects.FormDefinition)targetObject;

      formDefinition.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      formDefinition.Name = RecordReaderHelper.ReadString(dataReader, "NAME"); 
      formDefinition.FormTypeName = RecordReaderHelper.ReadString(dataReader, "FORM_TYPE_NAME"); 
      formDefinition.Text = RecordReaderHelper.ReadString(dataReader, "TEXT"); 
      formDefinition.GraphicResourceName = RecordReaderHelper.ReadString(dataReader, "GRAPHIC_RESOURCE_NAME");
      formDefinition.MasterAreaId = RecordReaderHelper.ReadInt32(dataReader,"MASTER_AREA_ID");
      formDefinition.AdName = RecordReaderHelper.ReadString(dataReader, "AD_NAME");
    }
  }
}
