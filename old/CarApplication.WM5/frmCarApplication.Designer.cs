namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmCarApplication
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
      this.map = new Telogis.GeoBase.MapCtrl();
      this.lblGpsFixState = new System.Windows.Forms.Label();
      this.edtDestination = new System.Windows.Forms.Label();
      this.pbHeading = new System.Windows.Forms.PictureBox();
      this.btnPower = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnRepeatAudio = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnItinerary = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnMenu = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnPauseAudio = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnPauseRouter = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnPlayAudio = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnResumeRouter = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.SuspendLayout();
      // 
      // map
      // 
      this.map.ForeColor = System.Drawing.Color.White;
      this.map.Location = new System.Drawing.Point(0, 23);
      this.map.Name = "map";
      this.map.Size = new System.Drawing.Size(401, 249);
      this.map.TabIndex = 14;
      // 
      // lblGpsFixState
      // 
      this.lblGpsFixState.BackColor = System.Drawing.Color.White;
      this.lblGpsFixState.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
      this.lblGpsFixState.ForeColor = System.Drawing.Color.White;
      this.lblGpsFixState.Location = new System.Drawing.Point(361, 0);
      this.lblGpsFixState.Name = "lblGpsFixState";
      this.lblGpsFixState.Size = new System.Drawing.Size(40, 22);
      this.lblGpsFixState.Text = "GPS";
      this.lblGpsFixState.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // edtDestination
      // 
      this.edtDestination.BackColor = System.Drawing.Color.White;
      this.edtDestination.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
      this.edtDestination.ForeColor = System.Drawing.Color.White;
      this.edtDestination.Location = new System.Drawing.Point(0, 0);
      this.edtDestination.Name = "edtDestination";
      this.edtDestination.Size = new System.Drawing.Size(338, 22);
      // 
      // pbHeading
      // 
      this.pbHeading.BackColor = System.Drawing.Color.White;
      this.pbHeading.Location = new System.Drawing.Point(339, 0);
      this.pbHeading.Name = "pbHeading";
      this.pbHeading.Size = new System.Drawing.Size(22, 22);
      // 
      // btnPower
      // 
      this.btnPower.BackColor = System.Drawing.SystemColors.Window;
      this.btnPower.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnPower.ForeColor = System.Drawing.Color.White;
      this.btnPower.LineSeparation = ((short)(1));
      this.btnPower.Location = new System.Drawing.Point(402, 2);
      this.btnPower.Name = "btnPower";
      this.btnPower.Size = new System.Drawing.Size(74, 43);
      this.btnPower.TabIndex = 9;
      this.btnPower.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnPower.Click += new System.EventHandler(this.btnPower_Click);
      // 
      // btnRepeatAudio
      // 
      this.btnRepeatAudio.BackColor = System.Drawing.Color.White;
      this.btnRepeatAudio.Enabled = false;
      this.btnRepeatAudio.ForeColor = System.Drawing.Color.White;
      this.btnRepeatAudio.LineSeparation = ((short)(1));
      this.btnRepeatAudio.Location = new System.Drawing.Point(402, 93);
      this.btnRepeatAudio.Name = "btnRepeatAudio";
      this.btnRepeatAudio.Size = new System.Drawing.Size(74, 43);
      this.btnRepeatAudio.TabIndex = 8;
      this.btnRepeatAudio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnRepeatAudio.Click += new System.EventHandler(this.btnRepeatAudio_Click);
      // 
      // btnItinerary
      // 
      this.btnItinerary.BackColor = System.Drawing.Color.White;
      this.btnItinerary.ForeColor = System.Drawing.Color.White;
      this.btnItinerary.LineSeparation = ((short)(1));
      this.btnItinerary.Location = new System.Drawing.Point(402, 228);
      this.btnItinerary.Name = "btnItinerary";
      this.btnItinerary.Size = new System.Drawing.Size(74, 43);
      this.btnItinerary.TabIndex = 6;
      this.btnItinerary.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnItinerary.Click += new System.EventHandler(this.btnItinerary_Click);
      // 
      // btnMenu
      // 
      this.btnMenu.BackColor = System.Drawing.Color.White;
      this.btnMenu.ForeColor = System.Drawing.Color.White;
      this.btnMenu.LineSeparation = ((short)(1));
      this.btnMenu.Location = new System.Drawing.Point(402, 183);
      this.btnMenu.Name = "btnMenu";
      this.btnMenu.Size = new System.Drawing.Size(74, 43);
      this.btnMenu.TabIndex = 4;
      this.btnMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
      // 
      // btnPauseAudio
      // 
      this.btnPauseAudio.BackColor = System.Drawing.Color.White;
      this.btnPauseAudio.Enabled = false;
      this.btnPauseAudio.ForeColor = System.Drawing.Color.White;
      this.btnPauseAudio.LineSeparation = ((short)(1));
      this.btnPauseAudio.Location = new System.Drawing.Point(402, 48);
      this.btnPauseAudio.Name = "btnPauseAudio";
      this.btnPauseAudio.Size = new System.Drawing.Size(74, 43);
      this.btnPauseAudio.TabIndex = 10;
      this.btnPauseAudio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnPauseAudio.Click += new System.EventHandler(this.btnPauseAudio_Click);
      // 
      // btnPauseRouter
      // 
      this.btnPauseRouter.BackColor = System.Drawing.Color.White;
      this.btnPauseRouter.Enabled = false;
      this.btnPauseRouter.ForeColor = System.Drawing.Color.White;
      this.btnPauseRouter.LineSeparation = ((short)(1));
      this.btnPauseRouter.Location = new System.Drawing.Point(402, 138);
      this.btnPauseRouter.Name = "btnPauseRouter";
      this.btnPauseRouter.Size = new System.Drawing.Size(74, 43);
      this.btnPauseRouter.TabIndex = 5;
      this.btnPauseRouter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnPauseRouter.Click += new System.EventHandler(this.btnPauseRouter_Click);
      // 
      // btnPlayAudio
      // 
      this.btnPlayAudio.BackColor = System.Drawing.Color.White;
      this.btnPlayAudio.Enabled = false;
      this.btnPlayAudio.ForeColor = System.Drawing.Color.White;
      this.btnPlayAudio.LineSeparation = ((short)(1));
      this.btnPlayAudio.Location = new System.Drawing.Point(402, 48);
      this.btnPlayAudio.Name = "btnPlayAudio";
      this.btnPlayAudio.Size = new System.Drawing.Size(74, 43);
      this.btnPlayAudio.TabIndex = 13;
      this.btnPlayAudio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnPlayAudio.Visible = false;
      this.btnPlayAudio.Click += new System.EventHandler(this.btnPlayAudio_Click);
      // 
      // btnResumeRouter
      // 
      this.btnResumeRouter.BackColor = System.Drawing.Color.White;
      this.btnResumeRouter.ForeColor = System.Drawing.Color.White;
      this.btnResumeRouter.LineSeparation = ((short)(1));
      this.btnResumeRouter.Location = new System.Drawing.Point(402, 138);
      this.btnResumeRouter.Name = "btnResumeRouter";
      this.btnResumeRouter.Size = new System.Drawing.Size(74, 43);
      this.btnResumeRouter.TabIndex = 12;
      this.btnResumeRouter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnResumeRouter.Visible = false;
      this.btnResumeRouter.Click += new System.EventHandler(this.btnResumeRouter_Click);
      // 
      // frmCarApplication
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.pbHeading);
      this.Controls.Add(this.edtDestination);
      this.Controls.Add(this.lblGpsFixState);
      this.Controls.Add(this.map);
      this.Controls.Add(this.btnPower);
      this.Controls.Add(this.btnRepeatAudio);
      this.Controls.Add(this.btnItinerary);
      this.Controls.Add(this.btnMenu);
      this.Controls.Add(this.btnPauseAudio);
      this.Controls.Add(this.btnPauseRouter);
      this.Controls.Add(this.btnPlayAudio);
      this.Controls.Add(this.btnResumeRouter);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmCarApplication";
      this.Text = "Car Application";
      this.Load += new System.EventHandler(this.frmCarApplication_Load);
      this.Activated += new System.EventHandler(this.frmCarApplication_Activated);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmCarApplication_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.MultilineTextButton btnMenu;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnPauseRouter;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnItinerary;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnRepeatAudio;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnPower;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnPauseAudio;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnResumeRouter;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnPlayAudio;
    private System.Windows.Forms.Label lblGpsFixState;
    private System.Windows.Forms.Label edtDestination;
    private System.Windows.Forms.PictureBox pbHeading;
    public Telogis.GeoBase.MapCtrl map;

  }
}

