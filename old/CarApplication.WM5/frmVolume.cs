using System;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmVolume : frmCarApplicationBase
  {
    public static void ShowModal()
    {
      frmVolume frm = new frmVolume();
      frm.ShowDialog();
    }

    private frmVolume()
    {
      InitializeComponent();

      BtnMap.Visible = false;

      BtnSoftKey1.Visible = false;
      BtnSoftKey2.Visible = false;
      BtnSoftKey3.Visible = false;
      BtnSoftKey4.Visible = false;
      BtnSoftKey5.Visible = false;

      LoadResources();
    }

    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl, ref handled, ref clickData);
      if (handled == true)
        return;

      if (navigationControl == BtnMap)
      {
        handled = true;
        Close();
        return;
      }

      if (navigationControl == BtnBack)
      {
        handled = true;
        Close();
        return;
      }
    }

    private UInt16 Volume
    {
      set
      {
        edtVolume.Text = value.ToString();
      }
    }

    private void LoadResources()
    {
      btnDown.Image = CarApplication.Instance.ImageManager.ImgSelectorPrevious;
      btnUp.Image = CarApplication.Instance.ImageManager.ImgSelectorNext;
      edtVolume.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgSelectorBackground;
    }

    private void frmVolume_Load(object sender, EventArgs e)
    {
      Volume = CarApplication.Instance.DeviceVolume;
    }
    private void btnDown_MouseUp(object sender, MouseEventArgs e)
    {
      Volume = CarApplication.Instance.DecreaseVolume();
    }
    private void btnUp_MouseUp(object sender, MouseEventArgs e)
    {
      Volume = CarApplication.Instance.IncreaseVolume();
    }
  }
}