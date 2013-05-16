using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class DestinationClassification
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
    public Int32? DestinationTypeId;
    public string Name;
    public string Description;
    public string Code;

    #endregion
  }

  public class DestinationClassifications : List<DestinationClassification>
  {
  }
}
