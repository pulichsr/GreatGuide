//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ItineraryDataset
  {
    public partial class ItineraryRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableItinerary.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sFirstName
      {
        get
        {
          if (IsNull(tableItinerary.FirstNameColumn) == true)
            return string.Empty;
          else
            return FirstName;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.FirstNameColumn] = DBNull.Value;
          else
            FirstName = value.Trim();
        }
      }
      public string sLastName
      {
        get
        {
          if (IsNull(tableItinerary.LastNameColumn) == true)
            return string.Empty;
          else
            return LastName;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.LastNameColumn] = DBNull.Value;
          else
            LastName = value.Trim();
        }
      }
      public string sTitle
      {
        get
        {
          if (IsNull(tableItinerary.TitleColumn) == true)
            return string.Empty;
          else
            return Title;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.TitleColumn] = DBNull.Value;
          else
            Title = value.Trim();
        }
      }
      public string sArrivalDat
      {
        get
        {
          if (IsNull(tableItinerary.ArrivalDatColumn) == true)
            return string.Empty;
          else
            return ArrivalDat.ToString("yyyy-MM-dd hh:mm:ss");
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.ArrivalDatColumn] = DBNull.Value;
          else
            ArrivalDat = Convert.ToDateTime(value.Trim());
        }
      }
      public string sDepartureDat
      {
        get
        {
          if (IsNull(tableItinerary.DepartureDatColumn) == true)
            return string.Empty;
          else
            return DepartureDat.ToString("yyyy-MM-dd hh:mm:ss");
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.DepartureDatColumn] = DBNull.Value;
          else
            DepartureDat = Convert.ToDateTime(value.Trim());
        }
      }
      public string sGracePeriod
      {
        get
        {
          if (IsNull(tableItinerary.GracePeriodColumn) == true)
            return string.Empty;
          else
            return GracePeriod.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.GracePeriodColumn] = DBNull.Value;
          else
            GracePeriod = Convert.ToInt16(value.Trim());
        }
      }
      public string sBookingReference
      {
        get
        {
          if (IsNull(tableItinerary.BookingReferenceColumn) == true)
            return string.Empty;
          else
            return BookingReference;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.BookingReferenceColumn] = DBNull.Value;
          else
            BookingReference = value.Trim();
        }
      }
      public string sGeofenceData
      {
        get
        {
          if (IsNull(tableItinerary.GeofenceDataColumn) == true)
            return string.Empty;
          else
            return GeofenceData;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.GeofenceDataColumn] = DBNull.Value;
          else
            GeofenceData = value.Trim();
        }
      }
      public string sBranding1
      {
        get
        {
          if (IsNull(tableItinerary.Branding1Column) == true)
            return string.Empty;
          else
            return Branding1;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.Branding1Column] = DBNull.Value;
          else
            Branding1 = value.Trim();
        }
      }
      public string sBranding2
      {
        get
        {
          if (IsNull(tableItinerary.Branding2Column) == true)
            return string.Empty;
          else
            return Branding2;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.Branding2Column] = DBNull.Value;
          else
            Branding2 = value.Trim();
        }
      }
      public string sBranding3
      {
        get
        {
          if (IsNull(tableItinerary.Branding3Column) == true)
            return string.Empty;
          else
            return Branding3;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.Branding3Column] = DBNull.Value;
          else
            Branding3 = value.Trim();
        }
      }
      public string sCulture
      {
        get
        {
          if (IsNull(tableItinerary.CultureColumn) == true)
            return string.Empty;
          else
            return Culture;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItinerary.CultureColumn] = DBNull.Value;
          else
            Culture = value.Trim();
        }
      }
      #endregion
 
    }
  }
}