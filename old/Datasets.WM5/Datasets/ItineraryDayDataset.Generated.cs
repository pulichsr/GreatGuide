//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ItineraryDayDataset
  {
    public partial class ItineraryDayRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableItineraryDay.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItineraryDay.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sItineraryDat
      {
        get
        {
          if (IsNull(tableItineraryDay.ItineraryDatColumn) == true)
            return string.Empty;
          else
            return ItineraryDat.ToString("yyyy-MM-dd hh:mm:ss");
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItineraryDay.ItineraryDatColumn] = DBNull.Value;
          else
            ItineraryDat = Convert.ToDateTime(value.Trim());
        }
      }
      public string sComment
      {
        get
        {
          if (IsNull(tableItineraryDay.CommentColumn) == true)
            return string.Empty;
          else
            return Comment;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItineraryDay.CommentColumn] = DBNull.Value;
          else
            Comment = value.Trim();
        }
      }
      #endregion
 
    }
  }
}