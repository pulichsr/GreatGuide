using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmAlphaNumericKeyboard : Form
  {
    public enum Modes
    {
      Alpha,
      Numeric
    }

    public static DialogResult GetText(string prompt,out string text)
    {
      frmAlphaNumericKeyboard frm = new frmAlphaNumericKeyboard(prompt);
      DialogResult Result = frm.ShowDialog();
      
      text = frm.EnteredText;
      frm.Dispose();

      return Result;
    }

    protected frmAlphaNumericKeyboard(string prompt)
    {
      InitializeComponent();

      LoadResources();

      lblPrompt.Text = prompt;
    }

    protected Modes Mode
    {
      set
      {
        if (mode == value)
          return;

        mode = value;

        switch (mode)
        {
          case Modes.Alpha:
            pnlAlpha.Visible = true;
            pnlNumeric.Visible = false;
            break;
          case Modes.Numeric:
            pnlAlpha.Visible = false;
            pnlNumeric.Visible = true;
            break;
        }
      }
    }

    public string EnteredText
    {
      get { return enteredText; }
      set
      {        
        enteredText = value;
        edtText.Text = enteredText;
      }
    }

    private void LoadResources()
    {
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
      btnUp.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgListUp;
      btnDown.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgListDown;
    }

    private void btnOK_Click(object sender, System.EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
    private void btnBackspace_MouseUp(object sender, MouseEventArgs e)
    {
      if (EnteredText.Length == 0)
        return;

      EnteredText = EnteredText.Substring(0, EnteredText.Length - 1);
    }
    private void btnKeyboard_MouseUp(object sender, MouseEventArgs e)
    {
      EnteredText += ((Control)sender).Text;
    }
    private void btnUp_MouseUp(object sender, MouseEventArgs e)
    {
    }
    private void btnDown_MouseUp(object sender, MouseEventArgs e)
    {
    }
    private void btnBack_Click(object sender, System.EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    private void btnAlpha_Click(object sender, System.EventArgs e)
    {
      Mode = Modes.Alpha;
    }
    private void btnNumeric_Click(object sender, System.EventArgs e)
    {
      Mode = Modes.Numeric;
    }

    private string enteredText = string.Empty;
    private Modes mode = Modes.Alpha;


  }
}