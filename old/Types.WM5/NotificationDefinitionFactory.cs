using System;

namespace Nucleo.GoodGuide.Types
{
  public static class NotificationDefinitionFactory
  {
    public const char FieldSeparator = ',';

    public static NotificationDefinition Create(string metadata)
    {
      Guard.ArgumentNotNullOrEmptyString(metadata, "metadata");

      string[] fields = metadata.Split(FieldSeparator);
      if (fields.Length != 3)
        throw new FormatException(string.Format("{0} is an invalid metadata format",metadata));

      #region NotificationType
      NotificationType notificationType;
      switch (fields[0].ToUpper())
      {
        case "D":
          notificationType = NotificationType.Distance;
          break;
        case "T":
          notificationType = NotificationType.Time;
          break;
        default:
          throw new FormatException(string.Format("{0} is an invalid NotificationType", fields[0]));
      }
      #endregion

      #region Lower
      Int32 lower = 0;
      try
      {
        lower = Convert.ToInt32(fields[1]);
      }
      catch
      {
        throw new FormatException(string.Format("'{0}' is an invalid lower limit", fields[1]));
      }
      #endregion

      #region Upper
      Int32 upper = 0;
      try
      {
        upper = Convert.ToInt32(fields[2]);
      }
      catch
      {
        throw new FormatException(string.Format("'{0}' is an invalid upper limit", fields[2]));
      }
      #endregion

      return new NotificationDefinition(notificationType,lower,upper);
    }
  }
}