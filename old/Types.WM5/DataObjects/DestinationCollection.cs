using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class DestinationCollection
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
		public string Code;
    
    #endregion
  }

  public class DestinationCollections : List<DestinationCollection>
  {
  }
}
