using System;
using System.Collections.Generic;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class DestinationCollectionMember
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

	    // No rules to generate.

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public Int32? CollectionId;
    public Int32? DestinationId;

    #endregion
  }

  public class DestinationCollectionMembers : List<DestinationCollectionMember>
  {
  }
}
