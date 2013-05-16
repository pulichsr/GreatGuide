using System;
using System.Collections.Generic;
using Nucleo.Data;

///
/// Auto genetared with ObjectGeneratorApp
///
namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class FormDefinition
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
		public string FormTypeName;
		public string Text;
		public string GraphicResourceName;
		public Int32? MasterAreaId;
    public string AdName;
    
    #endregion
  }

  public class FormDefinitions: List<FormDefinition>
  {
  }
}