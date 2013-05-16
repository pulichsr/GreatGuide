using Nucleo.GoodGuide.Datasets.Datasets;
using DataObjects = Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters
{
  public static class FormDefinitionConverter
  {
    public static DataObjects.FormDefinition ToDataOject(FormDefinitionDataset.FormDefinitionRow row)
    {
      DataObjects.FormDefinition dataObject = new DataObjects.FormDefinition();

      dataObject.FormTypeName = row.sFormTypeName;
      dataObject.GraphicResourceName = row.sGraphicResourceName;
      dataObject.Id = row.IsIdNull() ? new int?() : row.Id;
      dataObject.MasterAreaId = row.IsMasterAreaIdNull() ? new int?() : row.MasterAreaId;
      dataObject.Name = row.sName;
      dataObject.Text = row.sText;
      dataObject.AdName = row.sAdName;
    
      return dataObject;
    }
  }
}