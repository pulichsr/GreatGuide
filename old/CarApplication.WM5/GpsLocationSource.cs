using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.CarApplication
{
  public class GpsLocationSource:
    ICurrentLocationSource
  {
    public GpsLocationSource(double latitude,double longitude)
    {
      this.latitude = latitude;
      this.longitude = longitude;
    }

    #region ICurrentLocationSource
    public double Latitude
    {
      get { return latitude; }
    }
    public double Longitude
    {
      get { return longitude; }
    }

    #endregion

    private readonly double latitude;
    private readonly double longitude;
  }
}