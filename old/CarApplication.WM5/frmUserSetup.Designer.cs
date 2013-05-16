namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmUserSetup
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
      this.btnFactorySetup = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.btnFirstTimeUse = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnFactorySetup
      // 
      this.btnFactorySetup.Location = new System.Drawing.Point(328, 97);
      this.btnFactorySetup.Name = "btnFactorySetup";
      this.btnFactorySetup.Size = new System.Drawing.Size(149, 40);
      this.btnFactorySetup.TabIndex = 5;
      this.btnFactorySetup.Text = "Factory Setup";
      this.btnFactorySetup.Click += new System.EventHandler(this.btnFactorySetup_Click);
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
      // btnFirstTimeUse
      // 
      this.btnFirstTimeUse.Location = new System.Drawing.Point(0, 97);
      this.btnFirstTimeUse.Name = "btnFirstTimeUse";
      this.btnFirstTimeUse.Size = new System.Drawing.Size(149, 40);
      this.btnFirstTimeUse.TabIndex = 12;
      this.btnFirstTimeUse.Text = "First Time Use";
      this.btnFirstTimeUse.Click += new System.EventHandler(this.btnFirstUse_Click);
      // 
      // frmUserSetup
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.btnFirstTimeUse);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.btnFactorySetup);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmUserSetup";
      this.Text = "Factory Setup";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnFactorySetup;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnFirstTimeUse;
  }
}