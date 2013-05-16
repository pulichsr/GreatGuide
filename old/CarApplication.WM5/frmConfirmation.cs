using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmConfirmation : Form
  {
    public static DialogResult GetConfirmation(string text)
    {
      frmConfirmation frm = new frmConfirmation(text);
      DialogResult result = frm.ShowDialog();
      
      frm.Dispose();

      return result;
    }

    protected frmConfirmation(string text)
    {
      InitializeComponent();

      lblText.Text = text;
    }


    private void btnYes_Click(object sender, System.EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Yes;
    }
    private void btnNo_Click(object sender, System.EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.No;
    }
  }
}