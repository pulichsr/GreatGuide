using System;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Search
{
  public class SearchDestinationController
  {
    public SearchDestinationController(ILogger logger, IDestinationRepository destinationRepository, 
      IDestinationSearcher destinationSearcher, IDestinationSelector destinationSelector,
      IBusyIndicator busyIndicator, IMessageDisplay messageDisplay)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(destinationRepository, "searchRegionRepository");
      Guard.ArgumentNotNull(destinationSearcher, "regionSearcher");
      Guard.ArgumentNotNull(destinationSelector, "regionSelector");
      Guard.ArgumentNotNull(busyIndicator, "busyIndicator");
      Guard.ArgumentNotNull(messageDisplay, "messageDisplay");

      this.logger = logger;
      this.destinationRepository = destinationRepository;
      this.destinationSearcher = destinationSearcher;
      this.destinationSelector = destinationSelector;
      this.busyIndicator = busyIndicator;
      this.messageDisplay = messageDisplay;
    }

    public SearchResult Execute(out DataObjects.Destination destination)
    {
      string searchCriteria = string.Empty;
      destination = null;

      while (true)
      {
        #region Search for destination
        Boolean searchDestinationResult = destinationSearcher.Search(ref searchCriteria);
        if (searchDestinationResult == false)
          break;
        #endregion

        #region
        while (true)
        {
          #region Select destination
          Boolean selectDestinationResult = destinationSelector.Select(searchCriteria, out destination);
          if (selectDestinationResult == false)
            break;
          #endregion

          return SearchResult.Success;
        }
        #endregion
      }

      return SearchResult.Cancel;
    }

    #region Fields
    private readonly ILogger logger;
    private readonly IDestinationRepository destinationRepository;
    private readonly IDestinationSearcher destinationSearcher;
    private readonly IDestinationSelector destinationSelector;
    private readonly IBusyIndicator busyIndicator;
    private readonly IMessageDisplay messageDisplay;
    #endregion

  }
}