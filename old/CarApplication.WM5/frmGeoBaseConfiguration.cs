using System;
using System.ComponentModel;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmGeoBaseConfiguration : Form
  {
    public static void ShowModal()
    {
      frmGeoBaseConfiguration frm = new frmGeoBaseConfiguration();
      frm.ShowDialog();
    }

    private frmGeoBaseConfiguration()
    {
      InitializeComponent();

    }

    private Boolean ValidateInput()
    {
      #region Notifications metadata
      try
      {
        NotificationDefinitions definitions = NotificationDefinitionsFactory.Create(edtNotificationsMetadata.Text);
      }
      catch (Exception exc)
      {
        MessageBox.Show(ExceptionManager.Message("Error in notification definitions", exc));
        return false;
      }
      CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("GeoBaseNavigator.NotificationDefinitions", edtNotificationsMetadata.Text.ToUpper());
      #endregion

      CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("GeoBaseNavigator.IsDepartureNotificationActive", chkIsDepartureNotificationActive.Checked);
      CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("GeoBaseNavigator.IsAfterTurnDistanceNotificationActive", chkIsAfterTurnDistanceNotificationActive.Checked);
      CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("GeoBaseNavigator.IsRoundaboutExitNotificationActive", chkIsRoundaboutExitNotificationActive.Checked);

      #region Notifications timeout
      Int32 timeout;
      try
      {
        timeout = Convert.ToInt32(edtNotificationsTimeout.Text);
      }
      catch (Exception exc)
      {
        MessageBox.Show("Invalid Notifications Timeout");
        return false;
      }
      CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().SetInt32("GeoBaseNavigator.NotificationsTimeout", timeout);
      #endregion

      #region Routing strategy
      if (edtRoutingStrategy.Text.ToUpper() != "F" && edtRoutingStrategy.Text.ToUpper() != "S")
      {
        MessageBox.Show("Invalid Routing Strategy");
        return false;
      }
      CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().SetString("GeoBaseNavigator.RoutingStrategy", edtRoutingStrategy.Text.ToUpper());
      #endregion

      CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().SetBoolean("GeoBaseNavigator.IsLoggingActive", chkLogging.Checked);

      return true;
    }

    private void frmGeoBaseConfiguration_Load(object sender, EventArgs e)
    {
      try
      {
        edtNotificationsMetadata.Text = CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("GeoBaseNavigator.NotificationDefinitions");
        chkIsDepartureNotificationActive.Checked = CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("GeoBaseNavigator.IsDepartureNotificationActive").Value;
        chkIsAfterTurnDistanceNotificationActive.Checked = CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("GeoBaseNavigator.IsAfterTurnDistanceNotificationActive").Value;
        chkIsRoundaboutExitNotificationActive.Checked = CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("GeoBaseNavigator.IsRoundaboutExitNotificationActive").Value;

        edtNotificationsTimeout.Text = CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().GetInt32("GeoBaseNavigator.NotificationsTimeout").ToString();
        edtRoutingStrategy.Text = CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().GetString("GeoBaseNavigator.RoutingStrategy");
        chkLogging.Checked = CarApplication.Instance.RepositoryLocator.LocateRepository<INamedParameterRepository>().GetBoolean("GeoBaseNavigator.IsLoggingActive").Value;
      }
      catch (Exception exc)
      {
        MessageBox.Show(ExceptionManager.Message("Error loading configuration",exc));
      }
    }
    private void frmGeoBaseConfiguration_Closing(object sender, CancelEventArgs e)
    {
      if (ValidateInput() == false)
        e.Cancel = true;
    }
    private void edit_LostFocus(object sender, EventArgs e)
    {
      inputPanel1.Enabled = false;
    }
    private void edit_GotFocus(object sender, EventArgs e)
    {
      inputPanel1.Enabled = true;
    }
  }
}