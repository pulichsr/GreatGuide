//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class GpsRegionDataset
  {
    public partial class GpsRegionRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableGpsRegion.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sRegionType
      {
        get
        {
          if (IsNull(tableGpsRegion.RegionTypeColumn) == true)
            return string.Empty;
          else
            return RegionType;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.RegionTypeColumn] = DBNull.Value;
          else
            RegionType = value.Trim();
        }
      }
      public string sRegionData
      {
        get
        {
          if (IsNull(tableGpsRegion.RegionDataColumn) == true)
            return string.Empty;
          else
            return RegionData;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.RegionDataColumn] = DBNull.Value;
          else
            RegionData = value.Trim();
        }
      }
      public string sMinLatitude
      {
        get
        {
          if (IsNull(tableGpsRegion.MinLatitudeColumn) == true)
            return string.Empty;
          else
            return MinLatitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.MinLatitudeColumn] = DBNull.Value;
          else
            MinLatitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sMaxLatitude
      {
        get
        {
          if (IsNull(tableGpsRegion.MaxLatitudeColumn) == true)
            return string.Empty;
          else
            return MaxLatitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.MaxLatitudeColumn] = DBNull.Value;
          else
            MaxLatitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sMinLongitude
      {
        get
        {
          if (IsNull(tableGpsRegion.MinLongitudeColumn) == true)
            return string.Empty;
          else
            return MinLongitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.MinLongitudeColumn] = DBNull.Value;
          else
            MinLongitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sMaxLongitude
      {
        get
        {
          if (IsNull(tableGpsRegion.MaxLongitudeColumn) == true)
            return string.Empty;
          else
            return MaxLongitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.MaxLongitudeColumn] = DBNull.Value;
          else
            MaxLongitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sResetOnEntry
      {
        get
        {
          if (IsNull(tableGpsRegion.ResetOnEntryColumn) == true)
            return string.Empty;
          else
            return ResetOnEntry.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.ResetOnEntryColumn] = DBNull.Value;
          else
            ResetOnEntry = Convert.ToBoolean(value.Trim());
        }
      }
      public string sMasterAreaId
      {
        get
        {
          if (IsNull(tableGpsRegion.MasterAreaIdColumn) == true)
            return string.Empty;
          else
            return MasterAreaId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.MasterAreaIdColumn] = DBNull.Value;
          else
            MasterAreaId = Convert.ToInt32(value.Trim());
        }
      }
      public string sThemeId
      {
        get
        {
          if (IsNull(tableGpsRegion.ThemeIdColumn) == true)
            return string.Empty;
          else
            return ThemeId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.ThemeIdColumn] = DBNull.Value;
          else
            ThemeId = Convert.ToInt32(value.Trim());
        }
      }
      public string sName
      {
        get
        {
          if (IsNull(tableGpsRegion.NameColumn) == true)
            return string.Empty;
          else
            return Name;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableGpsRegion.NameColumn] = DBNull.Value;
          else
            Name = value.Trim();
        }
      }
      #endregion
 
    }
  }
}