//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;


namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ControlDefinitionDataset
  {
    public partial class ControlDefinitionRow
    {
		#region String properties
		public string sId
		{
			get
			{
			  if (IsNull(tableControlDefinition.IdColumn) == true)
			    return string.Empty;
			  else
			    return Id.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.IdColumn] = DBNull.Value;
			  else
			    Id = Convert.ToInt32(value.Trim());
			}
		}
		public string sFormId
		{
			get
			{
			  if (IsNull(tableControlDefinition.FormIdColumn) == true)
			    return string.Empty;
			  else
			    return FormId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.FormIdColumn] = DBNull.Value;
			  else
			    FormId = Convert.ToInt32(value.Trim());
			}
		}
		public string sName
		{
			get
			{
			  if (IsNull(tableControlDefinition.NameColumn) == true)
			    return string.Empty;
			  else
			    return Name;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.NameColumn] = DBNull.Value;
			  else
			    Name = value.Trim();
			}
		}
		public string sText
		{
			get
			{
			  if (IsNull(tableControlDefinition.TextColumn) == true)
			    return string.Empty;
			  else
			    return Text;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.TextColumn] = DBNull.Value;
			  else
			    Text = value.Trim();
			}
		}
		public string sDescription
		{
			get
			{
			  if (IsNull(tableControlDefinition.DescriptionColumn) == true)
			    return string.Empty;
			  else
			    return Description;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.DescriptionColumn] = DBNull.Value;
			  else
			    Description = value.Trim();
			}
		}
		public string sGraphicResourceName
		{
			get
			{
			  if (IsNull(tableControlDefinition.GraphicResourceNameColumn) == true)
			    return string.Empty;
			  else
			    return GraphicResourceName;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.GraphicResourceNameColumn] = DBNull.Value;
			  else
			    GraphicResourceName = value.Trim();
			}
		}
		public string sColour
		{
			get
			{
			  if (IsNull(tableControlDefinition.ColourColumn) == true)
			    return string.Empty;
			  else
			    return Colour;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.ColourColumn] = DBNull.Value;
			  else
			    Colour = value.Trim();
			}
		}
		public string sAction
		{
			get
			{
			  if (IsNull(tableControlDefinition.ActionColumn) == true)
			    return string.Empty;
			  else
			    return Action;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.ActionColumn] = DBNull.Value;
			  else
			    Action = value.Trim();
			}
		}
		public string sActionTarget
		{
			get
			{
			  if (IsNull(tableControlDefinition.ActionTargetColumn) == true)
			    return string.Empty;
			  else
			    return ActionTarget;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.ActionTargetColumn] = DBNull.Value;
			  else
			    ActionTarget = value.Trim();
			}
		}
		public string sActionData
		{
			get
			{
			  if (IsNull(tableControlDefinition.ActionDataColumn) == true)
			    return string.Empty;
			  else
			    return ActionData;
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.ActionDataColumn] = DBNull.Value;
			  else
			    ActionData = value.Trim();
			}
		}
		public string sMasterAreaId
		{
			get
			{
			  if (IsNull(tableControlDefinition.MasterAreaIdColumn) == true)
			    return string.Empty;
			  else
			    return MasterAreaId.ToString();
			}
			set
			{
			  if (value.Trim().Length == 0)
			    this[tableControlDefinition.MasterAreaIdColumn] = DBNull.Value;
			  else
			    MasterAreaId = Convert.ToInt32(value.Trim());
			}
		}
		#endregion
 
    }
  }
}
		  
