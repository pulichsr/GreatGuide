using System;
using Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.Types.Interfaces.DriverDisplay
{
  public interface IDriverDisplayServices
  {
    LoggingHelper LoggingHelper {get;}

    string Version { get;}
    string WaypointFilename { get;}
    string MasterArea { get;}
    Boolean GpsFixState {get;}
    void GetGpsData(out GpsPositionEvent gpsPosition, out GpsRawEvent gpsRawData);

    Boolean IsTripRecording {get;}
    string TripName {get;}

    MasterAreas GetMasterAreas();
    Boolean IsValidLicense { get;}
    string LicenseExpiryText { get;}

    string PlayingMedia { get;}
    ManualMediaItems GetManualMedia();

    Boolean SelectMasterArea(Int32 masterAreaId);
    Boolean IsMasterAreasChanged { get;}
    Boolean PaState { set;}
    Boolean IsGpsRunning { set;}
    void ResetPlayedAreas();

    Boolean AudioTestState { get;set;}
    
    Boolean StartTripRecording(string name);
    void StopTripRecording();
    Boolean StartTripPlayback(string name);
    void StopTripPlayback();
    void Shutdown();
    Boolean PlayMedia(string name);
    void StopMedia(string name);

    void ImportData();
    void ExportData();

    void ShowComponentStatus();
    void ComponentTrace(string name,bool isActive);

    void MasterAreaChangedHandler(object sender,EventArgs e);
}
}