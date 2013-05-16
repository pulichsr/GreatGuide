using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class ContentItem
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (Id == null)
        result.AddValidationError("Id is undefined");

      if (string.IsNullOrEmpty(ContentTypeCode))
        result.AddValidationError("ContentTypeCode is undefined");

      if (string.IsNullOrEmpty(Filename))
        result.AddValidationError("Filename is undefined");

      if (ChannelContentId == null)
        result.AddValidationError("ChannelContentId is undefined");

      if (ChannelGroupId == null)
        result.AddValidationError("ChannelGroupId is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2
    public Int32? Id;
    public string ContentTypeCode;
    public string Filename;
    public Int32? ChannelContentId;
    public Int32? ChannelGroupId;

    public string Description;
    public bool IsFillerContent;
    public Int32? DisplaySeq;
    #endregion
  }

  public class ContentItems: List<ContentItem>
  {
  }
}
