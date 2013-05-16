using System;
using System.Xml;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.Datasets.XmlDatasets
{
  public class GetDataResponse: XmlDataSet
  {
    public override void Decode(IXmlDataTableDecoderFactory ADecoderFactory, string AXML)
    {
      if (AXML.IndexOf(RootElementName) == -1)
        throw new FormatException("Could not decode response. RootElement not found.");

      base.Decode(ADecoderFactory, AXML);
    }
    protected override void WritePreamble(XmlTextWriter AWriter)
    {
      base.WritePreamble(AWriter);

      AWriter.WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
      AWriter.WriteStartElement(RootElementName);
    }
    protected override void WritePostamble(XmlTextWriter AWriter)
    {
      AWriter.WriteEndElement();

      base.WritePreamble(AWriter);
    }
  }
}