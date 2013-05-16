//========================================================================
// This file was generated using the MyGeneration tool in combination
// with the XSDToVS2005TypedRowStringProps template, $Rev: 1h $
//========================================================================
using System;

namespace Nucleo.GoodGuide.Datasets.Datasets
{
  public partial class ConfigParameterDataset
  {
    public partial class ConfigParameterRow
    {
      #region String properties
      public string sParamName
      {
        get
        {
          if (IsNull(this.tableConfigParameter.ParamNameColumn) == true)
            return string.Empty;
          else
            return ParamName;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[this.tableConfigParameter.ParamNameColumn] = DBNull.Value;
          else
            ParamName = value.Trim();
        }
      }
      public string sParamValue
      {
        get
        {
          if (IsNull(this.tableConfigParameter.ParamValueColumn) == true)
            return string.Empty;
          else
            return ParamValue;
        }
        set
        {
          if (value.Trim().Length == 0)
            this[this.tableConfigParameter.ParamValueColumn] = DBNull.Value;
          else
            ParamValue = value.Trim();
        }
      }
      #endregion
 
    }
  }
}