using System.Data;
using System.Windows.Forms;
using Nucleo.GoodGuide.Datasets.Datasets;
using Resco.Controls.SmartGrid;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectMasterArea : Form
  {
    public static DialogResult Select(out MasterAreaDataset.MasterAreaRow masterArea)
    {
      frmSelectMasterArea frm = new frmSelectMasterArea();
      DialogResult Result = frm.ShowDialog();
      masterArea = frm.SelectedRow;

      return Result;
    }

    private frmSelectMasterArea()
    {
      InitializeComponent();

      LoadResources();

      SmartGrid.DefaultRowHeight = 25;
    }

    protected object DataSource
    {
      set
      {
        sgData.DataSource = value;
        if (sgData.Rows.Count > 0)
        {
          sgData.ActivateCell(0, 0);
          btnUp.Visible = true;
          btnDown.Visible = true;
        }
        else
        {
          btnUp.Visible = false;
          btnDown.Visible = false;
        }
      }
    }
    protected MasterAreaDataset.MasterAreaRow SelectedRow
    {
      get
      {
        if (masterAreas.Rows.Count == 0)
          return null;
        else
          return (MasterAreaDataset.MasterAreaRow)(((DataRowView)(this.BindingContext[sgData.DataSource].Current)).Row);
      }
    }

    private void OkClicked()
    {
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }

    private void LoadResources()
    {
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
      btnUp.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgListUp;
      btnDown.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgListDown;
    }

    private void btnOK_Click(object sender, System.EventArgs e)
    {
      OkClicked();
    }
    private void btnUp_MouseUp(object sender, MouseEventArgs e)
    {
      if (sgData.ActiveRowIndex == 0)
        return;

      sgData.ActivateCell(sgData.ActiveRowIndex - 1, 0);
    }
    private void btnDown_MouseUp(object sender, MouseEventArgs e)
    {
      if (sgData.ActiveRowIndex == sgData.Rows.Count - 1)
        return;

      sgData.ActivateCell(sgData.ActiveRowIndex + 1, 0);
    }
    private void btnBack_Click(object sender, System.EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    private void frmSelectMasterArea_Load(object sender, System.EventArgs e)
    {
      try
      {
        masterAreas = CarApplication.Instance.MasterAreaBll.GetAll();
        DataSource = masterAreas;
      }
      catch
      {
      }
    }

    private MasterAreaDataset.MasterAreaDataTable masterAreas = null;
  }
}