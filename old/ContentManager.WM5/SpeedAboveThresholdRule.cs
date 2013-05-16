using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class SpeedAboveThresholdRule : IContentManagerRule
  {
    public Boolean IsValid(
      ChannelGroupManager channelGroupContent,
      ChannelContentDataset.ChannelContentRow channelContentItem,
      GpsPositionEvent lastGpsPosition,
      GpsPositionEvent lastMediaStartedGpsPosition,
      DateTime currentDtm,
      out string message)
    {
      message = string.Empty;

    // If SpeedAboveThreshold is not defined then this rule allows content to play
      if (channelContentItem.IsSpeedAboveThresholdNull() == true) 
        return true;

      // If SpeedBelowThreshold and SpeedAboveThreshold is defined then this rule allows content to play because it is a SpeedBetweenThresholdsRule
      if ((channelContentItem.IsSpeedBelowThresholdNull() == false) && (channelContentItem.IsSpeedAboveThresholdNull() == false))
        return true;

      // If last GPS position is not defined then this rule allows content to play
      if (lastGpsPosition == null)
        return true;

      // Get last speed
      float LastSpeed = lastGpsPosition.Speed;
      if (channelContentItem.IsNull("LastSpeed") == false)
        LastSpeed = (float)channelContentItem["LastSpeed"];

      // Get last speed as current speed
      channelContentItem["LastSpeed"] = lastGpsPosition.Speed;

      // If the speed <= speed threshold then content must not play
      if (lastGpsPosition.Speed <= channelContentItem.SpeedAboveThreshold)
      {
        message = string.Format("Speed={0}, SpeedAboveThreshold={1}", lastGpsPosition.Speed, channelContentItem.SpeedAboveThreshold);
        return false;
      }

      // Speed > speed threshold 
      // If content must play while above threshold
      if (channelContentItem.WhileSpeedAboveThreshold == true)
        return true;
      else
      {
        // Only play if last speed < speed threshold
        if (LastSpeed <= channelContentItem.SpeedAboveThreshold)
          return true;
      }

      message = "No conditions met";
      return false;
    }
  }
}
