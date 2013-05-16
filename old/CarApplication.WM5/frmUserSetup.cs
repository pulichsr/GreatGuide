using System;
using System.Windows.Forms;
using Nucleo.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmUserSetup : Form
  {
    public static DialogResult ShowForm()
    {
      frmUserSetup frm = new frmUserSetup();
      return frm.ShowDialog();
    }

    private frmUserSetup()
    {
      InitializeComponent();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    private void btnFirstUse_Click(object sender, EventArgs e)
    {
      btnClose.Enabled = false;
      WaitCursor.Show(true);
      CarApplication.Instance.SetFirstTimeUse(null);
      WaitCursor.Show(false);
      btnClose.Enabled = true;
      this.BringToFront();
    }
    private void btnFactorySetup_Click(object sender, EventArgs e)
    {
      string Password;
      DialogResult PasswordResult = frmAlphaNumericKeyboard.GetText("Enter password", out Password);
      if ((PasswordResult == System.Windows.Forms.DialogResult.OK) && (CarApplication.Instance.IsValidFactorySetupPassword(Password) == true))
      {
        DialogResult TerminateApplication = frmFactorySetup.ShowForm();
        if (TerminateApplication == System.Windows.Forms.DialogResult.Yes)
        {
          DialogResult = System.Windows.Forms.DialogResult.Yes;
          return;          
        }
      }

      DialogResult = System.Windows.Forms.DialogResult.No;
    }
  }
}