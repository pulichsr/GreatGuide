
namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmSelectAudioSource
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
      this.btn1 = new Nucleo.GoodGuide.Controls.TwoLineMenuButton();
      this.btn2 = new Nucleo.GoodGuide.Controls.TwoLineMenuButton();
      this.SuspendLayout();
      // 
      // btn1
      // 
      this.btn1.BackColor = System.Drawing.SystemColors.Window;
      this.btn1.DescriptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
      this.btn1.DescriptionText = "";
      this.btn1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular);
      this.btn1.ForeColor = System.Drawing.Color.White;
      this.btn1.Location = new System.Drawing.Point(13, 46);
      this.btn1.Name = "btn1";
      this.btn1.Size = new System.Drawing.Size(224, 44);
      this.btn1.TabIndex = 2;
      this.btn1.Visible = false;
      this.btn1.Click += new System.EventHandler(this.btn1_Click);
      // 
      // btn2
      // 
      this.btn2.BackColor = System.Drawing.SystemColors.Window;
      this.btn2.DescriptionFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
      this.btn2.DescriptionText = "";
      this.btn2.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular);
      this.btn2.ForeColor = System.Drawing.Color.White;
      this.btn2.Location = new System.Drawing.Point(243, 46);
      this.btn2.Name = "btn2";
      this.btn2.Size = new System.Drawing.Size(224, 44);
      this.btn2.TabIndex = 3;
      this.btn2.Visible = false;
      this.btn2.Click += new System.EventHandler(this.btn2_Click);
      // 
      // frmSelectAudioSource
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.btn1);
      this.Controls.Add(this.btn2);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmSelectAudioSource";
      this.Text = "frmTenButton";
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.TwoLineMenuButton btn2;
    private Nucleo.GoodGuide.Controls.TwoLineMenuButton btn1;

  }
}