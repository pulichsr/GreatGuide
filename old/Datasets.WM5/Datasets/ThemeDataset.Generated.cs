//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ThemeDataset
  {
    public partial class ThemeRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableTheme.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableTheme.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sName
      {
        get
        {
          if (IsNull(tableTheme.NameColumn) == true)
            return string.Empty;
          else
            return Name;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableTheme.NameColumn] = DBNull.Value;
          else
            Name = value.Trim();
        }
      }
      public string sDescription
      {
        get
        {
          if (IsNull(tableTheme.DescriptionColumn) == true)
            return string.Empty;
          else
            return Description;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableTheme.DescriptionColumn] = DBNull.Value;
          else
            Description = value.Trim();
        }
      }
      #endregion
 
    }
  }
}