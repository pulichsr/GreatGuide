using System;
using System.IO;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmHelp : frmCarApplicationBase
  {
    public static void ShowForm(string helpFilename)
    {
      frmHelp frm = new frmHelp();
      frm.helpFilename = helpFilename;
      frm.ShowDialog();
    }

    public frmHelp()
    {
      InitializeComponent();

      AddNavigationControl(BtnBack);

      btnMap.Visible = false;
      btnSoftKey1.Visible = false;
      btnSoftKey2.Visible = false;
      btnSoftKey3.Visible = false;
      btnSoftKey4.Visible = false;
      btnSoftKey5.Visible = false;
    }

    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl, ref handled, ref clickData);
      if (handled == true)
        return;

      DialogResult = DialogResult.Cancel;
    }

    private void frmHelp_Load(object sender, EventArgs e)
    {
      if (File.Exists(helpFilename) == false)
        return;

      TextReader Reader;
      try
      {
        Reader = new StreamReader(helpFilename);
      }
      catch
      {
        return;
      }

      edtHelp.Text = Reader.ReadToEnd();
      Reader.Close();
    }

    private string helpFilename = string.Empty;


  }
}