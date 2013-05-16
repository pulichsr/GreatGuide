using System;
using System.Threading;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.WinCe;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmHowItWorks : Form
  {
    public frmHowItWorks()
    {
      InitializeComponent();
    }

    private void LoadResources()
    {
      btnPlay.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmHowItWorks.btnPlay");
      btnPause.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmHowItWorks.btnPause");
      btnContinue.Image = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnContinue");
    }
    private void Play()
    {
      CarApplication.Instance.WriteLog(this,">> Play");

      CarApplication.Instance.WriteLog(this, "   userRequestedStop = true");

      CarApplication.Instance.StopIntroVideo();
      Thread.Sleep(2000);
      CarApplication.Instance.PlayIntroVideo();

      this.BringToFront();
    }
    private void Pause()
    {
      CarApplication.Instance.PauseIntroVideo();
    }
    private void Continue()
    {
      CarApplication.Instance.MediaStateChanged -= Instance_MediaStateChanged;
     
      WaitCursor.Show(true);

      CarApplication.Instance.StopIntroVideo();
      InvalidateAll();

      CarApplication.Instance.MediaType = MediaTypes.Sound;

      Close();
    }
    private void InvalidateAll()
    {
      this.Invalidate();
      lblToggle.Invalidate();
      
      btnPlay.Invalidate();
      lblPlay.Invalidate();

      btnPause.Invalidate();
      lblPause.Invalidate();

      btnContinue.Invalidate();

      this.BringToFront();
    }
    private void ToggleVideoSize()
    {
      fullScreen = !fullScreen;
      CarApplication.Instance.VideoSize = fullScreen ? VideoSize.FullScreen : VideoSize.HalfScreen;

      InvalidateAll();
    }

    private void frmHowItWorks_Load(object sender, EventArgs e)
    {
      WaitCursor.Show(false);

      CarApplication.Instance.MediaType = MediaTypes.Video;
      LoadResources();

      CarApplication.Instance.MediaStateChanged += Instance_MediaStateChanged;
      Play();
      ToggleVideoSize();
    }
    private void frmHowItWorks_MouseUp(object sender, MouseEventArgs e)
    {
      ToggleVideoSize();
    }
    private void Instance_MediaStateChanged(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(this, string.Format("Instance_MediaStateChanged State:{0}",CarApplication.Instance.DeviceMediaState));

      switch(CarApplication.Instance.DeviceMediaState)
      {
        case MediaStateEvent.MediaStates.Stopped:
          btnPause.Visible = false;
          lblPause.Visible = false;
          break;
        case MediaStateEvent.MediaStates.Playing:
          btnPause.Visible = true;
          lblPause.Visible = true;
          break;
        case MediaStateEvent.MediaStates.Paused:
          btnPause.Visible = true;
          lblPause.Visible = true;
          break;
        case MediaStateEvent.MediaStates.Ended:
          Continue();
          break;
      }
    }

    private void btnPlay_Click(object sender, EventArgs e)
    {
      if (fullScreen)
      {
        ToggleVideoSize();
        return;
      }

      Play();
    }
    private void btnPause_Click(object sender, EventArgs e)
    {
      if (fullScreen)
      {
        ToggleVideoSize();
        return;
      }

      Pause();
    }
    private void btnContinue_Click(object sender, EventArgs e)
    {
      if (fullScreen)
      {
        ToggleVideoSize();
        return;
      }

      Continue();
    }

    private Boolean fullScreen = false;
  }
}