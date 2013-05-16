using System.IO;

namespace Nucleo.GoodGuide.ItineraryImporter
{
  public class ItineraryImporter
  {
    public ItineraryImporter(string filename)
    {
      Guard.ArgumentNotNullOrEmptyString(filename, "filename");
      if (File.Exists(filename) == false)
        throw new FileNotFoundException(string.Format("Itinerary file {0} not found",filename));

      this.filename = filename;
    }

    private readonly string filename;
  }
}
