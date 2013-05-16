using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.Windows.Forms.DynamicForms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmEmergency : frmDynamicBase
  {
    public frmEmergency()
    {
      InitializeComponent();

      AddNavigationControl(btnBack);
      AddNavigationControl(btnMap);

      LoadResources();

      float Latitude;
      float Longitude;
      if (CarApplication.Instance.CurrentGpsPosition != null)
      {
        Latitude = CarApplication.Instance.CurrentGpsPosition.Latitude;
        Longitude = CarApplication.Instance.CurrentGpsPosition.Longitude;

        NavigatorPositionEvent EventData = CarApplication.Instance.GetAddress(Latitude, Longitude);

        lblLatitude.Text = Latitude.ToString();
        lblLongitude.Text = Longitude.ToString();

        lblDescription.Text = EventData.Description;
        lblAddress1.Text = string.Format("{0} {1}", EventData.House, EventData.Street);
        lblAddress2.Text = string.Format("{0},{1}", EventData.City, EventData.Zip);
        lblTelephoneNo.Text = EventData.TelephoneNo;
      }
      else
      {
        lblLatitude.Text = "Location unknown";
        lblLongitude.Text = "Location unknown";

        lblDescription.Text = "Location unknown";
        lblAddress1.Text = "Location unknown";
        lblAddress2.Text = "Location unknown";
        lblTelephoneNo.Text = "Location unknown";
      }
    }

    public override void BuildPresentation(FormDefinition formDefinition, object formData)
    {
      base.BuildPresentation(formDefinition, formData);

      lblHeading.Text = formDefinition.Text;     
    }

    protected override void NavigationControlClicked(System.Windows.Forms.Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl, ref handled, ref clickData);
      if (handled == true)
        return;

      if (navigationControl == btnMap)
      {
        handled = true;
        Close();
        return;
      }
    }

    private void LoadResources()
    {
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
      btnMap.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgMap;
      btnCarRental.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgEmergencyButton;
      btnEmbassy.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgEmergencyButton;
      btnHospital.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgEmergencyButton;
      btnPolice.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgEmergencyButton;
    }
    private void NavigateTo(PoiCategory Category)
    {
      MoreResults Result = CarApplication.Instance.NavigateToPoiCategory(Category.Index);
      if ((Result == MoreResults.TakeMeThere) || (Result == MoreResults.ViewMap))
        Close();
    }

    private void btnEmbassy_Click(object sender, System.EventArgs e)
    {
      if (CarApplication.Instance.PoiCategories == null)
        return;

      PoiCategory Category = CarApplication.Instance.PoiCategories.GetByName("Embassy");
      if (Category == null)
        return;

      NavigateTo(Category);
    }
    private void btnHospital_Click(object sender, System.EventArgs e)
    {
      if (CarApplication.Instance.PoiCategories == null)
        return;

      PoiCategory Category = CarApplication.Instance.PoiCategories.GetByName("Hospital");
      if (Category == null)
        return;

      NavigateTo(Category);
    }
    private void btnPolice_Click(object sender, System.EventArgs e)
    {
      if (CarApplication.Instance.PoiCategories == null)
        return;

      PoiCategory Category = CarApplication.Instance.PoiCategories.GetByName("Police Station");
      if (Category == null)
        return;

      NavigateTo(Category);
    }
    private void btnCarRental_Click(object sender, System.EventArgs e)
    {
      if (CarApplication.Instance.PoiCategories == null)
        return;

      PoiCategory Category = CarApplication.Instance.PoiCategories.GetByName("Rental Car Agency");
      if (Category == null)
        return;

      NavigateTo(Category);
    }

  }
}