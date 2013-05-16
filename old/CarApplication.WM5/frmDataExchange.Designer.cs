namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmDataExchange
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
      this.btnLoadFormDefinitions = new System.Windows.Forms.Button();
      this.btnLoadContent = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.btnLoadItinerary = new System.Windows.Forms.Button();
      this.edtItineraryFile = new System.Windows.Forms.TextBox();
      this.edtContentFile = new System.Windows.Forms.TextBox();
      this.edtFormFile = new System.Windows.Forms.TextBox();
      this.edtDestinationFile = new System.Windows.Forms.TextBox();
      this.btnLoadDestinations = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnLoadFormDefinitions
      // 
      this.btnLoadFormDefinitions.Location = new System.Drawing.Point(3, 49);
      this.btnLoadFormDefinitions.Name = "btnLoadFormDefinitions";
      this.btnLoadFormDefinitions.Size = new System.Drawing.Size(149, 40);
      this.btnLoadFormDefinitions.TabIndex = 2;
      this.btnLoadFormDefinitions.Text = "Load Form Definitions";
      this.btnLoadFormDefinitions.Click += new System.EventHandler(this.btnLoadFormDefinitions_Click);
      // 
      // btnLoadContent
      // 
      this.btnLoadContent.Location = new System.Drawing.Point(3, 95);
      this.btnLoadContent.Name = "btnLoadContent";
      this.btnLoadContent.Size = new System.Drawing.Size(149, 40);
      this.btnLoadContent.TabIndex = 3;
      this.btnLoadContent.Text = "Load Content";
      this.btnLoadContent.Click += new System.EventHandler(this.btnLoadContent_Click);
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
      // btnLoadItinerary
      // 
      this.btnLoadItinerary.Location = new System.Drawing.Point(3, 3);
      this.btnLoadItinerary.Name = "btnLoadItinerary";
      this.btnLoadItinerary.Size = new System.Drawing.Size(149, 40);
      this.btnLoadItinerary.TabIndex = 11;
      this.btnLoadItinerary.Text = "Load Itinerary";
      this.btnLoadItinerary.Click += new System.EventHandler(this.btnLoadItinerary_Click);
      // 
      // edtItineraryFile
      // 
      this.edtItineraryFile.Location = new System.Drawing.Point(158, 12);
      this.edtItineraryFile.Name = "edtItineraryFile";
      this.edtItineraryFile.Size = new System.Drawing.Size(260, 23);
      this.edtItineraryFile.TabIndex = 12;
      // 
      // edtContentFile
      // 
      this.edtContentFile.Location = new System.Drawing.Point(158, 104);
      this.edtContentFile.Name = "edtContentFile";
      this.edtContentFile.Size = new System.Drawing.Size(260, 23);
      this.edtContentFile.TabIndex = 13;
      // 
      // edtFormFile
      // 
      this.edtFormFile.Location = new System.Drawing.Point(158, 58);
      this.edtFormFile.Name = "edtFormFile";
      this.edtFormFile.Size = new System.Drawing.Size(260, 23);
      this.edtFormFile.TabIndex = 14;
      // 
      // edtDestinationFile
      // 
      this.edtDestinationFile.Location = new System.Drawing.Point(158, 150);
      this.edtDestinationFile.Name = "edtDestinationFile";
      this.edtDestinationFile.Size = new System.Drawing.Size(260, 23);
      this.edtDestinationFile.TabIndex = 16;
      // 
      // btnLoadDestinations
      // 
      this.btnLoadDestinations.Location = new System.Drawing.Point(3, 141);
      this.btnLoadDestinations.Name = "btnLoadDestinations";
      this.btnLoadDestinations.Size = new System.Drawing.Size(149, 40);
      this.btnLoadDestinations.TabIndex = 15;
      this.btnLoadDestinations.Text = "Load Destinations";
      this.btnLoadDestinations.Click += new System.EventHandler(this.btnLoadDestinations_Click);
      // 
      // frmDataExchange
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.edtDestinationFile);
      this.Controls.Add(this.btnLoadDestinations);
      this.Controls.Add(this.edtFormFile);
      this.Controls.Add(this.edtContentFile);
      this.Controls.Add(this.edtItineraryFile);
      this.Controls.Add(this.btnLoadItinerary);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnLoadContent);
      this.Controls.Add(this.btnLoadFormDefinitions);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmDataExchange";
      this.Text = "Factory Setup";
      this.Activated += new System.EventHandler(this.frmDataExchange_Activated);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnLoadFormDefinitions;
    private System.Windows.Forms.Button btnLoadContent;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnLoadItinerary;
    private System.Windows.Forms.TextBox edtItineraryFile;
    private System.Windows.Forms.TextBox edtContentFile;
    private System.Windows.Forms.TextBox edtFormFile;
    private System.Windows.Forms.TextBox edtDestinationFile;
    private System.Windows.Forms.Button btnLoadDestinations;
  }
}