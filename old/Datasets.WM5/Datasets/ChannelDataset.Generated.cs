//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ChannelDataset
  {
    public partial class ChannelRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableChannel.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannel.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sChannelGroupId
      {
        get
        {
          if (IsNull(tableChannel.ChannelGroupIdColumn) == true)
            return string.Empty;
          else
            return ChannelGroupId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannel.ChannelGroupIdColumn] = DBNull.Value;
          else
            ChannelGroupId = Convert.ToInt32(value.Trim());
        }
      }
      public string sContentPath
      {
        get
        {
          if (IsNull(tableChannel.ContentPathColumn) == true)
            return string.Empty;
          else
            return ContentPath;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannel.ContentPathColumn] = DBNull.Value;
          else
            ContentPath = value.Trim();
        }
      }
      public string sLanguage
      {
        get
        {
          if (IsNull(tableChannel.LanguageColumn) == true)
            return string.Empty;
          else
            return Language;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableChannel.LanguageColumn] = DBNull.Value;
          else
            Language = value.Trim();
        }
      }
      #endregion
 
    }
  }
}