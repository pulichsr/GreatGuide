using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.Datasets.XmlDatasets
{
  public class EncoderFactory : IXmlDataTableEncoderFactory
  {
    public IXmlDataTableEncoder GetEncoder(string tableName)
    {
      if (tableName == "ContentItem")
        return new ContentItemXmlDataTableEncoder(tableName,tableName,tableName + "Row");
      if (tableName == "ChannelContent")
        return new ChannelContentXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "GpsRegion")
        return new GpsRegionXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "Theme")
        return new ThemeXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "Channel")
        return new ChannelXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "ChannelGroup")
        return new ChannelGroupXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "MasterArea")
        return new MasterAreaXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "ControlDefinition")
        return new ControlDefinitionXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "Destination")
        return new DestinationXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "DestinationType")
        return new DestinationTypeXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "DestinationClassification")
        return new DestinationClassificationXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "Itinerary")
        return new ItineraryXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "ItineraryDay")
        return new ItineraryDayXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "ItineraryDestination")
        return new ItineraryDestinationXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "DestinationCollection")
        return new DestinationCollectionXmlDataTableEncoder(tableName, tableName, tableName + "Row");
      if (tableName == "DestinationCollectionMember")
        return new DestinationCollectionMemberXmlDataTableEncoder(tableName, tableName, tableName + "Row");

      return null;
    }
  }
}
