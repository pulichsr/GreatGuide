namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmHelp
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
      this.edtHelp = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // edtHelp
      // 
      this.edtHelp.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtHelp.Location = new System.Drawing.Point(2, 45);
      this.edtHelp.Multiline = true;
      this.edtHelp.Name = "edtHelp";
      this.edtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.edtHelp.Size = new System.Drawing.Size(476, 224);
      this.edtHelp.TabIndex = 0;
      // 
      // frmHelp
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.edtHelp);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmHelp";
      this.Text = "frmHelp";
      this.Load += new System.EventHandler(this.frmHelp_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox edtHelp;

  }
}