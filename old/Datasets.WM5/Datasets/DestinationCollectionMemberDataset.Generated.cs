//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class DestinationCollectionMemberDataset
  {
    public partial class DestinationCollectionMemberRow
    {
      #region String properties
      public string sCollectionId
      {
        get
        {
          if (IsNull(tableDestinationCollectionMember.CollectionIdColumn) == true)
            return string.Empty;
          else
            return CollectionId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationCollectionMember.CollectionIdColumn] = DBNull.Value;
          else
            CollectionId = Convert.ToInt32(value.Trim());
        }
      }
      public string sDestinationId
      {
        get
        {
          if (IsNull(tableDestinationCollectionMember.DestinationIdColumn) == true)
            return string.Empty;
          else
            return DestinationId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableDestinationCollectionMember.DestinationIdColumn] = DBNull.Value;
          else
            DestinationId = Convert.ToInt32(value.Trim());
        }
      }
      #endregion
 
    }
  }
}