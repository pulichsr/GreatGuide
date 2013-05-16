//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class MasterAreaDataset
  {
    public partial class MasterAreaRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableMasterArea.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableMasterArea.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sName
      {
        get
        {
          if (IsNull(tableMasterArea.NameColumn) == true)
            return string.Empty;
          else
            return Name;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableMasterArea.NameColumn] = DBNull.Value;
          else
            Name = value.Trim();
        }
      }
      public string sDescription
      {
        get
        {
          if (IsNull(tableMasterArea.DescriptionColumn) == true)
            return string.Empty;
          else
            return Description;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableMasterArea.DescriptionColumn] = DBNull.Value;
          else
            Description = value.Trim();
        }
      }
      public string sMinLongitude
      {
        get
        {
          if (IsNull(tableMasterArea.MinLongitudeColumn) == true)
            return string.Empty;
          else
            return MinLongitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableMasterArea.MinLongitudeColumn] = DBNull.Value;
          else
            MinLongitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sMaxLongitude
      {
        get
        {
          if (IsNull(tableMasterArea.MaxLongitudeColumn) == true)
            return string.Empty;
          else
            return MaxLongitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableMasterArea.MaxLongitudeColumn] = DBNull.Value;
          else
            MaxLongitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sMinLatitude
      {
        get
        {
          if (IsNull(tableMasterArea.MinLatitudeColumn) == true)
            return string.Empty;
          else
            return MinLatitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableMasterArea.MinLatitudeColumn] = DBNull.Value;
          else
            MinLatitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sMaxLatitude
      {
        get
        {
          if (IsNull(tableMasterArea.MaxLatitudeColumn) == true)
            return string.Empty;
          else
            return MaxLatitude.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableMasterArea.MaxLatitudeColumn] = DBNull.Value;
          else
            MaxLatitude = Convert.ToSingle(value.Trim());
        }
      }
      public string sContentBasePath
      {
        get
        {
          if (IsNull(tableMasterArea.ContentBasePathColumn) == true)
            return string.Empty;
          else
            return ContentBasePath;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableMasterArea.ContentBasePathColumn] = DBNull.Value;
          else
            ContentBasePath = value.Trim();
        }
      }
      public string sRegionData
      {
        get
        {
          if (IsNull(tableMasterArea.RegionDataColumn) == true)
            return string.Empty;
          else
            return RegionData;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableMasterArea.RegionDataColumn] = DBNull.Value;
          else
            RegionData = value.Trim();
        }
      }
      #endregion
 
    }
  }
}