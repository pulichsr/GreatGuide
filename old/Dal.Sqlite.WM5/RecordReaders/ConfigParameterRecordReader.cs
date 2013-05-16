using System.Data;
using Nucleo.Data.DataAccess;

namespace Nucleo.GoodGuide.Dal.Sqlite.RecordReaders
{
  public class ConfigParameterRecordReader:
    IRecordReader
  {
    public void Read(IDataReader dataReader,object targetObject)
    {
      WrapperObjects.ConfigParameter configParameter = (WrapperObjects.ConfigParameter)targetObject;

      configParameter.ParamName = RecordReaderHelper.ReadString(dataReader,"PARAM_NAME");
      configParameter.ParamValue = RecordReaderHelper.ReadString(dataReader,"PARAM_VALUE");      
    }
  }
}
