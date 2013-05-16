using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmContentManagerConfiguration : Form
  {
    public static void ShowModal()
    {
      frmContentManagerConfiguration frm = new frmContentManagerConfiguration();
      frm.ShowDialog();
    }

    private frmContentManagerConfiguration()
    {
      InitializeComponent();

    }

    private Boolean ValidateInput()
    {
      CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("ContentManager.IsLoggingActive", chkLogging.Checked);

      return true;
    }
    private void frmContentManagerConfiguration_Load(object sender, EventArgs e)
    {
      try
      {
        chkLogging.Checked = CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("ContentManager.IsLoggingActive").Value;
      }
      catch (Exception exc)
      {
        MessageBox.Show(ExceptionManager.Message("Error loading configuration",exc));
      }
    }

    private void frmContentManagerConfiguration_Closing(object sender, CancelEventArgs e)
    {
      if (ValidateInput() == false)
        e.Cancel = true;
    }
  }
}