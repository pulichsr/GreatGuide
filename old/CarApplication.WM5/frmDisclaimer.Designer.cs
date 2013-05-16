namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmDisclaimer
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
      this.lblPleaseWait = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.timPlsWait = new System.Windows.Forms.Timer();
      this.SuspendLayout();
      // 
      // lblVersion
      // 
      this.lblVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
      this.lblVersion.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
      this.lblVersion.ForeColor = System.Drawing.Color.White;
      this.lblVersion.Location = new System.Drawing.Point(4, 245);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new System.Drawing.Size(134, 24);
      this.lblVersion.TabIndex = 16;
      this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      // 
      // lblPleaseWait
      // 
      this.lblPleaseWait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
      this.lblPleaseWait.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
      this.lblPleaseWait.ForeColor = System.Drawing.Color.White;
      this.lblPleaseWait.Location = new System.Drawing.Point(379, 245);
      this.lblPleaseWait.Name = "lblPleaseWait";
      this.lblPleaseWait.Size = new System.Drawing.Size(100, 24);
      this.lblPleaseWait.TabIndex = 17;
      this.lblPleaseWait.Text = "Please wait...";
      this.lblPleaseWait.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      // 
      // timPlsWait
      // 
      this.timPlsWait.Interval = 2000;
      this.timPlsWait.Tick += new System.EventHandler(this.timPlsWait_Tick);
      // 
      // frmDisclaimer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblPleaseWait);
      this.Controls.Add(this.lblVersion);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmDisclaimer";
      this.Text = "frmDisclaimer";
      this.Load += new System.EventHandler(this.frmDisclaimer_Load);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmDisclaimer_Paint);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmDisclaimer_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblVersion;
    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblPleaseWait;
    private System.Windows.Forms.Timer timPlsWait;
  }
}