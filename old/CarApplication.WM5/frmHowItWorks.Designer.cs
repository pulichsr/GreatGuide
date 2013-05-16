namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmHowItWorks
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnContinue = new System.Windows.Forms.PictureBox();
      this.lblPlay = new System.Windows.Forms.Label();
      this.lblPause = new System.Windows.Forms.Label();
      this.lblToggle = new System.Windows.Forms.Label();
      this.btnPause = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnPlay = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.SuspendLayout();
      // 
      // btnContinue
      // 
      this.btnContinue.Location = new System.Drawing.Point(385, 229);
      this.btnContinue.Name = "btnContinue";
      this.btnContinue.Size = new System.Drawing.Size(92, 40);
      this.btnContinue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
      // 
      // lblPlay
      // 
      this.lblPlay.Location = new System.Drawing.Point(3, 205);
      this.lblPlay.Name = "lblPlay";
      this.lblPlay.Size = new System.Drawing.Size(81, 62);
      this.lblPlay.Text = "Press Play to \r\nrestart the video";
      this.lblPlay.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // lblPause
      // 
      this.lblPause.Location = new System.Drawing.Point(218, 205);
      this.lblPause.Name = "lblPause";
      this.lblPause.Size = new System.Drawing.Size(61, 62);
      this.lblPause.Text = "Press Pause to \r\npause or resume";
      this.lblPause.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.lblPause.Visible = false;
      // 
      // lblToggle
      // 
      this.lblToggle.Location = new System.Drawing.Point(378, 4);
      this.lblToggle.Name = "lblToggle";
      this.lblToggle.Size = new System.Drawing.Size(98, 88);
      this.lblToggle.Text = "Press the video\r\nto toggle between small size and full screen.";
      // 
      // btnPause
      // 
      this.btnPause.BackColor = System.Drawing.Color.White;
      this.btnPause.ForeColor = System.Drawing.Color.White;
      this.btnPause.LineSeparation = ((short)(1));
      this.btnPause.Location = new System.Drawing.Point(285, 205);
      this.btnPause.Name = "btnPause";
      this.btnPause.Size = new System.Drawing.Size(74, 43);
      this.btnPause.TabIndex = 12;
      this.btnPause.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnPause.Visible = false;
      this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
      // 
      // btnPlay
      // 
      this.btnPlay.BackColor = System.Drawing.Color.White;
      this.btnPlay.ForeColor = System.Drawing.Color.White;
      this.btnPlay.LineSeparation = ((short)(1));
      this.btnPlay.Location = new System.Drawing.Point(90, 205);
      this.btnPlay.Name = "btnPlay";
      this.btnPlay.Size = new System.Drawing.Size(74, 43);
      this.btnPlay.TabIndex = 11;
      this.btnPlay.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
      // 
      // frmHowItWorks
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblToggle);
      this.Controls.Add(this.lblPause);
      this.Controls.Add(this.lblPlay);
      this.Controls.Add(this.btnPause);
      this.Controls.Add(this.btnPlay);
      this.Controls.Add(this.btnContinue);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmHowItWorks";
      this.Text = "frmHowItWorks";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.frmHowItWorks_Load);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmHowItWorks_MouseUp);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox btnContinue;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnPlay;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnPause;
    private System.Windows.Forms.Label lblPlay;
    private System.Windows.Forms.Label lblPause;
    private System.Windows.Forms.Label lblToggle;
  }
}