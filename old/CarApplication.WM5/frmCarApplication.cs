using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.Windows.Forms.DynamicForms;
using Timer=System.Threading.Timer;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmCarApplication : Form
  {
    public const Int32 DestinationArrivalClearTimeout = 30000;
    public const Int32 HelpClickTimeout = 750;

    public delegate void InvokerDelegate();
    public delegate void DestinationInvokerDelegate(DestinationDataset.DestinationRow destination);

    public frmCarApplication()
    {
      CarApplication.Instance.WriteLog(this, ">> frmCarApplication.ctor");
      CarApplication.Instance.MediaStateChanged += CarApplication_MediaStateChanged;
      CarApplication.Instance.GpsFixStateChanged += CarApplication_GpsStateChanged;
      CarApplication.Instance.GpsPositionChanged += CarApplication_GpsPositionChanged;
      CarApplication.Instance.RouterStateChanged += CarApplication_RouterStateChanged;
      CarApplication.Instance.BookingPeriodExpired += CarApplication_BookingPeriodExpired;
      CarApplication.Instance.ArrivedAtDestination += CarApplication_ArrivedAtDestination;
      CarApplication.Instance.IsNavigatorAnnouncementActiveChanged += CarApplication_IsNavigatorAnnouncementActiveChanged;
      CarApplication.Instance.NavigatorDirectionsChanged += CarApplication_NavigatorDirectionsChanged;
      CarApplication.Instance.CloseApplicationRequested += CarApplication_CloseApplicationRequested;

      updateRouterStateDelegate = UpdateRouterState;
      arrivedAtDestinationDelegate = ArrivedAtDestination;
      destinationArrivalClearDelegate = ClearDestinationArrival;
      updateDeviceMediaStateDelegate = UpdateDeviceMediaState;
      updateGpsStateDelegate = UpdateGpsState;
      updateGpsPositionDelegate = UpdateGpsPosition;
      bookingPeriodExpiredDelegate = LockDevice;
      isNavigatorAnnouncementActiveDelegate = UpdateIsNavigatorAnnouncementActive;
      navigatorDirectionsDelegate = UpdateNavigatorDirections;

      eventWindow = new EventWindow();
      CarApplication.Instance.EventWindow = eventWindow;

      InitializeComponent();
      edtDestination.BackColor = Color.Red;
      edtDestination.ForeColor = Color.White;

      LoadResources();

      pbHeading.BackColor = Color.Red;
      pbHeading.Image = CarApplication.Instance.ImageManager.ImgHeadingInvalid;
      lblGpsFixState.BackColor =  Color.Red;

      CarApplication.Instance.WriteLog(this, "<< frmCarApplication.ctor");
    }

    private void UpdateRouterState()
    {
      switch (CarApplication.Instance.RouterState)
      {
        case DestinationRouterStateEvent.RouterStates.NoDestination:
          btnResumeRouter.Visible = false;
          btnPauseRouter.Visible = true;
          btnPauseRouter.Enabled = false;
          break;
        case DestinationRouterStateEvent.RouterStates.Routing:
          btnResumeRouter.Visible = false;
          btnPauseRouter.Visible = true;
          btnPauseRouter.Enabled = true;
          break;
        case DestinationRouterStateEvent.RouterStates.RoutePaused:
          btnResumeRouter.Visible = true;
          btnPauseRouter.Visible = false;
          break;
      }

      edtDestination.Text = FormatDestinationText();
      edtDestination.Text = FormatDestinationText();
    }
    private void UpdateDeviceMediaState()
    {
      switch (CarApplication.Instance.DeviceMediaState)
      {
        case MediaStateEvent.MediaStates.Stopped:
          btnPlayAudio.Visible = false;
          btnPlayAudio.Enabled = false;

          btnPauseAudio.Visible = true;
          btnPauseAudio.Enabled = false;

          btnRepeatAudio.Visible = true;
          btnRepeatAudio.Enabled = firstContentPlayed;
          break;
        case MediaStateEvent.MediaStates.Playing:
          btnPlayAudio.Visible = false;
          btnPauseAudio.Enabled = false;

          btnPauseAudio.Visible = true;
          btnPauseAudio.Enabled = true;

          btnRepeatAudio.Visible = true;
          btnRepeatAudio.Enabled = true;

          if (CarApplication.Instance.MediaType == MediaTypes.Sound)
            firstContentPlayed = true;
          break;
        case MediaStateEvent.MediaStates.Paused:
          btnPlayAudio.Visible = true;
          btnPlayAudio.Enabled = true;

          btnPauseAudio.Visible = false;

          btnRepeatAudio.Visible = true;
          btnRepeatAudio.Enabled = false;
          break;
      }

      edtDestination.Text = FormatDestinationText();
      edtDestination.Text = FormatDestinationText();
    }
    private void UpdateGpsState()
    {
      UpdateHeadingIndicator();
      CarApplication.Instance.CheckBookingPeriod();
    }
    private void UpdateGpsPosition()
    {
      edtDestination.Text = FormatDestinationText();
      UpdateHeadingIndicator();
    }
    private void UpdateIsNavigatorAnnouncementActive()
    {
      CarApplication.Instance.WriteLog(this, ">> UpdateIsNavigatorAnnouncementActive");
      if (CarApplication.Instance.DeviceMediaState != MediaStateEvent.MediaStates.Paused)
      {
        CarApplication.Instance.WriteLog(this, "  MediaState <> Paused. Returning...");
        return;
      }

      CarApplication.Instance.WriteLog(this, string.Format("  Setting btnPlayAudio.Enabled to {0}", CarApplication.Instance.IsNavigatorAnnouncementActive == false));
      btnPlayAudio.Enabled = CarApplication.Instance.IsNavigatorAnnouncementActive == false;
    }
    private void UpdateNavigatorDirections()
    {
      frmDirections.ShowModal(CarApplication.Instance.NavigatorDirections);
    }
    private void UpdateHeadingIndicator()
    {
      if (CarApplication.Instance.IsGpsFixValid == false)
      {
        lblGpsFixState.BackColor = Color.Red;
        lblGpsFixState.Text = "GPS";

        pbHeading.BackColor = Color.Red;
        pbHeading.Image = CarApplication.Instance.ImageManager.ImgHeadingInvalid;
      }
      else
      {
        lblGpsFixState.BackColor = Color.Green;

        Direction.Directions heading = Direction.FromHeading((Int16)CarApplication.Instance.CurrentGpsPosition.Heading);
        if (headingIcons.ContainsKey(heading) == true)
        {
          // Only show pointer up. This means that the direction of travel (heading up) is whatever the text indicates
          if (pbHeading.Image != headingIcons[Direction.Directions.North])
            pbHeading.Image = headingIcons[Direction.Directions.North];
        }

        string headingText = Direction.ToShortString(heading);
        if (lblGpsFixState.Text != headingText)
          lblGpsFixState.Text = headingText;
      }
    }

    private void LockDevice()
    {
      CarApplication.Instance.LockDevice();
    }
    private void ArrivedAtDestination(DestinationDataset.DestinationRow destination)
    {
      edtDestination.Text = string.Format("Arrived at {0}",destination.Code);
      destinationArrivalClearTimer = new Timer(destinationArrivalClearTimerCallback,null,DestinationArrivalClearTimeout,Timeout.Infinite);
    }
    private void ClearDestinationArrival()
    {
      edtDestination.Text = string.Empty;
    }
    private void LoadResources()
    {
      btnPower.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgPower;

      btnPlayAudio.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnPlayAudio");
      btnPauseAudio.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnPauseAudio");
      btnPauseAudio.InactiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnDisabled");

      btnRepeatAudio.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnRepeatAudio");
      btnRepeatAudio.InactiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnDisabled");

      btnPauseRouter.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnPauseRouter");
      btnPauseRouter.InactiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnDisabled");

      btnResumeRouter.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnResumeRouter");
      btnMenu.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnMenu");
      btnItinerary.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnMyItinerary");

      headingIcons[Direction.Directions.North] = CarApplication.Instance.ImageManager.ImgHeadingValidN;
      headingIcons[Direction.Directions.NorthEast] = CarApplication.Instance.ImageManager.ImgHeadingValidNe;
      headingIcons[Direction.Directions.East] = CarApplication.Instance.ImageManager.ImgHeadingValidE;
      headingIcons[Direction.Directions.SouthEast] = CarApplication.Instance.ImageManager.ImgHeadingValidSe;
      headingIcons[Direction.Directions.South] = CarApplication.Instance.ImageManager.ImgHeadingValidS;
      headingIcons[Direction.Directions.SouthWest] = CarApplication.Instance.ImageManager.ImgHeadingValidSw;
      headingIcons[Direction.Directions.West] = CarApplication.Instance.ImageManager.ImgHeadingValidW;
      headingIcons[Direction.Directions.NorthWest] = CarApplication.Instance.ImageManager.ImgHeadingValidNw;
    }
    private string FormatDestinationText()
    {
      string destinationText = string.Empty;
      string mediaText = string.Empty;
      string gpsText = string.Empty;

      if (CarApplication.Instance.RouterState == DestinationRouterStateEvent.RouterStates.Routing)
        destinationText = string.Format("{0}", CarApplication.Instance.CurrentDestination.Code);

      if (CarApplication.Instance.ShowContentFilenames == true)
      {
        if ((CarApplication.Instance.DeviceMediaState == MediaStateEvent.MediaStates.Playing) || (CarApplication.Instance.DeviceMediaState == MediaStateEvent.MediaStates.Paused))      
          mediaText = System.IO.Path.GetFileNameWithoutExtension(CarApplication.Instance.MediaFilename);

        if (CarApplication.Instance.CurrentGpsPosition != null)
          gpsText = string.Format("{0}", CarApplication.Instance.CurrentGpsPosition.FixDtm.ToString("yyyy-MM-dd HH:mm:ss"));
      }

      return string.Format("{0} {1} {2}",destinationText,mediaText,gpsText);
    }

    #region Event handlers
    #region Form
    private void frmCarApplication_Load(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(this, ">> frmCarApplication_Load");
      UpdateGpsState();
      CarApplication.Instance.MainForm = this;
      btnItinerary.Visible = CarApplication.Instance.IsNavigatorInstalled;
      CarApplication.Instance.LoadLastGpsPosition();
      CarApplication.Instance.WriteLog(this, "<< frmCarApplication_Load");
    }
    private void frmCarApplication_Activated(object sender, EventArgs e)
    {
      WinCe.WaitCursor.Show(false);
      try
      {
        buttonClickTimer = new Timer(ButtonClickTimerCallback, null, 2000, Timeout.Infinite);
        canClickButtons = false;
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this, "Error creating timer", exc);
      }
    }
    private void frmCarApplication_Closing(object sender, CancelEventArgs e)
    {
      CarApplication.Instance.MediaStateChanged -= CarApplication_MediaStateChanged;
      CarApplication.Instance.GpsFixStateChanged -= CarApplication_GpsStateChanged;
      CarApplication.Instance.GpsPositionChanged -= CarApplication_GpsPositionChanged;
      CarApplication.Instance.RouterStateChanged -= CarApplication_RouterStateChanged;
      CarApplication.Instance.BookingPeriodExpired -= CarApplication_BookingPeriodExpired;
      CarApplication.Instance.ArrivedAtDestination -= CarApplication_ArrivedAtDestination;
      CarApplication.Instance.IsNavigatorAnnouncementActiveChanged -= CarApplication_IsNavigatorAnnouncementActiveChanged;
      CarApplication.Instance.NavigatorDirectionsChanged -= CarApplication_NavigatorDirectionsChanged;
    }

    private void btnPower_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.AudibleFeedback();

