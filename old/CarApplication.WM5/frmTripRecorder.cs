using System;
using System.ComponentModel;
using System.Windows.Forms;
using Nucleo.Events;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmTripRecorder : Form
  {
    public delegate void InvokerDelegate();

    public static void ShowForm()
    {
      frmTripRecorder frm = new frmTripRecorder();
      frm.ShowDialog();
      frm.Dispose();
    }

    #region Eventbroker
    [EventPublisher(EventTopics.TripRecorder.RequestCurrentState)]
    public event EventHandler<GoodGuideEventArgs> TripRecorderRequestCurrentState;

    [EventPublisher(EventTopics.TripRecorder.Control)]
    public event EventHandler<GoodGuideEventArgs> TripRecorderControl;

    [EventSubscriber(EventTopics.TripRecorder.StateChange)]
    public void TripRecorderStateChangeHandler(object sender, GoodGuideEventArgs e)
    {
      TripRecorderStateEvent EventData = (TripRecorderStateEvent)e.EventData;
      TripRecorderState = EventData.State;

      ClearFields();
    }

    [EventSubscriber(EventTopics.GpsAdapter.GpsPosition)]
    public void TripRecorderGpsPositionHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is GpsPositionEvent == false)
        return;

      Invoke(updatePositionDelegate, sender, e);
    }
    #endregion

    private frmTripRecorder()
    {
      InitializeComponent();

      updatePositionDelegate = UpdateGpsPosition;

      LoadResources();
    }

    private TripRecorderStates TripRecorderState
    {
      get { return tripRecorderState; }
      set 
      { 
        tripRecorderState = value;
        btnTripName.Visible = (value == TripRecorderStates.Stopped);

        switch (value)
        {
          case TripRecorderStates.Stopped:
            btnStop.Enabled = false;
            btnRecord.Enabled = true;
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
            break;
          case TripRecorderStates.Recording:
            btnStop.Enabled = true;
            btnRecord.Enabled = false;
            btnPlay.Enabled = false;
            btnPause.Enabled = false;
            break;
          case TripRecorderStates.Playing:
            btnStop.Enabled = true;
            btnRecord.Enabled = false;
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
            break;
          case TripRecorderStates.Paused:
            btnStop.Enabled = false;
            btnRecord.Enabled = false;
            btnPlay.Enabled = true;
            btnPause.Enabled = false;
            break;
        }
      }
    }
    private string TripName
    {
      get { return edtTripName.Text; }  
      set
      {
        edtTripName.Text = value;
        btnRecord.Enabled = string.IsNullOrEmpty(TripName) == false || TripRecorderState == TripRecorderStates.Stopped;
        btnPlay.Enabled = string.IsNullOrEmpty(TripName) == false || TripRecorderState == TripRecorderStates.Stopped;
      }
    }

    private void LoadResources()
    {
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;

      btnStop.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgActiveSoftkey;
      btnStop.InactiveBackgroundImage = CarApplication.Instance.ImageManager.ImgInactiveSoftkey;

      btnRecord.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgActiveSoftkey;
      btnRecord.InactiveBackgroundImage = CarApplication.Instance.ImageManager.ImgInactiveSoftkey;

      btnPlay.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgActiveSoftkey;
      btnPlay.InactiveBackgroundImage = CarApplication.Instance.ImageManager.ImgInactiveSoftkey;

      btnPause.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgActiveSoftkey;
      btnPause.InactiveBackgroundImage = CarApplication.Instance.ImageManager.ImgInactiveSoftkey;

      btnTripName.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgEdit;
    }
    private void UpdateGpsPosition(object sender, GoodGuideEventArgs e)
    {
      GpsPositionEvent EventData = (GpsPositionEvent)e.EventData;

      edtTime.Text = EventData.FixDtm.ToString("yyyy-MM-dd hh:mm:ss");
      edtLatitude.Text = EventData.Latitude.ToString();
      edtLongitude.Text = EventData.Longitude.ToString();
      edtSpeed.Text = EventData.Speed.ToString();
      edtHeading.Text = EventData.Heading.ToString();

      if (tripRecorderState != TripRecorderStates.Recording)
        return;
    }
    public void UpdateTripRecorderState()
    {
      TripRecorderStateEvent EventData = new TripRecorderStateEvent();
      if (OnTripRecorderRequestCurrentState(EventData) == false)
        return;

      TripRecorderState = EventData.State;

      edtTripName.Text = EventData.Message;
    }
    private void ClearFields()
    {
      edtTime.Text = string.Empty;
      edtLatitude.Text = string.Empty;
      edtLongitude.Text = string.Empty;
      edtSpeed.Text = string.Empty;
      edtHeading.Text = string.Empty;
    }

    #region Event handlers
    private void frmTripRecorder_Load(object sender, EventArgs e)
    {
      CarApplication.Instance.EventBroker.Register(this);

      UpdateTripRecorderState();
    }
    private void frmTripRecorder_Closing(object sender, CancelEventArgs e)
    {
      CarApplication.Instance.EventBroker.Unregister(this);
    }
    private void btnStop_Click(object sender, EventArgs e)
    {
      OnTripRecorderControl(TripRecorderStates.Stopped,edtTripName.Text);
    }
    private void btnRecord_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(TripName) == true)
      {
        frmMessage.ShowText("Trip name is undefined");
        return;
      }

      OnTripRecorderControl(TripRecorderStates.Recording, edtTripName.Text);
    }
    private void btnPlay_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(TripName) == true)
      {
        CarApplication.Instance.WriteLog(this, "Trip name is undefined");
        frmMessage.ShowText("Trip name is undefined");
        return;
      }

      try
      {
        CarApplication.Instance.WriteLog(this, "Raising TripRecorderControl event");
        OnTripRecorderControl(TripRecorderStates.Playing, edtTripName.Text);
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this, "Error raising TripRecorderControl event",exc);
      }
    }
    private void btnPause_Click(object sender, EventArgs e)
    {
      OnTripRecorderControl(TripRecorderStates.Paused, edtTripName.Text);
    }
    private void btnTripName_Click(object sender, EventArgs e)
    {
      string tripName;
      DialogResult Result = frmAlphaNumericKeyboard.GetText("Enter trip name", out tripName);
      if (Result == DialogResult.Cancel)
        return;

      TripName = tripName;
    }
    private void btnBack_Click(object sender, EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
    #endregion

    #region Event dispatchers
    private Boolean OnTripRecorderRequestCurrentState(TripRecorderStateEvent EventData)
    {
      if (TripRecorderRequestCurrentState == null)
        return false;

      try
      {
        GoodGuideEventArgs Args = new GoodGuideEventArgs(GetType().ToString(), EventData);
        TripRecorderRequestCurrentState(this, Args);
      }
      catch
      {
        return false;
      }

      return true;
    }
    private void OnTripRecorderControl(TripRecorderStates state, string tripName)
    {
      if (TripRecorderControl == null)
        return;

      try
      {
        TripRecorderControlEvent EventData = new TripRecorderControlEvent(tripName, state);
        GoodGuideEventArgs Args = new GoodGuideEventArgs("", EventData);
        TripRecorderControl(this, Args);
        if (EventData.CanAction == false)
          frmMessage.ShowText(EventData.Message);
      }
      catch
      {

      }
    }
    #endregion

    private readonly EventHandler<GoodGuideEventArgs> updatePositionDelegate = null;
    private TripRecorderStates tripRecorderState = TripRecorderStates.Stopped;


  }
}