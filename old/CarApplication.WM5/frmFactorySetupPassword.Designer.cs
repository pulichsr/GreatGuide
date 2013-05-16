namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmFactorySetupPassword
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
      this.edtPassword = new System.Windows.Forms.TextBox();
      this.edtConfirmPassword = new System.Windows.Forms.TextBox();
      this.btnConfirmPassword = new System.Windows.Forms.Button();
      this.btnPassword = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnClose
      // 
      this.btnClose.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnClose.Location = new System.Drawing.Point(437, 3);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(40, 30);
      this.btnClose.TabIndex = 9;
      this.btnClose.Text = "X";
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // edtPassword
      // 
      this.edtPassword.Location = new System.Drawing.Point(125, 67);
      this.edtPassword.Name = "edtPassword";
      this.edtPassword.Size = new System.Drawing.Size(100, 23);
      this.edtPassword.TabIndex = 12;
      // 
      // edtConfirmPassword
      // 
      this.edtConfirmPassword.Location = new System.Drawing.Point(125, 103);
      this.edtConfirmPassword.Name = "edtConfirmPassword";
      this.edtConfirmPassword.Size = new System.Drawing.Size(100, 23);
      this.edtConfirmPassword.TabIndex = 14;
      // 
      // btnConfirmPassword
      // 
      this.btnConfirmPassword.Location = new System.Drawing.Point(231, 104);
      this.btnConfirmPassword.Name = "btnConfirmPassword";
      this.btnConfirmPassword.Size = new System.Drawing.Size(33, 20);
      this.btnConfirmPassword.TabIndex = 15;
      this.btnConfirmPassword.Text = "...";
      this.btnConfirmPassword.Click += new System.EventHandler(this.btnConfirmPassword_Click);
      // 
      // btnPassword
      // 
      this.btnPassword.Location = new System.Drawing.Point(231, 68);
      this.btnPassword.Name = "btnPassword";
      this.btnPassword.Size = new System.Drawing.Size(33, 20);
      this.btnPassword.TabIndex = 16;
      this.btnPassword.Text = "...";
      this.btnPassword.Click += new System.EventHandler(this.btnPassword_Click);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(3, 68);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 20);
      this.label1.Text = "Password";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(-1, 104);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(120, 20);
      this.label2.Text = "Confirm Password";
      // 
      // frmFactorySetupPassword
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnPassword);
      this.Controls.Add(this.btnConfirmPassword);
      this.Controls.Add(this.edtConfirmPassword);
      this.Controls.Add(this.edtPassword);
      this.Controls.Add(this.btnClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmFactorySetupPassword";
      this.Text = "Factory Setup";
      this.Load += new System.EventHandler(this.frmFactorySetupPassword_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.TextBox edtPassword;
    private System.Windows.Forms.TextBox edtConfirmPassword;
    private System.Windows.Forms.Button btnConfirmPassword;
    private System.Windows.Forms.Button btnPassword;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
  }
}