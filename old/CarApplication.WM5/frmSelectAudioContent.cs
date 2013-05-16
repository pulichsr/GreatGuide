using System.Windows.Forms;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectAudioContent : frmCarApplicationBase
  {
    public static DialogResult Select(out AudioSettings.Content audioContent)
    {
      frmSelectAudioContent frm = new frmSelectAudioContent();

      DialogResult Result = frm.ShowDialog();
      audioContent = frm.AudioContent;
      frm.Dispose();

      return Result;
    }

    private frmSelectAudioContent()
    {
      InitializeComponent();

      BtnSoftKey1.Visible = false;
      BtnSoftKey2.Visible = false;
      BtnSoftKey3.Visible = false;
      BtnSoftKey4.Visible = false;
      BtnSoftKey5.Visible = false;

      btn1.Text = AudioSettings.ToString(AudioSettings.Content.CommentaryOnly);
      btn2.Text = AudioSettings.ToString(AudioSettings.Content.NavigationAndCommentary);
      btn3.Text = AudioSettings.ToString(AudioSettings.Content.NavigationOnly);
      btn4.Text = AudioSettings.ToString(AudioSettings.Content.None);

      btn1.Visible = true;
      btn2.Visible = true;
      btn3.Visible = true;
      btn4.Visible = true;

      btn1.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnGreenMenuItem");
      btn2.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnGreenMenuItem");
      btn3.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnGreenMenuItem");
      btn4.ActiveBackgroundImage = CarApplication.Instance.ImageManager.GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnGreenMenuItem");

      BtnBack.Click += (BtnBack_Click);

      this.HeadingText = "Audio Content";
    }

    public AudioSettings.Content AudioContent
    {
      get { return audioContent; }
    }

    private void BtnBack_Click(object sender, System.EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    private void btn1_Click(object sender, System.EventArgs e)
    {
      audioContent = AudioSettings.Content.CommentaryOnly;
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
    private void btn2_Click(object sender, System.EventArgs e)
    {
      audioContent = AudioSettings.Content.NavigationAndCommentary;
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
    private void btn3_Click(object sender, System.EventArgs e)
    {
      audioContent = AudioSettings.Content.NavigationOnly;
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
    private void btn4_Click(object sender, System.EventArgs e)
    {
      audioContent = AudioSettings.Content.None;
      DialogResult = System.Windows.Forms.DialogResult.OK;
    }

    private AudioSettings.Content audioContent = AudioSettings.Content.NavigationAndCommentary;
  }
}