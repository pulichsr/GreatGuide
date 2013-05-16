namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmEnterStreetNumber
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
      this.btnBack = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.edtSearch = new System.Windows.Forms.TextBox();
      this.kb123 = new Nucleo.Windows.Forms.ImageKeyboard();
      this.btnNext = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.lblMainHeading = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.edtStreetNumbers = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnBack
      // 
      this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.btnBack.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnBack.ForeColor = System.Drawing.Color.White;
      this.btnBack.LineSeparation = ((short)(1));
      this.btnBack.Location = new System.Drawing.Point(4, 2);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(59, 42);
      this.btnBack.TabIndex = 52;
      this.btnBack.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // edtSearch
      // 
      this.edtSearch.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.edtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtSearch.Location = new System.Drawing.Point(2, 54);
      this.edtSearch.Name = "edtSearch";
      this.edtSearch.Size = new System.Drawing.Size(476, 29);
      this.edtSearch.TabIndex = 72;
      this.edtSearch.TextChanged += new System.EventHandler(this.edtSearch_TextChanged);
      // 
      // kb123
      // 
      this.kb123.Location = new System.Drawing.Point(2, 85);
      this.kb123.Name = "kb123";
      this.kb123.Size = new System.Drawing.Size(476, 101);
      // 
      // btnNext
      // 
      this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.btnNext.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnNext.ForeColor = System.Drawing.Color.White;
      this.btnNext.LineSeparation = ((short)(1));
      this.btnNext.Location = new System.Drawing.Point(417, 2);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(59, 42);
      this.btnNext.TabIndex = 83;
      this.btnNext.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnNext.Visible = false;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // lblMainHeading
      // 
      this.lblMainHeading.BackColor = System.Drawing.Color.White;
      this.lblMainHeading.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
      this.lblMainHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblMainHeading.Location = new System.Drawing.Point(71, 3);
      this.lblMainHeading.Name = "lblMainHeading";
      this.lblMainHeading.Size = new System.Drawing.Size(338, 23);
      this.lblMainHeading.TabIndex = 86;
      this.lblMainHeading.Text = "Enter Street Number";
      this.lblMainHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // edtStreetNumbers
      // 
      this.edtStreetNumbers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtStreetNumbers.Location = new System.Drawing.Point(67, 28);
      this.edtStreetNumbers.Name = "edtStreetNumbers";
      this.edtStreetNumbers.Size = new System.Drawing.Size(346, 23);
      this.edtStreetNumbers.TabIndex = 88;
      // 
      // frmEnterStreetNumber
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.edtStreetNumbers);
      this.Controls.Add(this.lblMainHeading);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.kb123);
      this.Controls.Add(this.edtSearch);
      this.Controls.Add(this.btnBack);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmEnterStreetNumber";
      this.Text = "frmDisclaimer";
      this.Load += new System.EventHandler(this.frmEnterStreetNumber_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.MultilineTextButton btnBack;
    private System.Windows.Forms.TextBox edtSearch;
    private Nucleo.Windows.Forms.ImageKeyboard kb123;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnNext;
    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblMainHeading;
    private System.Windows.Forms.TextBox edtStreetNumbers;

  }
}