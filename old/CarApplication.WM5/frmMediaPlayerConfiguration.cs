using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmMediaPlayerConfiguration : Form
  {
    public static void ShowModal()
    {
      frmMediaPlayerConfiguration frm = new frmMediaPlayerConfiguration();
      frm.ShowDialog();
    }

    private frmMediaPlayerConfiguration()
    {
      InitializeComponent();

    }

    private Boolean ValidateInput()
    {
      CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("TopbandMediaPlayer.IsLoggingActive", chkLogging.Checked);

      return true;
    }
    private void frmMediaPlayerConfiguration_Load(object sender, EventArgs e)
    {
      try
      {
        chkLogging.Checked = CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("TopbandMediaPlayer.IsLoggingActive").Value;
      }
      catch (Exception exc)
      {
        MessageBox.Show(ExceptionManager.Message("Error loading configuration",exc));
      }
    }

    private void frmMediaPlayerConfiguration_Closing(object sender, CancelEventArgs e)
    {
      if (ValidateInput() == false)
        e.Cancel = true;
    }
  }
}