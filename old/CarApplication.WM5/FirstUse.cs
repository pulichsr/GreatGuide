using System;
using System.IO;

namespace Nucleo.GoodGuide.CarApplication
{
  public static class FirstUse
  {
    static FirstUse()
    {
      filename = string.Format("{0}FirstUse.txt", Nucleo.Path.ExecutablePath);
    }

    public static Boolean IsFirstUse
    {
      get
      {
        if (firstUseDetermined == false)
        {
          IsFirstUse = DetermineIfFirstUse();
          firstUseDetermined = true;
        }

        return isFirstUse;
      }
      set
      {
        isFirstUse = value;

        #region Save FirstUse file
        try
        {
          TextWriter writer = new StreamWriter(filename);
          if (value == true)
            writer.Write("Y");
          else
            writer.Write("N");
          writer.Flush();
          writer.Close();
        }
        catch (Exception exc)
        {
}
        #endregion
      }
    }
    private static Boolean DetermineIfFirstUse()
    {
      if (File.Exists(filename) == false)
      {
        isFirstUse = true;
        return isFirstUse;
      }
      else
      {
        TextReader reader = new StreamReader(filename);
        string text = reader.ReadToEnd();
        reader.Close();

        isFirstUse = text.ToUpper().StartsWith("Y");
      }

      return isFirstUse;
    }

    private static readonly string filename;
    private static Boolean firstUseDetermined = false;
    private static Boolean isFirstUse = false;
  }


}
