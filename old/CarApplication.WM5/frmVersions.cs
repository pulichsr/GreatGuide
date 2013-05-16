using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types.Interfaces.Device;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmVersions : Form
  {
    public static DialogResult ShowForm(IDeviceServices deviceServices)
    {
      frmVersions frm = new frmVersions(deviceServices);

      DialogResult Result = frm.ShowDialog();

      return Result;
    }

    private frmVersions(IDeviceServices deviceServices)
    {
      InitializeComponent();

      this.deviceServices = deviceServices;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void frmVersions_Activated(object sender, EventArgs e)
    {
      if (deviceServices != null)
        edtFirmware.Text = string.Format("{0}",deviceServices.FlashVersion);

      edtItineraryFile.Text = CarApplication.Instance.LoadedItineraryFile;
      edtFormFile.Text = CarApplication.Instance.LoadedFormDefinitionFile;
      edtContentFile.Text = CarApplication.Instance.LoadedContentFile;
      edtDestinationFile.Text = CarApplication.Instance.LoadedDestinationFile;
    }

    private IDeviceServices deviceServices;
  }
}