using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmEnterFmFrequency : Form
  {

    public static DialogResult EnterValues(out Int16 fmChannel)
    {
      frmEnterFmFrequency frm = new frmEnterFmFrequency();

      DialogResult Result = frm.ShowDialog();
      fmChannel = frm.fmChannel;

      return Result;
    }

    private frmEnterFmFrequency()
    {
      InitializeComponent();

      LoadResources();
    }

    private TextBox FocusedControl
    {
      set
      {
        if (focusedControl != null)
          focusedControl.BackColor = Color.FromArgb(94,136,35); 

        focusedControl = value;

        if (focusedControl != null)
          focusedControl.BackColor = Color.FromArgb(134,94,113);
      }
    }

    private void LoadResources()
    {
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
    }
    private Boolean ValidateValues()
    {
      Boolean Result = true;

      if (edtFrequency.Text.Length == 0)
      {
        lblError.Text = "Undefined frequency";
        return false;
      }
      
      Int16 Frequency;
      try
      {
        Frequency = Convert.ToInt16(edtFrequency.Text);
      }
      catch
      {
        lblError.Text = "Invalid frequency";
        return false;
      }

      if ((Frequency < 80) || (Frequency > 150))
      {
        lblError.Text = "Invalid frequency";
        return false;
      }

      Int16 Decimals = 0;
      if (edtDecimals.Text.Length != 0)
      {
        try
        {
          Decimals = Convert.ToInt16(edtDecimals.Text);
        }
        catch
        {
          lblError.Text = "Invalid decimal";
          return false;
        }
      }

      fmChannel = (Int16)(Frequency * 10 + Decimals);

      return Result;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (ValidateValues() == false)
        return;

      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
    private void btnKeyboard_MouseUp(object sender, MouseEventArgs e)
    {
      lblError.Text = string.Empty;

      if (focusedControl == null)
        return;

      if (focusedControl.Text.Length == focusedControl.MaxLength)
        return;

      focusedControl.Text += ((Control)sender).Text;
    }
    private void btnBack_Click(object sender, EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    private void btnBackspace_MouseUp(object sender, MouseEventArgs e)
    {
      lblError.Text = string.Empty;

      if (focusedControl == null)
        return;

      if (focusedControl.Text.Length == 0)
        return;

      focusedControl.Text = focusedControl.Text.Substring(0, focusedControl.Text.Length - 1);
    }
    private void Editor_GotFocus(object sender, EventArgs e)
    {
      if (sender is TextBox)
        FocusedControl = (TextBox)sender;
    }
    private void frmEnterFmFrequency_Load(object sender, EventArgs e)
    {
      FocusedControl = edtFrequency;
    }

    private TextBox focusedControl = null;
    private Int16 fmChannel = 0;
  }
}