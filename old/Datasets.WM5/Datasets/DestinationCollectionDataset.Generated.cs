//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class DestinationCollectionDataset
  {
    public partial class DestinationCollectionRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableDestinationCollection.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationCollection.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sCode
      {
        get
        {
          if (IsNull(tableDestinationCollection.CodeColumn) == true)
            return string.Empty;
          else
            return Code;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationCollection.CodeColumn] = DBNull.Value;
          else
            Code = value.Trim();
        }
      }
      public string sName
      {
        get
        {
          if (IsNull(tableDestinationCollection.NameColumn) == true)
            return string.Empty;
          else
            return Name;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationCollection.NameColumn] = DBNull.Value;
          else
            Name = value.Trim();
        }
      }
      #endregion
 
    }
  }
}