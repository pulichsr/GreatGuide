//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ItineraryDestinationDataset
  {
    public partial class ItineraryDestinationRow
    {
      #region String properties
      public string sId
      {
        get
        {
          if (IsNull(tableItineraryDestination.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItineraryDestination.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sItineraryDayId
      {
        get
        {
          if (IsNull(tableItineraryDestination.ItineraryDayIdColumn) == true)
            return string.Empty;
          else
            return ItineraryDayId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItineraryDestination.ItineraryDayIdColumn] = DBNull.Value;
          else
            ItineraryDayId = Convert.ToInt32(value.Trim());
        }
      }
      public string sDestinationId
      {
        get
        {
          if (IsNull(tableItineraryDestination.DestinationIdColumn) == true)
            return string.Empty;
          else
            return DestinationId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItineraryDestination.DestinationIdColumn] = DBNull.Value;
          else
            DestinationId = Convert.ToInt32(value.Trim());
        }
      }
      public string sDestinationOrder
      {
        get
        {
          if (IsNull(tableItineraryDestination.DestinationOrderColumn) == true)
            return string.Empty;
          else
            return DestinationOrder.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableItineraryDestination.DestinationOrderColumn] = DBNull.Value;
          else
            DestinationOrder = Convert.ToInt16(value.Trim());
        }
      }
      #endregion
 
    }
  }
}