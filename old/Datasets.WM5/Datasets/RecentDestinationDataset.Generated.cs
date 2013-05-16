//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class RecentDestinationDataset
  {
    public partial class RecentDestinationRow
    {
      #region String properties
      public string sVisitedDat
      {
        get
        {
          if (IsNull(tableRecentDestination.VisitedDatColumn) == true)
            return string.Empty;
          else
            return VisitedDat.ToString("yyyy-MM-dd hh:mm:ss");
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableRecentDestination.VisitedDatColumn] = DBNull.Value;
          else
            VisitedDat = Convert.ToDateTime(value.Trim());
        }
      }
      public string sDestinationId
      {
        get
        {
          if (IsNull(tableRecentDestination.DestinationIdColumn) == true)
            return string.Empty;
          else
            return DestinationId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableRecentDestination.DestinationIdColumn] = DBNull.Value;
          else
            DestinationId = Convert.ToInt32(value.Trim());
        }
      }
      public string sLatitude
      {
        get
        {
          if (IsNull(tableRecentDestination.LatitudeColumn) == true)
            return string.Empty;
          else
            return Latitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableRecentDestination.LatitudeColumn] = DBNull.Value;
          else
            Latitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sLongitude
      {
        get
        {
          if (IsNull(tableRecentDestination.LongitudeColumn) == true)
            return string.Empty;
          else
            return Longitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableRecentDestination.LongitudeColumn] = DBNull.Value;
          else
            Longitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sName
      {
        get
        {
          if (IsNull(tableRecentDestination.NameColumn) == true)
            return string.Empty;
          else
            return Name;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableRecentDestination.NameColumn] = DBNull.Value;
          else
            Name = value.Trim();
        }
      }
      #endregion
 
    }
  }
}