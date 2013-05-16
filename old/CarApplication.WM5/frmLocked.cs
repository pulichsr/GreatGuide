using System.Drawing;
using System.Windows.Forms;
using Nucleo.Windows.Forms.TransparentControls;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmLocked : Form, IControlBackground
  {
    public static void ShowForm()
    {
      frmLocked frm = new frmLocked();
      frm.ShowDialog();
    }

    private frmLocked()
    {
      InitializeComponent();

      LoadResources();
    }

    public Image BackgroundImage
    {
      get { return backgroundImage; }
    }
    private void LoadResources()
    {
      try
      {
        backgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmLocked.Background");
      }
      catch
      {
      }
    }

    private void frmLocked_Paint(object sender, PaintEventArgs e)
    {
      if (backgroundImage != null)
        e.Graphics.DrawImage(backgroundImage, 0, 0);
    }

    private Bitmap backgroundImage = null;
  }
}