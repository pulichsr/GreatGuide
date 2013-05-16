using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmAudioSettings : frmCarApplicationBase
  {
    public frmAudioSettings()
    {
      InitializeComponent();

      BtnSoftKey1.Visible = false;
      BtnSoftKey2.Visible = false;
      BtnSoftKey3.Visible = false;
      BtnSoftKey4.Visible = false;
      BtnSoftKey5.Visible = false;

      LoadResources();
    }

    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl, ref handled, ref clickData);
      if (handled == true)
        return;

      #region Update Content if changed
      CarApplication.Instance.AudioContent = Content;
      #endregion

      #region Update Source if changed
      CarApplication.Instance.AudioSource = Source;

      CarApplication.Instance.FmTransmitterChannel = FmChannel;
      #endregion

      if (navigationControl == BtnMap)
      {
        handled = true;
        Close();
        return;
      }

      if (navigationControl == BtnBack)
      {
        // Return point must be handled by menu
        handled = false;
      }
    }

    private Int16 FmChannel
    {
      get { return fmChannel; }
      set
      {
        fmChannel = value;
        lblFrequency.Text = string.Format("{0} MHz",System.Math.Round((float)fmChannel / 10,1));
      }
    }
    private AudioSettings.Content Content
    {
      get { return content; }
      set
      {
        content = value;
        edtContent.Text = AudioSettings.ToString(content);
      }
    }
    private AudioSettings.Source Source
    {
      get { return source; }
      set
      {
        source = value;
        edtSource.Text = AudioSettings.ToString(source);

        lblFrequency.Visible = source == AudioSettings.Source.RadioOnly;
        btnEdit.Visible = source == AudioSettings.Source.RadioOnly;
      }
    }

    private void LoadResources()
    {
      btnPreviousSource.Image = CarApplication.Instance.ImageManager.ImgSelectorPrevious;
      btnNextSource.Image = CarApplication.Instance.ImageManager.ImgSelectorNext;
      btnPreviousContent.Image = CarApplication.Instance.ImageManager.ImgSelectorPrevious;
      btnNextContent.Image = CarApplication.Instance.ImageManager.ImgSelectorNext;
      edtContent.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgSelectorBackground;
      edtSource.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgSelectorBackground;
      btnEdit.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgEdit;
    }

    private void btnPreviousContent_MouseUp(object sender, MouseEventArgs e)
    {
      Content = CarApplication.Instance.PreviousAudioContent(content);
    }
    private void btnNextContent_MouseUp(object sender, MouseEventArgs e)
    {
      Content = CarApplication.Instance.NextAudioContent(content);
    }
    private void btnPreviousSource_MouseUp(object sender, MouseEventArgs e)
    {
      Source = CarApplication.Instance.PreviousAudioSource();
    }
    private void btnNextSource_MouseUp(object sender, MouseEventArgs e)
    {
      Source = CarApplication.Instance.NextAudioSource();
    }
    private void frmAudioSettings_Load(object sender, EventArgs e)
    {
      Content = CarApplication.Instance.AudioContent;
      Source = CarApplication.Instance.AudioSource;
      FmChannel = CarApplication.Instance.FmTransmitterChannel;
    }
    private void btnEdit_Click(object sender, EventArgs e)
    {
      Int16 NewFmChannel;
      DialogResult Result = frmEnterFmFrequency.EnterValues(out NewFmChannel);
      if (Result == System.Windows.Forms.DialogResult.Cancel)
        return;

      FmChannel = NewFmChannel;
    }

    private AudioSettings.Content content = AudioSettings.Content.NavigationAndCommentary;
    private AudioSettings.Source source = AudioSettings.Source.SpeakerOnly;
    private Int16 fmChannel = 0;

  }
}