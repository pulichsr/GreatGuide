using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class Theme
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (Id == null)
        result.AddValidationError("Id is undefined");

      if (string.IsNullOrEmpty(Name) == true)
        result.AddValidationError("Name is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public Int32? Id;
    public string Name;
    public string Description;

    #endregion
  }

  public class Themes: List<Theme>
  {
  }
}