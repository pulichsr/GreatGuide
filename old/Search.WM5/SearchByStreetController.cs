using System;
using Nucleo.GoodGuide.Types.DataObjects;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Search
{
  public class SearchByStreetController
  {
    public SearchByStreetController(ILogger logger,ISearchRegionRepository searchRegionRepository,IStreetLocationProvider streetLocationProvider,IStreetSearcher searcher,IStreetSelector selector,IStreetConfirmer confirmer,IStreetNumberEntry numberEntry,IBusyIndicator busyIndicator,IMessageDisplay messageDisplay)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(searchRegionRepository, "searchRegionRepository");
      Guard.ArgumentNotNull(streetLocationProvider, "streetLocationProvider");
      Guard.ArgumentNotNull(searcher, "searcher");
      Guard.ArgumentNotNull(selector, "selector");
      Guard.ArgumentNotNull(confirmer, "confirmer");
      Guard.ArgumentNotNull(numberEntry, "numberEntry");
      Guard.ArgumentNotNull(busyIndicator, "busyIndicator");
      Guard.ArgumentNotNull(messageDisplay, "messageDisplay");

      this.logger = logger;
      this.searchRegionRepository = searchRegionRepository;
      this.streetLocationProvider = streetLocationProvider;
      this.searcher = searcher;
      this.selector = selector;
      this.confirmer = confirmer;
      this.numberEntry = numberEntry;
      this.busyIndicator = busyIndicator;
      this.messageDisplay = messageDisplay;
    }

    public SearchResult Execute(out double latitude,out double longitude)
    {
      latitude = 0;
      longitude = 0;
      string searchCriteria = string.Empty;
      DataObjects.Street street;

      while (true)
      {
        #region Search for street
        Boolean searchResult = searcher.Search(null,ref searchCriteria);
        if (searchResult == false)
          break;
        #endregion

        while (true)
        {
          #region Select street
          Boolean selectResult = selector.Select(null,searchCriteria, out street);
          if (selectResult == false)
            break;
          #endregion

          while (true)
          {
            #region Confirm street
            Boolean confirmResult = confirmer.Confirm(street);
            if (confirmResult == false)
              break;
            #endregion

            while (true)
            {
              #region Enter street number if required
              Int16 streetNumber = -1;
              Boolean hasStreetNumbers = string.IsNullOrEmpty(street.StreetNumbers) == false;
              if (hasStreetNumbers)
              {
                Boolean enterNumberResult = numberEntry.EnterNumber(street, out streetNumber);
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

              #region Get region
              Region region = searchRegionRepository.GetById(street.RegionId);
              if (region == null)
              {
                logger.Write(this, string.Format("Region {0} not found", street.RegionId));
                return SearchResult.Error;
              }
              #endregion

              logger.Write(this, string.Format("Street {0} in {1}", street.Name, street.RegionCollatedName));
              logger.Write(this, string.Format("Region {0}", region.Name));

              busyIndicator.Show();
              Boolean result = streetLocationProvider.GetLocation(region, street, streetNumber, out latitude, out longitude);
              busyIndicator.Hide();

              if (result == false)
                messageDisplay.Display("Could not determine location. Try another street number");
              else
                return SearchResult.Success;
            }
          }
        }
      }

      return SearchResult.Cancel;
    }

    #region Fields
    private readonly ILogger logger;
    private readonly ISearchRegionRepository searchRegionRepository;
    private readonly IStreetLocationProvider streetLocationProvider;
    private readonly IStreetSearcher searcher;
    private readonly IStreetSelector selector;
    private readonly IStreetConfirmer confirmer;
    private readonly IStreetNumberEntry numberEntry;
    private readonly IBusyIndicator busyIndicator;
    private readonly IMessageDisplay messageDisplay;
    #endregion

  }
}