namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmAudioSettings
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
      this.btnPreviousSource = new System.Windows.Forms.PictureBox();
      this.btnPreviousContent = new System.Windows.Forms.PictureBox();
      this.btnNextSource = new System.Windows.Forms.PictureBox();
      this.btnNextContent = new System.Windows.Forms.PictureBox();
      this.edtSource = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.edtContent = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.lblFrequency = new System.Windows.Forms.Label();
      this.btnEdit = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.label2.Location = new System.Drawing.Point(85, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(310, 25);
      this.label2.Text = "Content";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.label3.Location = new System.Drawing.Point(87, 145);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(310, 25);
      this.label3.Text = "Source";
      this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.label3.Visible = false;
      // 
      // btnPreviousSource
      // 
      this.btnPreviousSource.Location = new System.Drawing.Point(2, 169);
      this.btnPreviousSource.Name = "btnPreviousSource";
      this.btnPreviousSource.Size = new System.Drawing.Size(80, 44);
      this.btnPreviousSource.Visible = false;
      this.btnPreviousSource.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPreviousSource_MouseUp);
      // 
      // btnPreviousContent
      // 
      this.btnPreviousContent.Location = new System.Drawing.Point(2, 79);
      this.btnPreviousContent.Name = "btnPreviousContent";
      this.btnPreviousContent.Size = new System.Drawing.Size(80, 44);
      this.btnPreviousContent.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPreviousContent_MouseUp);
      // 
      // btnNextSource
      // 
      this.btnNextSource.Location = new System.Drawing.Point(398, 169);
      this.btnNextSource.Name = "btnNextSource";
      this.btnNextSource.Size = new System.Drawing.Size(80, 44);
      this.btnNextSource.Visible = false;
      this.btnNextSource.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNextSource_MouseUp);
      // 
      // btnNextContent
      // 
      this.btnNextContent.Location = new System.Drawing.Point(398, 79);
      this.btnNextContent.Name = "btnNextContent";
      this.btnNextContent.Size = new System.Drawing.Size(80, 44);
      this.btnNextContent.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNextContent_MouseUp);
      // 
      // edtSource
      // 
      this.edtSource.BackColor = System.Drawing.SystemColors.Window;
      this.edtSource.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.edtSource.ForeColor = System.Drawing.Color.White;
      this.edtSource.LineSeparation = ((short)(1));
      this.edtSource.Location = new System.Drawing.Point(91, 169);
      this.edtSource.Name = "edtSource";
      this.edtSource.Size = new System.Drawing.Size(298, 44);
      this.edtSource.TabIndex = 34;
      this.edtSource.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.edtSource.Visible = false;
      // 
      // edtContent
      // 
      this.edtContent.BackColor = System.Drawing.SystemColors.Window;
      this.edtContent.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.edtContent.ForeColor = System.Drawing.Color.White;
      this.edtContent.LineSeparation = ((short)(1));
      this.edtContent.Location = new System.Drawing.Point(91, 79);
      this.edtContent.Name = "edtContent";
      this.edtContent.Size = new System.Drawing.Size(298, 44);
      this.edtContent.TabIndex = 35;
      this.edtContent.Text = "GERMAN";
      this.edtContent.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // lblFrequency
      // 
      this.lblFrequency.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.lblFrequency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblFrequency.Location = new System.Drawing.Point(91, 222);
      this.lblFrequency.Name = "lblFrequency";
      this.lblFrequency.Size = new System.Drawing.Size(118, 25);
      this.lblFrequency.Text = "Frequency";
      this.lblFrequency.Visible = false;
      // 
      // btnEdit
      // 
      this.btnEdit.BackColor = System.Drawing.SystemColors.Window;
      this.btnEdit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnEdit.ForeColor = System.Drawing.Color.White;
      this.btnEdit.LineSeparation = ((short)(1));
      this.btnEdit.Location = new System.Drawing.Point(307, 219);
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(82, 30);
      this.btnEdit.TabIndex = 43;
      this.btnEdit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnEdit.Visible = false;
      this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
      // 
      // frmAudioSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.btnEdit);
      this.Controls.Add(this.lblFrequency);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.edtContent);
      this.Controls.Add(this.edtSource);
      this.Controls.Add(this.btnNextContent);
      this.Controls.Add(this.btnNextSource);
      this.Controls.Add(this.btnPreviousContent);
      this.Controls.Add(this.btnPreviousSource);
      this.Controls.Add(this.label3);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MinimizeBox = false;
      this.Name = "frmAudioSettings";
      this.Text = "frmSettings";
      this.Load += new System.EventHandler(this.frmAudioSettings_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.PictureBox btnPreviousSource;
    private System.Windows.Forms.PictureBox btnPreviousContent;
    private System.Windows.Forms.PictureBox btnNextSource;
    private System.Windows.Forms.PictureBox btnNextContent;
    private Nucleo.GoodGuide.Controls.MultilineTextButton edtSource;
    private Nucleo.GoodGuide.Controls.MultilineTextButton edtContent;
    private System.Windows.Forms.Label lblFrequency;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnEdit;
  }
}