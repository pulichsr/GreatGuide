using Nucleo.GoodGuide.Types;
using Nucleo.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmLanguageUnitsSettings : frmCarApplicationBase
  {
    public frmLanguageUnitsSettings()
    {
      InitializeComponent();

      LoadResources();
    }

    private void LoadResources()
    {
      btnPreviousUnits.Image = CarApplication.Instance.ImageManager.ImgSelectorPrevious;
      btnNextUnits.Image = CarApplication.Instance.ImageManager.ImgSelectorNext;
      btnPreviousLanguage.Image = CarApplication.Instance.ImageManager.ImgSelectorPrevious;
      btnNextLanguage.Image = CarApplication.Instance.ImageManager.ImgSelectorNext;
      edtLanguage.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgSelectorBackground;
      edtUnits.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgSelectorBackground;
    }

    protected override void NavigationControlClicked(System.Windows.Forms.Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl,ref handled,ref clickData);
      if (handled == true)
        return;

      #region Update language if changed
      if (CarApplication.Instance.Language != language)
      {
        WaitCursor.Show(true);
        CarApplication.Instance.Language = language;
        WaitCursor.Show(false);
      }
      #endregion

      #region Update units if changed
      if (CarApplication.Instance.Units != units)
      {
        WaitCursor.Show(true);
        CarApplication.Instance.Units = units;
        WaitCursor.Show(false);
      }
      #endregion

      if (navigationControl == BtnMap)
      {
        handled = true;
        Close();
        return;
      }

      if (navigationControl == BtnBack)
      {
        // Return point must be handled by menu
        handled = false;
      }
    }

    private void btnPreviousLanguage_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      language = CarApplication.Instance.PreviousLanguage(language);
      edtLanguage.Text = LanguageHelper.DisplayName(language);
    }
    private void btnNextLanguage_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      language = CarApplication.Instance.NextLanguage(language);
      edtLanguage.Text = LanguageHelper.DisplayName(language);
    }
    private void btnPreviousUnits_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      units = CarApplication.Instance.PreviousUnits(units);
      edtUnits.Text = units.ToString();
    }
    private void btnNextUnits_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      units = CarApplication.Instance.NextUnits(units);
      edtUnits.Text = units.ToString();
    }
    private void frmLanguageUnitsSettings_Load(object sender, System.EventArgs e)
    {
      units = CarApplication.Instance.Units;
      edtUnits.Text = units.ToString();

      language = CarApplication.Instance.Language;
      edtLanguage.Text = LanguageHelper.DisplayName(language);
    }

    private Language language;
    private Units units;
   
  }
}