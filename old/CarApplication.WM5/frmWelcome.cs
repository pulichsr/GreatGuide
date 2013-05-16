using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.CarApplicationResources;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.WinCe;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmWelcome : Form
  {
    public frmWelcome()
    {
      InitializeComponent();
    }

    private void LoadResources()
    {
      try
      {
        btnContinue.Image = ImageResources.frmWelcome.btnContinue;
        pbWelcomeAd.Image = CarApplication.Instance.WelcomeAdProvider.GetAd();
      }
      catch(Exception exc)
      {
        CarApplication.Instance.WriteLog(this,"Error",exc);
      }
    }

    private void frmWelcome_Load(object sender, EventArgs e)
    {
      try
      {
        itinerary = CarApplication.Instance.Itinerary;

        LoadResources();

        lblUser.Text = CarApplication.FormatWelcome(itinerary.FirstName,itinerary.LastName,itinerary.Title);

        if (itinerary.sArrivalDat == string.Empty)
          lblArrivalDate.Text = string.Empty;
        else
          lblArrivalDate.Text = string.Format("Arrival: {0}",itinerary.ArrivalDat.ToString("yyyy-MM-dd"));

        if (itinerary.sDepartureDat == string.Empty)
          lblDepartureDate.Text = string.Empty;
        else
          lblDepartureDate.Text = string.Format("Departure: {0}", itinerary.DepartureDat.ToString("yyyy-MM-dd"));

      }
      catch
      {
      }
    }
    private void btnContinue_Click(object sender, EventArgs e)
    {
      WaitCursor.Show(true);
      Close();
    }

    private ItineraryDataset.ItineraryRow itinerary = null;

  }
}
