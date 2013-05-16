using System.ComponentModel;
using System.Windows.Forms;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmMasterArea : Form
  {
    public static void ShowForm()
    {
      frmMasterArea frm = new frmMasterArea();
      frm.ShowDialog();
      frm.Dispose();
    }

    private frmMasterArea()
    {
      InitializeComponent();

      chkAutoloadMasterArea.Checked = CarApplication.Instance.AutoloadMasterAreas;
      edtMasterArea.Text = CarApplication.Instance.MasterAreaName;
    }

    private void frmMasterArea_Closing(object sender, CancelEventArgs e)
    {
      CarApplication.Instance.AutoloadMasterAreas = chkAutoloadMasterArea.Checked;
    }
    private void btnClose_Click(object sender, System.EventArgs e)
    {
      Close();
    }
    private void btnMasterArea_Click(object sender, System.EventArgs e)
    {
      MasterAreaDataset.MasterAreaRow MasterArea;
      DialogResult Result = frmSelectMasterArea.Select(out MasterArea);
      if (Result == System.Windows.Forms.DialogResult.Cancel)
        return;

      CarApplication.Instance.SetMasterArea(MasterArea);
      edtMasterArea.Text = CarApplication.Instance.MasterAreaName;
    }
    private void chkAutoloadMasterArea_CheckStateChanged(object sender, System.EventArgs e)
    {
      btnMasterArea.Enabled = !chkAutoloadMasterArea.Checked;
    }
  }
}