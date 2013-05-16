using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types.DataObjects;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectStreet : Form
  {
    public static DialogResult Select(ILogger logger, IStreetSearchDataProvider dataProvider, string searchCriteria, out Street selected)
    {
      selected = null;

      frmSelectStreet frm = new frmSelectStreet(logger,dataProvider, searchCriteria);
      DialogResult result = frm.ShowDialog();
      if (result == DialogResult.Cancel)
        return result;

      selected = frm.listController.PageData[frm.lstItems.ActiveRowIndex];

      frm.Dispose();

      return result;
    }

    private frmSelectStreet(ILogger logger,IStreetSearchDataProvider dataProvider, string searchCriteria)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(dataProvider, "dataProvider");

      InitializeComponent();
      LoadResources();

      this.logger = logger;
      this.dataProvider = dataProvider;
      this.searchCriteria = searchCriteria;

      dataProvider.DataChanged += dataProvider_DataChanged;
      listController.PageNumberChanged += listController_PageNumberChanged;
      dataChangedDelegate = DataChanged;
    }

    private void LoadResources()
    {
///*
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
      btnNext.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgNext;
      btnPreviousPage.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgListUp;
      btnNextPage.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgListDown;
//*/
    }
    private void DataChanged()
    {
      if (InvokeRequired)
      {
        Invoke(dataChangedDelegate);
      }
      else
      {
        listController.DataSource = dataProvider.Data;
      }
    }

    private void frmSelectStreet_Load(object sender, EventArgs e)
    {
      try
      {
        dataProvider.SearchCriteria = searchCriteria;
      }
      catch (Exception exc)
      {
        logger.Write(this, "Error getting matching data", exc);
      }
    }
    private void frmSelectStreet_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      dataProvider.DataChanged -= dataProvider_DataChanged;
      listController.PageNumberChanged -= listController_PageNumberChanged;
    }
    private void listController_PageNumberChanged(object sender, EventArgs e)
    {
      lstItems.Rows.Clear();
      foreach (Street street in listController.PageData)
      {
        lstItems.Rows.Add(new Resco.Controls.SmartGrid.Row(21, new string[] { street.ToString() }));
      }

      btnPreviousPage.Visible = !listController.IsFirstPage;
      btnNextPage.Visible = !listController.IsLastPage;
    }
    private void dataProvider_DataChanged(object sender, EventArgs e)
    {
      DataChanged();
    }
    private void lstItems_SelectionChanged(object sender, EventArgs e)
    {
      btnNext.Visible = true;
    }
    private void btnPreviousPage_Click(object sender, EventArgs e)
    {
      listController.PreviousPage();
    }
    private void btnNextPage_Click(object sender, EventArgs e)
    {
      listController.NextPage();
    }
    private void btnBack_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
    private void btnNext_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }

    #region Fields
    private readonly ILogger logger;
    private readonly IStreetSearchDataProvider dataProvider;
    private readonly string searchCriteria;
    private readonly PagedListController<Street> listController = new PagedListController<Street>(10);
    private readonly MethodDelegate dataChangedDelegate;
    #endregion

  }
}