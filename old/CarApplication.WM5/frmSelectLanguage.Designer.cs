namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmSelectLanguage
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectLanguage));
      this.btnEn = new System.Windows.Forms.PictureBox();
      this.btnFr = new System.Windows.Forms.PictureBox();
      this.btnIt = new System.Windows.Forms.PictureBox();
      this.btnDe = new System.Windows.Forms.PictureBox();
      this.btnNl = new System.Windows.Forms.PictureBox();
      this.SuspendLayout();
      // 
      // btnEn
      // 
      this.btnEn.Image = ((System.Drawing.Image)(resources.GetObject("btnEn.Image")));
      this.btnEn.Location = new System.Drawing.Point(119, 112);
      this.btnEn.Name = "btnEn";
      this.btnEn.Size = new System.Drawing.Size(62, 48);
      this.btnEn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEn_MouseUp);
      // 
      // btnFr
      // 
      this.btnFr.Image = ((System.Drawing.Image)(resources.GetObject("btnFr.Image")));
      this.btnFr.Location = new System.Drawing.Point(209, 112);
      this.btnFr.Name = "btnFr";
      this.btnFr.Size = new System.Drawing.Size(62, 48);
      this.btnFr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnFr_MouseUp);
      // 
      // btnIt
      // 
      this.btnIt.Image = ((System.Drawing.Image)(resources.GetObject("btnIt.Image")));
      this.btnIt.Location = new System.Drawing.Point(299, 112);
      this.btnIt.Name = "btnIt";
      this.btnIt.Size = new System.Drawing.Size(62, 48);
      this.btnIt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnIt_MouseUp);
      // 
      // btnDe
      // 
      this.btnDe.Image = ((System.Drawing.Image)(resources.GetObject("btnDe.Image")));
      this.btnDe.Location = new System.Drawing.Point(29, 112);
      this.btnDe.Name = "btnDe";
      this.btnDe.Size = new System.Drawing.Size(62, 48);
      this.btnDe.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDe_MouseUp);
      // 
      // btnNl
      // 
      this.btnNl.Image = ((System.Drawing.Image)(resources.GetObject("btnNl.Image")));
      this.btnNl.Location = new System.Drawing.Point(389, 112);
      this.btnNl.Name = "btnNl";
      this.btnNl.Size = new System.Drawing.Size(62, 48);
      this.btnNl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNl_MouseUp);
      // 
      // frmSelectLanguage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.btnNl);
      this.Controls.Add(this.btnDe);
      this.Controls.Add(this.btnIt);
      this.Controls.Add(this.btnFr);
      this.Controls.Add(this.btnEn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MinimizeBox = false;
      this.Name = "frmSelectLanguage";
      this.Text = "frmSettings";
      this.TopMost = true;
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox btnEn;
    private System.Windows.Forms.PictureBox btnFr;
    private System.Windows.Forms.PictureBox btnIt;
    private System.Windows.Forms.PictureBox btnDe;
    private System.Windows.Forms.PictureBox btnNl;
  }
}