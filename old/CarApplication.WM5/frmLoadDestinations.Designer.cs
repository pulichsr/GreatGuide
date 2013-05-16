namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmLoadDestinations
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
      this.edtFilename = new System.Windows.Forms.TextBox();
      this.odFile = new System.Windows.Forms.OpenFileDialog();
      this.btnOpenFile = new System.Windows.Forms.Button();
      this.btnLoadFile = new System.Windows.Forms.Button();
      this.lblStep = new System.Windows.Forms.Label();
      this.btnBack = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.edtCurrentFile = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(0, 70);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(100, 20);
      this.label2.Text = "Load from file";
      // 
      // edtFilename
      // 
      this.edtFilename.Location = new System.Drawing.Point(0, 88);
      this.edtFilename.Name = "edtFilename";
      this.edtFilename.ReadOnly = true;
      this.edtFilename.Size = new System.Drawing.Size(255, 23);
      this.edtFilename.TabIndex = 4;
      // 
      // odFile
      // 
      this.odFile.Filter = "Data Files (*.xml)|*.xml";
      // 
      // btnOpenFile
      // 
      this.btnOpenFile.Location = new System.Drawing.Point(261, 89);
      this.btnOpenFile.Name = "btnOpenFile";
      this.btnOpenFile.Size = new System.Drawing.Size(33, 20);
      this.btnOpenFile.TabIndex = 5;
      this.btnOpenFile.Text = "...";
      this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
      // 
      // btnLoadFile
      // 
      this.btnLoadFile.Location = new System.Drawing.Point(300, 89);
      this.btnLoadFile.Name = "btnLoadFile";
      this.btnLoadFile.Size = new System.Drawing.Size(50, 20);
      this.btnLoadFile.TabIndex = 6;
      this.btnLoadFile.Text = "Load";
      this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
      // 
      // lblStep
      // 
      this.lblStep.Location = new System.Drawing.Point(0, 117);
      this.lblStep.Name = "lblStep";
      this.lblStep.Size = new System.Drawing.Size(477, 20);
      // 
      // btnBack
      // 
      this.btnBack.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnBack.Location = new System.Drawing.Point(405, 231);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(72, 38);
      this.btnBack.TabIndex = 9;
      this.btnBack.Text = "Back";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(3, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(188, 20);
      this.label3.Text = "Current destinations file";
      // 
      // edtCurrentFile
      // 
      this.edtCurrentFile.Location = new System.Drawing.Point(3, 18);
      this.edtCurrentFile.Name = "edtCurrentFile";
      this.edtCurrentFile.ReadOnly = true;
      this.edtCurrentFile.Size = new System.Drawing.Size(227, 23);
      this.edtCurrentFile.TabIndex = 19;
      // 
      // frmLoadDestinations
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.edtCurrentFile);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.lblStep);
      this.Controls.Add(this.btnLoadFile);
      this.Controls.Add(this.btnOpenFile);
      this.Controls.Add(this.edtFilename);
      this.Controls.Add(this.label2);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmLoadDestinations";
      this.Text = "Load Data";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmLoadData_Closing);
      this.Load += new System.EventHandler(this.frmLoadData_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox edtFilename;
    private System.Windows.Forms.OpenFileDialog odFile;
    private System.Windows.Forms.Button btnOpenFile;
    private System.Windows.Forms.Button btnLoadFile;
    private System.Windows.Forms.Label lblStep;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox edtCurrentFile;
  }
}