#if DEBUG
      Close();
      return;
#endif
      if (canClickButtons == false)
        return;

      if (frmConfirmation.GetConfirmation("Are you sure you want to switch off?") == DialogResult.No)
        return;

      CarApplication.Instance.PauseRouter();
      CarApplication.Instance.StopContent();
      CarApplication.Instance.Suspend();
    }
    private void btnPauseAudio_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.AudibleFeedback();

      CarApplication.Instance.PauseContent();
    }
    private void btnPlayAudio_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.AudibleFeedback();

      CarApplication.Instance.ResumeContent();
    }
    private void btnRepeatAudio_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.AudibleFeedback();

      CarApplication.Instance.RepeatContent();
    }
    private void btnPauseRouter_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.AudibleFeedback();

      CarApplication.Instance.PauseRouter();
    }
    private void btnResumeRouter_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.AudibleFeedback();
      WinCe.WaitCursor.Show(true);
      CarApplication.Instance.ResumeRouter();
      WinCe.WaitCursor.Show(false);
    }
    private void btnMenu_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.AudibleFeedback();

      if (FirstUse.IsFirstUse == true)
      {
        FirstUse.IsFirstUse = false;
      }

      CarApplication.Instance.CheckBookingPeriod();
      CarApplication.Instance.LoadNewFormData();
      try
      {
        DynamicFormManager.ShowForm(this, CarApplication.MainMenuFormName);
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this, "Error opening main menu form", exc);
      }
    }
    private void btnItinerary_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.CheckBookingPeriod();
      DynamicFormManager.ShowForm(this, CarApplication.ItineraryFormName);
    }

    private void ButtonClickTimerCallback(object state)
    {
      buttonClickTimer.Dispose();
      canClickButtons = true;
    }
    private void destinationArrivalClearTimerCallback(object state)
    {
      destinationArrivalClearTimer.Dispose();
      Invoke(destinationArrivalClearDelegate);
    }
    #endregion

    #region CarApplication
    private void CarApplication_MediaStateChanged(object sender,EventArgs e)
    {
      this.Invoke(updateDeviceMediaStateDelegate);
    }
    private void CarApplication_GpsStateChanged(object sender, EventArgs e)
    {
      this.Invoke(updateGpsStateDelegate);
    }
    private void CarApplication_GpsPositionChanged(object sender, EventArgs e)
    {
      this.Invoke(updateGpsPositionDelegate);
    }
    private void CarApplication_RouterStateChanged(object sender, EventArgs e)
    {
      this.Invoke(updateRouterStateDelegate);
    }
    private void CarApplication_BookingPeriodExpired(object sender, EventArgs e)
    {
      this.Invoke(bookingPeriodExpiredDelegate);
    }
    private void CarApplication_ArrivedAtDestination(object sender, DestinationArrivalEventArgs e)
    {
      this.Invoke(arrivedAtDestinationDelegate,e.Destination);
    }
    private void CarApplication_IsNavigatorAnnouncementActiveChanged(object sender, EventArgs e)
    {
      this.Invoke(isNavigatorAnnouncementActiveDelegate);
    }
    private void CarApplication_NavigatorDirectionsChanged(object sender, EventArgs e)
    {
      this.Invoke(navigatorDirectionsDelegate);
    }
    private void CarApplication_CloseApplicationRequested(object sender, EventArgs e)
    {
      Close();
    }
    #endregion

    #endregion

    #region Fields
    private readonly InvokerDelegate updateRouterStateDelegate;
    private readonly InvokerDelegate updateDeviceMediaStateDelegate;
    private readonly InvokerDelegate updateGpsStateDelegate;
    private readonly InvokerDelegate updateGpsPositionDelegate;
    private readonly InvokerDelegate bookingPeriodExpiredDelegate;
    private readonly InvokerDelegate destinationArrivalClearDelegate;
    private readonly DestinationInvokerDelegate arrivedAtDestinationDelegate;
    private readonly InvokerDelegate isNavigatorAnnouncementActiveDelegate;
    private readonly InvokerDelegate navigatorDirectionsDelegate;
    private Timer destinationArrivalClearTimer = null;
    private EventWindow eventWindow = null;
    private Boolean firstContentPlayed = false;
    private Timer buttonClickTimer = null;
    private Boolean canClickButtons = true;
    private readonly Dictionary<Direction.Directions, Image> headingIcons = new Dictionary<Direction.Directions, Image>();
    #endregion
  }
}




