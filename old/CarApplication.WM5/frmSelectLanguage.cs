using System.Globalization;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectLanguage : Form
  {
    public frmSelectLanguage()
    {
      InitializeComponent();

      LoadResources();
    }

    public CultureInfo Culture
    {
      get { return culture; }
      private set
      {
        culture = value;
        Close();
      }
    }

    private void LoadResources()
    {
      btnDe.Image = CarApplication.Instance.ImageManager.ImgFlagDe;
      btnEn.Image = CarApplication.Instance.ImageManager.ImgFlagEn;
      btnFr.Image = CarApplication.Instance.ImageManager.ImgFlagFr;
      btnIt.Image = CarApplication.Instance.ImageManager.ImgFlagIt;
      btnNl.Image = CarApplication.Instance.ImageManager.ImgFlagNl;
    }

    private void btnDe_MouseUp(object sender, MouseEventArgs e)
    {
      Culture = LanguageHelper.CreateCulture(Language.German);
    }
    private void btnEn_MouseUp(object sender, MouseEventArgs e)
    {
      Culture = LanguageHelper.CreateCulture(Language.English);
    }
    private void btnFr_MouseUp(object sender, MouseEventArgs e)
    {
      Culture = LanguageHelper.CreateCulture(Language.French);
    }
    private void btnIt_MouseUp(object sender, MouseEventArgs e)
    {
      Culture = LanguageHelper.CreateCulture(Language.Italian);
    }
    private void btnNl_MouseUp(object sender, MouseEventArgs e)
    {
      Culture = LanguageHelper.CreateCulture(Language.Dutch);
    }

    private CultureInfo culture;

  }
}