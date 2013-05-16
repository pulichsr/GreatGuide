using System;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Search
{
  public class SearchByRegionController
  {
    public SearchByRegionController(ILogger logger, ISearchRegionRepository searchRegionRepository, IStreetLocationProvider streetLocationProvider, 
      IRegionSearcher regionSearcher, IRegionSelector regionSelector, IRegionConfirmer regionConfirmer, 
      IStreetSearcher streetSearcher, IStreetSelector streetSelector, IStreetConfirmer streetConfirmer,
      IStreetNumberEntry numberEntry, IBusyIndicator busyIndicator, IMessageDisplay messageDisplay)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(searchRegionRepository, "searchRegionRepository");
      Guard.ArgumentNotNull(streetLocationProvider, "streetLocationProvider");
      Guard.ArgumentNotNull(regionSearcher, "regionSearcher");
      Guard.ArgumentNotNull(regionSelector, "regionSelector");
      Guard.ArgumentNotNull(regionConfirmer, "regionConfirmer");
      Guard.ArgumentNotNull(streetSearcher, "streetSearcher");
      Guard.ArgumentNotNull(streetSelector, "streetSelector");
      Guard.ArgumentNotNull(streetConfirmer, "streetConfirmer");
      Guard.ArgumentNotNull(numberEntry, "numberEntry");
      Guard.ArgumentNotNull(busyIndicator, "busyIndicator");
      Guard.ArgumentNotNull(messageDisplay, "messageDisplay");

      this.logger = logger;
      this.searchRegionRepository = searchRegionRepository;
      this.streetLocationProvider = streetLocationProvider;
      this.regionSearcher = regionSearcher;
      this.regionSelector = regionSelector;
      this.regionConfirmer = regionConfirmer;
      this.streetSearcher = streetSearcher;
      this.streetSelector = streetSelector;
      this.streetConfirmer = streetConfirmer;
      this.numberEntry = numberEntry;
      this.busyIndicator = busyIndicator;
      this.messageDisplay = messageDisplay;
    }

    public SearchResult Execute(out double latitude, out double longitude)
    {
      latitude = 0;
      longitude = 0;
      string regionSearchCriteria = string.Empty;
      string streetSearchCriteria = string.Empty;
      Nucleo.GoodGuide.Types.DataObjects.Region region;
      Nucleo.GoodGuide.Types.DataObjects.Street street;

      while (true)
      {
        #region Search for region
        Boolean searchRegionResult = regionSearcher.Search(ref regionSearchCriteria);
        if (searchRegionResult == false)
          break;
        #endregion

        #region
        while (true)
        {
          #region Select region
          Boolean selectRegionResult = regionSelector.Select(regionSearchCriteria, out region);
          if (selectRegionResult == false)
            break;
          #endregion

          #region 
          while (true)
          {
            #region Confirm region
            Boolean confirmRegionResult = regionConfirmer.Confirm(region);
            if (confirmRegionResult == false)
              break;
            #endregion

            #region
            while (true)
            {
              #region Search for street
              Boolean searchResult = streetSearcher.Search(region,ref streetSearchCriteria);
              if (searchResult == false)
                break;
              #endregion

              #region
              while (true)
              {
                #region Select street
                Boolean selectResult = streetSelector.Select(region,streetSearchCriteria,out street);
                if (selectResult == false)
                  break;
                #endregion

                #region
                while (true)
                {
                  #region Confirm street
                  Boolean confirmResult = streetConfirmer.Confirm(street);
                  if (confirmResult == false)
                    break;
                  #endregion

                  #region
                  while (true)
                  {
                    #region Enter street number if required
                    Int16 streetNumber = -1;
                    Boolean hasStreetNumbers = string.IsNullOrEmpty(street.StreetNumbers) == false;
                    if (hasStreetNumbers)
                    {
                      Boolean enterNumberResult = numberEntry.EnterNumber(street,out streetNumber);
                      if (enterNumberResult == false)
                        break;
                    }
                    else
                    {
                      latitude = street.Latitude;
                      longitude = street.Longitude;

                      return SearchResult.Success;
                    }
                    #endregion

                    logger.Write(this,string.Format("Street {0} in {1}",street.Name,street.RegionCollatedName));
                    logger.Write(this,string.Format("Region {0}",region.Name));

                    busyIndicator.Show();
                    Boolean result = streetLocationProvider.GetLocation(region,street,streetNumber,out latitude,out longitude);
                    busyIndicator.Hide();

                    if (result == false)
                      messageDisplay.Display("Could not determine location. Try another street number");
                    else
                      return SearchResult.Success;
                  }
                  #endregion
                }
                #endregion
              }
              #endregion
            }
            #endregion
          }
          #endregion
        }
        #endregion
      }

      return SearchResult.Cancel;
    }

    #region Fields
    private readonly ILogger logger;
    private readonly ISearchRegionRepository searchRegionRepository;
    private readonly IStreetLocationProvider streetLocationProvider;
    private readonly IRegionSearcher regionSearcher;
    private readonly IRegionSelector regionSelector;
    private readonly IRegionConfirmer regionConfirmer;
    private readonly IStreetSearcher streetSearcher;
    private readonly IStreetSelector streetSelector;
    private readonly IStreetConfirmer streetConfirmer;
    private readonly IStreetNumberEntry numberEntry;
    private readonly IBusyIndicator busyIndicator;
    private readonly IMessageDisplay messageDisplay;
    #endregion

  }
}