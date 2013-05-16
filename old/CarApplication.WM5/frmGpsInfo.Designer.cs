namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmGpsInfo
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
      this.lblGga = new System.Windows.Forms.Label();
      this.lblVtg = new System.Windows.Forms.Label();
      this.lblRmc = new System.Windows.Forms.Label();
      this.lblLatitude = new System.Windows.Forms.Label();
      this.lblLongtiude = new System.Windows.Forms.Label();
      this.lblValidFix = new System.Windows.Forms.Label();
      this.lblSpeed = new System.Windows.Forms.Label();
      this.lblHeading = new System.Windows.Forms.Label();
      this.lblTtff = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblGga
      // 
      this.lblGga.Location = new System.Drawing.Point(4, 4);
      this.lblGga.Name = "lblGga";
      this.lblGga.Size = new System.Drawing.Size(471, 20);
      this.lblGga.Text = "gga";
      // 
      // lblVtg
      // 
      this.lblVtg.Location = new System.Drawing.Point(3, 44);
      this.lblVtg.Name = "lblVtg";
      this.lblVtg.Size = new System.Drawing.Size(471, 20);
      this.lblVtg.Text = "vtg";
      // 
      // lblRmc
      // 
      this.lblRmc.Location = new System.Drawing.Point(3, 24);
      this.lblRmc.Name = "lblRmc";
      this.lblRmc.Size = new System.Drawing.Size(471, 20);
      this.lblRmc.Text = "rmc";
      // 
      // lblLatitude
      // 
      this.lblLatitude.Location = new System.Drawing.Point(3, 74);
      this.lblLatitude.Name = "lblLatitude";
      this.lblLatitude.Size = new System.Drawing.Size(249, 20);
      this.lblLatitude.Text = "Latitude";
      // 
      // lblLongtiude
      // 
      this.lblLongtiude.Location = new System.Drawing.Point(4, 94);
      this.lblLongtiude.Name = "lblLongtiude";
      this.lblLongtiude.Size = new System.Drawing.Size(248, 20);
      this.lblLongtiude.Text = "Longitude";
      // 
      // lblValidFix
      // 
      this.lblValidFix.Location = new System.Drawing.Point(3, 114);
      this.lblValidFix.Name = "lblValidFix";
      this.lblValidFix.Size = new System.Drawing.Size(153, 20);
      this.lblValidFix.Text = "ValidFix";
      // 
      // lblSpeed
      // 
      this.lblSpeed.Location = new System.Drawing.Point(3, 134);
      this.lblSpeed.Name = "lblSpeed";
      this.lblSpeed.Size = new System.Drawing.Size(153, 20);
      this.lblSpeed.Text = "Speed";
      // 
      // lblHeading
      // 
      this.lblHeading.Location = new System.Drawing.Point(3, 154);
      this.lblHeading.Name = "lblHeading";
      this.lblHeading.Size = new System.Drawing.Size(153, 20);
      this.lblHeading.Text = "Heading";
      // 
      // lblTtff
      // 
      this.lblTtff.Location = new System.Drawing.Point(4, 174);
      this.lblTtff.Name = "lblTtff";
      this.lblTtff.Size = new System.Drawing.Size(153, 20);
      this.lblTtff.Text = "TTFF";
      // 
      // frmGpsInfo
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(478, 247);
      this.Controls.Add(this.lblTtff);
      this.Controls.Add(this.lblHeading);
      this.Controls.Add(this.lblSpeed);
      this.Controls.Add(this.lblValidFix);
      this.Controls.Add(this.lblLongtiude);
      this.Controls.Add(this.lblLatitude);
      this.Controls.Add(this.lblRmc);
      this.Controls.Add(this.lblVtg);
      this.Controls.Add(this.lblGga);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmGpsInfo";
      this.Text = "GPS Info";
      this.Load += new System.EventHandler(this.frmGpsInfo_Load);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmGpsInfo_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblGga;
    private System.Windows.Forms.Label lblVtg;
    private System.Windows.Forms.Label lblRmc;
    private System.Windows.Forms.Label lblLatitude;
    private System.Windows.Forms.Label lblLongtiude;
    private System.Windows.Forms.Label lblValidFix;
    private System.Windows.Forms.Label lblSpeed;
    private System.Windows.Forms.Label lblHeading;
    private System.Windows.Forms.Label lblTtff;
  }
}