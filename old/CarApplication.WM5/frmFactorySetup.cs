using System;
using System.Windows.Forms;
using Nucleo.Events;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmFactorySetup : Form
  {
    public static DialogResult ShowForm()
    {
      frmFactorySetup frm = new frmFactorySetup();

      CarApplication.Instance.EventBroker.Register(frm);
      DialogResult Result = frm.ShowDialog();
      CarApplication.Instance.EventBroker.Unregister(frm);

      return Result;
    }

    [EventPublisher(EventTopics.TripRecorder.ShowForm)]
    public event EventHandler<GoodGuideEventArgs> ShowTripRecorderForm;

    private frmFactorySetup()
    {
      InitializeComponent();
    }

    private void frmFactorySetup_Load(object sender, EventArgs e)
    {
      WinCe.Taskbar.Show();
    }
    private void frmFactorySetup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      WinCe.Taskbar.Hide();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      DialogResult Result = MessageBox.Show("Are you sure you want to exit?", "Comfirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
      if (Result == DialogResult.No)
        return;

      DialogResult = System.Windows.Forms.DialogResult.Yes;
    }
    private void btnTripRecorder_Click(object sender, EventArgs e)
    {
      frmTripRecorder.ShowForm();
    }
    private void btnFactorySettings_Click(object sender, EventArgs e)
    {
      frmSystemSettings.ShowForm();
    }
    private void btnClose_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.SaveConfiguration();
      Close();
    }
    private void btnDataExchange_Click(object sender, EventArgs e)
    {
      frmDataExchange.ShowForm();
    }
    private void btnVersions_Click(object sender, EventArgs e)
    {
      frmVersions.ShowForm(CarApplication.Instance.DeviceServices);
    }
    private void btnFactoryPassword_Click(object sender, EventArgs e)
    {
      frmFactorySetupPassword.ShowForm();
    }
    private void btnMasterArea_Click(object sender, EventArgs e)
    {
      frmMasterArea.ShowForm();
    }
    private void btnGpsInfo_Click(object sender, EventArgs e)
    {
      frmGpsInfo.ShowModal();
    }
    private void btnGeoBase_Click(object sender, EventArgs e)
    {
      frmGeoBaseConfiguration.ShowModal();
    }
    private void btnMediaPlayer_Click(object sender, EventArgs e)
    {
      frmMediaPlayerConfiguration.ShowModal();
    }
    private void btnContentManager_Click(object sender, EventArgs e)
    {
      frmContentManagerConfiguration.ShowModal();
    }

  }
}