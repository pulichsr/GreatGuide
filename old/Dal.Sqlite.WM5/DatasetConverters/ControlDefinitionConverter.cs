using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class ControlDefinitionConverter
  {
    public static DataObjects.ControlDefinition ToDataOject(ControlDefinitionDataset.ControlDefinitionRow row)
    {
      DataObjects.ControlDefinition dataObject = new DataObjects.ControlDefinition();

      dataObject.Action = row.sAction;
      dataObject.ActionData = row.sActionData;
      dataObject.ActionTarget = row.sActionTarget;
      dataObject.Colour = row.sColour;
      dataObject.Description = row.sDescription;
      dataObject.FormId = row.IsFormIdNull() ? new int?() : row.FormId;
      dataObject.GraphicResourceName = row.sGraphicResourceName;
      dataObject.Id = row.IsIdNull() ? new int?() : row.Id;
      dataObject.MasterAreaId = row.IsMasterAreaIdNull() ? new int?() : row.MasterAreaId;
      dataObject.Name = row.sName;
      dataObject.Text = row.sText;
    
      return dataObject;
    }
  }
}