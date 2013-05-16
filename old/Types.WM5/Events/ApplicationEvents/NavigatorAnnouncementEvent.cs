using System;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
	public class NavigatorAnnouncementEvent : ApplicationEvent
  {
    public NavigatorAnnouncementEvent(Boolean abbouncementState)
    {
		  this.announcementState = abbouncementState;
		}

	  public Boolean AnnouncementState
    {
      get { return announcementState; }
      set { announcementState = value; }
    }

    private Boolean announcementState = false;
  }
}