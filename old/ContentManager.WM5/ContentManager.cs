using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Nucleo.Events;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Datasets;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Events.ControlEvents;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.Threading;

namespace Nucleo.GoodGuide.ContentManager
{
  public class ContentManager
  {
    public const Int32 DefaultFillerContentThreadSleep = 1000;
    public const Int32 DefaultPauseTimeout = 60000;

    public ContentManager(IRepositoryLocator repositoryLocator, ChannelContentBll channelContentBll, ChannelGroupBll channelGroupBll)
    {
      this.repositoryLocator = repositoryLocator;
      this.channelContentBll = channelContentBll;
      this.channelGroupBll = channelGroupBll;

      pauseTimer = new SingleShotTimer(DefaultPauseTimeout);
      pauseTimer.TimerExpired += PauseTimer_TimerExpired;
    }

    #region Event publishers
    [EventPublisher(EventTopics.ContentManager.MediaControl)]
    public event EventHandler<GoodGuideEventArgs> MediaControl;
    #endregion
    
    #region Event subscribers
    [EventSubscriber(EventTopics.System.ResetCounts)]
    public void SystemResetCountsHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is SystemResetCountsEvent == false)
        return;

      try
      {
        // Reset content in the database
        lock (channelContentBll)
        {
          channelContentBll.ResetCounts();          
        }

        // Reset filler content in memory
        for (Int32 RowNo = 0; RowNo < FillerContent.Count; RowNo++)
        {
          FillerContent[RowNo].PresentedCount = 0;
          FillerContent[RowNo].SequenceIsComplete = false;
          FillerContent[RowNo].SequencePredecessorIsComplete = false;
        }
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error executing SystemResetCountsHandler", exc);
      }
    }

    [EventSubscriber(EventTopics.MasterAreaTrigger.MasterAreaEnter)]
    public void MasterAreaEnterHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is MasterAreaEnterEvent == false)
        return;

      MasterAreaEnterEvent masterAreaEnterEvent = (MasterAreaEnterEvent)e.EventData;
      try
      {
        IChannelContentRepository channelContentRepository = repositoryLocator.LocateRepository<IChannelContentRepository>();
        if (channelContentRepository == null)
          return;

        channelContentRepository.Load(masterAreaEnterEvent.Region.Id);
      }
      catch (Exception exc)
      {
        Logger.Write(this, "Error executing MasterAreaEnterHandler", exc);
      }
    }

    [EventSubscriber(EventTopics.System.StopAllContent)]
    public void SystemStopAllContantHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is SystemStopAllContentEvent == false)
        return;

      StopAllContent(true);
    }

    [EventSubscriber(EventTopics.RegionTrigger.RegionEnter)]
    public void RegionEnteredHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is RegionEnterEvent == false)
        return;

      try
      {
        RegionEntered((RegionEnterEvent)e.EventData);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error executing RegionEntered", exc);
      }
    }

    [EventSubscriber(EventTopics.RegionTrigger.RegionExit)]
    public void RegionExitHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is RegionExitEvent == false)
        return;

      try
      {
        RegionExit((RegionExitEvent)e.EventData);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error executing RegionExit", exc);
      }
    }

    [EventSubscriber(EventTopics.ContentManager.Reset)]
    public void ResetHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is SystemResetContentEvent == false)
        return;

      StopAllContent(true);
    }

    [EventSubscriber(EventTopics.MediaPlayer.MediaStateChange)]
    public void MediaManagerStateChangeHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is MediaStateEvent == false)
        return;

      try
      {
        MediaManagerStateChange((MediaStateEvent)e.EventData);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error executing MediaManagerStateChangeHandler", exc);
      }
    }

    [EventSubscriber(EventTopics.GpsAdapter.GpsPosition)]
    public void GpsPositionHandler(object sender,GoodGuideEventArgs e)
    {
      if (e.EventData is GpsPositionEvent == false)
        return;

      GpsPositionEvent positionEvent = (GpsPositionEvent)e.EventData;

      lastGpsPosition = positionEvent;
    }

    [EventSubscriber(EventTopics.ContentManager.RunState)]
    public void RunStateHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is RunStateEvent == false)
        return;

      RunState((RunStateEvent)e.EventData);
    }

    [EventSubscriber(EventTopics.ContentManager.ContentControl)]
    public void ContentControlHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is ContentControlEvent == false)
        return;

      ContentControl((ContentControlEvent)e.EventData);
    }

    [EventSubscriber(EventTopics.ContentManager.RequestCurrentFillerContent)]
    public void RequestCurrentFillerContentHandler(object sender, GoodGuideEventArgs e)
    {
      ChannelContentCollectionEvent eventData = e.EventData as ChannelContentCollectionEvent;
      if (eventData == null)
        return;

      ChannelContentCollection currentFillerContent = new ChannelContentCollection();

      lock(FillerContentLock)
      {
        foreach (ChannelContentDataset.ChannelContentRow row in FillerContent)
          currentFillerContent.Add(row);
      }

      eventData.Collection = currentFillerContent;
    }

    [EventSubscriber(EventTopics.Navigator.BeforeNotification)]
    public void NavigatorBeforeNotificationHandler(object sender, GoodGuideEventArgs e)
    {
      Logger.Write(this, ">> NavigatorBeforeNotificationHandler");

      lock (channelGroupContentLock)
      {
        Boolean isAnyPlaying = channelGroupManagers.IsAnyPlaying;

        Logger.Write(this,string.Format("   isAnyPlaying:{0}, isPausedManually:{1}, isPausedForNotification:{0}",isAnyPlaying,isPausedManually,isPausedForNotification));

        if (isAnyPlaying && !isPausedForNotification)
        {
          Logger.Write(this,"   Some channel groups are playing. Pausing all content");

          PauseAllContent();
        }

        isPausedForNotification = true;
        Logger.Write(this, string.Format("   isPausedForNotification = {0}", isPausedForNotification));
      }
    }

    [EventSubscriber(EventTopics.Navigator.AfterNotification)]
    public void NavigatorAfterNotificationHandler(object sender, GoodGuideEventArgs e)
    {
      Logger.Write(this, ">> NavigatorAfterNotificationHandler");

      lock (channelGroupContentLock)
      {
        Boolean isAnyPlaying = channelGroupManagers.IsAnyPlaying;

        Logger.Write(this,string.Format("   isAnyPlaying:{0}, isPausedManually:{1}, isPausedForNotification:{0}",isAnyPlaying,isPausedManually,isPausedForNotification));
        if (isAnyPlaying && !isPausedManually && isPausedForNotification)
        {
          Logger.Write(this,"   Resuming all content");
          ResumeAllContent();
        }

        isPausedForNotification = false;
        Logger.Write(this, string.Format("   isPausedForNotification = {0}", isPausedForNotification));
      }
    }
    #endregion

    public ContentManagerRules Rules
    {
      get { return rules; }
    }
    public List<IChannelContentUpdater> CurrentMediaUpdaters
    {
      get { return currentMediaUpdaters; }
    }
    public ChannelContentCollection FillerContent
    {
      get { return fillerContent; }
    }
    public bool IsRunning
    {
      get { return isRunning; }
    }

    public void Initialise()
    {
      BuildChannelGroupContent();
    }
    public void Finalise()
    {
      RunState(false);

      if (FillerContent != null)
        fillerContent = null;
    }

    public void RunState(RunStateEvent eventData)
    {
      RunState(eventData.IsRunning);
    }
    public void RunState(Boolean runState)
    {
      if (IsRunning == runState)
        return;

      isRunning = runState;

      if (runState == false)
      {
        Logger.Write(this,"Set RunState to false");
        StopAllContent(true);
        StopFillerContentThread();
      }
      else
      {
        Logger.Write(this, "Set RunState to true");
        StartFillerContentThread();
      }
    }

    public void StopAllContent(Boolean clearFillerContent)
    {
      Logger.Write(this,">> ContentManager.StopAllContent");

      Boolean isAnyPlaying = channelGroupManagers.IsAnyPlaying;
      Logger.Write(this, string.Format("   isAnyPlaying:{0}, isPausedManually:{1}, isPausedForNotification:{0}", isAnyPlaying, isPausedManually, isPausedForNotification));

      isPausedManually = false;
      Logger.Write(this, string.Format("   isPausedManually = {0}", isPausedManually));
      
      pauseTimer.Stop();

      if (clearFillerContent == true)
      {
        lock (FillerContentLock)
        {
          FillerContent.Clear();
        }
      }

      for (Int32 ChannelGroupManagerNo = 0; ChannelGroupManagerNo < channelGroupManagers.Count; ChannelGroupManagerNo++)
      {
        ChannelGroupManager ChannelGroupManager = channelGroupManagers[ChannelGroupManagerNo];
        StopChannelGroupContent(ChannelGroupManager.Id, string.Empty);
        ChannelGroupManager.Stop(DateTime.Now);
      }

      //Logger.Write(this,"<< ContentManager.StopAllContent");
    }
    public void PauseAllContent()
    {
      Logger.Write(this,">> ContentManager.PauseAllContent");

      for (Int32 ChannelGroupManagerNo = 0; ChannelGroupManagerNo < channelGroupManagers.Count; ChannelGroupManagerNo++)
      {
        ChannelGroupManager ChannelGroupManager = channelGroupManagers[ChannelGroupManagerNo];
        PauseChannelGroupContent(ChannelGroupManager.Id, string.Empty);
      }

      pauseTimer.Start();
    }
    public void ResumeAllContent()
    {
      pauseTimer.Stop();

      //Logger.Write(this,">> ContentManager.ResumeAllContent");

      for (Int32 ChannelGroupManagerNo = 0; ChannelGroupManagerNo < channelGroupManagers.Count; ChannelGroupManagerNo++)
      {
        ChannelGroupManager ChannelGroupManager = channelGroupManagers[ChannelGroupManagerNo];
        ResumeChannelGroupContent(ChannelGroupManager.Id, string.Empty);
      }
      //Logger.Write(this,"<< ContentManager.ResumeAllContent");
    }
    public void RepeatAllContent()
    {
      //Logger.Write(this,">> ContentManager.RepeatAllContent");

      pauseTimer.Stop();

      for (Int32 ChannelGroupManagerNo = 0; ChannelGroupManagerNo < channelGroupManagers.Count; ChannelGroupManagerNo++)
      {
        ChannelGroupManager ChannelGroupManager = channelGroupManagers[ChannelGroupManagerNo];

        ChannelContentDataset.ChannelContentRow ContentToRepeat;
        if (ChannelGroupManager.IsCurrentlyPlaying() == false)
        {
          if (ChannelGroupManager.lastPlayedContent == null)
            continue;

          ContentToRepeat = ChannelGroupManager.lastPlayedContent;
        }
        else
        {
          ContentToRepeat = ChannelGroupManager.currentlyPlayingContent;
          StopChannelGroupContent(ChannelGroupManager.Id, ChannelGroupManager.currentlyPlayingContent.ContentItemFilename);
        }

        Boolean invalidContent;
        PlayChannelGroupContent(ChannelGroupManager.Id, ContentToRepeat,out invalidContent);

      }
      //Logger.Write(this,"<< ContentManager.RepeatAllContent");
    }

    /// <summary>
    /// This method handles the RegionEnterEvent. It is handled in the following way:
    /// 1. Update the filler content list to add any filler content for the entered region
    /// 2. Get all content that is defined for the entered region and channel(s) and Entry trigger type
    /// 3. Validate all items against the rules to determine which are eligible to be played
    /// 4. For the items that are eligible to be played, update the dependencies because the content is to be played.
    /// 5. Initiate the playing of the eligible content
    /// 
    /// If no content can be played, then the filler content base time for the region is reset
    /// </summary>
    /// <param name="eventData"></param>
    public void RegionEntered(RegionEnterEvent eventData)
    {
      Logger.Write(this, string.Format(">> RegionEntered({0})", eventData.Region.Id));

      if (IsRunning == false)
      {
        //Logger.Write(this,"  isRunning == false");
        //Logger.Write(this,"<< RegionEntered");

        return;
      }

      // Add filler content associated with the region to the list of filler content
      //Logger.Write(this,"  Adding filler content for region");
      UpdateFillerContentOnRegionEntry(eventData.Region.Id, eventData.GpsPosition);

      // Get the 'Entry' trigger type channel content for the entered region. This can only include content for one channel 
      // group because only one channel group is associated with a region.
      Logger.Write(this,"  Getting EntryTriggerContent");
      ChannelContentCollection ChannelContent = GetChannelContent(eventData, new string[] { Region.EntryTrigger });

      // If no Entry content was found, then reset the filler content base time for the region
      if (ChannelContent.Count == 0)
      {
        //Logger.Write(this,"  No EntryTrigger content found");
        ResetRegionFillerBaseTime(eventData.Region.Id);
        //Logger.Write(this,"<< RegionEntered");
        
        return;
      }

      // Validate the returned channel content rows. The rows not satisfying the criteria, will be removed from the list. The
      // remaining rows are eligible for playing, even if there is a currently playing item. Because these rows will all belong
      // to a single region, they will also have the same ChannelGroup
      Int32 ChannelGroupId = ChannelContent[0].ChannelGroupId;
      ChannelGroupManager ChannelGroupManager = channelGroupManagers.GetById(ChannelGroupId);
      if (ChannelGroupManager == null)
      {
        //Logger.Write(this,"  ChannelGroupManager not found");
        //Logger.Write(this,"<< RegionEntered");

        return;
      }

      if (isPausedForNotification == false)
      {
        #region Get valid content to play on entry
        Logger.Write(this,"  Validating content rules");
        ChannelContentCollection ValidContent = GetValidContent(ChannelGroupManager,ChannelContent,DateTime.Now);

        // If no content is eligible for playing, then reset the filler content base time for the region
        if (ValidContent.Count == 0)
        {
          Logger.Write(this,"  No valid content");
          ResetRegionFillerBaseTime(eventData.Region.Id);
          //Logger.Write(this,"<< RegionEntered");

          return;
        }
        #endregion

        #region Play the content
        Logger.Write(this,string.Format("  Playing valid content: GpsRegionId={0} Filename={1} Priority={2}",ValidContent[0].GpsRegionId,ValidContent[0].ContentItemFilename,ValidContent[0].Priority));
        Boolean invalidContent;
        Boolean CanPlay = PlayChannelGroupContent(ChannelGroupId,ValidContent[0],out invalidContent);
        Logger.Write(this,string.Format("  CanPlay={0}",CanPlay));

        // Update the dependencies for the content that is about to play
        if (CanPlay == true)
        {
          //Logger.Write(this,"  UpdateChannelContentDependencies");
          UpdateChannelContentDependencies(ValidContent);
        }
        #endregion
      }
      else
      {
        Logger.Write(this, "  isPausedForNotification == true: not playing Entry content");
      }

      //Logger.Write(this,"<< RegionEntered");
    }

    /// <summary>
    /// This method handles the RegionExitEvent. It is handled in the following way:
    /// 1. If there is a currently playing item and it is content associated with the region and with a WhileIn trigger type, the content should be stopped
    /// 2. Update the filler content list to remove any filler content for the entered region
    /// 3. Get all content that is defined for the exited region and channel(s) as well as the Exit trigger type
    /// 4. Validate all items against the rules to determine which are eligible to be played
    /// 5. For the items that are eligible to be played, update the dependencies because the content is to be played.
    /// 6. Initiate the playing of the eligible content
    /// </summary>
    /// <param name="eventData"></param>
    public void RegionExit(RegionExitEvent eventData)
    {
      Logger.Write(this, string.Format(">> RegionExit({0})", eventData.Region.Id));

      if (IsRunning == false)
      {
        //Logger.Write(this,"  isRunning == false");
        //Logger.Write(this,"<< RegionExit");

        return;
      }

      // Remove all filler content for the exited region
      //Logger.Write(this,"  Removing filler content for region");
      UpdateFillerContentOnRegionExit(eventData.Region.Id);

      // If the region has currently playing 'While In' content, stop it
      //Logger.Write(this,"  Stopping WhileInTrigger content");
      StopRegionFillerContent(eventData.Region.Id, Region.WhileInTrigger);

      // Get content with 'Exit' trigger type for the exited region
      Logger.Write(this,"  Getting ExitTriggerContent");
      ChannelContentCollection ChannelContent = GetChannelContent(eventData, new string[] { Region.ExitTrigger });
      if (ChannelContent.Count == 0)
      {
        Logger.Write(this,"  No ExitTrigger content found");
        //Logger.Write(this,"<< RegionExit");
        
        return;
      }

      // Validate the returned channel content rows. The rows not satisfying the criteria, will be removed from the list. The
      // remaining rows are eligible for playing, even if there is a currently playing item. Because these rows will all belong
      // to a single region, they will also have the same ChannelGroup
      Int32 ChannelGroupId = ChannelContent[0].ChannelGroupId;
      ChannelGroupManager ChannelGroupManager = channelGroupManagers.GetById(ChannelGroupId);
      if (ChannelGroupManager == null)
      {
        //Logger.Write(this,"  ChannelGroupManager not found");
        //Logger.Write(this,"<< RegionExit");

        return;
      }

      if (isPausedForNotification == false)
      {
        #region Get valid content to play on exit
        Logger.Write(this,"  Validating content rules");
        ChannelContentCollection ValidContent = GetValidContent(ChannelGroupManager,ChannelContent,DateTime.Now);
        if (ValidContent.Count == 0)
        {
          Logger.Write(this,"  No valid content");
          //Logger.Write(this,"<< RegionExit");

          return;
        }
        #endregion

        #region Play content
        //Logger.Write(this,string.Format("  Playing valid content: GpsRegionId={0} Filename={1} Priority={2}", ValidContent[0].GpsRegionId, ValidContent[0].ContentItemFilename, ValidContent[0].Priority));
        Boolean invalidContent;
        Boolean CanPlay = PlayChannelGroupContent(ChannelGroupId,ValidContent[0],out invalidContent);
        Logger.Write(this,string.Format("  CanPlay={0}",CanPlay));

        if (CanPlay == true)
        {
          //Logger.Write(this,"  UpdateChannelContentDependencies");
          UpdateChannelContentDependencies(ValidContent);
        }
        #endregion
      }
      else
      {
        Logger.Write(this, "  isPausedForNotification == true: not playing Exit content");
      }
      
      //Logger.Write(this,"<< RegionExit");
    }

    /// <summary>
    /// This method handles the media Stopped and MediaEnded event.
    /// </summary>
    /// <param name="e"></param>
    public void MediaManagerStateChange(MediaStateEvent e)
    {
      Logger.Write(this,string.Format(">> MediaManagerStateChange({0})",e.State));
      switch (e.State)
      {
        case MediaStateEvent.MediaStates.Stopped:
          Logger.Write(this, string.Format("isPausedManually:{0}, isPausedForNotification:{1}", isPausedManually, isPausedForNotification));
          
          isPausedManually = false;
          Logger.Write(this, string.Format("   isPausedManually = {0}", isPausedManually));

          lock (channelGroupContentLock)
          {
            try
            {
              ChannelGroupManager ChannelGroupManager = channelGroupManagers.GetById(e.ChannelGroupId);
              if (ChannelGroupManager == null)
                return;

              Logger.Write(this, string.Format("ChannelGroupManager.Stop({0},{1})", e.ChannelGroupId, DateTime.Now));
              ChannelGroupManager.Stop(DateTime.Now);
            }
            catch (Exception exc)
            {
              Logger.Write(this,"MediaManagerStateChange", exc);
            }
          }

          break;
      }
      //Logger.Write(this,"<< MediaManagerStateChange");
    }

    /// <summary>
    /// This method fetches all content defined for a region, channel(s) and trigger type
    /// </summary>
    /// <returns></returns>
    private ChannelContentCollection GetChannelContent(RegionEvent eventData, string[] triggerTypes)
    {
      ChannelContentCollection ChannelContent;

      lock (channelContentBll)
      {
        if (playAllChannels == true)
          ChannelContent = channelContentBll.GetByRegion(eventData.Region.Id, triggerTypes);
        else
          ChannelContent = channelContentBll.GetByRegionChannelGroup(eventData.Region.Id, selectedChannelGroupId, triggerTypes);
      }

      return ChannelContent;
    }

    /// <summary>
    /// This method modifies the list of loaded filler content as a result of a region that was entered. It does this
    /// by fetching WhileIn and Always content associated with the region entered and adding new items to the loaded filler content list.
    /// </summary>
    /// <param name="regionId"></param>
    private void UpdateFillerContentOnRegionEntry(Int32 regionId,GpsPositionEvent gpsPosition)
    {
      //Logger.Write(this,string.Format(">> UpdateFillerContentOnRegionEntry({0})", regionId));

      ChannelContentCollection ContentItems;

      // Get channel content items for the region for the filler trigger types
      lock (channelContentBll)
      {
        if (playAllChannels == true)
          ContentItems = channelContentBll.GetByRegion(regionId, new string[] { Region.AnywhereTrigger, Region.WhileInTrigger });
        else
          ContentItems = channelContentBll.GetByRegionChannelGroup(regionId, selectedChannelGroupId, new string[] { Region.AnywhereTrigger, Region.WhileInTrigger });
      }

      if (ContentItems.Count == 0)
      {
        Logger.Write(this,"  No filler content found for this region");
//        Logger.Write(this,"<< UpdateFillerContentOnRegionEntry");
        return;
      }

      // Add the filler content associated with the entered region and add it to the current filler content list
      lock (FillerContentLock)
      {
        try
        {
          for (Int32 ContentItemNo = 0; ContentItemNo < ContentItems.Count; ContentItemNo++)
          {
            if (ContentItems[ContentItemNo].IsChannelGroupIdNull() == true)
            {
              Logger.Write(this, string.Format("  Region filler content without ChannelGroupId.  RegionId={0} ContentItemFilename={1} Priority={2}", ContentItems[ContentItemNo].GpsRegionId, ContentItems[ContentItemNo].sContentItemFilename, ContentItems[ContentItemNo].Priority));
              continue;
            }

            Logger.Write(this, string.Format("  Adding region filler content.  RegionId={0} ContentItemFilename={1} Priority={2}", ContentItems[ContentItemNo].GpsRegionId, ContentItems[ContentItemNo].ContentItemFilename, ContentItems[ContentItemNo].Priority));
            ContentItems[ContentItemNo]["LastSpeed"] = gpsPosition.Speed;
            FillerContent.AddNoDuplicates(ContentItems[ContentItemNo]);
          }
        }
        catch (Exception exc)
        {
          Logger.Write(this,"UpdateFillerContentOnRegionEntry", exc);
        }
      }

      //Logger.Write(this,string.Format("  Added {0} filler content items for a current total of {1}: ", ContentItems.Count,fillerContent.Count));
      //for (Int32 FillerContentNo = 0; FillerContentNo < fillerContent.Count;FillerContentNo++)
        //Logger.Write(this,string.Format("    RegionId={0} ContentItemFilename={1} Priority={2}", fillerContent[FillerContentNo].GpsRegionId,fillerContent[FillerContentNo].ContentItemFilename, fillerContent[FillerContentNo].Priority));

      //Logger.Write(this,"<< UpdateFillerContentOnRegionEntry");
    }

    /// <summary>
    /// This method modifies the list of loaded filler content as a result of a region that was exited. It does this
    /// by iterating through the list and removing all items associated with the region exited.
    /// </summary>
    /// <param name="regionId"></param>
    private void UpdateFillerContentOnRegionExit(Int32 regionId)
    {
      // Remove the filler content associated with the exited region from the list 
      lock(FillerContentLock)
      {
        try
        {
          for (Int32 ContentItemNo = 0; ContentItemNo < FillerContent.Count; ContentItemNo++)
            if (FillerContent[ContentItemNo].GpsRegionId == regionId)
            {
              FillerContent.RemoveAt(ContentItemNo);
              ContentItemNo--;
            }
        }
        catch (Exception exc)
        {
          Logger.Write(this,"UpdateFillerContentOnRegionExit", exc);
        }
      }
    }

    /// <summary>
    /// This method evaluates the rules and returns content items that are eligible for playing
    /// </summary>
    /// <param name="channelGroupContent">The management structure that indicates the play status of the channel group</param>
    /// <param name="channelContentItems">The list cof content items to validate</param>
    /// <param name="currentDtm">The current time</param>
    private ChannelContentCollection GetValidContent(ChannelGroupManager channelGroupContent, ChannelContentCollection channelContentItems, DateTime currentDtm)
    {
      ChannelContentCollection ValidContent = new ChannelContentCollection();

      for (Int32 ChannelContentNo = 0; ChannelContentNo < channelContentItems.Count; ChannelContentNo++)
      {
        ChannelContentDataset.ChannelContentRow Row = channelContentItems[ChannelContentNo];

        // Validate trigger rules
        string FailedRule;
        string FailedRuleMessage;
        Logger.Write(this,"");
        Logger.Write(this,"Evaluating Rules...");
        if (rules.IsValid(channelGroupContent, Row, lastGpsPosition, lastMediaStartedGpsPosition, currentDtm, out FailedRule,out FailedRuleMessage) == true)
          ValidContent.Add(Row);
        else 
          Logger.Write(this,string.Format("  Failed rule: {0}, Message: {1}",FailedRule,FailedRuleMessage));
      }

      return ValidContent;
    }
    private ChannelContentCollection GetValidContent(ChannelGroupManagers channelGroupManagers, ChannelContentCollection channelContentItems,DateTime currentDtm)
    {
      //Logger.Write(this,">> GetValidContent");
      ChannelContentCollection ValidContent = new ChannelContentCollection();
      ChannelContentDataset.ChannelContentRow Row;
      ChannelGroupManager ChannelGroupManager;

      for (Int32 ChannelGroupNo = 0;ChannelGroupNo < channelGroupManagers.Count;ChannelGroupNo++)
      {
        ChannelGroupManager = channelGroupManagers[ChannelGroupNo];
        string PlayingContentName = "<not playing>";
        if (ChannelGroupManager.IsCurrentlyPlaying() == true)
          PlayingContentName = ChannelGroupManager.currentlyPlayingContent.ContentItemFilename;
        Logger.Write(this,string.Format("ChannelGroupId:{0}, Playing:{1}",ChannelGroupManager.Id,PlayingContentName));

        for (Int32 ChannelContentNo = 0; ChannelContentNo < channelContentItems.Count; ChannelContentNo++)
        {
          Row = channelContentItems[ChannelContentNo];

          if (Row.ChannelGroupId != ChannelGroupManager.Id)
            continue;

          // Validate trigger rules
          string FailedRule;
          string FailedRuleMessage;
          Logger.Write(this,"");
          Logger.Write(this,string.Format("  Evaluating ChannelGroupId={0}, Region={1}, Filename={2}. currentDtm={3}",
            Row.ChannelGroupId,
            Row.GpsRegionId,
            Row.ContentItemFilename,
            currentDtm));
          if (rules.IsValid(ChannelGroupManager, Row, lastGpsPosition,lastMediaStartedGpsPosition, currentDtm,out FailedRule,out FailedRuleMessage) == true)
            ValidContent.Add(Row);
          else
            Logger.Write(this,string.Format("    Failed rule: {0}, Message : {1}", FailedRule,FailedRuleMessage));
        }
      }

      //Logger.Write(this,"<< GetValidContent");
      return ValidContent;
    }
    private void UpdateChannelContentDependencies(ChannelContentCollection channelContentItems)
    {
      for (Int32 ChannelContentNo = 0; ChannelContentNo < channelContentItems.Count; ChannelContentNo++)
        UpdateChannelContentDependencies(channelContentItems[ChannelContentNo]);
    }
    private void UpdateChannelContentDependencies(ChannelContentDataset.ChannelContentRow channelContentItem)
    {
      for (Int32 DependencyUpdaterNo = 0; DependencyUpdaterNo < currentMediaUpdaters.Count; DependencyUpdaterNo++)
        currentMediaUpdaters[DependencyUpdaterNo].Update(channelContentItem,FillerContent);
    }

    /// <summary>
    /// Start the content on the channel group. At this point all rules have 
    /// been evaluated and any content that might be playing on the channel group at the time should be stopped.
    /// </summary>
    /// <param name="channelContentItem">The contents item that is to be played</param>
    private Boolean PlayChannelGroupContent(Int32 channelGroupId,ChannelContentDataset.ChannelContentRow channelContentItem,out Boolean invalidContent)
    {
      invalidContent = false;

      Logger.Write(this,string.Format(">> PlayChannelGroupContent({0},{1})", channelGroupId,channelContentItem.ContentItemFilename));

      #region Get Channel group manager
      ChannelGroupManager ChannelGroupManager = channelGroupManagers.GetById(channelGroupId);
      if (ChannelGroupManager == null)
        return false;
      #endregion

      #region Issue a stop request if currently playing content
      if (ChannelGroupManager.IsCurrentlyPlaying() == true)
      {
        Logger.Write(this,string.Format("  Content is currently playing: {0}. Stopping...", ChannelGroupManager.currentlyPlayingContent.ContentItemFilename));
        MediaControlEvent.MediaControlResults ControlResult = StopChannelGroupContent(channelGroupId, ChannelGroupManager.currentlyPlayingContent.ContentItemFilename);
        Logger.Write(this,string.Format("  Stop Result={0}", ControlResult));
        if (ControlResult != MediaControlEvent.MediaControlResults.Ok)
        {
          Logger.Write(this,string.Format("  Could not stop: {0}. Returning", ControlResult));
          return false;
        }
      }
      #endregion

      #region Create media event and initiate play
      MediaControlEvent eventData = new MediaControlEvent(MediaControlEvent.MediaControlStates.Play, channelGroupId, channelContentItem.ContentItemFilename,MediaTypes.Sound,false);
      Logger.Write(this,string.Format("  Initiating play: {0}",channelContentItem.ContentItemFilename));

      try
      {
        OnMediaControl(eventData);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error issuing play command",exc);
        return false;
      }
      
      Logger.Write(this,string.Format("  Play Result={0}", eventData.Result));
      if (eventData.Result != MediaControlEvent.MediaControlResults.Ok)
      {
        Logger.Write(this,"  Could not play. Returning");
        invalidContent = true;

        return false;
      }
      #endregion

      #region Indicate that new content is playing
      lock (channelGroupContentLock)
      {
        try
        {
          if (lastGpsPosition != null)
          {
            lastMediaStartedGpsPosition.Heading = lastGpsPosition.Heading;
            lastMediaStartedGpsPosition.Latitude = lastGpsPosition.Latitude;
            lastMediaStartedGpsPosition.Longitude = lastGpsPosition.Longitude;
            lastMediaStartedGpsPosition.FixDtm = DateTime.Now;//.lastGpsPosition.FixDtm;          
          }

          //Logger.Write(this,"  ChannelGroupManager.Play...");
          ChannelGroupManager.Play(channelContentItem,DateTime.Now);
        }
        catch (Exception exc)
        {
          Logger.Write(this,"PlayChannelGroupContent: Error updating ChannelGroupManager", exc);
          return false;
        }
      }
      #endregion

      return true;
    }

    /// <summary>
    /// Start the content on each of the channel groups. Only start the first content item for the channel group
    /// </summary>
    private ChannelContentDataset.ChannelContentRow PlayChannelGroupContent(ChannelContentCollection channelContentItems,ChannelContentCollection invalidContentItems)
    {
      invalidContentItems.Clear();

      ChannelGroupManager ChannelGroupManager;
      ChannelContentDataset.ChannelContentRow Row;

      for (Int32 ChannelGroupNo = 0;ChannelGroupNo < channelGroupManagers.Count;ChannelGroupNo++)
      {
        ChannelGroupManager = channelGroupManagers[ChannelGroupNo];

        // Determine if the content to be played belongs to the channel group
        for (Int32 ChannelContentNo = 0; ChannelContentNo < channelContentItems.Count; ChannelContentNo++)
        {
          Row = channelContentItems[ChannelContentNo];

          // If the content to be played belongs to the channel group, play the content
          if (Row.ChannelGroupId == ChannelGroupManager.Id)
          {
            Boolean invalidContent;
            Boolean CanPlay = PlayChannelGroupContent(ChannelGroupManager.Id, Row,out invalidContent);
            if (CanPlay == true)
              return Row;
            else
            {
              if (invalidContent == true)
                invalidContentItems.Add(Row);
            }
          }
        }
      }

      return null;
    }
    private MediaControlEvent.MediaControlResults StopChannelGroupContent(Int32 channelGroupId, string contentFilename)
    {
      MediaControlEvent EventData = new MediaControlEvent(MediaControlEvent.MediaControlStates.Stop,channelGroupId,contentFilename);

      Logger.Write(this, "Issuing stop command");
      try
      {
        OnMediaControl(EventData);
      }
      catch (Exception exc)
      {
        Logger.Write(this,"Error issuing stop command",exc);

        return MediaControlEvent.MediaControlResults.Exception;
      }

      if (EventData.Result == MediaControlEvent.MediaControlResults.Ok)
      {
        lock (channelGroupContentLock)
        {
          try
          {
            ChannelGroupManager ChannelGroupManager = channelGroupManagers.GetById(channelGroupId);
            if (ChannelGroupManager == null)
              return MediaControlEvent.MediaControlResults.InvalidChannelGroupId;

            ChannelGroupManager.Stop(DateTime.Now);
            //Logger.Write(this,string.Format("ChannelGroupManager.Stop({0},{1})", channelGroupId,DateTime.Now));
          }
          catch (Exception exc)
          {
            Logger.Write(this,"Error in ChannelGroupManager.Stop", exc);
          }
        }
      }


      //Logger.Write(this,string.Format("<< StopChannelGroupContent Result={0}",EventData.Result));
      return EventData.Result;
    }
    private void PauseChannelGroupContent(Int32 channelGroupId, string contentFilename)
    {
      //Logger.Write(this,string.Format(">> PauseChannelGroupContent({0})", contentFilename));

      MediaControlEvent EventData;
      EventData = new MediaControlEvent(MediaControlEvent.MediaControlStates.Pause, channelGroupId, contentFilename);

      OnMediaControl(EventData);
      //Logger.Write(this,"<< PauseChannelGroupContent");
    }
    private void ResumeChannelGroupContent(Int32 channelGroupId, string contentFilename)
    {
      //Logger.Write(this,string.Format(">> ResumeChannelGroupContent({0})", contentFilename));

      MediaControlEvent EventData;
      EventData = new MediaControlEvent(MediaControlEvent.MediaControlStates.Resume, channelGroupId, contentFilename);

      OnMediaControl(EventData);
      //Logger.Write(this,"<< ResumeChannelGroupContent");
    }

    private void StartFillerContentThread()
    {
      //Logger.Write(this,">> StartContentFillerThread");
      if (fillerContentThread != null)
      {
        //Logger.Write(this,"  fillerContentThread != null");
        //Logger.Write(this,"<< StartContentFillerThread");
        return;        
      }

      stopFillerContentThread = false;
      fillerContentThread = new Thread(FillerContentThread);
      fillerContentThread.Name = "ContentManager.FillerContentThread";
      fillerContentThread.Start();
      //Logger.Write(this,"<< StartContentFillerThread");
    }
    private Boolean StopFillerContentThread()
    {
      //Logger.Write(this,">> StopContentFillerThread");
      try
      {
        if (fillerContentThread != null)
        {
          stopFillerContentThread = true;

          //Logger.Write(this,"Awaiting FillerContentThread to stop...");
          Boolean ThreadTerminated = fillerContentThread.Join(5000);
          if (ThreadTerminated == false)
          {
            //Logger.Write(this,"FillerContentThread did not stop. Aborting...");
            fillerContentThread.Abort();

            //Logger.Write(this,"Awaiting FillerContentThread to abort...");
            ThreadTerminated = fillerContentThread.Join(20000);
            if (ThreadTerminated == false)
            {
              //Logger.Write(this,"FillerContentThread did not abort. Throwing exception...");
              throw new InvalidOperationException("Cannot terminate fillerContentThread");
            }
            else
              Logger.Write(this,"FillerContentThread aborted");
          }
          else
            Logger.Write(this,"FillerContentThread stopped");

        }

        fillerContentThread = null;
        //Logger.Write(this,"<< StopContentFillerThread");
        return true;
      }
      catch (Exception exc)
      {
        Logger.Write(this,"StopFillerContentThread Exception", exc);
        return false;
      }
    }

    /// <summary>
    /// This method is the filler background thread handler. This method is executed every timer interval to determine if
    /// filler content is to play. This method:
    /// 1. Obtains a copy of the filler content collection
    /// 2. Apply rules to determine if filler content is to be played
    /// 3. Apply dependencies to filler content in the database as well as the in-memory collection of filler content items
    /// 4. Play the filler content
    /// </summary>
    private void FillerContentThread()
    {
      Logger.Write(this,"Starting FillerContentThread");

        while (stopFillerContentThread == false)
        {
          Thread.Sleep(DefaultFillerContentThreadSleep);

          try
          {
            #region Main loop

            #region Do nothing if content manager is not running
            if (IsRunning == false)
              continue;
            #endregion
         
            #region Do nothing if paused for notification
            if (isPausedForNotification == true)
            {
              Logger.Write(this, "  isPausedForNotification == true: ignoring filler content");
              continue;
            }
            #endregion
         
            #region Do nothing if there is no filler content
            lock (FillerContentLock)
            {
              if (FillerContent.Count == 0)
                continue;
            }
            #endregion

            #region Do nothing if we have no GPS fix
            if (lastGpsPosition == null)
              continue;
            #endregion

            #region Get valid content
//            Logger.Write(this,string.Format("  {0} filler content items",fillerContent.Count));
//            Logger.Write(this,"Getting valid content");

            DateTime currentDtm;
            currentDtm = DateTime.Now;

            ChannelContentCollection ValidContent;
            lock (FillerContentLock)
            {
              ValidContent = GetValidContent(channelGroupManagers,FillerContent,currentDtm);
            }
            if (ValidContent.Count == 0)
              continue;

//            Logger.Write(this,string.Format("  {0} valid content items to play",ValidContent.Count));
            #endregion

            #region Play filler content item and get any invalid items
            ChannelContentCollection invalidContentItems = new ChannelContentCollection();
            ChannelContentDataset.ChannelContentRow PlayedFillerContent = PlayChannelGroupContent(ValidContent,invalidContentItems);
            if (PlayedFillerContent == null)
            {
              if (invalidContentItems.Count > 0)
              {
//                Logger.Write(this,"  Could not play filler content: Invalid content items. Removing items from the filler content list");

                lock (FillerContentLock)
                {
                  foreach (ChannelContentDataset.ChannelContentRow invalidContentItem in invalidContentItems)
                    FillerContent.Remove(invalidContentItem);
                }
              }
              continue;
            }
            #endregion

            #region Update the filler content item dependencies
            UpdateChannelContentDependencies(PlayedFillerContent);
            #endregion

            #region Check if filler content can be retriggered
            Boolean CanRetriggerSpeedBelowThreshold = PlayedFillerContent.SpeedThresholdCanRetrigger;
            Boolean CanRetrigerSpeedAboveThreshold = PlayedFillerContent.SpeedThresholdCanRetrigger;

            // Remove the filler content item that was played because it has already played for the region. If the region is exited and
            // entered again, then the filler item can play again. Only do this if it must not repeat due to speed
            if ((CanRetriggerSpeedBelowThreshold == false) && (CanRetrigerSpeedAboveThreshold == false))
              FillerContent.Remove(PlayedFillerContent);
            #endregion

            #endregion
          }
          catch (Exception exc)
          {
            #region Log exception
            Logger.Write(this, "ContentManager.FillerContentThread Exception", exc);

            string Filename = "ContentManager.FillerContentThread.Crashlog.txt";
            TextWriter CrashLogWriter = new StreamWriter(Path.ExecutablePath + Filename, false);
            CrashLogWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            CrashLogWriter.WriteLine(exc.ToString());
            CrashLogWriter.Flush();
            CrashLogWriter.Close();
            #endregion
          }
        }

      Logger.Write(this, "Stopping FillerContentThread");
    }

    private void OnMediaControl(MediaControlEvent eventData)
    {
      if (MediaControl == null)
        return;

      GoodGuideEventArgs Args = new GoodGuideEventArgs("ContentManager",eventData);

      MediaControl(this, Args);
    }
    
    /// <summary>
    /// This method resets the filler content base time for the channel group associated with the region if no 
    /// content is playing for the channel group.
    /// </summary>
    /// <param name="regionId"></param>
    private void ResetRegionFillerBaseTime(Int32 regionId)
    {
      ChannelContentDataset.ChannelContentRow Row;
      ChannelGroupManager ChannelGroupManager;

      // Iterate through filler content to find filler content for the region
      for (Int32 FillerContentNo = 0; FillerContentNo < FillerContent.Count; FillerContentNo++)
      {
        Row = FillerContent[FillerContentNo];
        if (Row.IsChannelGroupIdNull() == true)
        {
          if (Row.IsIdNull() == false)
            Logger.Write(this, string.Format("ResetRegionFillerBaseTime: Row.Id({0}) has no ChannelGroupId", Row.Id));
          else
            Logger.Write(this, "ResetRegionFillerBaseTime: Row has no ChannelGroupId");
          continue;
        }

        ChannelGroupManager = channelGroupManagers.GetById(Row.ChannelGroupId);
        if (ChannelGroupManager == null)
          continue;

        // If filler content for the region was found, then determine the ChannelGroup.
        // If the ChannelGroup is not playing, then reset the filler content base time
        // A region has only one ChannelGroup, therefore, if the channel group base time has been set, the routine can exit.
          if ((Row.GpsRegionId == regionId) && (ChannelGroupManager.currentlyPlayingContent == null))
        {
          lock (channelGroupContentLock)
          {
            ChannelGroupManager.ResetFillerBaseTime(DateTime.Now);
          }
          return;
        }
      }
    }

    /// <summary>
    /// Stop the playing of filler content for the region's associated channel group
    /// </summary>
    private void StopRegionFillerContent(Int32 regionId,string triggerType)
    {
      //Logger.Write(this,string.Format(">> StopRegionFillerContent(regionId={0} triggerType={1})", regionId,triggerType));

      // Iterate through filler content to find filler content for the region
      for (Int32 ChannelGroupManagerNo = 0; ChannelGroupManagerNo < channelGroupManagers.Count; ChannelGroupManagerNo++)
      {
        ChannelGroupManager ChannelGroupManager = channelGroupManagers[ChannelGroupManagerNo];
        if (ChannelGroupManager == null)
          continue;

        // If filler content for the region was found, then determine the ChannelGroup.
        // If the ChannelGroup is playing content for that region, then stop it
        // A region has only one ChannelGroup, therefore, if the channel group was stopped, the routine can exit.
        ChannelContentDataset.ChannelContentRow CurrentlyPlayingContent = ChannelGroupManager.currentlyPlayingContent;

        if ((CurrentlyPlayingContent != null) && (CurrentlyPlayingContent.TriggerType == triggerType) && (CurrentlyPlayingContent.GpsRegionId == regionId))
          {
            //Logger.Write(this,string.Format("   Stopping ChannelGroupId={0} ContentItemFilename={1})", CurrentlyPlayingContent.ChannelGroupId, CurrentlyPlayingContent.ContentItemFilename));
            StopChannelGroupContent(CurrentlyPlayingContent.ChannelGroupId, CurrentlyPlayingContent.ContentItemFilename);
            return;
        }
      }
    }

    #region Content control
    private void ContentControl(ContentControlEvent eventData)
    {
      Logger.Write(this, string.Format(">> ContentControl isPausedManually:{0}, isPausedForNotification:{1}", isPausedManually, isPausedForNotification));

      switch (eventData.ControlState)
      {
        case ContentControlEvent.ControlStates.Pause:
          Logger.Write(this, "Pausing all content");
          isPausedManually = true;
          PauseAllContent();
          break;
        case ContentControlEvent.ControlStates.Resume:
          if (isPausedManually && !isPausedForNotification)
          {
            Logger.Write(this, "Resuming all content");
            ResumeAllContent();
            isPausedManually = false;
          }
          break;
        case ContentControlEvent.ControlStates.Repeat:
          RepeatAllContent();
          break;
        case ContentControlEvent.ControlStates.Stop:
          StopAllContent(false);
          break;
      }

    }

    #endregion

    private void BuildChannelGroupContent()
    {
      ChannelGroupDataset.ChannelGroupDataTable ChannelGroups;
      lock (channelGroupBll)
      {
        ChannelGroups = (ChannelGroupDataset.ChannelGroupDataTable)channelGroupBll.GetAll();
      }
      for (Int32 ChannelGroupNo = 0;ChannelGroupNo < ChannelGroups.Rows.Count;ChannelGroupNo++)
      {
        channelGroupManagers.Add(new ChannelGroupManager(ChannelGroups[ChannelGroupNo].Id));
      }
    }
    private void PauseTimer_TimerExpired(object sender,EventArgs e)
    {
      Logger.Write(this, ">> PauseTimer_TimerExpired");

      StopAllContent(false);
    }

    #region Fields
    public readonly LoggingHelper Logger = new LoggingHelper();
    private readonly object mediaStateLock;

    private volatile Boolean isRunning = false;
    private readonly Boolean playAllChannels = true;
    private readonly Byte selectedChannelGroupId = 0;
    private readonly ChannelContentBll channelContentBll = null;
    private readonly ChannelGroupBll channelGroupBll = null;
    private readonly IRepositoryLocator repositoryLocator = null;
    private readonly ContentManagerRules rules = new ContentManagerRules();
    private readonly List<IChannelContentUpdater> currentMediaUpdaters = new List<IChannelContentUpdater>();
    private readonly object channelGroupContentLock = new object();

    public readonly object FillerContentLock = new object();
    private ChannelContentCollection fillerContent = new ChannelContentCollection();
    
    private readonly ChannelGroupManagers channelGroupManagers = new ChannelGroupManagers();
    private Thread fillerContentThread = null;
    private Boolean stopFillerContentThread = false;
    private GpsPositionEvent lastGpsPosition = null;
    private readonly GpsPositionEvent lastMediaStartedGpsPosition = new GpsPositionEvent();
    private volatile Boolean isPausedForNotification = false;
    private volatile Boolean isPausedManually = false;
    private readonly SingleShotTimer pauseTimer;

    #endregion
  }
}
