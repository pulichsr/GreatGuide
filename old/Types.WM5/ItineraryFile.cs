using System.Collections.Generic;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types
{
  public class ItineraryFile
  {
    public ItineraryFile(string filename,ItineraryDataset.ItineraryRow itinerary)
    {
      this.filename = filename;
      this.itinerary = itinerary;
    }

    public string Name
    {
      get
      {
        if (itinerary == null)
          return string.Empty;

        return string.Format("{0} {1} {2}",itinerary.Title,itinerary.FirstName,itinerary.LastName);
      }
    }
    public string ArrivalDat
    {
      get
      {
        if (itinerary == null)
          return string.Empty;

        return itinerary.ArrivalDat.ToString("yyyy-MM-dd");
      }
    }
    public string Filename
    {
      get { return filename; }
    }

    private readonly string filename = string.Empty;
    private readonly ItineraryDataset.ItineraryRow itinerary = null;
  }

  public class ItineraryFiles : List<ItineraryFile>
  {}
}
