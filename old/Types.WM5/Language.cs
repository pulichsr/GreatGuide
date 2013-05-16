
using System.Globalization;

namespace Nucleo.GoodGuide.Types
{
  public enum Language
  {
    Unknown,
    English,
    German,
    Dutch,
    Italian,
    French,
  }

  public static class LanguageHelper
  {
    public static Language GetLanguage(CultureInfo culture)
    {
      switch (culture.Name)
      {
        case "nl-NL": return Language.Dutch;
        case "en-US": return Language.English;
        case "fr-FR": return Language.French;
        case "de-DE": return Language.German;
        case "it-IT": return Language.Italian;
      }

      return Language.English;
    }
    public static CultureInfo CreateCulture(Language language)
    {
      switch(language)
      {
        case Language.Dutch: return new CultureInfo("nl-NL");
        case Language.English: return new CultureInfo("en-US");
        case Language.French: return new CultureInfo("fr-FR");
        case Language.German: return new CultureInfo("de-DE");
        case Language.Italian: return new CultureInfo("it-IT");
      }

      return null;
    }
    public static string DisplayName(Language language)
    {
      switch(language)
      {
        case Language.Dutch: return "Nederlands";
        case Language.English: return "English";
        case Language.French: return "Francais";
        case Language.German: return "Deutsch";
        case Language.Italian: return "Italiano";
      }

      return null;
    }
    public static string FormatCultureDependentFilename(CultureInfo culture,string filename)
    {
      Guard.ArgumentNotNull(culture, "culture");
      Guard.ArgumentNotNullOrEmptyString(filename, "filename");

      string cultureDependentFilename = string.Format("{0}-{1}{2}", System.IO.Path.GetFileNameWithoutExtension(filename), culture.Name, System.IO.Path.GetExtension(filename));
      cultureDependentFilename = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filename), cultureDependentFilename);

      return cultureDependentFilename;
    }
    public static string FormatCultureDependentPath(CultureInfo culture, string basePath, string filename)
    {
      Guard.ArgumentNotNull(culture, "culture");
      Guard.ArgumentNotNullOrEmptyString(filename, "filename");

      return string.Format("{0}{1}\\{2}",basePath,culture.Name,filename);
    }
  }
}
