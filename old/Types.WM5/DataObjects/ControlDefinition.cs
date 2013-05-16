using System;
using System.Collections.Generic;
using Nucleo.Data;

///
/// Auto genetared with ObjectGeneratorApp
///
namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class ControlDefinition
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

	  	if (Id == null)
        result.AddValidationError("Id is undefined");

			if (FormId == null)
        result.AddValidationError("FormId is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2
    
    public Int32? Id;
		public Int32? FormId;
		public string Name;
		public string Text;
		public string Description;
		public string GraphicResourceName;
		public string Colour;
		public string Action;
		public string ActionTarget;
		public Int32? MasterAreaId;
		public string ActionData;
    
    #endregion
  }

  public class ControlDefinitions: List<ControlDefinition>
  {
  }
}