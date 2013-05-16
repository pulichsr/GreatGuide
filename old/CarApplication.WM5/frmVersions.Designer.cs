namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmVersions
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
      this.btnClose = new System.Windows.Forms.Button();
      this.edtItineraryFile = new System.Windows.Forms.TextBox();
      this.edtContentFile = new System.Windows.Forms.TextBox();
      this.edtFormFile = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.edtFirmware = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.edtDestinationFile = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnClose
      // 
      this.btnClose.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnClose.Location = new System.Drawing.Point(437, 3);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(40, 30);
      this.btnClose.TabIndex = 9;
      this.btnClose.Text = "X";
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // edtItineraryFile
      // 
      this.edtItineraryFile.Location = new System.Drawing.Point(100, 39);
      this.edtItineraryFile.Name = "edtItineraryFile";
      this.edtItineraryFile.Size = new System.Drawing.Size(260, 23);
      this.edtItineraryFile.TabIndex = 12;
      // 
      // edtContentFile
      // 
      this.edtContentFile.Location = new System.Drawing.Point(100, 97);
      this.edtContentFile.Name = "edtContentFile";
      this.edtContentFile.Size = new System.Drawing.Size(260, 23);
      this.edtContentFile.TabIndex = 13;
      // 
      // edtFormFile
      // 
      this.edtFormFile.Location = new System.Drawing.Point(100, 68);
      this.edtFormFile.Name = "edtFormFile";
      this.edtFormFile.Size = new System.Drawing.Size(260, 23);
      this.edtFormFile.TabIndex = 14;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(7, 98);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(87, 20);
      this.label1.Text = "Content file";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(7, 69);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(66, 20);
      this.label2.Text = "Form file";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(7, 40);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(63, 20);
      this.label3.Text = "Itinerary";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(7, 11);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(63, 20);
      this.label4.Text = "Firmware";
      // 
      // edtFirmware
      // 
      this.edtFirmware.Location = new System.Drawing.Point(100, 10);
      this.edtFirmware.Name = "edtFirmware";
      this.edtFirmware.Size = new System.Drawing.Size(260, 23);
      this.edtFirmware.TabIndex = 21;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(7, 127);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(97, 20);
      this.label5.Text = "Destination file";
      // 
      // edtDestinationFile
      // 
      this.edtDestinationFile.Location = new System.Drawing.Point(100, 126);
      this.edtDestinationFile.Name = "edtDestinationFile";
      this.edtDestinationFile.Size = new System.Drawing.Size(260, 23);
      this.edtDestinationFile.TabIndex = 26;
      // 
      // frmVersions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.edtDestinationFile);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.edtFirmware);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.edtFormFile);
      this.Controls.Add(this.edtContentFile);
      this.Controls.Add(this.edtItineraryFile);
      this.Controls.Add(this.btnClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmVersions";
      this.Text = "Factory Setup";
      this.Activated += new System.EventHandler(this.frmVersions_Activated);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.TextBox edtItineraryFile;
    private System.Windows.Forms.TextBox edtContentFile;
    private System.Windows.Forms.TextBox edtFormFile;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox edtFirmware;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox edtDestinationFile;
  }
}