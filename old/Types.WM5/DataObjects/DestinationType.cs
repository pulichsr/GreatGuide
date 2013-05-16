using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class DestinationType
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (Id == null)
        result.AddValidationError("Id is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public Int32? Id;
    public string Name;
    public string Description;
    public string Code;
    public string Comment1Label;
    public string Comment2Label;
    public string Comment3Label;
    public string Comment4Label;
    public string IconResourceName;

    #endregion
  }

  public class DestinationTypes : List<DestinationType>
  {
  }
}
