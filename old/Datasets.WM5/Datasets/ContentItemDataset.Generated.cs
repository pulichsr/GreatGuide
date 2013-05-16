//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;


namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ContentItemDataset
  {
    public partial class ContentItemRow
    {
		#region String properties
		public string sId
		{
			get
			{
			  if (IsNull(tableContentItem.IdColumn) == true)
			    return string.Empty;
			  else
			    return Id.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableContentItem.IdColumn] = DBNull.Value;
			  else
			    Id = Convert.ToInt32(value.Trim());
			}
		}
		public string sContentTypeCode
		{
			get
			{
			  if (IsNull(tableContentItem.ContentTypeCodeColumn) == true)
			    return string.Empty;
			  else
			    return ContentTypeCode;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableContentItem.ContentTypeCodeColumn] = DBNull.Value;
			  else
			    ContentTypeCode = value.Trim();
			}
		}
		public string sFilename
		{
			get
			{
			  if (IsNull(tableContentItem.FilenameColumn) == true)
			    return string.Empty;
			  else
			    return Filename;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableContentItem.FilenameColumn] = DBNull.Value;
			  else
			    Filename = value.Trim();
			}
		}
		public string sChannelGroupId
		{
			get
			{
			  if (IsNull(tableContentItem.ChannelGroupIdColumn) == true)
			    return string.Empty;
			  else
			    return ChannelGroupId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableContentItem.ChannelGroupIdColumn] = DBNull.Value;
			  else
			    ChannelGroupId = Convert.ToInt32(value.Trim());
			}
		}
		public string sChannelContentId
		{
			get
			{
			  if (IsNull(tableContentItem.ChannelContentIdColumn) == true)
			    return string.Empty;
			  else
			    return ChannelContentId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableContentItem.ChannelContentIdColumn] = DBNull.Value;
			  else
			    ChannelContentId = Convert.ToInt32(value.Trim());
			}
		}
    public string sDescription
    {
      get
      {
        if (IsNull(tableContentItem.DescriptionColumn) == true)
          return string.Empty;
        else
          return Description;
      }
      set
      {
        if (value.Trim().Length == 0)
          this[tableContentItem.DescriptionColumn] = string.Empty;
        else
          Description = value;
      }
    }
      public string sDisplaySeq
      {
        get
        {
          if (IsNull(tableContentItem.DisplaySeqColumn) == true)
            return string.Empty;
          else
            return DisplaySeq.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableContentItem.DisplaySeqColumn] = DBNull.Value;
          else
            DisplaySeq = Convert.ToInt32(value.Trim());
        }
      }

		#endregion
 
    }
  }
}
		  
