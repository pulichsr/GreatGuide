using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ControlDefinitionRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.ControlDefinition controlDefinition = (WrapperObjects.ControlDefinition)targetObject;

      controlDefinition.Id = RecordReaderHelper.ReadInt32(dataReader,"ID");
      controlDefinition.FormId = RecordReaderHelper.ReadInt32(dataReader,"FORM_ID");
      controlDefinition.Name = RecordReaderHelper.ReadString(dataReader, "NAME"); 
      controlDefinition.Text = RecordReaderHelper.ReadString(dataReader, "TEXT"); 
      controlDefinition.Description = RecordReaderHelper.ReadString(dataReader, "DESCRIPTION");
      controlDefinition.GraphicResourceName = RecordReaderHelper.ReadString(dataReader, "GRAPHIC_RESOURCE_NAME");
      controlDefinition.Colour = RecordReaderHelper.ReadString(dataReader, "COLOUR");
      controlDefinition.Action = RecordReaderHelper.ReadString(dataReader, "ACTION");
      controlDefinition.ActionTarget = RecordReaderHelper.ReadString(dataReader, "ACTION_TARGET");
      controlDefinition.MasterAreaId = RecordReaderHelper.ReadInt32(dataReader,"MASTER_AREA_ID");
      controlDefinition.ActionData = RecordReaderHelper.ReadString(dataReader, "ACTION_DATA");
    }
  }
}

