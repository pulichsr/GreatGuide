//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;


namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ChannelGroupDataset
  {
    public partial class ChannelGroupRow
    {
		#region String properties
		public string sId
		{
			get
			{
			  if (IsNull(tableChannelGroup.IdColumn) == true)
			    return string.Empty;
			  else
			    return Id.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableChannelGroup.IdColumn] = DBNull.Value;
			  else
			    Id = Convert.ToInt32(value.Trim());
			}
		}
		public string sName
		{
			get
			{
			  if (IsNull(tableChannelGroup.NameColumn) == true)
			    return string.Empty;
			  else
			    return Name;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableChannelGroup.NameColumn] = DBNull.Value;
			  else
			    Name = value.Trim();
			}
		}
		#endregion
 
    }
  }
}
		  
