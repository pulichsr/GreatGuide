namespace Nucleo.GoodGuide.ItineraryImporter
{
  partial class frmMain
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
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.timStart = new System.Windows.Forms.Timer();
      this.SuspendLayout();
      // 
      // listBox1
      // 
      this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listBox1.Location = new System.Drawing.Point(0, 0);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new System.Drawing.Size(480, 258);
      this.listBox1.TabIndex = 0;
      // 
      // timStart
      // 
      this.timStart.Interval = 500;
      this.timStart.Tick += new System.EventHandler(this.timStart_Tick);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.Controls.Add(this.listBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmMain";
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.Timer timStart;

  }
}