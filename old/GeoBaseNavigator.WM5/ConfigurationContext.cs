using Nucleo.GoodGuide.GeoBaseNavigator;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.GeoBaseNavigator
{
  public static class ConfigurationContext
  {
    public static Configuration Configuration = new Configuration();

    public static void LoadConfiguration()
    {
      if (System.IO.File.Exists(configFilename) == false)
      {
        SaveDefaultConfiguration();
        return;
      }

      Configuration = XmlSerialization.DeserializeFromFile<Configuration>(configFilename);
    }
    public static void SaveConfiguration()
    {
      XmlSerialization.Serialize(Configuration, configFilename);
    }

    public static void SaveDefaultConfiguration()
    {
      Configuration.ResourcePath = @"C:\Program Files\Telogis\GeoBase\GeoBaseResources";
      //Path for repository
      Configuration.RepositoryPath = @"data\gb.3.5";
      //Path for sound folder
      Configuration.SoundPath = "langs";

      Configuration.DefaultLanguage = GeoBaseNavigator.geoBaseDefaultLanguage;
      Configuration.LanguageInUse = Configuration.DefaultLanguage;

      SaveConfiguration();
    }

    private static readonly string configFilename = "geobase.nucleo.settings";
  }
}