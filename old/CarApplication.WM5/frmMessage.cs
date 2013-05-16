using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmMessage : Form
  {
    public static void ShowText(string text)
    {
      frmMessage frm = new frmMessage(text);
      frm.ShowDialog();
      
      frm.Dispose();
    }

    protected frmMessage(string text)
    {
      InitializeComponent();

      lblText.Text = text;
    }


    private void btnOK_Click(object sender, System.EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
  }
}