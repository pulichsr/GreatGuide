//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;


namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class MySelectionDataset
  {
    public partial class MySelectionRow
    {
		#region String properties
		public string sDestinationId
		{
			get
			{
			  if (IsNull(tableMySelection.DestinationIdColumn) == true)
			    return string.Empty;
			  else
			    return DestinationId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableMySelection.DestinationIdColumn] = DBNull.Value;
			  else
			    DestinationId = Convert.ToInt32(value.Trim());
			}
		}
		#endregion
 
    }
  }
}
		  
