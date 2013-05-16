//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;
using System.Data;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class FormDefinitionDataset
  {
    public partial class FormDefinitionRow
    {
      #region String properties
      public string sText
      {
        get
        {
          if (IsNull(tableFormDefinition.TextColumn) == true)
            return string.Empty;
          else
            return Text;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableFormDefinition.TextColumn] = DBNull.Value;
          else
            Text = value.Trim();
        }
      }
      public string sName
      {
        get
        {
          if (IsNull(tableFormDefinition.NameColumn) == true)
            return string.Empty;
          else
            return Name;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableFormDefinition.NameColumn] = DBNull.Value;
          else
            Name = value.Trim();
        }
      }
      public string sFormTypeName
      {
        get
        {
          if (IsNull(tableFormDefinition.FormTypeNameColumn) == true)
            return string.Empty;
          else
            return FormTypeName;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableFormDefinition.FormTypeNameColumn] = DBNull.Value;
          else
            FormTypeName = value.Trim();
        }
      }
      public string sGraphicResourceName
      {
        get
        {
          if (IsNull(tableFormDefinition.GraphicResourceNameColumn) == true)
            return string.Empty;
          else
            return GraphicResourceName;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableFormDefinition.GraphicResourceNameColumn] = DBNull.Value;
          else
            GraphicResourceName = value.Trim();
        }
      }
      public string sId
      {
        get
        {
          if (IsNull(tableFormDefinition.IdColumn) == true)
            return string.Empty;
          else
            return Id.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableFormDefinition.IdColumn] = DBNull.Value;
          else
            Id = Convert.ToInt32(value.Trim());
        }
      }
      public string sMasterAreaId
      {
        get
        {
          if (IsNull(tableFormDefinition.MasterAreaIdColumn) == true)
            return string.Empty;
          else
            return MasterAreaId.ToString();
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableFormDefinition.MasterAreaIdColumn] = DBNull.Value;
          else
            MasterAreaId = Convert.ToInt32(value.Trim());
        }
      }
      public string sAdName
      {
        get
        {
          if (IsNull(tableFormDefinition.AdNameColumn) == true)
            return string.Empty;
          else
            return AdName;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[tableFormDefinition.AdNameColumn] = DBNull.Value;
          else
            AdName = value.Trim();
        }
      }
      #endregion
 
    }
  }
}