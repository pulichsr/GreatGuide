using System;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class PoiDestinationListEvent : ApplicationEvent
  {
    public PoiDestinationListEvent(Int32 categoryIndex, Int32 subcategoryIndex, float latitude, float longitude, int radius)
    {
      this.categoryIndex = categoryIndex;
      this.subcategoryIndex = subcategoryIndex;
      this.latitude = latitude;
      this.longitude = longitude;
      this.radius = radius;
    }

    public Int32 CategoryIndex
    {
      get { return categoryIndex; }
      set { categoryIndex = value; }
    }
    public int SubcategoryIndex
    {
      get { return subcategoryIndex; }
      set { subcategoryIndex = value; }
    }
    public float Latitude
    {
      get { return latitude; }
      set { latitude = value; }
    }
    public float Longitude
    {
      get { return longitude; }
      set { longitude = value; }
    }
    public int Radius
    {
      get { return radius; }
      set { radius = value; }
    }
    public DestinationDataset.DestinationDataTable Destinations
    {
      get { return destinations; }
      set { destinations = value; }
    }

    private Int32 categoryIndex;
    private Int32 subcategoryIndex;
    private float latitude;
    private float longitude;
    private Int32 radius;
    private DestinationDataset.DestinationDataTable destinations = null;
  }
}
