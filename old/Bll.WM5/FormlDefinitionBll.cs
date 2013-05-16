using System;
using System.Collections.Generic;
using System.Data;
using Nucleo.Drawing;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using ControlDefinition=Nucleo.Windows.Forms.DynamicForms.ControlDefinition;
using FormDefinition=Nucleo.Windows.Forms.DynamicForms.FormDefinition;
using FormDefinitions=Nucleo.Windows.Forms.DynamicForms.FormDefinitions;

namespace Nucleo.GoodGuide.Bll
{
  public class FormDefinitionBll : ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public FormDefinitionBll(IFormDefinitionRepository formDefinitionRepository, IControlDefinitionRepository controlDefinitionRepository)
    {
      this.formDefinitionRepository = formDefinitionRepository;
      this.controlDefinitionRepository = controlDefinitionRepository;
    }

    public string DatasetName
    {
      get { return "FormDefinition"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      FormDefinitionDataset.FormDefinitionDataTable data = (FormDefinitionDataset.FormDefinitionDataTable)newData.Tables["FormDefinition"];
      if (data != null)
      {
        if (merge == false)
          DeleteAll();

        for (Int32 RowNo = 0; RowNo < data.Rows.Count; RowNo++)
        {
          data[RowNo].Id += offset;

          if (data[RowNo].IsMasterAreaIdNull() == false) data[RowNo].MasterAreaId += offset;

          Insert(data[RowNo]);
        }

        data.Dispose();
      }
    }

    public void Insert(FormDefinitionDataset.FormDefinitionRow Row)
    {
      ValidateRow(Row);

      formDefinitionRepository.Insert(Row);
    }
    public void DeleteAll()
    {
      formDefinitionRepository.DeleteAll();
    }

    public void BuildDefinitions(FormDefinitions formDefinitions)
    {
      ControlDefinitionDataset.ControlDefinitionDataTable controlDefinitionTable = controlDefinitionRepository.Get();
      FormDefinitionDataset.FormDefinitionDataTable formDefinitionTable = formDefinitionRepository.Get();

      Dictionary<Int32, List<ControlDefinition>> controlDefinitions = CreateControlDefinitions(controlDefinitionTable);
      CreateFormDefinitions(formDefinitions, formDefinitionTable, controlDefinitions);
    }
    public void BuildDefinitions(FormDefinitions formDefinitions,Int32 masterAreaId)
    {
      ControlDefinitionDataset.ControlDefinitionDataTable controlDefinitionTable = controlDefinitionRepository.GetByMasterArea(masterAreaId);
      FormDefinitionDataset.FormDefinitionDataTable formDefinitionTable = formDefinitionRepository.GetByMasterArea(masterAreaId);

      Dictionary<Int32,List<ControlDefinition>> controlDefinitions = CreateControlDefinitions(controlDefinitionTable);
      CreateFormDefinitions(formDefinitions,formDefinitionTable,controlDefinitions);
    }

    private Dictionary<Int32, List<ControlDefinition>> CreateControlDefinitions(ControlDefinitionDataset.ControlDefinitionDataTable table)
    {
      Dictionary<Int32, List<ControlDefinition>> controlDefinitions = new Dictionary<Int32, List<ControlDefinition>>();

      foreach (ControlDefinitionDataset.ControlDefinitionRow row in table)
      {
        ControlDefinition controlDefinition = new ControlDefinition();
        if (controlDefinitions.ContainsKey(row.FormId) == false)
          controlDefinitions[row.FormId] = new List<ControlDefinition>();

        controlDefinitions[row.FormId].Add(controlDefinition);

        controlDefinition.Name = row.Name;

        switch (row.Action.ToUpper())
        {
          case "N":
            controlDefinition.Action = ControlDefinition.ControlActions.None;
            break;
          case "C":
            controlDefinition.Action = ControlDefinition.ControlActions.CloseCurrent;
            break;
          case "S":
            controlDefinition.Action = ControlDefinition.ControlActions.ShowTarget;
            break;
          case "I":
            controlDefinition.Action = ControlDefinition.ControlActions.InvokeMethod;
            break;
        }

        if ((row.IsColourNull() == false) && (row.Colour != string.Empty))
          controlDefinition.Color = Color.FromRgbString(row.Colour);
        if (row.IsTextNull() == false)
          controlDefinition.Text = row.Text;
        if (row.IsDescriptionNull() == false)
          controlDefinition.Description = row.Description;
        if (row.IsGraphicResourceNameNull() == false)
          controlDefinition.GraphicResourceName = row.GraphicResourceName;
        if (row.IsActionTargetNull() == false)
          controlDefinition.ActionTarget = row.ActionTarget;
        if (row.IsActionDataNull() == false)
          controlDefinition.ActionData = row.ActionData;
      }

      return controlDefinitions;
    }
    private void CreateFormDefinitions(FormDefinitions formDefinitions, FormDefinitionDataset.FormDefinitionDataTable table,Dictionary<Int32, List<ControlDefinition>> controlDefinitions)
    {
      formDefinitions.Clear();

      foreach (FormDefinitionDataset.FormDefinitionRow formDefinitionRow in table)
      {
        FormDefinition formDefinition = new FormDefinition(formDefinitionRow.Name,formDefinitionRow.FormTypeName);

        if (formDefinitionRow.IsGraphicResourceNameNull() == false)
          formDefinition.GraphicResourceName = formDefinitionRow.GraphicResourceName;
        if (formDefinitionRow.IsTextNull() == false)
          formDefinition.Text = formDefinitionRow.Text;

        formDefinitions.Add(formDefinition);

        if (controlDefinitions.ContainsKey(formDefinitionRow.Id) == true)
          formDefinition.Controls.AddRange(controlDefinitions[formDefinitionRow.Id]);
      }
    }
    private static void ValidateRow(FormDefinitionDataset.FormDefinitionRow Row)
    {
      if (Row.IsIdNull() == true)
        throw new DataException("FormDefinition.Id is null");

      if (Row.IsFormTypeNameNull() == true)
        throw new DataException("FormDefinition.FormTypeName is null");

      if (Row.IsNameNull() == true)
        throw new DataException("FormDefinition.Name is null");

      if (Row.IsMasterAreaIdNull() == true)
        throw new DataException("FormDefinition.MasterAreaId is null");
    }

    private readonly IFormDefinitionRepository formDefinitionRepository = null;
    private readonly IControlDefinitionRepository controlDefinitionRepository = null;
  }

}
