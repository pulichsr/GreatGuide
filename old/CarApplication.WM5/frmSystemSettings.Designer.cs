namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmSystemSettings
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
      this.label1 = new System.Windows.Forms.Label();
      this.chkLogging = new System.Windows.Forms.CheckBox();
      this.btnClose = new System.Windows.Forms.Button();
      this.chkShowContentFilenames = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.chkExceptionLogging = new System.Windows.Forms.CheckBox();
      this.label3 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(12, 10);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(56, 20);
      this.label1.Text = "Logging";
      // 
      // chkLogging
      // 
      this.chkLogging.Location = new System.Drawing.Point(140, 10);
      this.chkLogging.Name = "chkLogging";
      this.chkLogging.Size = new System.Drawing.Size(28, 20);
      this.chkLogging.TabIndex = 9;
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
      // chkShowContentFilenames
      // 
      this.chkShowContentFilenames.Location = new System.Drawing.Point(140, 70);
      this.chkShowContentFilenames.Name = "chkShowContentFilenames";
      this.chkShowContentFilenames.Size = new System.Drawing.Size(28, 20);
      this.chkShowContentFilenames.TabIndex = 14;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(12, 63);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(118, 34);
      this.label2.Text = "Show Content filenames";
      // 
      // chkExceptionLogging
      // 
      this.chkExceptionLogging.Location = new System.Drawing.Point(140, 40);
      this.chkExceptionLogging.Name = "chkExceptionLogging";
      this.chkExceptionLogging.Size = new System.Drawing.Size(28, 20);
      this.chkExceptionLogging.TabIndex = 18;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(12, 40);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(118, 20);
      this.label3.Text = "Exception Logging";
      // 
      // frmSystemSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.chkExceptionLogging);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.chkShowContentFilenames);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnClose);
      this.Controls.Add(this.chkLogging);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSystemSettings";
      this.Text = "Factory Settings";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmFactorySettings_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox chkLogging;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.CheckBox chkShowContentFilenames;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox chkExceptionLogging;
    private System.Windows.Forms.Label label3;
  }
}