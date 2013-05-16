using System;
using Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.Text;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.Types
{
  public static class LicenceSerializer
  {
    public static Licences Deserialize(IStringCodec codec,string data)
    {
      if (codec == null)
        throw new NullReferenceException("Codec is undefined");

      #region Decode licence data
      string decodedData;
      try
      {
        decodedData = codec.Decode(data);
      }
      catch
      {
        return new Licences();
      }
      #endregion

      #region Deserialize licence data
      Licences licences;
      try
      {
        licences = XmlSerialization.DeserializeFromString<Licences>(decodedData);
      }
      catch
      {
        return new Licences();
      }
      #endregion

      return licences;
    }
    public static string Serialize(IStringCodec codec,Licences licences)
    {
      if (codec == null)
        throw new NullReferenceException("Codec is undefined");

      string licenceXml = XmlSerialization.Serialize(licences);
      return codec.Encode(licenceXml);
    }
  }
}