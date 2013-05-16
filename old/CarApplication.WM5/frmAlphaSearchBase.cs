using System.Windows.Forms;
using Resco.Controls.SmartGrid;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmAlphaSearchBase : Form
  {
    public enum Modes
    {
      Alpha,
      Numeric,
      None
    }

    protected frmAlphaSearchBase()
    {
      InitializeComponent();

      LoadResources();
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
    protected object SelectedRow
    {
      get
      {
        if (this.BindingContext[sgData.DataSource] == null)
          return null;
        if (this.BindingContext[sgData.DataSource].Count == 0)
          return null;
        if (this.BindingContext[sgData.DataSource].Position == -1)
          return null;
        return this.BindingContext[sgData.DataSource].Current;
      }
    }
    protected ColumnCollection Columns
    {
      get { return sgData.Columns; }
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
            pnlNone.Visible = false;
            sgData.Height = 79;
            break;
          case Modes.Numeric:
            pnlAlpha.Visible = false;
            pnlNumeric.Visible = true;
            pnlNone.Visible = false;
            sgData.Height = 79;
            break;
          case Modes.None:
            pnlAlpha.Visible = false;
            pnlNumeric.Visible = false;
            pnlNone.Visible = true;
            sgData.Height = 191;
            break;
        }
      }
    }

    protected virtual void EnteredTextChanged(string text)
    {
      
    }
    protected virtual void OkClicked()
    {     
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

      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
    private void btnBackspace_MouseUp(object sender, MouseEventArgs e)
    {
      if (enteredText.Length == 0)
        return;

      enteredText = enteredText.Substring(0, enteredText.Length - 1);
      lblSearch.Text = enteredText;
      EnteredTextChanged(enteredText);
    }
    private void btnKeyboard_MouseUp(object sender, MouseEventArgs e)
    {
      enteredText += ((Control)sender).Text;
      lblSearch.Text = enteredText;
      EnteredTextChanged(enteredText);
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
    private void btnAlpha_Click(object sender, System.EventArgs e)
    {
      Mode = Modes.Alpha;
    }
    private void btnNumeric_Click(object sender, System.EventArgs e)
    {
      Mode = Modes.Numeric;
    }
    private void btnNoneAbc_Click(object sender, System.EventArgs e)
    {
      Mode = Modes.Alpha;
    }
    private void btnNone123_Click(object sender, System.EventArgs e)
    {
      Mode = Modes.Numeric;
    }
    private void keyboardButton1_Click(object sender, System.EventArgs e)
    {
      Mode = Modes.None;
    }
    private void keyboardButton6_Click(object sender, System.EventArgs e)
    {
      Mode = Modes.None;
    }

    private string enteredText = string.Empty;
    private Modes mode = Modes.Alpha;


  }
}