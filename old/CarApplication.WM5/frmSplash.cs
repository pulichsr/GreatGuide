using System;
using System.Drawing;
using System.Windows.Forms;
using Nucleo.Windows.Forms.TransparentControls;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSplash : Form, IControlBackground
  {
    public delegate void UpdateTextDelegate(string text);

    public frmSplash()
    {
      InitializeComponent();
    }

    public string Version
    {
      set { lblVersion.Text = value; }
    }
    public string Copyright
    {
      set { lblCopyright.Text = value; }
    }
    public string Helpline
    {
      set { lblHelpline.Text = value; }
    }

    public Image BackgroundImage
    {
      get { return backgroundImage; }
    }

    private void LoadResources()
    {
      try
      {
        backgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmSplash.Background");
      }
      catch
      {
      }
    }

    private void frmSplash_Load(object sender, EventArgs e)
    {
      LoadResources();
    }
    private void frmSplash_Paint(object sender, PaintEventArgs e)
    {
      if (backgroundImage != null)
        e.Graphics.DrawImage(backgroundImage, 0, 0);
    }

    private Bitmap backgroundImage = null;

  }
}