using System;
using System.Collections.Generic;
using Telogis.GeoBase;

namespace Nucleo.GoodGuide.CarApplication
{
  public class GeocodedAddressFactory
  {
    public GeocodedAddressFactory(DrillDownGeoCoder geoCoder)
    {
      Guard.ArgumentNotNull(geoCoder, "geoCoder");

      this.geoCoder = geoCoder;
    }

    public IEnumerable<string> Create(double latitude, double longitude)
    {
      geoCoder.Reset();
      List<string> addressEntries = new List<string>();

      Address address = GeoCoder.ReverseGeoCode(new LatLon(latitude, longitude));
      if (address.Regions.Length > 0)
      {
        Int32 regionLevel = 0;
        for (int i = address.Regions.Length - 1; i >= 0; i--)
        {
          string region = address.Regions[i];
          RegionData rd = geoCoder.GetRegions(regionLevel, region).Results[0];
          geoCoder.SetRegion(rd);
          regionLevel++;

          addressEntries.Add(address.Regions[i]);
        }
      }

      return addressEntries;
    }

    #region Fields
    private readonly DrillDownGeoCoder geoCoder;
    #endregion
    
  }
}
