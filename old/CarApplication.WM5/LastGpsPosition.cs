using System;
using System.IO;

namespace Nucleo.GoodGuide.CarApplication
{
  public static class LastGpsPosition
  {
    static LastGpsPosition()
    {
      filename = Path.FileInExecutablePath("LastGpsPosition.txt");
    }

    public static Boolean GetLastGpsPostition(out string ggaData,out string rmcData,out string vtgData)
    {
      ggaData = string.Empty;
      rmcData = string.Empty;
      vtgData = string.Empty;

      if (File.Exists(filename) == false)
        return false;
      else
      {
        string data = IO.File.ReadAllText(filename);
        string[] fields = data.Split('|');
        if (fields.Length != 3)
          return false;

        rmcData = fields[0];
        vtgData = fields[1];
        ggaData = fields[2];

        return true;
      }
    }
    public static void SetLastGpsPostition(string ggaData,string rmcData,string vtgData)
    {
      if (string.IsNullOrEmpty(ggaData) == true)
        return;
      if (string.IsNullOrEmpty(rmcData) == true)
        return;
      if (string.IsNullOrEmpty(vtgData) == true)
        return;

      string data = string.Format("{0}|{1}|{2}",rmcData,vtgData,ggaData);
      Nucleo.IO.File.WriteAllText(filename,data);
    }

    private static readonly string filename;
  }


}
