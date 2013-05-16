namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmVolume
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
      this.label2 = new System.Windows.Forms.Label();
      this.btnDown = new System.Windows.Forms.PictureBox();
      this.btnUp = new System.Windows.Forms.PictureBox();
      this.edtVolume = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.label2.Location = new System.Drawing.Point(85, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(310, 25);
      this.label2.Text = "Volume";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // btnDown
      // 
      this.btnDown.Location = new System.Drawing.Point(2, 79);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(80, 44);
      this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseUp);
      // 
      // btnUp
      // 
      this.btnUp.Location = new System.Drawing.Point(398, 79);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(80, 44);
      this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
      // 
      // edtVolume
      // 
      this.edtVolume.BackColor = System.Drawing.SystemColors.Window;
      this.edtVolume.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold);
      this.edtVolume.ForeColor = System.Drawing.Color.White;
      this.edtVolume.LineSeparation = ((short)(1));
      this.edtVolume.Location = new System.Drawing.Point(91, 79);
      this.edtVolume.Name = "edtVolume";
      this.edtVolume.Size = new System.Drawing.Size(298, 44);
      this.edtVolume.TabIndex = 35;
      this.edtVolume.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // frmVolume
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.label2);
      this.Controls.Add(this.edtVolume);
      this.Controls.Add(this.btnUp);
      this.Controls.Add(this.btnDown);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MinimizeBox = false;
      this.Name = "frmVolume";
      this.Text = "frmSettings";
      this.Load += new System.EventHandler(this.frmVolume_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox btnDown;
    private System.Windows.Forms.PictureBox btnUp;
    private Nucleo.GoodGuide.Controls.MultilineTextButton edtVolume;
  }
}