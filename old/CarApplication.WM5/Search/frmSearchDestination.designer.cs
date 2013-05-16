namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmSearchDestination
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
      this.edtStreet = new System.Windows.Forms.TextBox();
      this.edtSearch = new System.Windows.Forms.TextBox();
      this.kb123 = new Nucleo.Windows.Forms.ImageKeyboard();
      this.kbAbc = new Nucleo.Windows.Forms.ImageKeyboard();
      this.lblMainHeading = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.btnNext = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnNextKeyboard = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.btnBack = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.SuspendLayout();
      // 
      // edtStreet
      // 
      this.edtStreet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtStreet.Location = new System.Drawing.Point(67, 28);
      this.edtStreet.Name = "edtStreet";
      this.edtStreet.Size = new System.Drawing.Size(346, 23);
      this.edtStreet.TabIndex = 62;
      // 
      // edtSearch
      // 
      this.edtSearch.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.edtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtSearch.Location = new System.Drawing.Point(2, 54);
      this.edtSearch.Name = "edtSearch";
      this.edtSearch.Size = new System.Drawing.Size(476, 29);
      this.edtSearch.TabIndex = 72;
      // 
      // kb123
      // 
      this.kb123.Location = new System.Drawing.Point(2, 85);
      this.kb123.Name = "kb123";
      this.kb123.Size = new System.Drawing.Size(476, 101);
      // 
      // kbAbc
      // 
      this.kbAbc.Location = new System.Drawing.Point(2, 85);
      this.kbAbc.Name = "kbAbc";
      this.kbAbc.Size = new System.Drawing.Size(476, 151);
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
      this.lblMainHeading.Text = "Search for Destination";
      this.lblMainHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
      // btnNextKeyboard
      // 
      this.btnNextKeyboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.btnNextKeyboard.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnNextKeyboard.ForeColor = System.Drawing.Color.White;
      this.btnNextKeyboard.Location = new System.Drawing.Point(2, 242);
      this.btnNextKeyboard.Name = "btnNextKeyboard";
      this.btnNextKeyboard.Size = new System.Drawing.Size(80, 30);
      this.btnNextKeyboard.TabIndex = 77;
      this.btnNextKeyboard.Text = "ABC";
      this.btnNextKeyboard.Click += new System.EventHandler(this.btnNextKeyboard_Click);
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
      // frmSearchDestination
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblMainHeading);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnNextKeyboard);
      this.Controls.Add(this.kb123);
      this.Controls.Add(this.kbAbc);
      this.Controls.Add(this.edtSearch);
      this.Controls.Add(this.edtStreet);
      this.Controls.Add(this.btnBack);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmSearchDestination";
      this.Text = "frmDisclaimer";
      this.Load += new System.EventHandler(this.frmSearchByStreet_Load);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmSearchByStreet_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.MultilineTextButton btnBack;
    private System.Windows.Forms.TextBox edtStreet;
    private System.Windows.Forms.TextBox edtSearch;
    private Nucleo.Windows.Forms.ImageKeyboard kb123;
    private Nucleo.Windows.Forms.ImageKeyboard kbAbc;
    private Nucleo.GoodGuide.Controls.KeyboardButton btnNextKeyboard;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnNext;
    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblMainHeading;

  }
}