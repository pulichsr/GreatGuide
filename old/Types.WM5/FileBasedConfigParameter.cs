using System;
using System.IO;
using Nucleo.Text;

namespace Nucleo.GoodGuide.Types
{
  public class FileBasedConfigParameter
  {
    public FileBasedConfigParameter(string parameterName, Boolean cacheValue)
    {
      Guard.ArgumentNotNullOrEmptyString(parameterName, "parameterName");
      Int32 validCharacters = StringUtils.ValidateCharacters(parameterName,"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_-");
      if (validCharacters != -1)
        throw new ArgumentException(string.Format("Invalid character at {0}",validCharacters));

      this.parameterName = parameterName;
      this.cacheValue = cacheValue;
      
      filename = string.Format("{0}{1}.cfg", Path.ExecutablePath,parameterName);
    }

    public string ParameterValue
    {
      get
      {
        if ((cacheValue == false) || (valueDetermined == false))
        {
          ReadValue();
        }

        return parameterValue;
      }
      set
      {
        parameterValue = value;
        WriteValue();
      }
    }
    private void ReadValue()
    {
      if (File.Exists(filename) == false)
      {
        parameterValue = null;
      }
      else
      {
        TextReader reader = new StreamReader(filename);
        parameterValue = reader.ReadToEnd();
        reader.Close();

        valueDetermined = true;
      }
    }
    private void WriteValue()
    {
      TextWriter writer = new StreamWriter(filename);
      writer.Write(parameterValue);
      writer.Flush();
      writer.Close();
    }

    private readonly string parameterName;
    private readonly Boolean cacheValue = true;
    private readonly string filename;
    private Boolean valueDetermined = false;
    private string parameterValue;
  }
}