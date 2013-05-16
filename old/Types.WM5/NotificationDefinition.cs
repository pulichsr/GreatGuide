using System;
using System.Collections.Generic;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.Types
{
  public class NotificationDefinition
  {
    public NotificationDefinition(NotificationType notificationType,int lower,int upper)
    {
      this.notificationType = notificationType;
      this.lower = lower;
      this.upper = upper;
    }

    public NotificationType NotificationType
    {
      get { return notificationType; }
    }
    public int Lower
    {
      get { return lower; }
    }
    public int Upper
    {
      get { return upper; }
    }


    private readonly NotificationType notificationType;
    private readonly Int32 lower;
    private readonly Int32 upper;
  }

  public class NotificationDefinitions:
    List<NotificationDefinition>
  {}
}