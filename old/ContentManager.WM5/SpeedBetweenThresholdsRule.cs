using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public class SpeedBetweenThresholdsRule : IContentManagerRule
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

      // If SpeedBelowThreshold or SpeedAboveThreshold is undefined then this rule allows content to play because 
      // it is not a SpeedBetweenThresholdsRule
      if ((channelContentItem.IsSpeedBelowThresholdNull() == true) || (channelContentItem.IsSpeedAboveThresholdNull() == true))
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

      // If the speed <= SpeedAboveThreshold threshold then content must not play
      if (lastGpsPosition.Speed <= channelContentItem.SpeedAboveThreshold)
      {
        message = string.Format("Speed={0}, SpeedAboveThreshold={1}", lastGpsPosition.Speed, channelContentItem.SpeedAboveThreshold);
        return false;
      }
      // If the speed >= SpeedBelowThreshold threshold then content must not play
      if (lastGpsPosition.Speed >= channelContentItem.SpeedBelowThreshold)
      {
        message = string.Format("Speed={0}, SpeedBelowThreshold={1}", lastGpsPosition.Speed, channelContentItem.SpeedBelowThreshold);
        return false;
      }

      // (Speed > SpeedAboveThreshold) and (Speed > SpeedAboveThreshold)
      // If content must play while above threshold
      if ((channelContentItem.WhileSpeedBelowThreshold == true) || (channelContentItem.WhileSpeedAboveThreshold == true))
        return true;
      else
      {
        // Only play if last speed is not between speed thresholds
        if ((LastSpeed >= channelContentItem.SpeedBelowThreshold) || (LastSpeed <= channelContentItem.SpeedAboveThreshold))
          return true;
      }

      message = "No conditions met";
      return false;
    }
  }
}
