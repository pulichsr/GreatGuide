using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using Nucleo.GoodGuide.Controls;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Resco.Controls.AdvancedList;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmDestinationListBase : frmCarApplicationBase
  {
    public frmDestinationListBase():
      this(true)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="isManaged">Identifies whether the form is managed by the DynamicFormsManager or not. If so, the forms
    /// manager handles the click events</param>
    public frmDestinationListBase(Boolean isManaged)
    {
      Nucleo.WinCe.WaitCursor.Show(true);
      this.isManaged = isManaged;

      InitializeComponent();

      pageController.PageNumberChanged += Controller_PageNumberChanged;
      btnSoftKey3.Click += btnSoftKey3_Click;
    }

    public override void BuildPresentation(Windows.Forms.DynamicForms.FormDefinition formDefinition, object newFormData)
    {
      base.BuildPresentation(formDefinition, newFormData);

      BtnSoftKey3.Text = "TAKE ME|THERE";
      BtnSoftKey4.Text = "PREVIOUS|PAGE";
      BtnSoftKey5.Text = "NEXT|PAGE";
    }

    protected DestinationDataset.DestinationDataTable DataSource
    {
      get { return (DestinationDataset.DestinationDataTable)pageController.DataSource; }
      set
      {
        if (value == null)
          return;

        if (alData.ShowHeader == false)
          pageController.ItemsPerPage = 4;
        else
          pageController.ItemsPerPage = 3;

        pageController.DataSource = value;
        UpdatePageNumber();
        PageNumberChanged();

        try
        {
          alData.DataSource = pageController.PageData;
          alData.SelectedRow = null;

          BtnSoftKey3.Enabled = false;
        }
        catch (Exception exc)
        {
          CarApplication.Instance.WriteLog(this,string.Format("Exception in {0}.set_DataSource:{1}", Name,ExceptionManager.Message(exc)));
        }

        UpdateControls();
      }
    }
    protected DestinationDataset.DestinationRow SelectedRow
    {
      get
      {
        if (alData.SelectedRow == null)
          return null;

        if (alData.DataRows == null)
          return null;

        Int32 SelectedIndex = alData.DataRows.IndexOf(alData.SelectedRow);

        if (pageController.PageData == null)
          return null;
        if (pageController.PageData[SelectedIndex] == null)
          return null;

        return ((DestinationDataset.DestinationRow)(((DataRowView)(pageController.PageData[SelectedIndex])).Row));
      }
    }
    protected RowTemplate CommentRowTemplate
    {
      get { return commentRowTemplate; }
    }
    protected RowTemplate RowTemplate
    {
      get { return rowTemplate; }
    }
    protected RowTemplate SelectedRowTemplate
    {
      get { return selectedRowTemplate; }
    }
    protected Int16 ItemsPerPage
    {
      get { return pageController.ItemsPerPage; }
      set { pageController.ItemsPerPage = value; }
    }
    protected Int16 PageNumber
    {
      get { return pageController.PageNumber; }
    }
    protected Int16 PageCount
    {
      get { return pageController.PageCount; }
    }
    protected Boolean ShowHeader
    {
      get { return alData.ShowHeader; }
      set { alData.ShowHeader = value; }
    }
    protected string Comment
    {
      get
      {
        if (CommentCell == null)
          return string.Empty;

        return CommentCell.CellSource.ConstantData;
      }
      set
      {
        if (CommentCell != null)
          CommentCell.CellSource = new CellSource(CellSourceType.Constant, value);

        UpdateShowHeader();
      }
    }
    protected Cell CommentCell
    {
      get { return commentCell; }
    }
    protected Cell CommentMoreCell
    {
      get { return commentMoreCell; }
    }

    protected void FirstPage()
    {
      pageController.FirstPage();
      BtnSoftKey4.Enabled = false;
    }
    protected void NextPage()
    {
      pageController.NextPage();
      UpdateControls();
    }
    protected void PreviousPage()
    {
      pageController.PreviousPage();
      UpdateControls();
    }
    protected void LoadTemplate(string resourceName)
    {
      CarApplication.Instance.WriteLog(this,">>LoadTemplate");
      XmlTextReader TextReader = CarApplication.Instance.TemplateManager.GetXml(resourceName);
      if (TextReader == null)
        return;

      alData.LoadXml(TextReader);

      commentRowTemplate = alData.Templates["CommentRow"];
      if (commentRowTemplate != null)
      {
        alData.HeaderRow.TemplateIndex = alData.Templates.IndexOf(commentRowTemplate);
        commentCell = GetTemplateCellByName(commentRowTemplate,"lblComment");
        commentMoreCell = GetTemplateCellByName(commentRowTemplate, "MoreBackground");
      }

      rowTemplate = alData.Templates["Row"];
      if (rowTemplate != null)
      {
        alData.TemplateIndex = alData.Templates.IndexOf(rowTemplate);
        directionImageCell = (ImageCell)GetTemplateCellByName(rowTemplate, "imgDirection");
        if (directionImageCell != null)
          directionImageCell.ImageList = CarApplication.Instance.ImageManager.UnselectedDirectionImages;

        typeImageCell = (ImageCell)GetTemplateCellByName(rowTemplate, "imgType");
        if (typeImageCell != null)
          typeImageCell.ImageList = CarApplication.Instance.ImageManager.UnselectedDestinationTypeImages;
      }

      selectedRowTemplate = alData.Templates["SelectedRow"];
      if (selectedRowTemplate != null)
      {
        alData.SelectedTemplateIndex = alData.Templates.IndexOf(selectedRowTemplate);
        selectedDirectionImageCell = (ImageCell)GetTemplateCellByName(selectedRowTemplate, "imgDirection");
        if (selectedDirectionImageCell != null)
          selectedDirectionImageCell.ImageList = CarApplication.Instance.ImageManager.SelectedDirectionImages;

        selectedTypeImageCell = (ImageCell)GetTemplateCellByName(selectedRowTemplate, "imgType");
        if (selectedTypeImageCell != null)
          selectedTypeImageCell.ImageList = CarApplication.Instance.ImageManager.SelectedDestinationTypeImages;
      }

      CarApplication.Instance.WriteLog(this, "<<LoadTemplate");
    }
    protected static Cell GetTemplateCellByName(RowTemplate template, string name)
    {
      for (Int32 CellNo = 0; CellNo < template.CellTemplates.Count; CellNo++)
        if (template.CellTemplates[CellNo].Name == name)
          return template.CellTemplates[CellNo];

      return null;
    }

    protected virtual void RowMoreClicked()
    {
      
    }
    protected virtual void HeaderMoreClicked()
    {

    }
    protected virtual void PageNumberChanged()
    {

    }
    protected virtual void ValidateRow(ValidateDataArgs e)
    {
      object Value = e.DataRow["Recommendation"];
      if (Value != DBNull.Value)
        e.DataRow["RecommendationText"] = Recommendation.ToString((Int16)Value);
    }
    protected virtual void UpdateControls()
    {
      BtnSoftKey3.Enabled = alData.SelectedRow != null;
      BtnSoftKey4.Enabled = pageController.PageNumber > 1;
      BtnSoftKey5.Enabled = pageController.PageNumber < pageController.PageCount;
    }
    protected virtual void RowSelected()
    {
    }
    protected virtual DestinationDataset.DestinationDataTable GetData()
    {
      return null;
    }

    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl,ref handled,ref clickData);
      if (handled == true)
      {
        return;
      }

      if (navigationControl == BtnMap)
      {
        handled = true;
        Close();
        return;
      }

      if ((navigationControl == BtnSoftKey3) && (SelectedRow != null))
      {
        try
        {
          NavigateTo(SelectedRow);
        }
        catch (Exception exc)
        {
          CarApplication.Instance.WriteLog(this, "Error navigating to destination", exc);
        }
        handled = true;
        Close();
        return;
      }

      if (navigationControl == BtnSoftKey4)
      {
        PreviousPage();
        handled = true;
        return;
      }

      if (navigationControl == BtnSoftKey5)
      {
        NextPage();
        handled = true;
        return;
      }
    }

    private void UpdatePageNumber()
    {
      lblPage.Text = string.Format("Page {0}/{1}",pageController.PageNumber,pageController.PageCount);
    }
    private void UpdateShowHeader()
    {
      if (string.IsNullOrEmpty(Comment) == true)
      {
        alData.ShowHeader = false;
        return;
      }

      alData.ShowHeader = pageController.PageNumber == 1;
      CarApplication.Instance.WriteLog(this, string.Format("ShowHeader: {0}", alData.ShowHeader));
    }
    private void NavigateTo(DestinationDataset.DestinationRow destination)
    {
      CarApplication.Instance.WriteLog(this,string.Format("Navigating to {0}:{1},{2}",destination.Code,destination.Latitude,destination.Longitude));
      WinCe.WaitCursor.Show(true);
      try
      {
        CarApplication.Instance.NavigateTo(destination, false);
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this,"Error navigating to destination",exc);
      }
      WinCe.WaitCursor.Show(false);
    }

    #region Event handlers
    private void frmDestinationListBase_Load(object sender, EventArgs e)
    {
      DataSource = GetData();
    }
    private void frmDestinationListBase_Activated(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(this, "frmDestinationListBase_Activated");
    }
    private void Controller_PageNumberChanged(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(this, ">> Controller_PageNumberChanged");
      alData.DataSource = pageController.PageData;
      alData.SelectedRow = null;

      UpdatePageNumber();
      UpdateShowHeader();
      PageNumberChanged();
      UpdateControls();
    }
    private void alData_CellClick(object sender, CellEventArgs e)
    {
      try
      {
        alData.DataRows[e.RowIndex].Selected = true;

        RowSelected();

        if ((e.Cell.Name == "MoreBackground") || 
          (e.Cell.Name == "lblMore") || 
            (e.Cell.Name == "lblDistance") || 
              (e.Cell.Name == "lblDirection") || 
                (e.Cell.Name == "imgDirection"))
          RowMoreClicked();
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this,"Error displaying Destination More",exc);
      }
    }
    private void alData_HeaderClick(object sender, CellEventArgs e)
    {
      if ((e.Cell != CommentMoreCell) && (e.Cell.Name != "lblCommentMore"))
        return;

      try
      {
        HeaderMoreClicked();
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this, "Error in HeaderMoreClicked", exc);
      }
    }
    private void alData_ValidateData(object sender, ValidateDataArgs e)
    {
      try
      {
        ValidateRow(e);
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this, "Error in ValidateRow", exc);
      }
    }
    private void btnSoftKey3_Click(object sender, EventArgs e)
    {
      if (isManaged == true)
        return;

      try
      {
        NavigateTo(SelectedRow);
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this, "Error navigating to destination", exc);
      }
    }
    #endregion

    #region Fields
    private readonly ListPageController pageController = new ListPageController(3);
    private RowTemplate commentRowTemplate = null;
    private RowTemplate rowTemplate = null;
    private RowTemplate selectedRowTemplate = null;
    private Cell commentCell = null;
    private Cell commentMoreCell = null;
    private ImageCell directionImageCell = null;
    private ImageCell selectedDirectionImageCell = null;
    private ImageCell typeImageCell = null;
    private ImageCell selectedTypeImageCell = null;
    private readonly Boolean isManaged;
    #endregion
  }


}