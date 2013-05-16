namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmTripRecorder
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
      this.edtTime = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.edtHeading = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.edtSpeed = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.edtLongitude = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.edtLatitude = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.edtTripName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnPause = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnPlay = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnRecord = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnSoftKey5 = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnStop = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnTripName = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnBack = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.SuspendLayout();
      // 
      // edtTime
      // 
      this.edtTime.Location = new System.Drawing.Point(171, 68);
      this.edtTime.Name = "edtTime";
      this.edtTime.ReadOnly = true;
      this.edtTime.Size = new System.Drawing.Size(152, 23);
      this.edtTime.TabIndex = 39;
      this.edtTime.TabStop = false;
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(90, 69);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(39, 20);
      this.label6.Text = "Time";
      // 
      // edtHeading
      // 
      this.edtHeading.Location = new System.Drawing.Point(171, 188);
      this.edtHeading.Name = "edtHeading";
      this.edtHeading.ReadOnly = true;
      this.edtHeading.Size = new System.Drawing.Size(60, 23);
      this.edtHeading.TabIndex = 38;
      this.edtHeading.TabStop = false;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(90, 189);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(55, 20);
      this.label5.Text = "Heading";
      // 
      // edtSpeed
      // 
      this.edtSpeed.Location = new System.Drawing.Point(171, 158);
      this.edtSpeed.Name = "edtSpeed";
      this.edtSpeed.ReadOnly = true;
      this.edtSpeed.Size = new System.Drawing.Size(60, 23);
      this.edtSpeed.TabIndex = 37;
      this.edtSpeed.TabStop = false;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(90, 159);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(55, 20);
      this.label4.Text = "Speed";
      // 
      // edtLongitude
      // 
      this.edtLongitude.Location = new System.Drawing.Point(171, 128);
      this.edtLongitude.Name = "edtLongitude";
      this.edtLongitude.ReadOnly = true;
      this.edtLongitude.Size = new System.Drawing.Size(103, 23);
      this.edtLongitude.TabIndex = 36;
      this.edtLongitude.TabStop = false;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(90, 129);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(39, 20);
      this.label3.Text = "Long";
      // 
      // edtLatitude
      // 
      this.edtLatitude.Location = new System.Drawing.Point(171, 98);
      this.edtLatitude.Name = "edtLatitude";
      this.edtLatitude.ReadOnly = true;
      this.edtLatitude.Size = new System.Drawing.Size(103, 23);
      this.edtLatitude.TabIndex = 35;
      this.edtLatitude.TabStop = false;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(90, 99);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(39, 20);
      this.label2.Text = "Lat";
      // 
      // edtTripName
      // 
      this.edtTripName.Location = new System.Drawing.Point(171, 38);
      this.edtTripName.Name = "edtTripName";
      this.edtTripName.ReadOnly = true;
      this.edtTripName.Size = new System.Drawing.Size(152, 23);
      this.edtTripName.TabIndex = 34;
      this.edtTripName.TabStop = false;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(90, 39);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(76, 20);
      this.label1.Text = "Trip name";
      // 
      // btnPause
      // 
      this.btnPause.BackColor = System.Drawing.SystemColors.Window;
      this.btnPause.Enabled = false;
      this.btnPause.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnPause.ForeColor = System.Drawing.Color.White;
      this.btnPause.LineSeparation = ((short)(0));
      this.btnPause.Location = new System.Drawing.Point(292, 238);
      this.btnPause.Name = "btnPause";
      this.btnPause.Size = new System.Drawing.Size(82, 29);
      this.btnPause.TabIndex = 49;
      this.btnPause.Text = "PAUSE";
      this.btnPause.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
      // 
      // btnPlay
      // 
      this.btnPlay.BackColor = System.Drawing.SystemColors.Window;
      this.btnPlay.Enabled = false;
      this.btnPlay.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnPlay.ForeColor = System.Drawing.Color.White;
      this.btnPlay.LineSeparation = ((short)(0));
      this.btnPlay.Location = new System.Drawing.Point(199, 238);
      this.btnPlay.Name = "btnPlay";
      this.btnPlay.Size = new System.Drawing.Size(82, 29);
      this.btnPlay.TabIndex = 50;
      this.btnPlay.Text = "PLAY";
      this.btnPlay.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
      // 
      // btnRecord
      // 
      this.btnRecord.BackColor = System.Drawing.SystemColors.Window;
      this.btnRecord.Enabled = false;
      this.btnRecord.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnRecord.ForeColor = System.Drawing.Color.White;
      this.btnRecord.LineSeparation = ((short)(0));
      this.btnRecord.Location = new System.Drawing.Point(106, 238);
      this.btnRecord.Name = "btnRecord";
      this.btnRecord.Size = new System.Drawing.Size(82, 29);
      this.btnRecord.TabIndex = 51;
      this.btnRecord.Text = "RECORD";
      this.btnRecord.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
      // 
      // btnSoftKey5
      // 
      this.btnSoftKey5.BackColor = System.Drawing.SystemColors.Window;
      this.btnSoftKey5.Enabled = false;
      this.btnSoftKey5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnSoftKey5.ForeColor = System.Drawing.Color.White;
      this.btnSoftKey5.LineSeparation = ((short)(0));
      this.btnSoftKey5.Location = new System.Drawing.Point(385, 238);
      this.btnSoftKey5.Name = "btnSoftKey5";
      this.btnSoftKey5.Size = new System.Drawing.Size(82, 29);
      this.btnSoftKey5.TabIndex = 52;
      this.btnSoftKey5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnSoftKey5.Visible = false;
      // 
      // btnStop
      // 
      this.btnStop.BackColor = System.Drawing.SystemColors.Window;
      this.btnStop.Enabled = false;
      this.btnStop.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnStop.ForeColor = System.Drawing.Color.White;
      this.btnStop.LineSeparation = ((short)(0));
      this.btnStop.Location = new System.Drawing.Point(13, 238);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(82, 29);
      this.btnStop.TabIndex = 53;
      this.btnStop.Text = "STOP";
      this.btnStop.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
      // 
      // btnTripName
      // 
      this.btnTripName.BackColor = System.Drawing.SystemColors.Window;
      this.btnTripName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnTripName.ForeColor = System.Drawing.Color.White;
      this.btnTripName.LineSeparation = ((short)(0));
      this.btnTripName.Location = new System.Drawing.Point(329, 34);
      this.btnTripName.Name = "btnTripName";
      this.btnTripName.Size = new System.Drawing.Size(82, 30);
      this.btnTripName.TabIndex = 54;
      this.btnTripName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnTripName.Click += new System.EventHandler(this.btnTripName_Click);
      // 
      // btnBack
      // 
      this.btnBack.BackColor = System.Drawing.SystemColors.Window;
      this.btnBack.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnBack.ForeColor = System.Drawing.Color.White;
      this.btnBack.LineSeparation = ((short)(1));
      this.btnBack.Location = new System.Drawing.Point(4, 2);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(59, 42);
      this.btnBack.TabIndex = 61;
      this.btnBack.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // frmTripRecorder
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.btnTripName);
      this.Controls.Add(this.btnPause);
      this.Controls.Add(this.btnPlay);
      this.Controls.Add(this.btnRecord);
      this.Controls.Add(this.btnSoftKey5);
      this.Controls.Add(this.btnStop);
      this.Controls.Add(this.edtTime);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.edtHeading);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.edtSpeed);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.edtLongitude);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.edtLatitude);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.edtTripName);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmTripRecorder";
      this.Text = "Car Application";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmTripRecorder_Closing);
      this.Load += new System.EventHandler(this.frmTripRecorder_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox edtTime;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox edtHeading;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox edtSpeed;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox edtLongitude;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox edtLatitude;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox edtTripName;
    private System.Windows.Forms.Label label1;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnPause;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnPlay;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnRecord;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnSoftKey5;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnStop;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnTripName;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnBack;


  }
}