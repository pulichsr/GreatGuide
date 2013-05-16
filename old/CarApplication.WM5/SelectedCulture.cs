using System;
using System.Globalization;
using System.IO;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public static class SelectedCulture
  {
    static SelectedCulture()
    {
      filename = string.Format("{0}SelectedCulture.txt", Path.ExecutablePath);
    }

    public static CultureInfo Culture
    {
      get
      {
        if (cultureDetermined == false)
        {
          Culture = DetermineCulture();
          cultureDetermined = true;
        }

        return culture;
      }
      set
      {
        culture = value;

        #region Save file
        try
        {
          TextWriter writer = new StreamWriter(filename);
          writer.Write(value.Name);
          writer.Flush();
          writer.Close();
        }
        catch (Exception exc)
        {
}
        #endregion
      }
    }
    private static CultureInfo DetermineCulture()
    {
      if (File.Exists(filename) == false)
      {
        culture = LanguageHelper.CreateCulture(Types.Language.English);
        return culture;
      }
      else
      {
        TextReader reader = new StreamReader(filename);
        string text = reader.ReadToEnd();
        reader.Close();

        try
        {
          culture = CultureInfo.GetCultureInfo(text);
        }
        catch (Exception exc)
        {
          culture = LanguageHelper.CreateCulture(Types.Language.English);
        }
      }

      return culture;
    }

    private static readonly string filename;
    private static Boolean cultureDetermined = false;
    private static CultureInfo culture = LanguageHelper.CreateCulture(Types.Language.English);
  }


}
