using System.ComponentModel;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSystemSettings : Form
  {
    public static void ShowForm()
    {
      frmSystemSettings frm = new frmSystemSettings();
      frm.ShowDialog();
      frm.Dispose();
    }

    private frmSystemSettings()
    {
      InitializeComponent();

      chkLogging.Checked = CarApplication.Instance.IsLoggingActive;
      chkExceptionLogging.Checked = CarApplication.Instance.IsExceptionLoggingActive;
      chkShowContentFilenames.Checked = CarApplication.Instance.ShowContentFilenames;
    }

    private void frmFactorySettings_Closing(object sender, CancelEventArgs e)
    {
      CarApplication.Instance.IsLoggingActive = chkLogging.Checked;
      CarApplication.Instance.IsExceptionLoggingActive = chkExceptionLogging.Checked;
      CarApplication.Instance.ShowContentFilenames = chkShowContentFilenames.Checked;
    }
    private void btnClose_Click(object sender, System.EventArgs e)
    {
      Close();
    }
  }
}