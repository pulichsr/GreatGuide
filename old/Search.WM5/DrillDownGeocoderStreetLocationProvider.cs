using System;
using System.Collections.Generic;
using Nucleo;
using Nucleo.GoodGuide.Types.Interfaces;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Telogis.GeoBase;

namespace Nucleo.GoodGuide.Search
{
  public class DrillDownGeocoderStreetLocationProvider:
    IStreetLocationProvider
  {
    public DrillDownGeocoderStreetLocationProvider(ILogger logger,DrillDownGeoCoder geoCoder)
    {
      Guard.ArgumentNotNull(geoCoder, "geoCoder");
      Guard.ArgumentNotNull(logger, "logger");

      this.geoCoder = geoCoder;
      this.logger = logger;
    }

    #region IStreetLocationProvider Members
    public Boolean GetLocation(DataObjects.Region region, DataObjects.Street street, Int16 streetNumber,out double latitude, out double longitude)
    {
      logger.Write(this, ">> GetLocation");
      Guard.ArgumentNotNull(region, "region");
      Guard.ArgumentNotNull(street, "street");

      logger.Write(this,string.Format("Region: Id:{0}, Name:{1}",region.Id,region.Name));
      logger.Write(this, string.Format("Street: Id:{0}, Name:{1}", street.Id, street.Name));

      geoCoder.SearchNeighbouringRegions = false;
      latitude = 0;
      longitude = 0;

      #region Get region
      logger.Write(this, "Getting region");
      RegionSearchResult regionSearchResult = geoCoder.GetRegions(region.Level, region.Name,1000);
      if (regionSearchResult.Status != Telogis.GeoBase.SearchResult.SearchCompleted)
      {
        logger.Write(this, string.Format("Invalid search result {0} for region {1} on level {1}", regionSearchResult.Status,region.Name, region.Level));
        return false;
      }
      if (regionSearchResult.Results.Length == 0)
      {
        logger.Write(this, string.Format("Region {0} on level {1} not found", region.Name, region.Level));
        return false;
      }
      logger.Write(this, string.Format("{0} region(s) found. Finding best match", regionSearchResult.Results.Length));

      RegionData regionData = FindBestMatch(region.Name,regionSearchResult.Results);
      #endregion

      logger.Write(this, "Set DDGC region");
      geoCoder.SetRegion(regionData);

      #region Get street
      logger.Write(this, "Getting street");
      Telogis.GeoBase.StreetSearchResult streetSearchResult = geoCoder.GetStreets(street.Name,2000);
      if (streetSearchResult.Status != Telogis.GeoBase.SearchResult.SearchCompleted)
      {
        logger.Write(this, string.Format("Invalid search result for street {0}", street.Name));
        return false;
      }
      if (streetSearchResult.Results.Length == 0)
      {
        logger.Write(this, string.Format("Street {0} not found", street.Name));
        return false;
      }
      logger.Write(this, string.Format("{0} street(s) found", streetSearchResult.Results.Length));
      #endregion

      #region Get address
      logger.Write(this, "Getting address");
      StreetData streetData = streetSearchResult.Results[0];
      List <GeocodeAddress> addresses = streetData.GetAddressLocation(streetNumber);
      if (addresses.Count == 0)
      {
        logger.Write(this, string.Format("Street number {0} not found", streetNumber));
        return false;
      }
      if (addresses.Count > 1)
      {
        logger.Write(this, "TOO MANY GeocodeAddresses:");
        foreach (GeocodeAddress geocodeAddress in addresses)
        {
          logger.Write(this,string.Format("City:{0}, PostalCode:{1}, Street:{2}, Region:{3}",geocodeAddress.City,geocodeAddress.PostalCode,geocodeAddress.Street,geocodeAddress.Region));
        }

        return false;
      }
      #endregion
      logger.Write(this, "Got address");

      latitude = addresses[0].Location.Lat;
      longitude = addresses[0].Location.Lon;

      return true;
    }
    #endregion

    private RegionData FindBestMatch(string regionName,RegionData[] data)
    {
      List<RegionData> matches = new List<RegionData>();
      foreach (RegionData regionData in data)
        if (regionData.Name == regionName)
        {
          logger.Write(this,string.Format("Found match {0} on level {1} location ({2},{3})",regionData.Name,regionData.Level,regionData.Location.Lat,regionData.Location.Lon));
          matches.Add(regionData);
        }

      return matches[0];
    }

    private readonly DrillDownGeoCoder geoCoder;
    private readonly ILogger logger;
  }
}