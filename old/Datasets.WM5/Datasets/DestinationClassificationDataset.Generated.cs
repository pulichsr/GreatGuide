//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class DestinationClassificationDataset
  {
    public partial class DestinationClassificationRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableDestinationClassification.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationClassification.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sDestinationTypeId
      {
        get
        {
          if (IsNull(tableDestinationClassification.DestinationTypeIdColumn) == true)
            return string.Empty;
          else
            return DestinationTypeId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationClassification.DestinationTypeIdColumn] = DBNull.Value;
          else
            DestinationTypeId = Convert.ToInt32(value.Trim());
        }
      }
      public string sName
      {
        get
        {
          if (IsNull(tableDestinationClassification.NameColumn) == true)
            return string.Empty;
          else
            return Name;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationClassification.NameColumn] = DBNull.Value;
          else
            Name = value.Trim();
        }
      }
      public string sDescription
      {
        get
        {
          if (IsNull(tableDestinationClassification.DescriptionColumn) == true)
            return string.Empty;
          else
            return Description;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationClassification.DescriptionColumn] = DBNull.Value;
          else
            Description = value.Trim();
        }
      }
      public string sCode
      {
        get
        {
          if (IsNull(tableDestinationClassification.CodeColumn) == true)
            return string.Empty;
          else
            return Code;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationClassification.CodeColumn] = DBNull.Value;
          else
            Code = value.Trim();
        }
      }
      #endregion
 
    }
  }
}