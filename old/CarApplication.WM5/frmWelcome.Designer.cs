using Nucleo.Windows.Forms.TransparentControls;

namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmWelcome
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
      this.pbWelcomeAd = new System.Windows.Forms.PictureBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label4 = new System.Windows.Forms.Label();
      this.btnContinue = new System.Windows.Forms.PictureBox();
      this.lblDepartureDate = new System.Windows.Forms.Label();
      this.lblArrivalDate = new System.Windows.Forms.Label();
      this.lblUser = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pbWelcomeAd
      // 
      this.pbWelcomeAd.Dock = System.Windows.Forms.DockStyle.Top;
      this.pbWelcomeAd.Location = new System.Drawing.Point(0, 0);
      this.pbWelcomeAd.Name = "pbWelcomeAd";
      this.pbWelcomeAd.Size = new System.Drawing.Size(480, 172);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.Green;
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.btnContinue);
      this.panel1.Controls.Add(this.lblDepartureDate);
      this.panel1.Controls.Add(this.lblArrivalDate);
      this.panel1.Controls.Add(this.lblUser);
      this.panel1.Location = new System.Drawing.Point(0, 173);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(480, 99);
      // 
      // label4
      // 
      this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(4, 78);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(372, 20);
      this.label4.Text = "Device will automatically deactivate after the above departure date";
      // 
      // btnContinue
      // 
      this.btnContinue.Location = new System.Drawing.Point(385, 56);
      this.btnContinue.Name = "btnContinue";
      this.btnContinue.Size = new System.Drawing.Size(92, 40);
      this.btnContinue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
      // 
      // lblDepartureDate
      // 
      this.lblDepartureDate.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
      this.lblDepartureDate.ForeColor = System.Drawing.Color.White;
      this.lblDepartureDate.Location = new System.Drawing.Point(4, 50);
      this.lblDepartureDate.Name = "lblDepartureDate";
      this.lblDepartureDate.Size = new System.Drawing.Size(300, 20);
      this.lblDepartureDate.Text = "lblDepartureDate";
      // 
      // lblArrivalDate
      // 
      this.lblArrivalDate.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
      this.lblArrivalDate.ForeColor = System.Drawing.Color.White;
      this.lblArrivalDate.Location = new System.Drawing.Point(4, 30);
      this.lblArrivalDate.Name = "lblArrivalDate";
      this.lblArrivalDate.Size = new System.Drawing.Size(350, 20);
      this.lblArrivalDate.Text = "lblArrivalDate";
      // 
      // lblUser
      // 
      this.lblUser.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.lblUser.ForeColor = System.Drawing.Color.White;
      this.lblUser.Location = new System.Drawing.Point(4, 6);
      this.lblUser.Name = "lblUser";
      this.lblUser.Size = new System.Drawing.Size(400, 20);
      this.lblUser.Text = "lblUser";
      // 
      // frmWelcome
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.Gainsboro;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.pbWelcomeAd);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmWelcome";
      this.Text = "frmWelcome";
      this.Load += new System.EventHandler(this.frmWelcome_Load);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pbWelcomeAd;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblDepartureDate;
    private System.Windows.Forms.Label lblArrivalDate;
    private System.Windows.Forms.Label lblUser;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.PictureBox btnContinue;
  }
}