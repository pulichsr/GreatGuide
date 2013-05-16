//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class DestinationTypeDataset
  {
    public partial class DestinationTypeRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableDestinationType.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationType.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sName
      {
        get
        {
          if (IsNull(tableDestinationType.NameColumn) == true)
            return string.Empty;
          else
            return Name;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationType.NameColumn] = DBNull.Value;
          else
            Name = value.Trim();
        }
      }
      public string sDescription
      {
        get
        {
          if (IsNull(tableDestinationType.DescriptionColumn) == true)
            return string.Empty;
          else
            return Description;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationType.DescriptionColumn] = DBNull.Value;
          else
            Description = value.Trim();
        }
      }
      public string sCode
      {
        get
        {
          if (IsNull(tableDestinationType.CodeColumn) == true)
            return string.Empty;
          else
            return Code;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationType.CodeColumn] = DBNull.Value;
          else
            Code = value.Trim();
        }
      }
      public string sComment1Label
      {
        get
        {
          if (IsNull(tableDestinationType.Comment1LabelColumn) == true)
            return string.Empty;
          else
            return Comment1Label;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationType.Comment1LabelColumn] = DBNull.Value;
          else
            Comment1Label = value.Trim();
        }
      }
      public string sComment2Label
      {
        get
        {
          if (IsNull(tableDestinationType.Comment2LabelColumn) == true)
            return string.Empty;
          else
            return Comment2Label;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationType.Comment2LabelColumn] = DBNull.Value;
          else
            Comment2Label = value.Trim();
        }
      }
      public string sComment3Label
      {
        get
        {
          if (IsNull(tableDestinationType.Comment3LabelColumn) == true)
            return string.Empty;
          else
            return Comment3Label;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationType.Comment3LabelColumn] = DBNull.Value;
          else
            Comment3Label = value.Trim();
        }
      }
      public string sComment4Label
      {
        get
        {
          if (IsNull(tableDestinationType.Comment4LabelColumn) == true)
            return string.Empty;
          else
            return Comment4Label;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationType.Comment4LabelColumn] = DBNull.Value;
          else
            Comment4Label = value.Trim();
        }
      }
      public string sIconResourceName
      {
        get
        {
          if (IsNull(tableDestinationType.IconResourceNameColumn) == true)
            return string.Empty;
          else
            return IconResourceName;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationType.IconResourceNameColumn] = DBNull.Value;
          else
            IconResourceName = value.Trim();
        }
      }
      #endregion
 
    }
  }
}