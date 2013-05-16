namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmLanguageUnitsSettings
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
      this.label3 = new System.Windows.Forms.Label();
      this.btnPreviousUnits = new System.Windows.Forms.PictureBox();
      this.btnPreviousLanguage = new System.Windows.Forms.PictureBox();
      this.btnNextUnits = new System.Windows.Forms.PictureBox();
      this.btnNextLanguage = new System.Windows.Forms.PictureBox();
      this.edtUnits = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.edtLanguage = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.label2.Location = new System.Drawing.Point(85, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(310, 25);
      this.label2.Text = "Language";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.label3.Location = new System.Drawing.Point(87, 145);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(310, 25);
      this.label3.Text = "Units";
      this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // btnPreviousUnits
      // 
      this.btnPreviousUnits.Location = new System.Drawing.Point(2, 169);
      this.btnPreviousUnits.Name = "btnPreviousUnits";
      this.btnPreviousUnits.Size = new System.Drawing.Size(80, 44);
      this.btnPreviousUnits.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPreviousUnits_MouseUp);
      // 
      // btnPreviousLanguage
      // 
      this.btnPreviousLanguage.Location = new System.Drawing.Point(2, 79);
      this.btnPreviousLanguage.Name = "btnPreviousLanguage";
      this.btnPreviousLanguage.Size = new System.Drawing.Size(80, 44);
      this.btnPreviousLanguage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPreviousLanguage_MouseUp);
      // 
      // btnNextUnits
      // 
      this.btnNextUnits.Location = new System.Drawing.Point(398, 169);
      this.btnNextUnits.Name = "btnNextUnits";
      this.btnNextUnits.Size = new System.Drawing.Size(80, 44);
      this.btnNextUnits.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNextUnits_MouseUp);
      // 
      // btnNextLanguage
      // 
      this.btnNextLanguage.Location = new System.Drawing.Point(398, 79);
      this.btnNextLanguage.Name = "btnNextLanguage";
      this.btnNextLanguage.Size = new System.Drawing.Size(80, 44);
      this.btnNextLanguage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNextLanguage_MouseUp);
      // 
      // edtUnits
      // 
      this.edtUnits.BackColor = System.Drawing.SystemColors.Window;
      this.edtUnits.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.edtUnits.ForeColor = System.Drawing.Color.White;
      this.edtUnits.LineSeparation = ((short)(1));
      this.edtUnits.Location = new System.Drawing.Point(91, 169);
      this.edtUnits.Name = "edtUnits";
      this.edtUnits.Size = new System.Drawing.Size(298, 44);
      this.edtUnits.TabIndex = 34;
      this.edtUnits.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // edtLanguage
      // 
      this.edtLanguage.BackColor = System.Drawing.SystemColors.Window;
      this.edtLanguage.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.edtLanguage.ForeColor = System.Drawing.Color.White;
      this.edtLanguage.LineSeparation = ((short)(1));
      this.edtLanguage.Location = new System.Drawing.Point(91, 79);
      this.edtLanguage.Name = "edtLanguage";
      this.edtLanguage.Size = new System.Drawing.Size(298, 44);
      this.edtLanguage.TabIndex = 35;
      this.edtLanguage.Text = "GERMAN";
      this.edtLanguage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // frmLanguageUnitsSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.label2);
      this.Controls.Add(this.edtLanguage);
      this.Controls.Add(this.edtUnits);
      this.Controls.Add(this.btnNextLanguage);
      this.Controls.Add(this.btnNextUnits);
      this.Controls.Add(this.btnPreviousLanguage);
      this.Controls.Add(this.btnPreviousUnits);
      this.Controls.Add(this.label3);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MinimizeBox = false;
      this.Name = "frmLanguageUnitsSettings";
      this.Text = "frmSettings";
      this.Load += new System.EventHandler(this.frmLanguageUnitsSettings_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.PictureBox btnPreviousUnits;
    private System.Windows.Forms.PictureBox btnPreviousLanguage;
    private System.Windows.Forms.PictureBox btnNextUnits;
    private System.Windows.Forms.PictureBox btnNextLanguage;
    private Nucleo.GoodGuide.Controls.MultilineTextButton edtUnits;
    private Nucleo.GoodGuide.Controls.MultilineTextButton edtLanguage;
  }
}