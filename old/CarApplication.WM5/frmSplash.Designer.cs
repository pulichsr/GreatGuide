namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmSplash
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
      this.lblVersion = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.lblCopyright = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.lblHelpline = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.SuspendLayout();
      // 
      // lblVersion
      // 
      this.lblVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
      this.lblVersion.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
      this.lblVersion.ForeColor = System.Drawing.Color.White;
      this.lblVersion.Location = new System.Drawing.Point(65, 169);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(351, 24);
      this.lblVersion.TabIndex = 13;
      this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // lblCopyright
      // 
      this.lblCopyright.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
      this.lblCopyright.ForeColor = System.Drawing.Color.White;
      this.lblCopyright.Location = new System.Drawing.Point(65, 252);
      this.lblCopyright.Name = "lblCopyright";
      this.lblCopyright.Size = new System.Drawing.Size(351, 20);
      this.lblCopyright.TabIndex = 15;
      this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // lblHelpline
      // 
      this.lblHelpline.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.lblHelpline.ForeColor = System.Drawing.Color.White;
      this.lblHelpline.Location = new System.Drawing.Point(65, 222);
      this.lblHelpline.Name = "lblHelpline";
      this.lblHelpline.Size = new System.Drawing.Size(351, 26);
      this.lblHelpline.TabIndex = 16;
      this.lblHelpline.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      // 
      // frmSplash
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.Gainsboro;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblHelpline);
      this.Controls.Add(this.lblCopyright);
      this.Controls.Add(this.lblVersion);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MinimizeBox = false;
      this.Name = "frmSplash";
      this.Load += new System.EventHandler(this.frmSplash_Load);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSplash_Paint);
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblVersion;
    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblCopyright;
    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblHelpline;
  }
}