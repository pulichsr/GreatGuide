using System.Collections.Generic;
using System.IO;

namespace Nucleo.GoodGuide.Types
{
  public class ItineraryFileLocator
  {
    public static IList<string> GetFilenames(string path)
    {
      Guard.ArgumentNotNullOrEmptyString(path,"path");
      if (Directory.Exists(path) == false)
        throw new DirectoryNotFoundException(string.Format("Path {0} not found",path));

      string[] filenames = Directory.GetFiles(path,"*.ggi");
      List<string> list = new List<string>();
      list.AddRange(filenames);

      return list;
    }
  }
}
