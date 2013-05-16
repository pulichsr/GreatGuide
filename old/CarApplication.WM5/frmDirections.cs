using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nucleo.GoodGuide.Controls;
using Resco.Controls.SmartGrid;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmDirections : frmCarApplicationBase
  {
    public static void ShowModal(string[] directions)
    {
      frmDirections frm = new frmDirections(directions);
      frm.ShowDialog();
      frm.Dispose();
    }
    private frmDirections(string[] directions)
    {
      Guard.ArgumentNotNull(directions, "directions");

      foreach (string direction in directions)
        this.directions.Add(direction);

      InitializeComponent();

      BtnSoftKey4.Text = "PREVIOUS|PAGE";
      BtnSoftKey5.Text = "NEXT|PAGE";

      BtnBack.Click += btnBack_Click;
      BtnMap.Click += btnMap_Click;
      BtnSoftKey4.Click += PreviousPageClick;
      BtnSoftKey5.Click += NextPageClick;
      pageController.PageNumberChanged += Controller_PageNumberChanged;
    }

    protected List<string> DataSource
    {
      get { return (List<string>)pageController.DataSource; }
      set
      {
        if (value == null)
          return;

        pageController.DataSource = value;
        PageNumberChanged();
        UpdateControls();
      }
    }
    protected Int16 PageNumber
    {
      get { return pageController.PageNumber; }
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

    protected void PageNumberChanged()
    {
      try
      {
        sgData.Rows.Clear();
        foreach (string result in pageController.PageData)
        {
          Row row = new Row();
          row.Height = 22;
          row.StringData = new string[] { result };
          sgData.Rows.Add(row);
        }
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this, string.Format("Exception in {0}.set_DataSource:{1}", Name, ExceptionManager.Message(exc)));
      }
    }
    protected void UpdateControls()
    {
      BtnSoftKey4.Enabled = pageController.PageNumber > 1;
      BtnSoftKey5.Enabled = pageController.PageNumber < pageController.PageCount;
    }

    #region Event handlers
    private void frmDirection_Load(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(this, "Directions");
      foreach (string direction in directions)
        CarApplication.Instance.WriteLog(this, direction);

      DataSource = directions;
    }
    private void btnBack_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }
    private void btnMap_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }
    private void Controller_PageNumberChanged(object sender, EventArgs e)
    {
      PageNumberChanged();
    }
    private void PreviousPageClick(object sender, EventArgs e)
    {
      PreviousPage();
    }
    private void NextPageClick(object sender, EventArgs e)
    {
      NextPage();
    }
    #endregion

    private readonly List<string> directions = new List<string>();
    private readonly ListPageController pageController = new ListPageController(8);
  }


}