using System;
using System.Collections.Generic;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.ContentManager
{
  public interface IContentManagerRule
  {
    Boolean IsValid(
      ChannelGroupManager channelGroupContent,
      ChannelContentDataset.ChannelContentRow channelContentItem,
      GpsPositionEvent lastGpsPosition,
      GpsPositionEvent lastMediaStartedGpsPosition,
      DateTime currentDtm,
      out string message);
  }

  public class ContentManagerRules : List<IContentManagerRule> 
  {
    public Boolean IsValid(
      ChannelGroupManager channelGroupContent,
      ChannelContentDataset.ChannelContentRow channelContentItem,
      GpsPositionEvent lastGpsPosition,
      GpsPositionEvent lastMediaStartedGpsPosition,
      DateTime currentDtm,
      out string failedRule,
      out string failedRuleMessage)
    {
      Boolean IsValid;
 
      for (Int32 RuleNo = 0; RuleNo < Count; RuleNo++)
      {
        string Message;
        IsValid = this[RuleNo].IsValid(channelGroupContent,channelContentItem,lastGpsPosition,lastMediaStartedGpsPosition,currentDtm,out Message);
        if (IsValid == false)
        {
          failedRule = this[RuleNo].GetType().Name;
          failedRuleMessage = Message;
          return false;
        }
      }

      failedRule = string.Empty;
      failedRuleMessage = string.Empty;
      return true;
    }
  }
}
