using System;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class GpsRawEvent : ApplicationEvent
  {
    public GpsRawEvent(GpsPositionSources source, int eventSequence, string rmcData, string vtgData, string ggaData)
    {
      Source = source;
      EventSequence = eventSequence;

      RmcData = rmcData;
      VtgData = vtgData;
      GgaData = ggaData;
    }
    public GpsRawEvent(GpsPositionSources source,int eventSequence,string rmcData,string vtgData,string ggaData,string userField1,string userField2,string userField3,string userField4,string userField5)
    {
      Source = source;
      EventSequence = eventSequence;

      RmcData = rmcData;
      VtgData = vtgData;
      GgaData = ggaData;

      UserField1 = userField1;
      UserField2 = userField2;
      UserField3 = userField3;
      UserField4 = userField4;
      UserField5 = userField5;
    }

    public GpsPositionSources Source = GpsPositionSources.Undefined;
    public Int32 EventSequence;

    public string RmcData = string.Empty;
    public string VtgData = string.Empty;
    public string GgaData = string.Empty;
    
    public string UserField1 = string.Empty;
    public string UserField2 = string.Empty;
    public string UserField3 = string.Empty;
    public string UserField4 = string.Empty;
    public string UserField5 = string.Empty;
  }
}
