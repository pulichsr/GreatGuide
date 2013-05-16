using System;
using System.Windows.Forms;
using System.Xml;
using Nucleo.GoodGuide.Controls;
using Nucleo.GoodGuide.Types;
using Resco.Controls.AdvancedList;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmStreetAddressList : frmCarApplicationBase
  {
    public frmStreetAddressList(NavigatorAddresses addresses)
    {
      InitializeComponent();

      DataSource = addresses;

      try
      {
        LoadResources();
      }
      catch
      {
      }

      pageController.PageNumberChanged += Controller_PageNumberChanged;
    }

    public override void BuildPresentation(Windows.Forms.DynamicForms.FormDefinition formDefinition, object newFormData)
    {
      base.BuildPresentation(formDefinition, newFormData);

      BtnSoftKey3.Text = "TAKE ME|THERE";
      BtnSoftKey4.Text = "PREVIOUS|PAGE";
      BtnSoftKey5.Text = "NEXT|PAGE";
    }

    protected NavigatorAddresses DataSource
    {
      get { return (NavigatorAddresses)pageController.DataSource; }
      set
      {
        if (value == null)
          return;

        pageController.ItemsPerPage = 4;

        pageController.DataSource = value;
        UpdatePageNumber();

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
    protected NavigatorAddress SelectedRow
    {
      get
      {
        if (alData.SelectedRow == null)
          return null;

        Int32 SelectedIndex = alData.DataRows.IndexOf(alData.SelectedRow);
        return (NavigatorAddress)pageController.PageData[SelectedIndex];
      }
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

    private void NextPage()
    {
      pageController.NextPage();
      UpdateControls();
    }
    private void PreviousPage()
    {
      pageController.PreviousPage();
      UpdateControls();
    }
    private static Cell GetTemplateCellByName(RowTemplate template, string name)
    {
      for (Int32 CellNo = 0; CellNo < template.CellTemplates.Count; CellNo++)
        if (template.CellTemplates[CellNo].Name == name)
          return template.CellTemplates[CellNo];

      return null;
    }

    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl,ref handled,ref clickData);
      if (handled == true)
        return;

      if (navigationControl == BtnMap)
      {
        handled = true;
        Close();
        return;
      }

      if ((navigationControl == BtnSoftKey3) && (SelectedRow != null))
      {
//        CarApplication.Instance.NavigateTo(SelectedRow);
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

    private void UpdateControls()
    {
      BtnSoftKey4.Enabled = pageController.PageNumber > 1;
      BtnSoftKey5.Enabled = pageController.PageNumber < pageController.PageCount;
    }
    private void RowSelected()
    {
      BtnSoftKey3.Enabled = true;
    }
    private void LoadTemplate(string resourceName)
    {
      XmlTextReader TextReader = CarApplication.Instance.TemplateManager.GetXml(resourceName);
      if (TextReader == null)
        return;

      alData.LoadXml(TextReader);

      rowTemplate = alData.Templates["Row"];
      if (rowTemplate != null)
      {
        alData.TemplateIndex = alData.Templates.IndexOf(rowTemplate);
        directionImageCell = (ImageCell)GetTemplateCellByName(rowTemplate, "imgDirection");
        if (directionImageCell != null)
          directionImageCell.ImageList = CarApplication.Instance.ImageManager.UnselectedDirectionImages;
      }

      selectedRowTemplate = alData.Templates["SelectedRow"];
      if (selectedRowTemplate != null)
      {
        alData.SelectedTemplateIndex = alData.Templates.IndexOf(selectedRowTemplate);
        selectedDirectionImageCell = (ImageCell)GetTemplateCellByName(selectedRowTemplate, "imgDirection");
        if (selectedDirectionImageCell != null)
          selectedDirectionImageCell.ImageList = CarApplication.Instance.ImageManager.SelectedDirectionImages;
      }

    }
    private void LoadResources()
    {
      LoadTemplate(string.Format("Nucleo.GoodGuide.CarApplicationResources.Templates.{0}", GetType().Name));
    }
    private void UpdatePageNumber()
    {
      lblPage.Text = string.Format("Page {0}/{1}",pageController.PageNumber,pageController.PageCount);
    }

    #region Event handlers
    private void frmStreetAddressList_Load(object sender, EventArgs e)
    {
    }
    private void Controller_PageNumberChanged(object sender, EventArgs e)
    {
      alData.DataSource = pageController.PageData;
      UpdatePageNumber();
    }
    private void alData_CellClick(object sender, CellEventArgs e)
    {
      alData.DataRows[e.RowIndex].Selected = true;

      RowSelected();
    }
    #endregion

    private readonly ListPageController pageController = new ListPageController(4);
    private RowTemplate rowTemplate = null;
    private RowTemplate selectedRowTemplate = null;
    private ImageCell directionImageCell = null;
    private ImageCell selectedDirectionImageCell = null;
  }


}