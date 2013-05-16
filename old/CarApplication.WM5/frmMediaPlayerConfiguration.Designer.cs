namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmMediaPlayerConfiguration
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
      this.chkLogging = new System.Windows.Forms.CheckBox();
      this.label4 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // chkLogging
      // 
      this.chkLogging.Location = new System.Drawing.Point(140, 10);
      this.chkLogging.Name = "chkLogging";
      this.chkLogging.Size = new System.Drawing.Size(100, 20);
      this.chkLogging.TabIndex = 22;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(2, 11);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(117, 20);
      this.label4.Text = "Logging";
      // 
      // frmMediaPlayerConfiguration
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(478, 247);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.chkLogging);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmMediaPlayerConfiguration";
      this.Text = "Media Player Configuration";
      this.Load += new System.EventHandler(this.frmMediaPlayerConfiguration_Load);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMediaPlayerConfiguration_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox chkLogging;
    private System.Windows.Forms.Label label4;
  }
}