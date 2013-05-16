using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.Datasets.XmlDatasets
{
  public class DecoderFactory : IXmlDataTableDecoderFactory
  {
    public IXmlDataTableDecoder GetDecoder(string tableName)
    {
      if (tableName == "Channel")
        return new ChannelXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "ChannelContent")
        return new ChannelContentXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "ChannelGroup")
        return new ChannelGroupXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "ContentItem")
        return new ContentItemXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "ControlDefinition")
        return new ControlDefinitionXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "Destination")
        return new DestinationXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "DestinationClassification")
        return new DestinationClassificationXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "DestinationCollection")
        return new DestinationCollectionXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "DestinationCollectionMember")
        return new DestinationCollectionMemberXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "DestinationType")
        return new DestinationTypeXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "FormDefinition")
        return new FormDefinitionXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "GpsRegion")
        return new GpsRegionXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "Itinerary")
        return new ItineraryXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "ItineraryDay")
        return new ItineraryDayXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "ItineraryDestination")
        return new ItineraryDestinationXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "MasterArea")
        return new MasterAreaXmlDataTableDecoder(tableName, tableName, tableName + "Row");
      if (tableName == "Theme")
        return new ThemeXmlDataTableDecoder(tableName, tableName, tableName + "Row");

      return null;
    }

  }
}
