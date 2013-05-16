namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmSearch
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
      this.btnBack = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.lblMainHeading = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnByRegion = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.btnByStreet = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.transparentLabel1 = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.lblAddress = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.btnSearchGpsLocation = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.btnSearchDestinations = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
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
      this.btnBack.TabIndex = 1;
      this.btnBack.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // lblMainHeading
      // 
      this.lblMainHeading.BackColor = System.Drawing.Color.White;
      this.lblMainHeading.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
      this.lblMainHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblMainHeading.Location = new System.Drawing.Point(71, 0);
      this.lblMainHeading.Name = "lblMainHeading";
      this.lblMainHeading.Size = new System.Drawing.Size(338, 23);
      this.lblMainHeading.TabIndex = 8;
      this.lblMainHeading.Text = "Search";
      this.lblMainHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(210)))));
      this.panel1.Controls.Add(this.btnByRegion);
      this.panel1.Controls.Add(this.btnByStreet);
      this.panel1.Controls.Add(this.transparentLabel1);
      this.panel1.Controls.Add(this.lblAddress);
      this.panel1.Location = new System.Drawing.Point(4, 49);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(474, 125);
      // 
      // btnByRegion
      // 
      this.btnByRegion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.btnByRegion.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.btnByRegion.ForeColor = System.Drawing.Color.White;
      this.btnByRegion.Location = new System.Drawing.Point(3, 90);
      this.btnByRegion.Name = "btnByRegion";
      this.btnByRegion.Size = new System.Drawing.Size(182, 30);
      this.btnByRegion.TabIndex = 83;
      this.btnByRegion.Text = "Search By Region";
      this.btnByRegion.Click += new System.EventHandler(this.btnByRegion_Click);
      // 
      // btnByStreet
      // 
      this.btnByStreet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.btnByStreet.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.btnByStreet.ForeColor = System.Drawing.Color.White;
      this.btnByStreet.Location = new System.Drawing.Point(3, 55);
      this.btnByStreet.Name = "btnByStreet";
      this.btnByStreet.Size = new System.Drawing.Size(182, 30);
      this.btnByStreet.TabIndex = 82;
      this.btnByStreet.Text = "Search By Street";
      this.btnByStreet.Click += new System.EventHandler(this.btnByStreet_Click);
      // 
      // transparentLabel1
      // 
      this.transparentLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(210)))));
      this.transparentLabel1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
      this.transparentLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.transparentLabel1.Location = new System.Drawing.Point(4, 4);
      this.transparentLabel1.Name = "transparentLabel1";
      this.transparentLabel1.Size = new System.Drawing.Size(351, 23);
      this.transparentLabel1.TabIndex = 17;
      this.transparentLabel1.Text = "You are now in:";
      this.transparentLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      // 
      // lblAddress
      // 
      this.lblAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(236)))), ((int)(((byte)(210)))));
      this.lblAddress.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
      this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblAddress.Location = new System.Drawing.Point(4, 27);
      this.lblAddress.Name = "lblAddress";
      this.lblAddress.Size = new System.Drawing.Size(391, 23);
      this.lblAddress.TabIndex = 16;
      this.lblAddress.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      // 
      // btnSearchGpsLocation
      // 
      this.btnSearchGpsLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.btnSearchGpsLocation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.btnSearchGpsLocation.ForeColor = System.Drawing.Color.White;
      this.btnSearchGpsLocation.Location = new System.Drawing.Point(5, 225);
      this.btnSearchGpsLocation.Name = "btnSearchGpsLocation";
      this.btnSearchGpsLocation.Size = new System.Drawing.Size(215, 30);
      this.btnSearchGpsLocation.TabIndex = 84;
      this.btnSearchGpsLocation.Text = "Search for GPS Location";
      this.btnSearchGpsLocation.Click += new System.EventHandler(this.btnSearchGpsLocation_Click);
      // 
      // btnSearchDestinations
      // 
      this.btnSearchDestinations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.btnSearchDestinations.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.btnSearchDestinations.ForeColor = System.Drawing.Color.White;
      this.btnSearchDestinations.Location = new System.Drawing.Point(5, 190);
      this.btnSearchDestinations.Name = "btnSearchDestinations";
      this.btnSearchDestinations.Size = new System.Drawing.Size(182, 30);
      this.btnSearchDestinations.TabIndex = 86;
      this.btnSearchDestinations.Text = "Search Destinations";
      this.btnSearchDestinations.Click += new System.EventHandler(this.btnSearchDestinations_Click);
      // 
      // frmSearch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.btnSearchDestinations);
      this.Controls.Add(this.btnSearchGpsLocation);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.lblMainHeading);
      this.Controls.Add(this.btnBack);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmSearch";
      this.Text = "Destinations";
      this.Load += new System.EventHandler(this.frmSearch_Load);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmSearch_Closing);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblMainHeading;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnBack;
    private System.Windows.Forms.Panel panel1;
    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblAddress;
    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel transparentLabel1;
    private Nucleo.GoodGuide.Controls.KeyboardButton btnByStreet;
    private Nucleo.GoodGuide.Controls.KeyboardButton btnByRegion;
    private Nucleo.GoodGuide.Controls.KeyboardButton btnSearchGpsLocation;
    private Nucleo.GoodGuide.Controls.KeyboardButton btnSearchDestinations;

  }
}