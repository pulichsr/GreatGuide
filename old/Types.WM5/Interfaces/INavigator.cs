using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public enum ColourScheme
  {
    Day,
    Night,
  }

  public interface INavigator
  {
    Boolean IsNotificationsActive { set; }
    ColourScheme ColourScheme { set; }
    string[] Directions { get; }

    event EventHandler BeforeTurnNotification;
    event EventHandler AfterTurnNotification;
    event EventHandler DirectionsChanged;

    void Initialise(Control mapControl);
    void Finalise();

    Boolean RouteTo(float latitude, float longitude);
    void CancelRoute();

    PoiCategories GetPoiCategories();
    PoiSubcategories GetPoiSubCategories(PoiCategory category);

    DestinationDataset.DestinationDataTable GetPoiDestinations(PoiCategory category, 
                                                               PoiSubcategory subcategory, 
                                                               float latitude, 
                                                               float longitude, 
                                                               Int32 radiusMeters);

    Boolean SetLanguage(Language language);
    Boolean SetUnits(Units unit);

    Boolean GetAddressOfPosition(float latitude, float longitude,
                                 out string house,
                                 out string street,
                                 out string city,
                                 out string zip,
                                 out string description,
                                 out string telephoneNo);

    Boolean GpsDataHandler(GpsPositionEvent eventData);
    NavigatorAddresses SearchStreetAdress(string searchText);

  }
}