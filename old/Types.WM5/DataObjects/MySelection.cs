using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class MySelection
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (DestinationId == null)
        result.AddValidationError("DestinationId is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public Int32? DestinationId;
    
    #endregion
  }

  public class MySelections : List<MySelection>
  {
    public Boolean ContainsId(Int32 destinationId)
    {
      return FindById(destinationId) != null;
    }
    public MySelection FindById(Int32 destinationId)
    {
      for (Int32 destinationNo = 0; destinationNo < this.Count; destinationNo++)
        if (this[destinationNo].DestinationId == destinationId)
          return this[destinationNo];

      return null;
    }
  }
}
