using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class ConfigParameter
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (string.IsNullOrEmpty(ParamName))
        result.AddValidationError("ParamName is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public string ParamName;
    public string ParamValue;

    #endregion
  }

  public class ConfigParameters : List<ConfigParameter>
  {
  }
}
