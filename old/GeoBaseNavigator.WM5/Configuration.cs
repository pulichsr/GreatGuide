using System;

namespace Nucleo.GoodGuide.GeoBaseNavigator
{
  [Serializable]
  public class Configuration
  {
    public string ResourcePath
    {
      get { return resourcePath; }
      set { resourcePath = value; }
    }

    public string RepositoryPath
    {
      get { return repositoryPath; }
      set { repositoryPath = value; }
    }

    public string SoundPath
    {
      get { return soundPath; }
      set { soundPath = value; }
    }

    public string DefaultLanguage
    {
      get { return defaultLanguage; }
      set { defaultLanguage = value; }
    }

    public string LanguageInUse
    {
      get { return languageInUse; }
      set { languageInUse = value; }
    }

    private string resourcePath;
    private string repositoryPath;
    private string soundPath;

    private string defaultLanguage;
    private string languageInUse;
  }
}