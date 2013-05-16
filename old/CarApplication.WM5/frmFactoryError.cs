using System;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmFactoryError : Form
  {
    public static void ShowError(string errorText)
    {
      frmFactoryError frm = new frmFactoryError(errorText);
      frm.ShowDialog();
      frm.Dispose();
    }
    public static void ShowException(Exception exception)
    {
      frmFactoryError frm = new frmFactoryError(exception);
      frm.ShowDialog();
      frm.Dispose();
    }

    internal frmFactoryError(string errorText)
    {
      InitializeComponent();

      edtText.Text = errorText;
    }
    internal frmFactoryError(Exception exception)
    {
      InitializeComponent();

      edtText.Text = ExceptionManager.Message(exception);
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }

  }
}