using System;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmFactorySetupPassword : Form
  {
    public static DialogResult ShowForm()
    {
      frmFactorySetupPassword frm = new frmFactorySetupPassword();

      DialogResult Result = frm.ShowDialog();

      return Result;
    }

    private frmFactorySetupPassword()
    {
      InitializeComponent();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      if (edtPassword.Text != edtConfirmPassword.Text)
      {
        MessageBox.Show("Password confirmation error","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
        return;
      }

      CarApplication.Instance.FactorySetupPassword = edtPassword.Text;
      Close();
    }
    private void btnPassword_Click(object sender, EventArgs e)
    {
      string Password;

      DialogResult Result = frmAlphaNumericKeyboard.GetText("Enter password",out Password);
      if (Result == System.Windows.Forms.DialogResult.Cancel)
        return;

      edtPassword.Text = Password.ToUpper();
    }
    private void btnConfirmPassword_Click(object sender, EventArgs e)
    {
      string Password;

      DialogResult Result = frmAlphaNumericKeyboard.GetText("Confirm password", out Password);
      if (Result == System.Windows.Forms.DialogResult.Cancel)
        return;

      edtConfirmPassword.Text = Password.ToUpper();
    }
    private void frmFactorySetupPassword_Load(object sender, EventArgs e)
    {
      edtPassword.Text = CarApplication.Instance.FactorySetupPassword;
      edtConfirmPassword.Text = CarApplication.Instance.FactorySetupPassword;
    }

  }
}