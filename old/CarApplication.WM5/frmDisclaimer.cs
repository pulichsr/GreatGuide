using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Nucleo.GoodGuide.CarApplicationResources;
using Nucleo.GoodGuide.Types;
using Nucleo.Windows.Forms.TransparentControls;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmDisclaimer : Form,IControlBackground
  {
    public frmDisclaimer(CultureInfo culture)
    {
      Guard.ArgumentNotNull(culture, "culture");

      InitializeComponent();

      this.culture = culture;
    }

    public Image BackgroundImage
    {
      get { return backgroundImage; }
    }

    private void LoadResources()
    {
      try
      {

        string filename = LanguageHelper.FormatCultureDependentPath(
          culture,
          string.Format("{0}\\Resources\\", Path.ExecutablePath),
          "Disclaimer.bmp");
        
        if (File.Exists(filename) == false)
        {
          backgroundImage = ImageResources.frmDisclaimer.Background;
        }
        else
        {
          backgroundImage = new Bitmap(filename);
        }
      }
      catch
      {
      }
    }
    private void frmDisclaimer_Paint(object sender, PaintEventArgs e)
    {
      if (backgroundImage != null)
        e.Graphics.DrawImage(backgroundImage, 0, 0);
    }
    private void frmDisclaimer_Load(object sender, EventArgs e)
    {
      LoadResources();
      lblVersion.Text = CarApplication.Instance.Version;

      timPlsWait.Enabled = true;
    }
    private void frmDisclaimer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      timPlsWait.Enabled = false;
    }
    private void timPlsWait_Tick(object sender, EventArgs e)
    {
      lblPleaseWait.Visible = !lblPleaseWait.Visible;
      Application.DoEvents();
    }

    private Bitmap backgroundImage;
    private readonly CultureInfo culture;


 }
}