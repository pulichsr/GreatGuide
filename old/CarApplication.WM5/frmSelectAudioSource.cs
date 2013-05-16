using System.Windows.Forms;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectAudioSource : frmCarApplicationBase
  {
    public static DialogResult Select(out AudioSettings.Source audioSource)
    {
      frmSelectAudioSource frm = new frmSelectAudioSource();

      DialogResult Result = frm.ShowDialog();
      audioSource = frm.AudioSource;
      frm.Dispose();

      return Result;
    }

    private frmSelectAudioSource()
    {
      InitializeComponent();

      BtnSoftKey1.Visible = false;
      BtnSoftKey2.Visible = false;
      BtnSoftKey3.Visible = false;
      BtnSoftKey4.Visible = false;
      BtnSoftKey5.Visible = false;

      btn1.Text = AudioSettings.ToString(AudioSettings.Source.RadioOnly);
      btn2.Text = AudioSettings.ToString(AudioSettings.Source.SpeakerOnly);

      btn1.Visible = true;
      btn2.Visible = true;

      btn1.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnGreenMenuItem");
      btn2.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnGreenMenuItem");

      BtnBack.Click += (BtnBack_Click);

      this.HeadingText = "Audio Source";
    }

    public AudioSettings.Source AudioSource
    {
      get { return audioSource; }
    }

    private void BtnBack_Click(object sender, System.EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    private void btn1_Click(object sender, System.EventArgs e)
    {
      audioSource = AudioSettings.Source.RadioOnly;
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
    private void btn2_Click(object sender, System.EventArgs e)
    {
      audioSource = AudioSettings.Source.SpeakerOnly;
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }

    private AudioSettings.Source audioSource = AudioSettings.Source.SpeakerOnly;
  }
}