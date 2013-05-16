using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmGpsInfo : Form
  {
    public static void ShowModal()
    {
      frmGpsInfo frm = new frmGpsInfo();
      frm.ShowDialog();
    }

    private frmGpsInfo()
    {
      InitializeComponent();

      isValidFix = false;
      invalidFixDtm = DateTime.Now;
      validFixDtm = DateTime.Now;
    }

    private void UpdateRaw()
    {
      if (InvokeRequired == true)
        Invoke(new MethodDelegate(UpdateRaw));
      else
      {
        if (CarApplication.Instance.LastGpsRawEvent == null)
          return;

        lblGga.Text = CarApplication.Instance.LastGpsRawEvent.GgaData;
        lblRmc.Text = CarApplication.Instance.LastGpsRawEvent.RmcData;
        lblVtg.Text = CarApplication.Instance.LastGpsRawEvent.VtgData;
      }
    }

    private void UpdateProcessed()
    {
      if (InvokeRequired == true)
        Invoke(new MethodDelegate(UpdateProcessed));
      else
      {
        if (CarApplication.Instance.CurrentGpsPosition == null)
          return;

        CarApplication.Instance.WriteLog(this, string.Format("isValidFix: {0}", isValidFix));
        CarApplication.Instance.WriteLog(this, string.Format("CarApplication.Instance.IsGpsFixValid: {0}", CarApplication.Instance.IsGpsFixValid));
        if (CarApplication.Instance.IsGpsFixValid == false)
        {
          #region Not a valid fix

          #region Update fields
          lblLatitude.Text = "Latitude:";
          lblLongtiude.Text = "Longitude:";
          lblValidFix.Text = "Valid Fix: No";
          lblSpeed.Text = "Speed:";
          lblHeading.Text = "Heading:";
          #endregion

          if (isValidFix)
          {
            #region Lost fix
            invalidFixDtm = DateTime.Now;
            validFixDtm = DateTime.Now;
            TimeSpan ttff = validFixDtm - invalidFixDtm;
            lblTtff.Text = string.Format("TTFF: {0}",ttff);
            CarApplication.Instance.WriteLog(this, string.Format("Lost fix. TTFF: {0}",ttff));
            #endregion
          }
          else
          {
            #region No fix
            TimeSpan ttff = DateTime.Now - invalidFixDtm;
            lblTtff.Text = string.Format("TTFF: {0}", ttff);
            CarApplication.Instance.WriteLog(this, string.Format("No fix. TTFF: {0}", ttff));
            #endregion
          }
          #endregion
        }
        else
        {
          #region Valid fix

          #region Update fields
          lblLatitude.Text = string.Format("Latitude: {0}",CarApplication.Instance.CurrentGpsPosition.Latitude);
          lblLongtiude.Text = string.Format("Longitude: {0}",CarApplication.Instance.CurrentGpsPosition.Longitude);
          lblValidFix.Text = "Valid Fix: Yes";
          lblSpeed.Text = string.Format("Speed: {0}",CarApplication.Instance.CurrentGpsPosition.Speed);
          lblHeading.Text = string.Format("Heading: {0}",CarApplication.Instance.CurrentGpsPosition.Heading);
          #endregion

          if (isValidFix == false)
          {
            #region New fix
            validFixDtm = DateTime.Now;
            TimeSpan ttff = validFixDtm - invalidFixDtm;
            lblTtff.Text = string.Format("TTFF: {0}", ttff);
            CarApplication.Instance.WriteLog(this, string.Format("New fix. TTFF: {0}", ttff));
            #endregion
          }
          #endregion
        }

        isValidFix = CarApplication.Instance.IsGpsFixValid;
      }
    }
    private void frmGpsInfo_Load(object sender, EventArgs e)
    {
      CarApplication.Instance.LastGpsRawDataChanged += Instance_LastGpsRawDataChanged;
      CarApplication.Instance.GpsPositionChanged += Instance_GpsPositionChanged;
      CarApplication.Instance.GpsFixStateChanged += Instance_GpsFixStateChanged;
    }
    private void frmGpsInfo_Closing(object sender, CancelEventArgs e)
    {
      CarApplication.Instance.LastGpsRawDataChanged -= Instance_LastGpsRawDataChanged;
      CarApplication.Instance.GpsPositionChanged -= Instance_GpsPositionChanged;
      CarApplication.Instance.GpsFixStateChanged -= Instance_GpsFixStateChanged;
    }
    void Instance_LastGpsRawDataChanged(object sender, EventArgs e)
    {
      UpdateRaw();
    }
    void Instance_GpsPositionChanged(object sender, EventArgs e)
    {
      UpdateProcessed();
    }
    void Instance_GpsFixStateChanged(object sender, EventArgs e)
    {
      UpdateProcessed();
    }

    private bool isValidFix;
    private DateTime invalidFixDtm;
    private DateTime validFixDtm;
  }
}