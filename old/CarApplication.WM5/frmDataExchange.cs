using System;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmDataExchange : Form
  {
    public static DialogResult ShowForm()
    {
      frmDataExchange frm = new frmDataExchange();

      DialogResult Result = frm.ShowDialog();

      return Result;
    }

    private frmDataExchange()
    {
      InitializeComponent();
    }

    private void btnLoadFormDefinitions_Click(object sender, EventArgs e)
    {
      frmLoadFormDefinition.ShowForm();
    }
    private void btnLoadContent_Click(object sender, EventArgs e)
    {
      frmLoadContent.ShowForm();
    }
    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
    private void btnLoadItinerary_Click(object sender, EventArgs e)
    {
      frmLoadItinerary.ShowForm();
    }
    private void btnLoadDestinations_Click(object sender, EventArgs e)
    {
      frmLoadDestinations.ShowForm();
    }
    private void frmDataExchange_Activated(object sender, EventArgs e)
    {
      edtItineraryFile.Text = CarApplication.Instance.LoadedItineraryFile;
      edtFormFile.Text = CarApplication.Instance.LoadedFormDefinitionFile;
      edtContentFile.Text = CarApplication.Instance.LoadedContentFile;
      edtDestinationFile.Text = CarApplication.Instance.LoadedDestinationFile;
    }

  }
}