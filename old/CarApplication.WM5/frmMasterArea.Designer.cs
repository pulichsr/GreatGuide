namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmMasterArea
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
      this.btnClose = new System.Windows.Forms.Button();
      this.chkAutoloadMasterArea = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.edtMasterArea = new System.Windows.Forms.TextBox();
      this.btnMasterArea = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnClose
      // 
      this.btnClose.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnClose.Location = new System.Drawing.Point(437, 3);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(40, 30);
      this.btnClose.TabIndex = 11;
      this.btnClose.Text = "X";
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // chkAutoloadMasterArea
      // 
      this.chkAutoloadMasterArea.Location = new System.Drawing.Point(162, 41);
      this.chkAutoloadMasterArea.Name = "chkAutoloadMasterArea";
      this.chkAutoloadMasterArea.Size = new System.Drawing.Size(28, 20);
      this.chkAutoloadMasterArea.TabIndex = 14;
      this.chkAutoloadMasterArea.CheckStateChanged += new System.EventHandler(this.chkAutoloadMasterArea_CheckStateChanged);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(12, 41);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(142, 20);
      this.label2.Text = "Autoload Master Area";
      // 
      // edtMasterArea
      // 
      this.edtMasterArea.Location = new System.Drawing.Point(160, 75);
      this.edtMasterArea.Name = "edtMasterArea";
      this.edtMasterArea.Size = new System.Drawing.Size(184, 23);
      this.edtMasterArea.TabIndex = 16;
      // 
      // btnMasterArea
      // 
      this.btnMasterArea.Location = new System.Drawing.Point(350, 76);
      this.btnMasterArea.Name = "btnMasterArea";
      this.btnMasterArea.Size = new System.Drawing.Size(30, 20);
      this.btnMasterArea.TabIndex = 17;
      this.btnMasterArea.Text = "...";
      this.btnMasterArea.Click += new System.EventHandler(this.btnMasterArea_Click);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(12, 76);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(142, 20);
      this.label1.Text = "Current Master Area";
      // 
      // frmMasterArea
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnMasterArea);
      this.Controls.Add(this.edtMasterArea);
      this.Controls.Add(this.chkAutoloadMasterArea);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmMasterArea";
      this.Text = "Factory Settings";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMasterArea_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.CheckBox chkAutoloadMasterArea;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox edtMasterArea;
    private System.Windows.Forms.Button btnMasterArea;
    private System.Windows.Forms.Label label1;
  }
}