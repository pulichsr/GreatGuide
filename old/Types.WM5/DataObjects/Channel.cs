using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class Channel
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (Id == null)
        result.AddValidationError("Id is undefined");

      if (ChannelGroupId == null)
        result.AddValidationError("ChannelGroupId is undefined");

      if (string.IsNullOrEmpty(ContentPath) == true)
        ContentPath = string.Empty;

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public Int32? Id;
	  public Int32? ChannelGroupId;
		public string ContentPath;
		public string Language;

    #endregion
  }

  public class Channels: List<Channel>
  {
  }
}
